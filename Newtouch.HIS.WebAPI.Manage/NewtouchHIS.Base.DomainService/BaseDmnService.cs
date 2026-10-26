using NewtouchHIS.Base.Repository;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Attributes;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using SqlSugar;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace NewtouchHIS.Base.DomainService
{
    /// <summary>
    /// kế thừaDBContext,đối vớiservice mởISqlSugarClient giao diện
    /// hỗ trợ base.Context thay đổi cơ sở dữ liệu
    /// mặc định base.Context làTEntity được liên kếtTenantAttr
    /// ghi chú：thêm、xóa、sửa cấm vượt cơ sở dữ liệu，cấm dùngsqlcâu lệnh，nếu cần thì thêm mớiservice，thông quabase.Context chỉ định cơ sở dữ liệu đích
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseDmnService<TEntity> : SugarRepository<TEntity> where TEntity : class, new()
    {
        public BusinessConfig sysConfig = ConfigInitHelper.SysConfig;
        protected string? GetAppDB(string appId)
        {
            if (string.IsNullOrWhiteSpace(appId) || sysConfig.AppAPIHostName == null)
            {
                return null;
            }
            PropertyInfo propertyInfo = sysConfig.AppAPIHostName!.GetPropertyByValue(appId);
            if (propertyInfo == null)
            {
                return null;
            }
            return propertyInfo.GetCustomAttribute<DbTagAttribute>()?._dbId?.ToString();
        }

        /// <summary>
        /// kiểm tra không trống
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool ValidRequired<T>(T t, out string? errMsg)
        {
            errMsg = string.Empty;
            bool valid = true;
            var props = t!.GetType()!.GetProperties().Where(p => p.CustomAttributes.Any());
            foreach (var p in props)
            {
                if (p.CustomAttributes!.Where(x => x.AttributeType.Name.Contains("Required")).Any()
                    && (p.GetValue(t) == null || string.IsNullOrWhiteSpace(p.GetValue(t)?.ToString())))
                {
                    errMsg += $"[{p.Name}]";
                    if (valid)
                    {
                        valid = false;
                    }
                }
            }
            errMsg = string.IsNullOrWhiteSpace(errMsg) ? errMsg : $"请补充必要信息:{errMsg}";
            return valid;
        }
        /// <summary>
        /// kiểm tra giá trị enum 
        /// nếu muốn nhắc rõ ràng cần thêm DescriptionAttribute
        /// </summary>
        /// <param name="enumList"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool ValidEnum(Dictionary<Type, int> enumList, out string? errMsg)
        {
            errMsg = string.Empty;
            bool valid = true;
            foreach (var d in enumList)
            {
                var type = d.Key;
                var typeDesc = type.GetCustomAttributes<DescriptionAttribute>();
                if (!Enum.IsDefined(type, d.Value))
                {

                    errMsg += $"{typeDesc?.FirstOrDefault()?.Description}[{d.Value}]";
                    if (valid)
                    {
                        valid = false;
                    }
                }
            }
            errMsg = string.IsNullOrWhiteSpace(errMsg) ? errMsg : $"枚举信息取值异常:{errMsg}";
            return valid;
        }
        /// <summary>
        /// xác thực enumvaluetính hiệu lực 
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="errmsg">nhắc lỗi</param>
        /// <returns></returns>
        public bool ValidEnum<TEnum>(int value) where TEnum : Enum
        {
            if (!Enum.IsDefined(typeof(TEnum), value))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// tự động lắp rápwhere truy vấn（theo T củaDb truy vấn đặc tính）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="isOrderBy"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>       
        public Expression<Func<T, bool>> AssembledWhereEqual<T, TQueryParams>(TQueryParams entity)
            where T : class, new()
        {
            var t = Expression.Parameter(typeof(T), "t");
            var trueExpression = Expression.Constant(true);
            var resultExpression = Expression.AndAlso(trueExpression, trueExpression);

            var type = typeof(TQueryParams).GetProperties().Where(p => p.GetValue(entity) != null);
            foreach (var prop in type)
            {
                var v = prop.GetValue(entity);
                if (v != null && !string.IsNullOrWhiteSpace(v.ToString()))
                {
                    var paramValue = Expression.Constant(v, prop.PropertyType);
                    var entityProperty = Expression.Property(t, prop.Name);
                    var p = Expression.Convert(paramValue, entityProperty.Type);
                    var equalExpression = Expression.Equal(entityProperty, p);
                    resultExpression = Expression.AndAlso(resultExpression, equalExpression);
                }

            }
            return Expression.Lambda<Func<T, bool>>(resultExpression, t);
        }

        #region 联表查询
        /// <summary>
        /// truy vấn hai bảng，
        /// </summary>
        /// <typeparam name="T1">bảng trái</typeparam>
        /// <typeparam name="T2">bảng phải</typeparam>
        /// <param name="joinExp">phương thức liên bảng，trường liên bảng（quan hệ khóa chính – khóa ngoại）</param>
        /// <param name="selectExp">kết quả truy vấn liên bảng</param>
        /// <param name="isWhere">có cần điều kiện truy vấn hay không</param>
        /// <param name="whereExp">truy vấn theo điều kiện</param>
        /// <param name="isOrderBy">có cần sắp xếp hay không</param>
        /// <param name="orderBy">điều kiện sắp xếp</param>
        /// <param name="orderByType">kiểu sắp xếp（Asc、Desc）</param>
        /// <returns></returns>
        public async Task<List<TResp>> GetJoinList<T1, T2, TResp>(Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, TResp>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy, orderByType).Select(selectExp).ToListAsync();
        }
        public async Task<List<TResp>> GetJoinList<T1, T2, T3, TResp>(Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, TResp>> selectExp, bool isWhere = false, Expression<Func<T1, T2, T3, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy, orderByType).Select(selectExp).ToListAsync();
        }
        public async Task<List<TResp>> GetJoinList<T1, T2, T3, T4, TResp>(Expression<Func<T1, T2, T3, T4, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, T4, TResp>> selectExp, bool isWhere = false, Expression<Func<T1, T2, T3, T4, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, T4, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy, orderByType).Select(selectExp).ToListAsync();
        }
        public async Task<List<TResp>> GetJoinListWithAttr<T1, T2, TResp>(Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, TResp>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await dbTran.GetConnectionScopeWithAttr<T1>().Queryable(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy).Select(selectExp).ToListAsync();
        }
        public async Task<List<TResp>> GetJoinListWithAttr<T1, T2, T3, TResp>(Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, TResp>> selectExp, bool isWhere = false, Expression<Func<T1, T2, T3, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await dbTran.GetConnectionScopeWithAttr<T1>().Queryable(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy).Select(selectExp).ToListAsync();
        }
        public async Task<List<TResp>> GetJoinListWithAttr<T1, T2, T3, T4, TResp>(Expression<Func<T1, T2, T3, T4, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, T4, TResp>> selectExp, bool isWhere = false, Expression<Func<T1, T2, T3, T4, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, T4, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await dbTran.GetConnectionScopeWithAttr<T1>().Queryable(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy).Select(selectExp).ToListAsync();
        }
        #endregion

        #region 分页查询
        /// <summary>
        /// truy vấn phân trang một bảng
        /// </summary>
        /// <typeparam name="TEntity">bảng cần truy vấn</typeparam>
        /// <param name="pageIndex">số trang</param>
        /// <param name="pageSize">dung lượng trang</param>
        /// <param name="isWhere">có cần truy vấn theo điều kiện hay không</param>
        /// <param name="whereExp">điều kiện truy vấn</param>
        /// <param name="isOrderBy">có cần điều kiện sắp xếp hay không</param>
        /// <param name="orderBy">điều kiện sắp xếp</param>
        /// <param name="orderByType">kiểu sắp xếp（Asc、Desc）</param>
        /// <returns></returns>
        public async Task<PageResponseRow<List<T>>> GetPageList<T>(int offset, int limit, bool isWhere = false, Expression<Func<T, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            RefAsync<int> totalNumber = 0;
            var result = await Context.Queryable<T>().WhereIF(isWhere, whereExp).OrderByIF(isOrderBy, orderBy).ToPageListAsync(PageIndexOffset(offset, limit), limit, totalNumber);
            return new PageResponseRow<List<T>> { rows = result, total = totalNumber };
        }
        /// <summary>
        ///  hai bảng truy vấn phân trang
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResp"></typeparam>
        /// <param name="totalNumber"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="joinExp"></param>
        /// <param name="selectExp"></param>
        /// <param name="isWhere"></param>
        /// <param name="whereExp"></param>
        /// <param name="isOrderBy"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        public async Task<PageResponseRow<List<TResp>>> GetJoinPageList<T1, T2, TResp>(int offset, int limit, Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, TResp>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            RefAsync<int> totalNumber = 0;
            var result = await Context.Queryable(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy, orderByType)
                .WhereIF(isWhere, whereExp)
                .Select(selectExp)
                .ToPageListAsync(PageIndexOffset(offset, limit), limit, totalNumber);
            return new PageResponseRow<List<TResp>> { rows = result, total = totalNumber };
        }

        /// <summary>
        /// kết nối ba bảng phân trang truy vấn
        /// </summary>
        /// <typeparam name="T1">bảng1</typeparam>
        /// <typeparam name="T2">bảng2</typeparam>
        /// <typeparam name="T3">bảng3</typeparam>
        /// <param name="joinExp">phương thức liên bảng，trường liên bảng（quan hệ khóa chính – khóa ngoại）</param>
        /// <param name="selectExp">kết quả truy vấn liên bảng</param>
        /// <param name="isWhere">có cần điều kiện truy vấn hay không</param>
        /// <param name="whereExp">điều kiện truy vấn</param>
        /// <param name="isOrderBy">có cần điều kiện sắp xếp hay không</param>
        /// <param name="orderBy">điều kiện sắp xếp</param>
        /// <param name="orderByType">kiểu sắp xếp（Asc、Desc）</param>
        /// <returns></returns>
        public async Task<PageResponseRow<List<TResp>>> GetJoinPageList<T1, T2, T3, TResp>(int offset, int limit, Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, TResp>> selectExp, bool isWhere = false, Expression<Func<T1, T2, T3, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            RefAsync<int> totalNumber = 0;
            var result = await Context.Queryable(joinExp)
                .WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy)
                .Select(selectExp)
                .ToPageListAsync(PageIndexOffset(offset, limit), limit, totalNumber);
            return new PageResponseRow<List<TResp>> { rows = result, total = totalNumber };
        }
        public async Task<PageResponseRow<List<TResp>>> GetJoinPageList<T1, T2, T3, T4, TResp>(int offset, int limit, Expression<Func<T1, T2, T3, T4, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, T4, TResp>> selectExp, bool isWhere = false, Expression<Func<T1, T2, T3, T4, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, T4, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            RefAsync<int> totalNumber = 0;
            var result = await Context.Queryable(joinExp)
                .WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy)
                .Select(selectExp)
                .ToPageListAsync(PageIndexOffset(offset, limit), limit, totalNumber);
            return new PageResponseRow<List<TResp>> { rows = result, total = totalNumber };
        }
        public async Task<PageResponseRow<List<TResp>>> GetJoinOrderbyPageList<T1, T2, T3, T4, TResp>(int offset, int limit, string orderby, OrderByType orderbyType, Expression<Func<T1, T2, T3, T4, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, T4, TResp>> selectExp, bool isWhere = false, Expression<Func<T1, T2, T3, T4, bool>> whereExp = null)
        {
            RefAsync<int> totalNumber = 0;
            var result = await Context.Queryable(joinExp)
                .WhereIF(isWhere, whereExp)
                .Select(selectExp)
                .MergeTable()
                .OrderByPropertyName(orderby, orderbyType)
                .ToPageListAsync(PageIndexOffset(offset, limit), limit, totalNumber);
            return new PageResponseRow<List<TResp>> { rows = result, total = totalNumber };
        }
        #endregion

        #region 无实体
        /// <summary>
        /// truy vấn liên bảng（bảng thực thể chưa định nghĩa）
        /// </summary>
        /// <typeparam name="TResp"></typeparam>
        /// <param name="mainTbName">tên bảng chính</param>
        /// <param name="mainTbAs">bảng chính as bí danh</param>
        /// <param name="joinInfoParameters"></param>
        /// <param name="selectExp"></param>
        /// <param name="isWhere"></param>
        /// <param name="whereExp"></param>
        /// <param name="isOrderBy"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        //public async Task<List<TResp>> GetTableJoinList<TResp>(string mainTbName, string mainTbAs, List<JoinInfoParameter> joinInfoParameters, Expression<Func<TResp>> selectExp, bool isWhere = false, Expression<Func<TResp, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<TResp, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        //{
        //    return await Context.Queryable<TResp>().AS(mainTbName, mainTbAs)
        //        .AddJoinInfo(joinInfoParameters)
        //        .Select<TResp>(selectExp).WhereIF(isWhere, whereExp)
        //        .OrderByIF(isOrderBy, orderBy, orderByType)
        //        .ToListAsync();
        //}
        //public async Task<List<TResp>> GetTableJoinList<TResp>(string db,string mainTbName, string mainTbAs, List<JoinInfoParameter> joinInfoParameters, Expression<Func<TResp>> selectExp, bool isWhere = false, Expression<Func<TResp, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<TResp, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        //{
        //    return await dbTran.GetConnectionScope(db).Queryable<TResp>().AS(mainTbName, mainTbAs)
        //        .AddJoinInfo(joinInfoParameters)
        //        .Select<TResp>(selectExp).WhereIF(isWhere, whereExp)
        //        .OrderByIF(isOrderBy, orderBy, orderByType)
        //        .ToListAsync();
        //}
        public async Task<List<TResp>> GetTableJoinList<TResp>(string db,string mainTbName, string mainTbAs, List<JoinInfoParameter> joinInfoParameters, List<SelectModel> selectExp, bool isWhere = false, ObjectFuncModel whereFunc = null, bool isOrderBy = false, Expression<Func<TResp, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await dbTran.GetConnectionScope(db).Queryable<TResp>().AS(mainTbName, mainTbAs)
                .AddJoinInfo(joinInfoParameters)
                .Select<TResp>(selectExp).Where(whereFunc)
                .OrderByIF(isOrderBy, orderBy, orderByType)
                .ToListAsync();
        }
        /// <summary>
        /// truy vấn phân trang liên bảng（bảng thực thể chưa định nghĩa）
        /// </summary>
        /// <typeparam name="TResp"></typeparam>
        /// <param name="totalNumber"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="mainTbName"></param>
        /// <param name="mainTbAs"></param>
        /// <param name="joinInfoParameters"></param>
        /// <param name="selectExp"></param>
        /// <param name="isWhere"></param>
        /// <param name="whereExp"></param>
        /// <param name="isOrderBy"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        public PageResponseRow<List<TResp>> GetTableJoinPageList<TResp>(ref int totalNumber, int offset, int limit, string mainTbName, string mainTbAs, List<JoinInfoParameter> joinInfoParameters, Expression<Func<TResp>> selectExp, bool isWhere = false, Expression<Func<TResp, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<TResp, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            var result = Context.Queryable<TResp>().AS(mainTbName, mainTbAs)
                .AddJoinInfo(joinInfoParameters)
                .Select<TResp>(selectExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy, orderByType)
                .ToPageList(PageIndexOffset(offset, limit), limit, ref totalNumber);
            return new PageResponseRow<List<TResp>> { rows = result, total = totalNumber };
        }
        public async Task<PageResponseRow<List<TResp>>> GetTableJoinPageList<TResp>(int offset, int limit, string mainTbName, string mainTbAs, List<JoinInfoParameter> joinInfoParameters, Expression<Func<TResp>> selectExp, bool isWhere = false, Expression<Func<TResp, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<TResp, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            RefAsync<int> totalNumber = 0;
            var result = await Context.Queryable<TResp>().AS(mainTbName, mainTbAs)
                .AddJoinInfo(joinInfoParameters)
                .Select<TResp>(selectExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy, orderByType)
                .ToPageListAsync(PageIndexOffset(offset, limit), limit, totalNumber);
            return new PageResponseRow<List<TResp>> { total = totalNumber, rows = result };
        }
        #endregion

        #region private
        private int PageIndexOffset(int offset, int limit)
        {
            if (offset > 0 && limit > 0)
            {
                return (offset / limit) + 1;
            }
            return 1;
        }


        #endregion
    }
}
