using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using NewtouchHIS.Lib.Base.Model;
using SqlSugar;

namespace Newtouch.Infrastructure
{
    public interface IDatabaseFactory { }

    public static class PdsDb
    {
        public static SqlSugarProvider GetConnection()
        {
            return NewtouchHIS.Lib.DataBaseSvr.SqlSugarDbContext.Db.GetConnection("PdsDb");
        }
    }

    public class PdsDatabaseOps
    {
        public List<T> SqlQuery<T>(string sql, params SugarParameter[] parameters)
        {
            return PdsDb.GetConnection().Ado.SqlQuery<T>(sql, parameters);
        }

        public int ExecuteSqlCommand(string sql, params SugarParameter[] parameters)
        {
            return PdsDb.GetConnection().Ado.ExecuteCommand(sql, parameters);
        }

        public int ExecuteSqlCommand(string sql, object[] parameters)
        {
            var pars = (parameters ?? new object[0]).OfType<SugarParameter>().ToArray();
            return PdsDb.GetConnection().Ado.ExecuteCommand(sql, pars);
        }
    }

    public class PdsDatabase
    {
        public PdsDatabaseOps Database { get; } = new PdsDatabaseOps();
    }

    public class EFDBBase
    {
        protected PdsDatabase _dataContext = new PdsDatabase();

        public EFDBBase() { }

        public EFDBBase(IDatabaseFactory databaseFactory) { }

        public List<T> QueryWithPage<T>(string sql, Pagination pagination, params SugarParameter[] parameters) where T : class, new()
        {
            return PagedQuery.Run<T>(sql, pagination, parameters);
        }
    }

    internal static class PagedQuery
    {
        public static List<T> Run<T>(string sql, Pagination pagination, SugarParameter[] parameters) where T : class, new()
        {
            var clean = StripOrderBy(sql);
            var db = PdsDb.GetConnection();
            var q = db.SqlQueryable<T>(clean);
            if (parameters != null && parameters.Length > 0) q = q.AddParameters(parameters);
            if (pagination != null && !string.IsNullOrWhiteSpace(pagination.sidx)
                && Regex.IsMatch(pagination.sidx, @"^[A-Za-z0-9_,\s]+$"))
            {
                // QA-phase2: nhieu controller dat sidx kem huong san
                // ("CreateTime desc", "Ypdm desc"); noi them sord se tao
                // "CreateTime desc asc" -> loi SQL. Khi sidx da ket thuc bang
                // asc/desc thi dung nguyen sidx; neu khong thi noi sord nhu cu.
                // Chi anh huong cac truy van truoc day loi cu phap (khong the
                // chay duoc), khong doi truy van sidx la ten cot don.
                var sidx = pagination.sidx.Trim();
                if (Regex.IsMatch(sidx, @"\s+(asc|desc)$", RegexOptions.IgnoreCase))
                {
                    q = q.OrderBy(sidx);
                }
                else
                {
                    var sord = (pagination.sord ?? "").Trim().ToLower() == "desc" ? "desc" : "asc";
                    q = q.OrderBy(sidx + " " + sord);
                }
            }
            int total = 0;
            var page = pagination?.page ?? 1;
            if (page < 1) page = 1;
            var rows = pagination?.rows ?? 15;
            if (rows < 1) rows = 15;
            var list = q.ToPageList(page, rows, ref total);
            if (pagination != null) pagination.records = total;
            return list;
        }

        private static string StripOrderBy(string sql)
        {
            if (string.IsNullOrWhiteSpace(sql)) return sql;
            var matches = Regex.Matches(sql, @"(?i)\border\s+by\b");
            if (matches.Count == 0) return sql;
            var last = matches[matches.Count - 1];
            var tail = sql.Substring(last.Index + last.Length);
            if (tail.IndexOf("select", StringComparison.OrdinalIgnoreCase) >= 0) return sql;
            if (tail.Contains("(") || tail.Contains(")")) return sql;
            return sql.Substring(0, last.Index);
        }
    }

    public static class ServiceLocator
    {
        public static IServiceProvider Provider { get; set; }

        public static T GetInstance<T>()
        {
            if (Provider == null) return default;
            return Provider.GetService<T>();
        }

