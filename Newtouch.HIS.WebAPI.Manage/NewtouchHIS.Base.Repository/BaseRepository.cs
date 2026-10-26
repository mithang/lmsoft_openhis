using NewtouchHIS.Base.Domain;
using NewtouchHIS.Lib.DataBaseSvr;
using SqlSugar;
using System.Data.Common;
using System.Linq.Expressions;

namespace NewtouchHIS.Base.Repository
{
    public class BaseRepository<TEntity> :  DbContext<TEntity>, IBaseRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// nhiều khách thuê xử lý giao dịch
        /// </summary>
        public ITenant dbTent => Db;
        public BaseRepository() { 
            
        }
        /// <summary>
        /// ghi dữ liệu thực thể
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<long> Add(TEntity model)
        {
            var i = await Task.Run(() => Db.Insertable(model).ExecuteCommand());
            //được trả vềiCólongloại,Ở đây bạn có thể xử lý theo nhu cầu nghiệp vụ của mình
            return i;
        }

        /// <summary>
        /// theoIDxóa danh sách
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByIds(object[] ids)
        {
            var i = await Task.Run(() => Db.Deleteable<TEntity>().In(ids).ExecuteCommand());
            return i > 0;
        }

        /// <summary>
        /// theoIDxóa
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> DeleteById(object id)
        {
            var i = await Task.Run(() => Db.Deleteable<TEntity>().In(id).ExecuteCommand());
            return i > 0;
        }

        /// <summary>
        /// cập nhật dữ liệu thực thể
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Update(TEntity model)
        {
            //phương thức này sẽ lấy khóa chính làm điều kiện
            var i = await Task.Run(() => Db.Updateable(model).ExecuteCommand());
            return i > 0;
        }

        #region Query
        /// <summary>
        /// truy vấn theo khóa chính
        /// </summary>
        /// <param name="pkValue">khóa chính</param>
        /// <returns></returns>
        public async Task<TEntity> FindKey(object pkValue)
        {
            return await Db.Queryable<TEntity>().InSingleAsync(pkValue);
        }

        /// <summary>
        /// truy vấn tất cả dữ liệu
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> FindAll()
        {
            return await Task.Run(() => Db.Queryable<TEntity>().ToListAsync());
        }

