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
        /// Đa tenant Xử lý transaction
        /// </summary>
        public ITenant dbTent => Db;
        public BaseRepository() { 
            
        }
        /// <summary>
        /// Ghi dữ liệu thực thể
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<long> Add(TEntity model)
        {
            var i = await Task.Run(() => Db.Insertable(model).ExecuteCommand());
            //trả vềiCólongLoại,tại đây bạn có thể xử lý theo nhu cầu nghiệp vụ của mình
            return i;
        }

        /// <summary>
        /// TheoIDXóa theo danh sách
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByIds(object[] ids)
        {
            var i = await Task.Run(() => Db.Deleteable<TEntity>().In(ids).ExecuteCommand());
            return i > 0;
        }

        /// <summary>
        /// TheoIDXóa
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> DeleteById(object id)
        {
            var i = await Task.Run(() => Db.Deleteable<TEntity>().In(id).ExecuteCommand());
            return i > 0;
        }

        /// <summary>
        /// Cập nhật dữ liệu thực thể
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Update(TEntity model)
        {
            //Cách này sẽ lấyKhóa chínhlàm điều kiện
            var i = await Task.Run(() => Db.Updateable(model).ExecuteCommand());
            return i > 0;
        }

        #region Query
        /// <summary>
        /// TheoKhóa chínhTruy vấn
        /// </summary>
        /// <param name="pkValue">Khóa chính</param>
        /// <returns></returns>
        public async Task<TEntity> FindKey(object pkValue)
        {
            return await Db.Queryable<TEntity>().InSingleAsync(pkValue);
        }

        /// <summary>
        /// Truy vấn tất cả dữ liệu
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> FindAll()
        {
            return await Task.Run(() => Db.Queryable<TEntity>().ToListAsync());
        }

        /// <summary>
        /// Truy vấn tất cả dữ liệu
        /// </summary>
        /// <returns></returns>
        public async Task<List<TEntity>> FindAll(bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Db.Queryable<TEntity>().OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// Lấy đầu num bản ghi
        /// </summary>
        /// <param name="num">Lấy mấy bản ghi đầu</param>
        /// <returns></returns>
        public async Task<List<TEntity>> getTakeList(int num, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Db.Queryable<TEntity>().OrderByIF(isOrderBy, orderBy, orderByType).Take(num).ToListAsync();
        }

        /// <summary>
        /// Lấy dữ liệu một bảng Dữ liệu phân trang
        /// </summary>
        /// <param name="skip">Bỏ qua mấy bản ghi</param>
        /// <param name="take">Lấy mấy bản ghi</param>
        /// <param name="whereExp">Bỏ qua mấy bản ghi</param>
        /// <param name="orderBy">Điều kiện sắp xếp</param>
        /// <param name="orderByType">Kiểu sắp xếp(Asc,Desc)</param>
        /// <returns></returns>
        public async Task<List<TEntity>> getPageList(int skip, int take, Expression<Func<TEntity, bool>> whereExp, Expression<Func<TEntity, object>> orderBy, OrderByType orderByType = OrderByType.Asc)
        {
            return await Db.Queryable<TEntity>().Skip(skip).Take(take).OrderBy(orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// Lấy các bản ghi đầu thỏa điều kiện num bản ghi
        /// </summary>
        /// <param name="where">Điều kiện</param>
        /// <param name="num">Lấy mấy bản ghi đầu</param>
        /// <returns></returns> 
        public async Task<List<TEntity>> getTakeList(Expression<Func<TEntity, bool>> where, int num, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Db.Queryable<TEntity>().Where(where).OrderByIF(isOrderBy, orderBy, orderByType).Take(num).ToListAsync();
        }

        /// <summary>
        /// Lấy theo điều kiện Một bản ghi 
        /// </summary>
        /// <param name="where">Điều kiện</param>
        /// <returns></returns>
        public async Task<TEntity> getFirstOrDefault(Expression<Func<TEntity, bool>> where)
        {
            return await Db.Queryable<TEntity>().FirstAsync(where);//Truy vấn một bản ghi, không có dữ liệu thì trả vềNULL, Singlevượt quá1bản ghi sẽ báo lỗi,Firstsẽ không
        }

        /// <summary>
        /// TheoKhóa chính In  Truy vấn
        /// </summary>
        /// <typeparam name="S">Khóa chínhkiểu</typeparam>
        /// <param name="list">Khóa chính In Tập kết quả của thao tác</param>
        /// <returns></returns>
        public async Task<List<TEntity>> getByIn<S>(List<S> list, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Db.Queryable<TEntity>().In<S>(list).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// Theo cột chỉ định In Truy vấn
        /// </summary>
        /// <typeparam name="S">Kiểu của cột chỉ định</typeparam>
        /// <param name="column">Cột chỉ định</param>
        /// <param name="list">Cột chỉ định In Thao tác tập kết quả</param>
        /// <returns></returns>
        public async Task<List<TEntity>> getByIn<S>(Expression<Func<TEntity, object>> column, List<S> list, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Db.Queryable<TEntity>().In<S>(column, list).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// Theo cột chỉ định Not In (!Contain)Truy vấn
        /// </summary>
        /// <typeparam name="S">Kiểu cột chỉ định</typeparam>
        /// <param name="list">Not Intập kết quả</param>
        /// <param name="field">Cột chỉ định</param>
        /// <returns></returns>
        public async Task<List<TEntity>> getByNotIn<S>(List<S> list, object field, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Db.Queryable<TEntity>().Where(t => !SqlFunc.ContainsArray(list, field)).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// Theo điều kiện Truy vấn
        /// </summary>
        /// <param name="where">Điều kiện</param>
        /// <returns></returns>
        public async Task<List<TEntity>> getByWhere(Expression<Func<TEntity, bool>> where, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Db.Queryable<TEntity>().Where(where).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// Một điều kiện Theo isWhere Xét Có sử dụng điều kiện này để truy vấn hay không
        /// </summary>
        /// <param name="isWhere">Điều kiện xét có sử dụng điều kiện truy vấn này hay không</param>
        /// <param name="where">Điều kiện truy vấn</param>
        /// <returns></returns>
        public async Task<List<TEntity>> getByWhereIF(bool isWhere, Expression<Func<TEntity, bool>> where, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Db.Queryable<TEntity>().WhereIF(isWhere, where).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// Nhiều điều kiện Theo wheres.value  Xét có sử dụng hay không này wheres.key điều kiện
        /// </summary>
        /// <param name="wheres">Điều kiện truy vấn</param>
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
        /// Truy vấn Giá trị của cột chỉ định tại startđếnend dữ liệu trong khoảng
        /// </summary>
        /// <param name="value">Lớp chỉ định</param>
        /// <param name="start">Bắt đầu</param>
        /// <param name="end">Kết thúc</param>
        /// <returns></returns>
        public async Task<List<TEntity>> getByBetween(object value, object start, object end, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Db.Queryable<TEntity>().Where(it => SqlFunc.Between(value, start, end)).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// Xét bản ghi này có tồn tại hay không
        /// </summary>
        /// <param name="where">Điều kiện</param>
        /// <returns></returns>
        public async Task<bool> getIsAny(Expression<Func<TEntity, bool>> where)
        {
            return await Db.Queryable<TEntity>().AnyAsync(where);
        }

        /// <summary>
        /// Truy vấn phân trang một bảng
        /// </summary>
        /// <typeparam name="TEntity">Bảng cần truy vấn</typeparam>
        /// <param name="pageIndex">Số trang</param>
        /// <param name="pageSize">Số bản ghi mỗi trang</param>
        /// <param name="isWhere">Có cần truy vấn theo điều kiện hay không</param>
        /// <param name="whereExp">Điều kiện truy vấn</param>
        /// <param name="isOrderBy">Có cần điều kiện sắp xếp hay không</param>
        /// <param name="orderBy">Điều kiện sắp xếp</param>
        /// <param name="orderByType">Kiểu sắp xếp(Asc,Desc)</param>
        /// <returns></returns>
        public async Task<List<T>> getPageList<T>(int pageIndex, int pageSize, bool isWhere = false, Expression<Func<T, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Db.Queryable<T>().WhereIF(isWhere, whereExp).OrderByIF(isOrderBy, orderBy).ToPageListAsync(pageIndex, pageSize);
        }

        /// <summary>
        /// Truy vấn hai bảng,
        /// </summary>
        /// <typeparam name="T1">Bảng trái</typeparam>
        /// <typeparam name="T2">Bảng phải</typeparam>
        /// <param name="joinExp">Cách nối bảng,Trường nối bảng(Quan hệ khóa chính - khóa ngoại)</param>
        /// <param name="selectExp">Kết quả truy vấn nối bảng</param>
        /// <param name="isWhere">Có cần điều kiện truy vấn hay không</param>
        /// <param name="whereExp">Truy vấn theo điều kiện</param>
        /// <param name="isOrderBy">Có cần sắp xếp hay không</param>
        /// <param name="orderBy">Điều kiện sắp xếp</param>
        /// <param name="orderByType">Kiểu sắp xếp(Asc,Desc)</param>
        /// <returns></returns>
        public async Task<List<object>> getJoinList<T1, T2>(Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {

            return await Db.Queryable(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy).Select(selectExp).ToListAsync();
        }

        /// <summary>
        /// Hai bảng Truy vấn phân trang,
        /// </summary>
        /// <typeparam name="T1">Bảng trái</typeparam>
        /// <typeparam name="T2">Bảng phải</typeparam>
        /// <param name="joinExp">Cách nối bảng,Trường nối bảng(Quan hệ khóa chính - khóa ngoại)</param>
        /// <param name="selectExp">Kết quả truy vấn nối bảng</param>
        /// <param name="isWhere">Có cần điều kiện truy vấn hay không</param>
        /// <param name="whereExp">Truy vấn theo điều kiện</param>
        /// <param name="isOrderBy">Có cần sắp xếp hay không</param>
        /// <param name="orderBy">Điều kiện sắp xếp</param>
        /// <param name="orderByType">Kiểu sắp xếp(Asc,Desc)</param>
        /// <returns></returns>
        public async Task<dynamic> getJoinPageList<T1, T2>(int pageIndex, int pageSize, Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {

            return await Db.Queryable(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy).Select(selectExp).ToPageListAsync(pageIndex, pageSize);
        }

        /// <summary>
        /// Nối ba bảng Truy vấn
        /// </summary>
        /// <typeparam name="T1">Bảng1</typeparam>
        /// <typeparam name="T2">Bảng2</typeparam>
        /// <typeparam name="T3">Bảng3</typeparam>
        /// <param name="joinExp">Cách nối bảng,Trường nối bảng(Quan hệ khóa chính - khóa ngoại)</param>
        /// <param name="selectExp">Kết quả truy vấn nối bảng</param>
        /// <param name="isWhere">Có cần điều kiện truy vấn hay không</param>
        /// <param name="whereExp">Điều kiện truy vấn</param>
        /// <param name="isOrderBy">Có cần điều kiện sắp xếp hay không</param>
        /// <param name="orderBy">Điều kiện sắp xếp</param>
        /// <param name="orderByType">Kiểu sắp xếp(Asc,Desc)</param>
        /// <returns></returns>
        public async Task<dynamic> getJoinList<T1, T2, T3>(Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Db.Queryable(joinExp).WhereIF(isWhere, whereExp).OrderByIF(isOrderBy, orderBy).Select(selectExp).ToListAsync();
        }

        /// <summary>
        /// Nối ba bảng Phân trang Truy vấn
        /// </summary>
        /// <typeparam name="T1">Bảng1</typeparam>
        /// <typeparam name="T2">Bảng2</typeparam>
        /// <typeparam name="T3">Bảng3</typeparam>
        /// <param name="joinExp">Cách nối bảng,Trường nối bảng(Quan hệ khóa chính - khóa ngoại)</param>
        /// <param name="selectExp">Kết quả truy vấn nối bảng</param>
        /// <param name="isWhere">Có cần điều kiện truy vấn hay không</param>
        /// <param name="whereExp">Điều kiện truy vấn</param>
        /// <param name="isOrderBy">Có cần điều kiện sắp xếp hay không</param>
        /// <param name="orderBy">Điều kiện sắp xếp</param>
        /// <param name="orderByType">Kiểu sắp xếp(Asc,Desc)</param>
        /// <returns></returns>
        public async Task<dynamic> getJoinList<T1, T2, T3>(int pageIndex, int pageSize, Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Db.Queryable(joinExp).WhereIF(isWhere, whereExp).OrderByIF(isOrderBy, orderBy)
                .Select(selectExp).ToPageListAsync(pageIndex, pageSize);
        }

        /// <summary>
        /// Thực thi truy vấnsqlcâu lệnh ,Trả về tập dữ liệu
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <returns></returns>
        public async Task<List<TEntity>> getListBySql(string sql)
        {
            return await Db.SqlQueryable<TEntity>(sql).ToListAsync();
        }

        #region Ado 
        /// <summary>
        /// Thực thi câu lệnh không truy vấnsqlcâu lệnh,trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">Tham số hóa</param>
        /// <returns></returns>
        public async Task<bool> ExecuteCommandSql(string sql, params SugarParameter[] parameters)
        {
            return await Db.Ado.ExecuteCommandAsync(sql, parameters) > 0;
        }

        /// <summary>
        /// Thực thi truy vấnsqlcâu lệnh,Trả về tập kết quả truy vấn
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">Tham số hóa</param>
        /// <returns></returns>
        public async Task<List<TEntity>> getListBySqlQuery(string sql, params SugarParameter[] parameters)
        {
            return await Db.Ado.SqlQueryAsync<TEntity>(sql, parameters);
        }

        /// <summary>
        /// Thực thi truy vấnsqlcâu lệnh,Trả về Hàng đầu tiên, cột đầu tiên
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">Tham số hóa</param>
        /// <returns></returns>
        public async Task<object> getScalar(string sql, params SugarParameter[] parameters)
        {
            return await Db.Ado.GetScalarAsync(sql, parameters);
        }

        #endregion

        #endregion

        #region DbMuti Query
        /// <summary>
        /// Theo điều kiện Truy vấn (choose db)
        /// </summary>
        /// <param name="where">Điều kiện</param>
        /// <returns></returns>
        public async Task<List<T>> getByWhere<T>(string db, Expression<Func<T, bool>> where, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Db.GetConnectionScope(db).Queryable<T>().Where(where).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// Truy vấn hai bảng,(choose db)
        /// </summary>
        /// <typeparam name="T1">Bảng trái</typeparam>
        /// <typeparam name="T2">Bảng phải</typeparam>
        /// <param name="joinExp">Cách nối bảng,Trường nối bảng(Quan hệ khóa chính - khóa ngoại)</param>
        /// <param name="selectExp">Kết quả truy vấn nối bảng</param>
        /// <param name="isWhere">Có cần điều kiện truy vấn hay không</param>
        /// <param name="whereExp">Truy vấn theo điều kiện</param>
        /// <param name="isOrderBy">Có cần sắp xếp hay không</param>
        /// <param name="orderBy">Điều kiện sắp xếp</param>
        /// <param name="orderByType">Kiểu sắp xếp(Asc,Desc)</param>
        /// <returns></returns>
        public async Task<List<object>> getJoinList<T1, T2>(string db,Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Db.GetConnectionScope(db).Queryable(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy).Select(selectExp).ToListAsync();
        }

        #region Ado 
        /// <summary>
        /// Thực thi câu lệnh không truy vấnsqlcâu lệnh,trả về thao tác có thành công hay không(choose db)
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">Tham số hóa</param>
        /// <returns></returns>
        public async Task<bool> ExecuteCommandSql(string db, string sql, params SugarParameter[] parameters)
        {
            return await Db.GetConnectionScope(db).Ado.ExecuteCommandAsync(sql, parameters) > 0;
        }

        /// <summary>
        /// Thực thi truy vấnsqlcâu lệnh,Trả về tập kết quả truy vấn(choose db)
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">Tham số hóa</param>
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
        /// Thực thi truy vấnsqlcâu lệnh,Trả về Hàng đầu tiên, cột đầu tiên(choose db)
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">Tham số hóa</param>
        /// <returns></returns>
        public async Task<object> getScalar(string db, string sql, params SugarParameter[] parameters)
        {
            return await Db.GetConnectionScope(db).Ado.GetScalarAsync(sql, parameters);
        }

        #endregion
        #endregion
    }
}