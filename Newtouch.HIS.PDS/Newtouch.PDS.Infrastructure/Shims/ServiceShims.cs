using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FrameworkBase.MultiOrg.Infrastructure;
using NewtouchHIS.Lib.Base.Model;
using SqlSugar;

namespace Newtouch.Infrastructure.EF
{
    public class RepositoryBase<TEntity> where TEntity : class, new()
    {
        protected Newtouch.Infrastructure.PdsDatabase _dataContext = new Newtouch.Infrastructure.PdsDatabase();

        public RepositoryBase() { }

        public RepositoryBase(FrameworkBase.MultiOrg.Infrastructure.IDefaultDatabaseFactory databaseFactory) { }

        public RepositoryBase(Newtouch.Infrastructure.IDatabaseFactory databaseFactory) { }

        public IQueryable<TEntity> IQueryable()
        {
            return Newtouch.Infrastructure.PdsDb.GetConnection().Queryable<TEntity>().ToList().AsQueryable();
        }

        public IQueryable<TEntity> IQueryable(Expression<Func<TEntity, bool>> predicate)
        {
            return Newtouch.Infrastructure.PdsDb.GetConnection().Queryable<TEntity>().Where(predicate).ToList().AsQueryable();
        }

        public TEntity FindEntity(Expression<Func<TEntity, bool>> predicate)
        {
            return Newtouch.Infrastructure.PdsDb.GetConnection().Queryable<TEntity>().Where(predicate).ToList().FirstOrDefault();
        }

        public TEntity FindEntity(string sql, params SugarParameter[] parameters)
        {
            return FindEntity<TEntity>(sql, parameters);
        }

        public T2 FindEntity<T2>(string sql, params SugarParameter[] parameters)
        {
            return PdsDb.GetConnection().Ado.SqlQuery<T2>(sql, parameters).FirstOrDefault();
        }
        public TEntity FirstOrDefault(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate) => Newtouch.Infrastructure.PdsDb.GetConnection().Queryable<TEntity>().Where(predicate).ToList().FirstOrDefault();
        public T2 FirstOrDefault<T2>(string sql, params SugarParameter[] parameters) { return PdsDb.GetConnection().Ado.SqlQuery<T2>(sql, parameters).FirstOrDefault(); }

        private static T2 FindByKey<T2>(string keyValue)
        {
            var type = typeof(T2);
            var pkProp = type.GetProperties().FirstOrDefault(pr =>
                pr.GetCustomAttributes(typeof(SugarColumn), true).OfType<SugarColumn>().Any(a => a.IsPrimaryKey)
                || Attribute.IsDefined(pr, typeof(System.ComponentModel.DataAnnotations.KeyAttribute)));
            if (pkProp == null) return default;
            var all = Newtouch.Infrastructure.PdsDb.GetConnection().Queryable<T2>().ToList();
            foreach (var item in all)
            {
                var v = pkProp.GetValue(item);
                if (v != null && string.Equals(v.ToString(), keyValue, StringComparison.OrdinalIgnoreCase)) return item;
            }
            return default;
        }

        public List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate)
        {
            return Newtouch.Infrastructure.PdsDb.GetConnection().Queryable<TEntity>().Where(predicate).ToList();
        }

