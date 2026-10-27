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
        /// Thêm một bản ghi
        /// </summary>
        /// <param name="model">Đối tượng thực thể</param>
        /// <returns>Thao tác có thành công hay không</returns>
        public async Task<bool> Add(T model)
        {
            return await context.Insertable<T>(model).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// Thêm nhiều bản ghi
        /// </summary>
        /// <param name="list">Tập hợp thực thể</param>
        /// <returns>Thao tác có thành công hay không</returns>
        public async Task<bool> AddRange(List<T> list)
        {
            return await context.Insertable<T>(list).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// Thêm một bản ghi,và trả về Cột tự tăng
        /// </summary>
        /// <param name="model">Đối tượng thực thể</param>
        /// <returns></returns>
        public async Task<bool> AddReturnIdentity(T model)
        {
            return await context.Insertable<T>(model).ExecuteReturnIdentityAsync() > 0;//ExecuteReturnBigIdentity();//4.5.0.2 +  long
        }

        /// <summary>
        /// Thêm một bản ghi ,và trả về Thực thể
        /// </summary>
        /// <param name="model">Đối tượng thực thể</param>
        /// <returns></returns>
        public async Task<T> AddReturnEntity(T model)
        {
            return await context.Insertable<T>(model).ExecuteReturnEntityAsync();
        }

        /// <summary>
        /// Chỉ thêm cột chỉ định
        /// </summary>
        /// <param name="model">Đối tượng thực thể</param>
        /// <param name="columns">Cột chỉ định cần thêm</param>
        /// <returns></returns>
        public async Task<bool> AddColumns(T model, params string[] columns)
        {
            return await context.Insertable<T>(model).InsertColumns(columns).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// Không chèn cột chỉ định
        /// </summary>
        /// <param name="model">Đối tượng thực thể</param>
        /// <param name="IgnoreColumns">Cột cần bỏ qua</param>
        /// <returns></returns>
        public async Task<bool> AddColumnsByIgnoreColumns(T model, params string[] IgnoreColumns)
        {
            return await context.Insertable<T>(model)
                .IgnoreColumns(IgnoreColumns).ExecuteCommandAsync() > 0;
        }

        #endregion

        #region Delete

        /// <summary>
        /// TheoKhóa chínhXóa,và trả về thao tác có thành công hay không
        /// </summary>
        /// <typeparam name="S">Khóa chínhkiểu</typeparam>
        /// <param name="key">Khóa chính</param>
        /// <returns></returns>
        public async Task<bool> Delete<S>(S key)
        {
            return await context.Deleteable<T>().In(key).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// TheoKhóa chínhXóa,và trả về thao tác có thành công hay không
        /// </summary>
        /// <typeparam name="S">Khóa chínhLoại</typeparam>
        /// <param name="keys">Khóa chính</param>
        /// <returns></returns>
        public async Task<bool> DeleteRange<S>(params S[] keys)
        {
            return await context.Deleteable<T>().In(keys).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// Xóa theo điều kiện,và trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="where">Điều kiện</param>
        /// <returns></returns>
        public async Task<bool> DeleteWhere(Expression<Func<T, bool>> where)
        {
            return await context.Deleteable<T>().Where(where).ExecuteCommandAsync() > 0;
        }

        #endregion

        #region Update

        /// <summary>
        /// TheoKhóa chínhCập nhật ,trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Update(T model)
        {
            return await context.Updateable<T>(model).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// TheoKhóa chínhCập nhật,trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="list">Tập hợp thực thể</param>
        /// <returns></returns>
        public async Task<bool> UpdateRange(List<T> list)
        {
            return await context.Updateable<T>(list).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// Theo chỉ định Điều kiện cột Cập nhật ,và trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="model">Đối tượng thực thể</param>
        /// <param name="expression">Điều kiện cột Ví dụ: t=>t.id>5 </param>
        public async Task<bool> Update(T model, Expression<Func<T, object>> expression)
        {
            return await context.Updateable<T>(model).WhereColumns(expression).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// TheoKhóa chính,Cập nhật cột chỉ định,trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="model">Đối tượng thực thể</param>
        /// <param name="columns">Cột cần cập nhật</param>
        /// <returns></returns>
        public async Task<bool> UpdateColumns(T model, params string[] columns)
        {
            return await context.Updateable<T>(model).UpdateColumns(columns).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// TheoKhóa chính,Cập nhật cột chỉ định,trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="model">Đối tượng thực thể</param>
        /// <param name="columns">Cột cần cập nhật</param>
        /// <returns></returns>
        public async Task<bool> UpdateColumns(T model, Expression<Func<T, object>> columns)
        {
            return await context.Updateable<T>(model).UpdateColumns(columns).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// TheoKhóa chính , Bỏ qua cập nhật cột chỉ định,trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="model">Đối tượng thực thể</param>
        /// <param name="columns">không cập nhật Cột bỏ qua</param>
        /// <returns></returns>
        public async Task<bool> UpdateColumnsByIgnoreColumns(T model, params string[] columns)
        {
            return await context.Updateable<T>(model).IgnoreColumns(columns).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// TheoKhóa chính , Bỏ qua cập nhật cột chỉ định,trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="model">Đối tượng thực thể</param>
        /// <param name="columns">không cập nhật Cột bỏ qua</param>
        /// <returns></returns>
        public async Task<bool> UpdateColumnsByIgnoreColumns(T model, Expression<Func<T, object>> columns)
        {
            return await context.Updateable<T>(model).IgnoreColumns(columns).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// TheoKhóa chínhCập nhật Cột
        /// </summary>
        /// <param name="model">Đối tượng thực thể</param>
        /// <param name="ignoreAllNullColumns">Có hay không NULLcột không cập nhật</param>
        /// <param name="isOffIdentity">Có bỏ qua hay không Cột tự tăng</param>
        /// <param name="ignoreAllDefaultValue">Có hay không Bỏ qua cột giá trị mặc định</param>
        /// <returns></returns>
        public async Task<bool> UpdateNotNullColumns(T model, bool ignoreAllNullColumns, bool isOffIdentity = false, bool ignoreAllDefaultValue = false)
        {
            return await context.Updateable<T>()
                .IgnoreColumns(ignoreAllNullColumns: true, isOffIdentity: false, ignoreAllDefaultValue: false)
                .ExecuteCommandAsync() > 0;
        }

        //4.6.0.7 Cập nhật nối bảng

        /// <summary>
        /// //Theo điều kiện khác nhau thực thi cập nhật cột khác nhau 
        /// </summary>
        /// <param name="model">Đối tượng thực thể</param>
        /// <param name="dic">Điều kiện(key:Cột cần cập nhật,value:Điều kiện có cập nhật cột này hay không)</param>
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
        /// Truy vấn tất cả dữ liệu
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> FindAll(bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// Lấy đầu num bản ghi
        /// </summary>
        /// <param name="num">Lấy mấy bản ghi đầu</param>
        /// <returns></returns>
        public async Task<List<T>> getTakeList(int num, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().OrderByIF(isOrderBy, orderBy, orderByType).Take(num).ToListAsync();
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
        public async Task<List<T>> getPageList(int skip, int take, Expression<Func<T, bool>> whereExp, Expression<Func<T, object>> orderBy, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().Skip(skip).Take(take).OrderBy(orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// Lấy các bản ghi đầu thỏa điều kiện num bản ghi
        /// </summary>
        /// <param name="where">Điều kiện</param>
        /// <param name="num">Lấy mấy bản ghi đầu</param>
        /// <returns></returns> 
        public async Task<List<T>> getTakeList(Expression<Func<T, bool>> where, int num, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().Where(where).OrderByIF(isOrderBy, orderBy, orderByType).Take(num).ToListAsync();
        }

        /// <summary>
        /// TheoKhóa chínhTruy vấn
        /// </summary>
        /// <param name="pkValue">Khóa chính</param>
        /// <returns></returns>
        public async Task<T> getByPrimaryKey(object pkValue)
        {
            return await context.Queryable<T>().InSingleAsync(pkValue);
        }

        /// <summary>
        /// Lấy theo điều kiện Một bản ghi 
        /// </summary>
        /// <param name="where">Điều kiện</param>
        /// <returns></returns>
        public async Task<T> getFirstOrDefault(Expression<Func<T, bool>> where)
        {
            return await context.Queryable<T>().FirstAsync(where);//Truy vấn một bản ghi, không có dữ liệu thì trả vềNULL, Singlevượt quá1bản ghi sẽ báo lỗi,Firstsẽ không
        }

        /// <summary>
        /// TheoKhóa chính In  Truy vấn
        /// </summary>
        /// <typeparam name="S">Khóa chínhkiểu</typeparam>
        /// <param name="list">Khóa chính In Tập kết quả của thao tác</param>
        /// <returns></returns>
        public async Task<List<T>> getByIn<S>(List<S> list, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().In<S>(list).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// Theo cột chỉ định In Truy vấn
        /// </summary>
        /// <typeparam name="S">Kiểu của cột chỉ định</typeparam>
        /// <param name="column">Cột chỉ định</param>
        /// <param name="list">Cột chỉ định In Thao tác tập kết quả</param>
        /// <returns></returns>
        public async Task<List<T>> getByIn<S>(Expression<Func<T, object>> column, List<S> list, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().In<S>(column, list).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// Theo cột chỉ định Not In (!Contain)Truy vấn
        /// </summary>
        /// <typeparam name="S">Kiểu cột chỉ định</typeparam>
        /// <param name="list">Not Intập kết quả</param>
        /// <param name="field">Cột chỉ định</param>
        /// <returns></returns>
        public async Task<List<T>> getByNotIn<S>(List<S> list, object field, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().Where(t => !SqlFunc.ContainsArray(list, field)).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// Theo điều kiện Truy vấn
        /// </summary>
        /// <param name="where">Điều kiện</param>
        /// <returns></returns>
        public async Task<List<T>> getByWhere(Expression<Func<T, bool>> where, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().Where(where).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// Một điều kiện Theo isWhere Xét Có sử dụng điều kiện này để truy vấn hay không
        /// </summary>
        /// <param name="isWhere">Điều kiện xét có sử dụng điều kiện truy vấn này hay không</param>
        /// <param name="where">Điều kiện truy vấn</param>
        /// <returns></returns>
        public async Task<List<T>> getByWhereIF(bool isWhere, Expression<Func<T, bool>> where, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().WhereIF(isWhere, where).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// Nhiều điều kiện Theo wheres.value  Xét có sử dụng hay không này wheres.key điều kiện
        /// </summary>
        /// <param name="wheres">Điều kiện truy vấn</param>
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
        /// Truy vấn Giá trị của cột chỉ định tại startđếnend dữ liệu trong khoảng
        /// </summary>
        /// <param name="value">Lớp chỉ định</param>
        /// <param name="start">Bắt đầu</param>
        /// <param name="end">Kết thúc</param>
        /// <returns></returns>
        public async Task<List<T>> getByBetween(object value, object start, object end, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().Where(it => SqlFunc.Between(value, start, end)).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// Xét bản ghi này có tồn tại hay không
        /// </summary>
        /// <param name="where">Điều kiện</param>
        /// <returns></returns>
        public async Task<bool> getIsAny(Expression<Func<T, bool>> where)
        {
            return await context.Queryable<T>().AnyAsync(where);
        }

        /// <summary>
        /// Truy vấn phân trang một bảng
        /// </summary>
        /// <typeparam name="T">Bảng cần truy vấn</typeparam>
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
            return await context.Queryable<T>().WhereIF(isWhere, whereExp).OrderByIF(isOrderBy, orderBy).ToPageListAsync(pageIndex, pageSize);
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
        public async Task<dynamic> getJoinList<T1, T2>(Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {

            return await context.Queryable<T1, T2>(joinExp).WhereIF(isWhere, whereExp)
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

            return await context.Queryable<T1, T2>(joinExp).WhereIF(isWhere, whereExp)
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
            return await context.Queryable<T1, T2, T3>(joinExp).WhereIF(isWhere, whereExp).OrderByIF(isOrderBy, orderBy).Select(selectExp).ToListAsync();
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
            return await context.Queryable<T1, T2, T3>(joinExp).WhereIF(isWhere, whereExp).OrderByIF(isOrderBy, orderBy)
                .Select(selectExp).ToPageListAsync(pageIndex, pageSize);
        }

        /// <summary>
        /// Thực thi truy vấnsqlcâu lệnh ,Trả về tập dữ liệu
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <returns></returns>
        public async Task<List<T>> getListBySql(string sql)
        {
            return await context.SqlQueryable<T>(sql).ToListAsync();
        }

        /// <summary>
        /// Thực thi câu lệnh không truy vấnsqlcâu lệnh,trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">Tham số hóa</param>
        /// <returns></returns>
        public async Task<bool> ExecuteCommandSql(string sql, params SugarParameter[] parameters)
        {
            return await context.Ado.ExecuteCommandAsync(sql, parameters) > 0;
        }

        /// <summary>
        /// Thực thi truy vấnsqlcâu lệnh,Trả về tập kết quả truy vấn
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">Tham số hóa</param>
        /// <returns></returns>
        public async Task<List<T>> getListBySqlQuery(string sql, params SugarParameter[] parameters)
        {
            return await context.Ado.SqlQueryAsync<T>(sql, parameters);
        }

        /// <summary>
        /// Thực thi truy vấnsqlcâu lệnh,Trả về Hàng đầu tiên, cột đầu tiên
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">Tham số hóa</param>
        /// <returns></returns>
        public async Task<object> getScalar(string sql, params SugarParameter[] parameters)
        {
            return await context.Ado.GetScalarAsync(sql, parameters);
        }


        #endregion

        #region Other


        /// <summary>
        /// Sử dụng stored procedure,Trả về tập kết quả
        /// </summary>
        /// <param name="procedureName">Tên stored procedure </param>
        /// <param name="parameters">Tham số,Hỗ trợ output</param>
        /// <returns></returns>
        public async Task<DataTable> UseStoredProcedure(string procedureName, params SugarParameter[] parameters)
        {
            return await context.Ado.UseStoredProcedure().GetDataTableAsync(procedureName, parameters);
        }

        /// <summary>
        /// Sử dụng transaction ,Không có giá trị trả về
        /// </summary>
        /// <param name="action">Hành động thực thi</param>
        /// <param name="errorCallBack">Callback lỗi</param>
        public DbResult<bool> UseTran(Action action, Action<Exception> errorCallBack)
        {
            return context.Ado.UseTran(action, errorCallBack); ;
        }

        /// <summary>
        /// Sử dụng transaction,Có giá trị trả về
        /// </summary>
        /// <typeparam name="S">Kiểu giá trị trả về</typeparam>
        /// <param name="func">Hành động thực thi</param>
        /// <param name="errorCallBack">Callback lỗi</param>
        public DbResult<S> UseTran<S>(Func<S> func, Action<Exception> errorCallBack)
        {
            return context.Ado.UseTran(func, errorCallBack);
        }

        #endregion

    }
}