        /// <summary>
        /// truy vấn tất cả dữ liệu
        /// </summary>
        /// <returns></returns>
        public async Task<List<TEntity>> FindAll(bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Db.Queryable<TEntity>().OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// lấy đầu num bản ghi dữ liệu
        /// </summary>
        /// <param name="num">lấy vài bản ghi đầu</param>
        /// <returns></returns>
        public async Task<List<TEntity>> getTakeList(int num, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Db.Queryable<TEntity>().OrderByIF(isOrderBy, orderBy, orderByType).Take(num).ToListAsync();
        }

        /// <summary>
        /// lấy một bảng dữ liệu phân trang
        /// </summary>
        /// <param name="skip">bỏ qua vài bản ghi</param>
        /// <param name="take">lấy vài bản ghi</param>
        /// <param name="whereExp">bỏ qua vài bản ghi</param>
        /// <param name="orderBy">điều kiện sắp xếp</param>
        /// <param name="orderByType">kiểu sắp xếp（Asc、Desc）</param>
        /// <returns></returns>
        public async Task<List<TEntity>> getPageList(int skip, int take, Expression<Func<TEntity, bool>> whereExp, Expression<Func<TEntity, object>> orderBy, OrderByType orderByType = OrderByType.Asc)
        {
            return await Db.Queryable<TEntity>().Skip(skip).Take(take).OrderBy(orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// lấy các bản ghi đầu thỏa điều kiện num bản ghi dữ liệu
        /// </summary>
        /// <param name="where">điều kiện</param>
        /// <param name="num">lấy vài bản ghi đầu</param>
        /// <returns></returns> 
        public async Task<List<TEntity>> getTakeList(Expression<Func<TEntity, bool>> where, int num, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Db.Queryable<TEntity>().Where(where).OrderByIF(isOrderBy, orderBy, orderByType).Take(num).ToListAsync();
        }

        /// <summary>
        /// lấy theo điều kiện một bản ghi dữ liệu 
        /// </summary>
        /// <param name="where">điều kiện</param>
        /// <returns></returns>
        public async Task<TEntity> getFirstOrDefault(Expression<Func<TEntity, bool>> where)
        {
            return await Db.Queryable<TEntity>().FirstAsync(where);//truy vấn một bản ghi, không có dữ liệu trả vềNULL, Singlevượt quá1bản ghi sẽ báo lỗi，Firstkhông
        }

        /// <summary>
        /// theo khóa chính In  truy vấn
        /// </summary>
        /// <typeparam name="S">kiểu của khóa chính</typeparam>
        /// <param name="list">khóa chính In tập kết quả của thao tác</param>
        /// <returns></returns>
        public async Task<List<TEntity>> getByIn<S>(List<S> list, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Db.Queryable<TEntity>().In<S>(list).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// theo cột chỉ định In truy vấn
        /// </summary>
        /// <typeparam name="S">kiểu của cột chỉ định</typeparam>
        /// <param name="column">cột chỉ định</param>
        /// <param name="list">cột chỉ định In thao tác tập kết quả của</param>
        /// <returns></returns>
        public async Task<List<TEntity>> getByIn<S>(Expression<Func<TEntity, object>> column, List<S> list, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Db.Queryable<TEntity>().In<S>(column, list).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// theo cột chỉ định Not In (!Contain)truy vấn
        /// </summary>
        /// <typeparam name="S">kiểu cột chỉ định</typeparam>
        /// <param name="list">Not Intập kết quả của</param>
        /// <param name="field">cột chỉ định</param>
        /// <returns></returns>
        public async Task<List<TEntity>> getByNotIn<S>(List<S> list, object field, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Db.Queryable<TEntity>().Where(t => !SqlFunc.ContainsArray(list, field)).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// theo điều kiện truy vấn
        /// </summary>
        /// <param name="where">điều kiện</param>
        /// <returns></returns>
        public async Task<List<TEntity>> getByWhere(Expression<Func<TEntity, bool>> where, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Db.Queryable<TEntity>().Where(where).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// một điều kiện đơn theo isWhere phán đoán có sử dụng điều kiện này để truy vấn hay không
        /// </summary>
        /// <param name="isWhere">điều kiện phán đoán có sử dụng điều kiện truy vấn này hay không</param>
        /// <param name="where">điều kiện truy vấn</param>
        /// <returns></returns>
        public async Task<List<TEntity>> getByWhereIF(bool isWhere, Expression<Func<TEntity, bool>> where, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Db.Queryable<TEntity>().WhereIF(isWhere, where).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// nhiều điều kiện theo wheres.value  phán đoán có sử dụng hay không này wheres.key điều kiện của
        /// </summary>
        /// <param name="wheres">điều kiện truy vấn</param>
        /// <returns></returns>
        public async Task<List<TEntity>> getByWhereIF(Dictionary<Expression<Func<TEntity, bool>>, bool> wheres, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            var able = Db.Queryable<TEntity>();
            foreach (var item in wheres)
            {
                able.WhereIF(item.Value, item.Key);
            }
            return await able.OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// truy vấn giá trị của cột chỉ định trong startđếnend dữ liệu giữa
        /// </summary>
        /// <param name="value">lớp chỉ định</param>
        /// <param name="start">bắt đầu</param>
        /// <param name="end">kết thúc</param>
        /// <returns></returns>
        public async Task<List<TEntity>> getByBetween(object value, object start, object end, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Db.Queryable<TEntity>().Where(it => SqlFunc.Between(value, start, end)).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// phán đoán bản ghi này có tồn tại hay không
        /// </summary>
        /// <param name="where">điều kiện</param>
        /// <returns></returns>
        public async Task<bool> getIsAny(Expression<Func<TEntity, bool>> where)
        {
            return await Db.Queryable<TEntity>().AnyAsync(where);
        }

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
        public async Task<List<T>> getPageList<T>(int pageIndex, int pageSize, bool isWhere = false, Expression<Func<T, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Db.Queryable<T>().WhereIF(isWhere, whereExp).OrderByIF(isOrderBy, orderBy).ToPageListAsync(pageIndex, pageSize);
        }

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
        public async Task<List<object>> getJoinList<T1, T2>(Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {

            return await Db.Queryable(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy).Select(selectExp).ToListAsync();
        }

        /// <summary>
        /// hai bảng truy vấn phân trang，
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
        public async Task<dynamic> getJoinPageList<T1, T2>(int pageIndex, int pageSize, Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {

            return await Db.Queryable(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy).Select(selectExp).ToPageListAsync(pageIndex, pageSize);
        }

        /// <summary>
        /// kết nối ba bảng truy vấn
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
        public async Task<dynamic> getJoinList<T1, T2, T3>(Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Db.Queryable(joinExp).WhereIF(isWhere, whereExp).OrderByIF(isOrderBy, orderBy).Select(selectExp).ToListAsync();
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
        public async Task<dynamic> getJoinList<T1, T2, T3>(int pageIndex, int pageSize, Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Db.Queryable(joinExp).WhereIF(isWhere, whereExp).OrderByIF(isOrderBy, orderBy)
                .Select(selectExp).ToPageListAsync(pageIndex, pageSize);
        }

        /// <summary>
        /// thực thi truy vấnsqlcâu lệnh ，trả về tập dữ liệu
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <returns></returns>
        public async Task<List<TEntity>> getListBySql(string sql)
        {
            return await Db.SqlQueryable<TEntity>(sql).ToListAsync();
        }

        #region Ado 
        /// <summary>
        /// thực thi câu lệnh không phải truy vấnsqlcâu lệnh，trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">tham số hóa</param>
        /// <returns></returns>
        public async Task<bool> ExecuteCommandSql(string sql, params SugarParameter[] parameters)
        {
            return await Db.Ado.ExecuteCommandAsync(sql, parameters) > 0;
        }

        /// <summary>
        /// thực thi truy vấnsqlcâu lệnh，trả về tập kết quả truy vấn
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">tham số hóa</param>
        /// <returns></returns>
        public async Task<List<TEntity>> getListBySqlQuery(string sql, params SugarParameter[] parameters)
        {
            return await Db.Ado.SqlQueryAsync<TEntity>(sql, parameters);
        }

        /// <summary>
        /// thực thi truy vấnsqlcâu lệnh，trả về hàng đầu tiên, cột đầu tiên
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">tham số hóa</param>
        /// <returns></returns>
        public async Task<object> getScalar(string sql, params SugarParameter[] parameters)
        {
            return await Db.Ado.GetScalarAsync(sql, parameters);
        }

        #endregion

        #endregion

        #region DbMuti Query
        /// <summary>
        /// theo điều kiện truy vấn (choose db)
        /// </summary>
        /// <param name="where">điều kiện</param>
        /// <returns></returns>
        public async Task<List<T>> getByWhere<T>(string db, Expression<Func<T, bool>> where, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Db.GetConnectionScope(db).Queryable<T>().Where(where).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// truy vấn hai bảng，(choose db)
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
        public async Task<List<object>> getJoinList<T1, T2>(string db,Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Db.GetConnectionScope(db).Queryable(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy).Select(selectExp).ToListAsync();
        }

        #region Ado 
        /// <summary>
        /// thực thi câu lệnh không phải truy vấnsqlcâu lệnh，trả về thao tác có thành công hay không(choose db)
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">tham số hóa</param>
        /// <returns></returns>
        public async Task<bool> ExecuteCommandSql(string db, string sql, params SugarParameter[] parameters)
        {
            return await Db.GetConnectionScope(db).Ado.ExecuteCommandAsync(sql, parameters) > 0;
        }

        /// <summary>
        /// thực thi truy vấnsqlcâu lệnh，trả về tập kết quả truy vấn(choose db)
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">tham số hóa</param>
        /// <returns></returns>
        public async Task<List<T>> getListBySqlQuery<T>(string db, string sql, List<DbParameter> parameters)
        {
            List<SugarParameter> sugarPara = new List<SugarParameter>();
            parameters.ForEach(p =>
            {
                sugarPara.Add(new SugarParameter(p.ParameterName,p.Value));
            });
            return await Db.GetConnectionScope(db).Ado.SqlQueryAsync<T>(sql, sugarPara);
        }

        /// <summary>
        /// thực thi truy vấnsqlcâu lệnh，trả về hàng đầu tiên, cột đầu tiên(choose db)
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">tham số hóa</param>
        /// <returns></returns>
        public async Task<object> getScalar(string db, string sql, params SugarParameter[] parameters)
        {
            return await Db.GetConnectionScope(db).Ado.GetScalarAsync(sql, parameters);
        }

        #endregion
        #endregion
    }
}