        public static void ResolveFields(object obj)
        {
            if (obj == null || Provider == null) return;
            var t = obj.GetType();
            while (t != null && t != typeof(object))
            {
                foreach (var f in t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
                {
                    if (f.IsStatic) continue;
                    try
                    {
                        if (f.GetValue(obj) != null) continue;
                    }
                    catch { continue; }
                    var ft = f.FieldType;
                    if (ft.IsValueType || ft == typeof(string)) continue;
                    object svc = null;
                    try { svc = Provider.GetService(ft); } catch { }
                    if (svc != null)
                    {
                        try { f.SetValue(obj, svc); } catch { }
                    }
                }
                t = t.BaseType;
            }
        }
    }

    public static class PrfExtensions
    {
        public static void PRFAutoResolve(this object obj)
        {
            ServiceLocator.ResolveFields(obj);
        }
    }

    public class EFDbTransaction : IDisposable
    {
        private readonly SqlSugarProvider _db;
        private bool _committed;

        public EFDbTransaction()
        {
            _db = PdsDb.GetConnection();
        }
        public EFDbTransaction(FrameworkBase.MultiOrg.Infrastructure.IDefaultDatabaseFactory databaseFactory)
        {
            _db = PdsDb.GetConnection();
        }

        public EFDbTransaction BeginTrans()
        {
            _db.Ado.BeginTran();
            return this;
        }

        public EFDbTransaction BeginTran()
        {
            return BeginTrans();
        }

        public void Insert(object entity)
        {
            if (entity == null) return;
            dynamic db = _db;
            if (entity is System.Collections.IEnumerable en && !(entity is string))
            {
                var items = en.Cast<object>().ToList();
                if (items.Count == 0) return;
                var elemType = items[0].GetType();
                var listType = typeof(List<>).MakeGenericType(elemType);
                var typedList = Activator.CreateInstance(listType);
                var add = listType.GetMethod("Add");
                foreach (var o in items) add.Invoke(typedList, new[] { o });
                ((dynamic)db.Insertable((dynamic)typedList)).ExecuteCommand();
            }
            else
            {
                ((dynamic)db.Insertable((dynamic)entity)).ExecuteCommand();
            }
        }

        public void Update(object entity)
        {
            if (entity == null) return;
            UpdatePartialHelper.UpdateEntity(_db, entity, null, null);
        }

        public void Delete(object entity)
        {
            if (entity == null) return;
            dynamic db = _db;
            ((dynamic)db.Deleteable((dynamic)entity)).ExecuteCommand();
        }

        public void Delete<T>(Expression<Func<T, bool>> where) where T : class, new()
        {
            _db.Deleteable<T>().Where(where).ExecuteCommand();
        }

        public int ExecuteSqlCommand(string sql, params SugarParameter[] parameters)
        {
            return _db.Ado.ExecuteCommand(sql, parameters);
        }

        public List<T> SqlQuery<T>(string sql, params SugarParameter[] parameters) where T : class, new()
        {
            var q = _db.SqlQueryable<T>(sql);
            if (parameters != null && parameters.Length > 0) q = q.AddParameters(parameters);
            return q.ToList();
        }

        public List<T> FindList<T>(string sql, params SugarParameter[] parameters)
        {
            return _db.Ado.SqlQuery<T>(sql, parameters);
        }
        public T FindEntity<T>(System.Linq.Expressions.Expression<System.Func<T, bool>> predicate) where T : class, new()
            => _db.Queryable<T>().Where(predicate).First();
        public T FindEntity<T>(string sql, params SugarParameter[] parameters)
        {
            return _db.Ado.SqlQuery<T>(sql, parameters).FirstOrDefault();
        }
        public System.Linq.IQueryable<T> IQueryable<T>() where T : class, new()
            => _db.Queryable<T>().ToList().AsQueryable();
        public System.Linq.IQueryable<T> IQueryable<T>(System.Linq.Expressions.Expression<System.Func<T, bool>> predicate) where T : class, new()
            => _db.Queryable<T>().Where(predicate).ToList().AsQueryable();
        public T FirstOrDefault<T>(System.Linq.Expressions.Expression<System.Func<T, bool>> predicate) where T : class, new()
            => _db.Queryable<T>().Where(predicate).First();
        public T FirstOrDefault<T>(string sql, params SugarParameter[] parameters)
        {
            return _db.Ado.SqlQuery<T>(sql, parameters).FirstOrDefault();
        }
        public int ExecuteSqlCommandNoLog(string sql, params SugarParameter[] parameters)
            => _db.Ado.ExecuteCommand(sql, parameters);
        public void Commit()
        {
            _db.Ado.CommitTran();
            _committed = true;
        }