        public List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate, Pagination pagination)
        {
            var all = Newtouch.Infrastructure.PdsDb.GetConnection().Queryable<TEntity>().Where(predicate).ToList();
            if (pagination == null) return all;
            pagination.records = all.Count;
            var page = pagination.page ?? 1; if (page < 1) page = 1;
            var rows = pagination.rows ?? 15; if (rows < 1) rows = 15;
            return all.Skip((page - 1) * rows).Take(rows).ToList();
        }

        public List<T2> FindList<T2>(string sql, params SugarParameter[] parameters)
        {
            return PdsDb.GetConnection().Ado.SqlQuery<T2>(sql, parameters);
        }

        public List<T2> SqlQuery<T2>(string sql, params SugarParameter[] parameters)
        {
            return PdsDb.GetConnection().Ado.SqlQuery<T2>(sql, parameters);
        }

        public int ExecuteSqlCommand(string sql, params SugarParameter[] parameters)
        {
            return _dataContext.Database.ExecuteSqlCommand(sql, parameters);
        }

        public int ExecuteSqlCommand(string sql, object[] parameters)
        {
            var pars = (parameters ?? new object[0]).OfType<SugarParameter>().ToArray();
            return _dataContext.Database.ExecuteSqlCommand(sql, pars);
        }

        public int Insert(object entity)
        {
            if (entity == null) return 0;
            dynamic db = Newtouch.Infrastructure.PdsDb.GetConnection();
            if (entity is System.Collections.IEnumerable en && !(entity is string))
            {
                var items = en.Cast<object>().ToList();
                if (items.Count == 0) return 0;
                var elemType = items[0].GetType();
                var listType = typeof(List<>).MakeGenericType(elemType);
                var typedList = Activator.CreateInstance(listType);
                var add = listType.GetMethod("Add");
                foreach (var o in items) add.Invoke(typedList, new[] { o });
                return (int)((dynamic)db.Insertable((dynamic)typedList)).ExecuteCommand();
            }
            return (int)((dynamic)db.Insertable((dynamic)entity)).ExecuteCommand();
        }

        public virtual int Update(TEntity entity, IEnumerable<string> dstnFieldNameList = null, IEnumerable<string> ignoreFieldNameList = null, bool attach = true)
        {
            return Newtouch.Infrastructure.UpdatePartialHelper.UpdateEntity(Newtouch.Infrastructure.PdsDb.GetConnection(), entity, dstnFieldNameList, ignoreFieldNameList);
        }

        public int Update(object entity)
        {
            if (entity == null) return 0;
            return Newtouch.Infrastructure.UpdatePartialHelper.UpdateEntity(Newtouch.Infrastructure.PdsDb.GetConnection(), entity, null, null);
        }

        public int Delete(object entity)
        {
            if (entity == null) return 0;
            dynamic db = Newtouch.Infrastructure.PdsDb.GetConnection();
            return (int)((dynamic)db.Deleteable((dynamic)entity)).ExecuteCommand();
        }

        public int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            return Newtouch.Infrastructure.PdsDb.GetConnection().Deleteable<TEntity>().Where(predicate).ExecuteCommand();
        }

        public List<T2> QueryWithPage<T2>(string sql, Pagination pagination, params SugarParameter[] parameters) where T2 : class, new()
        {
            return Newtouch.Infrastructure.PagedQuery.Run<T2>(sql, pagination, parameters);
        }
    }

    public class IEntity<TEntity>
    {
        protected void setPropertyValue(string kind, IList<string> names, object value, bool isPrimaryKey = false)
        {
            try
            {
                var type = typeof(TEntity);
                System.Reflection.PropertyInfo prop = null;
                if (isPrimaryKey)
                {
                    prop = type.GetProperties().FirstOrDefault(p =>
                        p.GetCustomAttributes(typeof(SugarColumn), true).OfType<SugarColumn>().Any(a => a.IsPrimaryKey)
                        || Attribute.IsDefined(p, typeof(System.ComponentModel.DataAnnotations.KeyAttribute)));
                    if (prop == null) return;
                }
                else
                {
                    if (names == null) return;
                    foreach (var n in names)
                    {
                        prop = type.GetProperty(n, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                        if (prop != null) break;
                    }
                    if (prop == null) return;
                }
                if (!prop.CanWrite) return;
                var target = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                object v = value;
                if (v != null && !target.IsAssignableFrom(v.GetType()))
                {
                    if (target == typeof(Guid)) v = Guid.Parse(v.ToString());
                    else if (target.IsEnum) v = System.Enum.Parse(target, v.ToString());
                    else v = Convert.ChangeType(v, target);
                }
                prop.SetValue(this, v);
            }
            catch { }
        }
    }
}

namespace FrameworkBase.MultiOrg.Repository
{
    public class RepositoryBase<T> : Newtouch.Infrastructure.EF.RepositoryBase<T> where T : class, new()
    {
    }
}

namespace FrameworkBase.MultiOrg.Application
{
    public class AppBase
    {
        public AppBase()
        {
            Newtouch.Infrastructure.ServiceLocator.ResolveFields(this);
        }
        public string OrganizeId => NewtouchHIS.Lib.Framework.Operator.OperatorProvider.GetCurrent()?.OrganizeId;
        public NewtouchHIS.Lib.Base.Model.OperatorModel UserIdentity => NewtouchHIS.Lib.Framework.Operator.OperatorProvider.GetCurrent();

        public AppBase(IDefaultDatabaseFactory databaseFactory)
        {
            Newtouch.Infrastructure.ServiceLocator.ResolveFields(this);
        }
    }
}
