using NewtouchHIS.Base.Domain;
using NewtouchHIS.Lib.Base.Model;
using SqlSugar;
using System.Data;
using System.Linq.Expressions;

namespace NewtouchHIS.Base.Repository
{
    public class BaseRepositoryV1<T> :IBaseRepositoryV1<T> where T : class, new()
    {
        public SqlSugarClient context;
        public BaseRepositoryV1(SqlSugarClient db)
        {
            context = db;
        }

        #region Add
        /// <summary>
        /// thêm một bản ghi dữ liệu
        /// </summary>
        /// <param name="model">đối tượng thực thể</param>
        /// <returns>thao tác có thành công hay không</returns>
        public async Task<bool> Add(T model)
        {
            return await context.Insertable<T>(model).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// thêm nhiều bản ghi dữ liệu
        /// </summary>
        /// <param name="list">tập hợp thực thể</param>
        /// <returns>thao tác có thành công hay không</returns>
        public async Task<bool> AddRange(List<T> list)
        {
            return await context.Insertable<T>(list).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// thêm một bản ghi dữ liệu，đồng thời trả về cột tự tăng
        /// </summary>
        /// <param name="model">đối tượng thực thể</param>
        /// <returns></returns>
        public async Task<bool> AddReturnIdentity(T model)
        {
            return await context.Insertable<T>(model).ExecuteReturnIdentityAsync() > 0;//ExecuteReturnBigIdentity();//4.5.0.2 +  long
        }

        /// <summary>
        /// thêm một bản ghi dữ liệu ，đồng thời trả về thực thể
        /// </summary>
        /// <param name="model">đối tượng thực thể</param>
        /// <returns></returns>
        public async Task<T> AddReturnEntity(T model)
        {
            return await context.Insertable<T>(model).ExecuteReturnEntityAsync();
        }

        /// <summary>
        /// chỉ thêm cột chỉ định
        /// </summary>
        /// <param name="model">đối tượng thực thể</param>
        /// <param name="columns">chỉ định cột cần thêm</param>
        /// <returns></returns>
        public async Task<bool> AddColumns(T model, params string[] columns)
        {
            return await context.Insertable<T>(model).InsertColumns(columns).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// không chèn cột chỉ định
        /// </summary>
        /// <param name="model">đối tượng thực thể</param>
        /// <param name="IgnoreColumns">cột cần bỏ qua</param>
        /// <returns></returns>
        public async Task<bool> AddColumnsByIgnoreColumns(T model, params string[] IgnoreColumns)
        {
            return await context.Insertable<T>(model)
                .IgnoreColumns(IgnoreColumns).ExecuteCommandAsync() > 0;
        }

        #endregion

        #region Delete

        /// <summary>
        /// xóa theo khóa chính，đồng thời trả về thao tác có thành công hay không
        /// </summary>
        /// <typeparam name="S">kiểu của khóa chính</typeparam>
        /// <param name="key">khóa chính</param>
        /// <returns></returns>
        public async Task<bool> Delete<S>(S key)
        {
            return await context.Deleteable<T>().In(key).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// xóa theo khóa chính，đồng thời trả về thao tác có thành công hay không
        /// </summary>
        /// <typeparam name="S">kiểu khóa chính</typeparam>
        /// <param name="keys">khóa chính</param>
        /// <returns></returns>
        public async Task<bool> DeleteRange<S>(params S[] keys)
        {
            return await context.Deleteable<T>().In(keys).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// xóa theo điều kiện，đồng thời trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="where">điều kiện</param>
        /// <returns></returns>
        public async Task<bool> DeleteWhere(Expression<Func<T, bool>> where)
        {
            return await context.Deleteable<T>().Where(where).ExecuteCommandAsync() > 0;
        }

        #endregion

        #region Update

        /// <summary>
        /// cập nhật theo khóa chính ，trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Update(T model)
        {
            return await context.Updateable<T>(model).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// cập nhật theo khóa chính，trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="list">tập hợp thực thể</param>
        /// <returns></returns>
        public async Task<bool> UpdateRange(List<T> list)
        {
            return await context.Updateable<T>(list).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// theo chỉ định điều kiện cột cập nhật ，đồng thời trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="model">đối tượng thực thể</param>
        /// <param name="expression">điều kiện cột Ví dụ： t=>t.id>5 </param>
        public async Task<bool> Update(T model, Expression<Func<T, object>> expression)
        {
            return await context.Updateable<T>(model).WhereColumns(expression).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// theo khóa chính，cập nhật cột chỉ định,trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="model">đối tượng thực thể</param>
        /// <param name="columns">cột cần cập nhật</param>
        /// <returns></returns>
        public async Task<bool> UpdateColumns(T model, params string[] columns)
        {
            return await context.Updateable<T>(model).UpdateColumns(columns).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// theo khóa chính，cập nhật cột chỉ định,trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="model">đối tượng thực thể</param>
        /// <param name="columns">cột cần cập nhật</param>
        /// <returns></returns>
        public async Task<bool> UpdateColumns(T model, Expression<Func<T, object>> columns)
        {
            return await context.Updateable<T>(model).UpdateColumns(columns).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// theo khóa chính ， bỏ qua cập nhật cột chỉ định，trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="model">đối tượng thực thể</param>
        /// <param name="columns">không cập nhật cột bỏ qua</param>
        /// <returns></returns>
        public async Task<bool> UpdateColumnsByIgnoreColumns(T model, params string[] columns)
        {
            return await context.Updateable<T>(model).IgnoreColumns(columns).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// theo khóa chính ， bỏ qua cập nhật cột chỉ định，trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="model">đối tượng thực thể</param>
        /// <param name="columns">không cập nhật cột bỏ qua</param>
        /// <returns></returns>
        public async Task<bool> UpdateColumnsByIgnoreColumns(T model, Expression<Func<T, object>> columns)
        {
            return await context.Updateable<T>(model).IgnoreColumns(columns).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// cập nhật theo khóa chính cột
        /// </summary>
        /// <param name="model">đối tượng thực thể</param>
        /// <param name="ignoreAllNullColumns">có hay không NULLcột của không cập nhật</param>
        /// <param name="isOffIdentity">có bỏ qua hay không cột tự tăng</param>
        /// <param name="ignoreAllDefaultValue">có hay không bỏ qua cột giá trị mặc định</param>
        /// <returns></returns>
        public async Task<bool> UpdateNotNullColumns(T model, bool ignoreAllNullColumns, bool isOffIdentity = false, bool ignoreAllDefaultValue = false)
        {
            return await context.Updateable<T>()
                .IgnoreColumns(ignoreAllNullColumns: true, isOffIdentity: false, ignoreAllDefaultValue: false)
                .ExecuteCommandAsync() > 0;
        }

        //4.6.0.7 cập nhật liên bảng

        /// <summary>
        /// //thực thi cập nhật các cột khác nhau theo các điều kiện khác nhau 
        /// </summary>
        /// <param name="model">đối tượng thực thể</param>
        /// <param name="dic">điều kiện（key：cột cần cập nhật，value：điều kiện có cập nhật cột này hay không）</param>
        /// <returns></returns>
        public async Task<bool> UpdateIF(T model, Dictionary<Expression<Func<T, object>>, bool> dic)
        {
            var able = context.Updateable<T>(model);
            foreach (var item in dic)
            {
                able.UpdateColumnsIF(item.Value, item.Key);// s=>s.name  ture
            }

            return await able.ExecuteCommandAsync() > 0;
        }

        #endregion

        #region Query

        /// <summary>
        /// truy vấn tất cả dữ liệu
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> FindAll(bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// lấy đầu num bản ghi dữ liệu
        /// </summary>
        /// <param name="num">lấy vài bản ghi đầu</param>
        /// <returns></returns>
        public async Task<List<T>> getTakeList(int num, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().OrderByIF(isOrderBy, orderBy, orderByType).Take(num).ToListAsync();
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
        public async Task<List<T>> getPageList(int skip, int take, Expression<Func<T, bool>> whereExp, Expression<Func<T, object>> orderBy, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().Skip(skip).Take(take).OrderBy(orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// lấy các bản ghi đầu thỏa điều kiện num bản ghi dữ liệu
        /// </summary>
        /// <param name="where">điều kiện</param>
        /// <param name="num">lấy vài bản ghi đầu</param>
        /// <returns></returns> 
        public async Task<List<T>> getTakeList(Expression<Func<T, bool>> where, int num, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().Where(where).OrderByIF(isOrderBy, orderBy, orderByType).Take(num).ToListAsync();
        }

        /// <summary>
        /// truy vấn theo khóa chính
        /// </summary>
        /// <param name="pkValue">khóa chính</param>
        /// <returns></returns>
        public async Task<T> getByPrimaryKey(object pkValue)
        {
            return await context.Queryable<T>().InSingleAsync(pkValue);
        }

        /// <summary>
        /// lấy theo điều kiện một bản ghi dữ liệu 
        /// </summary>
        /// <param name="where">điều kiện</param>
        /// <returns></returns>
        public async Task<T> getFirstOrDefault(Expression<Func<T, bool>> where)
        {
            return await context.Queryable<T>().FirstAsync(where);//truy vấn một bản ghi, không có dữ liệu trả vềNULL, Singlevượt quá1bản ghi sẽ báo lỗi，Firstkhông
        }

        /// <summary>
        /// theo khóa chính In  truy vấn
        /// </summary>
        /// <typeparam name="S">kiểu của khóa chính</typeparam>
        /// <param name="list">khóa chính In tập kết quả của thao tác</param>
        /// <returns></returns>
        public async Task<List<T>> getByIn<S>(List<S> list, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().In<S>(list).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// theo cột chỉ định In truy vấn
        /// </summary>
        /// <typeparam name="S">kiểu của cột chỉ định</typeparam>
        /// <param name="column">cột chỉ định</param>
        /// <param name="list">cột chỉ định In thao tác tập kết quả của</param>
        /// <returns></returns>
        public async Task<List<T>> getByIn<S>(Expression<Func<T, object>> column, List<S> list, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().In<S>(column, list).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// theo cột chỉ định Not In (!Contain)truy vấn
        /// </summary>
        /// <typeparam name="S">kiểu cột chỉ định</typeparam>
        /// <param name="list">Not Intập kết quả của</param>
        /// <param name="field">cột chỉ định</param>
        /// <returns></returns>
        public async Task<List<T>> getByNotIn<S>(List<S> list, object field, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().Where(t => !SqlFunc.ContainsArray(list, field)).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// theo điều kiện truy vấn
        /// </summary>
        /// <param name="where">điều kiện</param>
        /// <returns></returns>
        public async Task<List<T>> getByWhere(Expression<Func<T, bool>> where, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().Where(where).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// một điều kiện đơn theo isWhere phán đoán có sử dụng điều kiện này để truy vấn hay không
        /// </summary>
        /// <param name="isWhere">điều kiện phán đoán có sử dụng điều kiện truy vấn này hay không</param>
        /// <param name="where">điều kiện truy vấn</param>
        /// <returns></returns>
        public async Task<List<T>> getByWhereIF(bool isWhere, Expression<Func<T, bool>> where, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().WhereIF(isWhere, where).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// nhiều điều kiện theo wheres.value  phán đoán có sử dụng hay không này wheres.key điều kiện của
        /// </summary>
        /// <param name="wheres">điều kiện truy vấn</param>
        /// <returns></returns>
        public async Task<List<T>> getByWhereIF(Dictionary<Expression<Func<T, bool>>, bool> wheres, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            var able = context.Queryable<T>();
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
        public async Task<List<T>> getByBetween(object value, object start, object end, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().Where(it => SqlFunc.Between(value, start, end)).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// phán đoán bản ghi này có tồn tại hay không
        /// </summary>
        /// <param name="where">điều kiện</param>
        /// <returns></returns>
        public async Task<bool> getIsAny(Expression<Func<T, bool>> where)
        {
            return await context.Queryable<T>().AnyAsync(where);
        }

        /// <summary>
        /// truy vấn phân trang một bảng
        /// </summary>
        /// <typeparam name="T">bảng cần truy vấn</typeparam>
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
            return await context.Queryable<T>().WhereIF(isWhere, whereExp).OrderByIF(isOrderBy, orderBy).ToPageListAsync(pageIndex, pageSize);
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
        public async Task<dynamic> getJoinList<T1, T2>(Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {

            return await context.Queryable<T1, T2>(joinExp).WhereIF(isWhere, whereExp)
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

            return await context.Queryable<T1, T2>(joinExp).WhereIF(isWhere, whereExp)
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
            return await context.Queryable<T1, T2, T3>(joinExp).WhereIF(isWhere, whereExp).OrderByIF(isOrderBy, orderBy).Select(selectExp).ToListAsync();
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
            return await context.Queryable<T1, T2, T3>(joinExp).WhereIF(isWhere, whereExp).OrderByIF(isOrderBy, orderBy)
                .Select(selectExp).ToPageListAsync(pageIndex, pageSize);
        }

        /// <summary>
        /// thực thi truy vấnsqlcâu lệnh ，trả về tập dữ liệu
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <returns></returns>
        public async Task<List<T>> getListBySql(string sql)
        {
            return await context.SqlQueryable<T>(sql).ToListAsync();
        }

        /// <summary>
        /// thực thi câu lệnh không phải truy vấnsqlcâu lệnh，trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">tham số hóa</param>
        /// <returns></returns>
        public async Task<bool> ExecuteCommandSql(string sql, params SugarParameter[] parameters)
        {
            return await context.Ado.ExecuteCommandAsync(sql, parameters) > 0;
        }

        /// <summary>
        /// thực thi truy vấnsqlcâu lệnh，trả về tập kết quả truy vấn
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">tham số hóa</param>
        /// <returns></returns>
        public async Task<List<T>> getListBySqlQuery(string sql, params SugarParameter[] parameters)
        {
            return await context.Ado.SqlQueryAsync<T>(sql, parameters);
        }

        /// <summary>
        /// thực thi truy vấnsqlcâu lệnh，trả về hàng đầu tiên, cột đầu tiên
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">tham số hóa</param>
        /// <returns></returns>
        public async Task<object> getScalar(string sql, params SugarParameter[] parameters)
        {
            return await context.Ado.GetScalarAsync(sql, parameters);
        }


        #endregion

        #region Other


        /// <summary>
        /// sử dụng thủ tục lưu trữ，trả về tập kết quả
        /// </summary>
        /// <param name="procedureName">tên thủ tục lưu trữ </param>
        /// <param name="parameters">tham số，hỗ trợ output</param>
        /// <returns></returns>
        public async Task<DataTable> UseStoredProcedure(string procedureName, params SugarParameter[] parameters)
        {
            return await context.Ado.UseStoredProcedure().GetDataTableAsync(procedureName, parameters);
        }

        /// <summary>
        /// sử dụng giao dịch ，không có giá trị trả về
        /// </summary>
        /// <param name="action">hành động thực thi</param>
        /// <param name="errorCallBack">gọi lại khi lỗi</param>
        public DbResult<bool> UseTran(Action action, Action<Exception> errorCallBack)
        {
            return context.Ado.UseTran(action, errorCallBack); ;
        }

        /// <summary>
        /// sử dụng giao dịch，có giá trị trả về
        /// </summary>
        /// <typeparam name="S">kiểu giá trị trả về</typeparam>
        /// <param name="func">hành động thực thi</param>
        /// <param name="errorCallBack">gọi lại khi lỗi</param>
        public DbResult<S> UseTran<S>(Func<S> func, Action<Exception> errorCallBack)
        {
            return context.Ado.UseTran(func, errorCallBack);
        }

        #endregion

    }
}