        public void CommitTrans()
        {
            Commit();
        }

        public void Rollback()
        {
            try { _db.Ado.RollbackTran(); } catch { }
            _committed = true;
        }

        public void RollbackTrans()
        {
            Rollback();
        }

        public void Dispose()
        {
            if (!_committed)
            {
                try { _db.Ado.RollbackTran(); } catch { }
            }
        }
    }

    internal static class UpdatePartialHelper
    {
        public static int UpdateEntity(SqlSugarProvider db, object entity, IEnumerable<string> dstnFieldNameList, IEnumerable<string> ignoreFieldNameList)
        {
            var type = entity.GetType();
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanRead && p.CanWrite).ToList();
            string PkProp = null;
            foreach (var p in props)
            {
                var sc = p.GetCustomAttributes(typeof(SugarColumn), true).OfType<SugarColumn>().FirstOrDefault();
                if ((sc != null && sc.IsPrimaryKey) || Attribute.IsDefined(p, typeof(System.ComponentModel.DataAnnotations.KeyAttribute)))
                {
                    PkProp = p.Name;
                    break;
                }
            }
            if (PkProp == null) return 0;
            var pkProp = props.First(p => p.Name == PkProp);
            var pkVal = pkProp.GetValue(entity);
            if (pkVal == null) return 0;
            var skip = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { PkProp, "CreatorCode", "CreateTime", "CreatorName" };
            var always = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "LastModifyTime", "LastModifierCode" };
            var chosen = new List<System.Reflection.PropertyInfo>();
            foreach (var p in props)
            {
                if (skip.Contains(p.Name)) continue;
                if (dstnFieldNameList != null && dstnFieldNameList.Any())
                {
                    if (!dstnFieldNameList.Contains(p.Name, StringComparer.OrdinalIgnoreCase) && !always.Contains(p.Name)) continue;
                }
                else if (ignoreFieldNameList != null && ignoreFieldNameList.Any())
                {
                    if (ignoreFieldNameList.Contains(p.Name, StringComparer.OrdinalIgnoreCase) && !always.Contains(p.Name)) continue;
                }
                chosen.Add(p);
            }
            if (chosen.Count == 0) return 0;
            var tableAttr = type.GetCustomAttributes(typeof(SugarTable), true).OfType<SugarTable>().FirstOrDefault();
            var tableName = tableAttr != null && !string.IsNullOrWhiteSpace(tableAttr.TableName) ? tableAttr.TableName : type.Name;
            string ColName(System.Reflection.PropertyInfo p)
            {
                var sc = p.GetCustomAttributes(typeof(SugarColumn), true).OfType<SugarColumn>().FirstOrDefault();
                return sc != null && !string.IsNullOrWhiteSpace(sc.ColumnName) ? sc.ColumnName : p.Name;
            }
            var setSql = string.Join(", ", chosen.Select((p, i) => "[" + ColName(p) + "] = @set" + i));
            var sql = "UPDATE [" + tableName + "] SET " + setSql + " WHERE [" + ColName(pkProp) + "] = @pk";
            var pars = new List<SugarParameter>();
            for (int i = 0; i < chosen.Count; i++)
            {
                var v = chosen[i].GetValue(entity);
                pars.Add(new SugarParameter("@set" + i, v ?? DBNull.Value));
            }
            pars.Add(new SugarParameter("@pk", pkVal));
            return db.Ado.ExecuteCommand(sql, pars.ToArray());
        }
    }
}

namespace Newtouch.Infrastructure.EF
{
}

namespace FrameworkBase.MultiOrg.Infrastructure
{
    public interface IDefaultDatabaseFactory : Newtouch.Infrastructure.IDatabaseFactory { }

    public static class DependencyDefaultInstanceResolver
    {
        public static void Register(Func<Type, object> resolver) { }

        public static T GetInstance<T>()
        {
            if (Newtouch.Infrastructure.ServiceLocator.Provider == null) return default;
            return Newtouch.Infrastructure.ServiceLocator.Provider.GetService<T>();
        }
    }
}
