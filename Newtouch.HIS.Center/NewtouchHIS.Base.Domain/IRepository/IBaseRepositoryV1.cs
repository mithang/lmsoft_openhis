
using SqlSugar;
using System.Data;
using System.Linq.Expressions;

namespace NewtouchHIS.Base.Domain
{
    public interface IBaseRepositoryV1<T> where T : class, new()
    {
        #region Add
        /// <summary>
        /// Thêm một bản ghi
        /// </summary>
        /// <param name="model">Đối tượng thực thể</param>
        /// <returns>Thao tác có thành công hay không</returns>
        public Task<bool> Add(T model);

        /// <summary>
        /// Thêm nhiều bản ghi
        /// </summary>
        /// <param name="list">Tập hợp thực thể</param>
        /// <returns>Thao tác có thành công hay không</returns>
        public Task<bool> AddRange(List<T> list);


        /// <summary>
        /// Thêm một bản ghi,và trả về Cột tự tăng
        /// </summary>
        /// <param name="model">Đối tượng thực thể</param>
        /// <returns></returns>
        public Task<bool> AddReturnIdentity(T model);

        /// <summary>
        /// Thêm một bản ghi ,và trả về Thực thể
        /// </summary>
        /// <param name="model">Đối tượng thực thể</param>
        /// <returns></returns>
        public Task<T> AddReturnEntity(T model);


        /// <summary>
        /// Chỉ thêm cột chỉ định
        /// </summary>
        /// <param name="model">Đối tượng thực thể</param>
        /// <param name="columns">Cột chỉ định cần thêm</param>
        /// <returns></returns>
        public Task<bool> AddColumns(T model, params string[] columns);


        /// <summary>
        /// Không chèn cột chỉ định
        /// </summary>
        /// <param name="model">Đối tượng thực thể</param>
        /// <param name="IgnoreColumns">Cột cần bỏ qua</param>
        /// <returns></returns>
        public Task<bool> AddColumnsByIgnoreColumns(T model, params string[] IgnoreColumns);


        #endregion

        #region Delete

        /// <summary>
        /// TheoKhóa chínhXóa,và trả về thao tác có thành công hay không
        /// </summary>
        /// <typeparam name="S">Khóa chínhkiểu</typeparam>
        /// <param name="key">Khóa chính</param>
        /// <returns></returns>
        public Task<bool> Delete<S>(S key);


        /// <summary>
        /// TheoKhóa chínhXóa,và trả về thao tác có thành công hay không
        /// </summary>
        /// <typeparam name="S">Khóa chínhLoại</typeparam>
        /// <param name="keys">Khóa chính</param>
        /// <returns></returns>
        public Task<bool> DeleteRange<S>(params S[] keys);


        /// <summary>
        /// Xóa theo điều kiện,và trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="where">Điều kiện</param>
        /// <returns></returns>
        public Task<bool> DeleteWhere(Expression<Func<T, bool>> where);


        #endregion

        #region Update

        /// <summary>
        /// TheoKhóa chínhCập nhật ,trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<bool> Update(T model);


        /// <summary>
        /// TheoKhóa chínhCập nhật,trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="list">Tập hợp thực thể</param>
        /// <returns></returns>
        public Task<bool> UpdateRange(List<T> list);


        /// <summary>
        /// Theo chỉ định Điều kiện cột Cập nhật ,và trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="model">Đối tượng thực thể</param>
        /// <param name="expression">Điều kiện cột Ví dụ: t=>t.id>5 </param>
        public Task<bool> Update(T model, Expression<Func<T, object>> expression);


        /// <summary>
        /// TheoKhóa chính,Cập nhật cột chỉ định,trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="model">Đối tượng thực thể</param>
        /// <param name="columns">Cột cần cập nhật</param>
        /// <returns></returns>
        public Task<bool> UpdateColumns(T model, params string[] columns);


        /// <summary>
        /// TheoKhóa chính,Cập nhật cột chỉ định,trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="model">Đối tượng thực thể</param>
        /// <param name="columns">Cột cần cập nhật</param>
        /// <returns></returns>
        public Task<bool> UpdateColumns(T model, Expression<Func<T, object>> columns);


        /// <summary>
        /// TheoKhóa chính , Bỏ qua cập nhật cột chỉ định,trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="model">Đối tượng thực thể</param>
        /// <param name="columns">không cập nhật Cột bỏ qua</param>
        /// <returns></returns>
        public Task<bool> UpdateColumnsByIgnoreColumns(T model, params string[] columns);


        /// <summary>
        /// TheoKhóa chính , Bỏ qua cập nhật cột chỉ định,trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="model">Đối tượng thực thể</param>
        /// <param name="columns">không cập nhật Cột bỏ qua</param>
        /// <returns></returns>
        public Task<bool> UpdateColumnsByIgnoreColumns(T model, Expression<Func<T, object>> columns);

        /// <summary>
        /// TheoKhóa chínhCập nhật Cột
        /// </summary>
        /// <param name="model">Đối tượng thực thể</param>
        /// <param name="ignoreAllNullColumns">Có hay không NULLcột không cập nhật</param>
        /// <param name="isOffIdentity">Có bỏ qua hay không Cột tự tăng</param>
        /// <param name="ignoreAllDefaultValue">Có hay không Bỏ qua cột giá trị mặc định</param>
        /// <returns></returns>
        public Task<bool> UpdateNotNullColumns(T model, bool ignoreAllNullColumns, bool isOffIdentity = false, bool ignoreAllDefaultValue = false);


        //4.6.0.7 Cập nhật nối bảng

        /// <summary>
        /// //Theo điều kiện khác nhau thực thi cập nhật cột khác nhau 
        /// </summary>
        /// <param name="model">Đối tượng thực thể</param>
        /// <param name="dic">Điều kiện(key:Cột cần cập nhật,value:Điều kiện có cập nhật cột này hay không)</param>
        /// <returns></returns>
        public Task<bool> UpdateIF(T model, Dictionary<Expression<Func<T, object>>, bool> dic);


        #endregion

        #region Query

        /// <summary>
        /// Truy vấn tất cả dữ liệu
        /// </summary>
        /// <returns></returns>
        public Task<List<T>> FindAll(bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// Lấy đầu num bản ghi
        /// </summary>
        /// <param name="num">Lấy mấy bản ghi đầu</param>
        /// <returns></returns>
        public Task<List<T>> getTakeList(int num, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// Lấy dữ liệu một bảng Dữ liệu phân trang
        /// </summary>
        /// <param name="skip">Bỏ qua mấy bản ghi</param>
        /// <param name="take">Lấy mấy bản ghi</param>
        /// <param name="whereExp">Bỏ qua mấy bản ghi</param>
        /// <param name="orderBy">Điều kiện sắp xếp</param>
        /// <param name="orderByType">Kiểu sắp xếp(Asc,Desc)</param>
        /// <returns></returns>
        public Task<List<T>> getPageList(int skip, int take, Expression<Func<T, bool>> whereExp, Expression<Func<T, object>> orderBy, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// Lấy các bản ghi đầu thỏa điều kiện num bản ghi
        /// </summary>
        /// <param name="where">Điều kiện</param>
        /// <param name="num">Lấy mấy bản ghi đầu</param>
        /// <returns></returns> 
        public Task<List<T>> getTakeList(Expression<Func<T, bool>> where, int num, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// TheoKhóa chínhTruy vấn
        /// </summary>
        /// <param name="pkValue">Khóa chính</param>
        /// <returns></returns>
        public Task<T> getByPrimaryKey(object pkValue);


        /// <summary>
        /// Lấy theo điều kiện Một bản ghi 
        /// </summary>
        /// <param name="where">Điều kiện</param>
        /// <returns></returns>
        public Task<T> getFirstOrDefault(Expression<Func<T, bool>> where);


        /// <summary>
        /// TheoKhóa chính In  Truy vấn
        /// </summary>
        /// <typeparam name="S">Khóa chínhkiểu</typeparam>
        /// <param name="list">Khóa chính In Tập kết quả của thao tác</param>
        /// <returns></returns>
        public Task<List<T>> getByIn<S>(List<S> list, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// Theo cột chỉ định In Truy vấn
        /// </summary>
        /// <typeparam name="S">Kiểu của cột chỉ định</typeparam>
        /// <param name="column">Cột chỉ định</param>
        /// <param name="list">Cột chỉ định In Thao tác tập kết quả</param>
        /// <returns></returns>
        public Task<List<T>> getByIn<S>(Expression<Func<T, object>> column, List<S> list, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// Theo cột chỉ định Not In (!Contain)Truy vấn
        /// </summary>
        /// <typeparam name="S">Kiểu cột chỉ định</typeparam>
        /// <param name="list">Not Intập kết quả</param>
        /// <param name="field">Cột chỉ định</param>
        /// <returns></returns>
        public Task<List<T>> getByNotIn<S>(List<S> list, object field, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// Theo điều kiện Truy vấn
        /// </summary>
        /// <param name="where">Điều kiện</param>
        /// <returns></returns>
        public Task<List<T>> getByWhere(Expression<Func<T, bool>> where, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// Một điều kiện Theo isWhere Xét Có sử dụng điều kiện này để truy vấn hay không
        /// </summary>
        /// <param name="isWhere">Điều kiện xét có sử dụng điều kiện truy vấn này hay không</param>
        /// <param name="where">Điều kiện truy vấn</param>
        /// <returns></returns>
        public Task<List<T>> getByWhereIF(bool isWhere, Expression<Func<T, bool>> where, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// Nhiều điều kiện Theo wheres.value  Xét có sử dụng hay không này wheres.key điều kiện
        /// </summary>
        /// <param name="wheres">Điều kiện truy vấn</param>
        /// <returns></returns>
        public Task<List<T>> getByWhereIF(Dictionary<Expression<Func<T, bool>>, bool> wheres, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// Truy vấn Giá trị của cột chỉ định tại startđếnend dữ liệu trong khoảng
        /// </summary>
        /// <param name="value">Lớp chỉ định</param>
        /// <param name="start">Bắt đầu</param>
        /// <param name="end">Kết thúc</param>
        /// <returns></returns>
        public Task<List<T>> getByBetween(object value, object start, object end, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// Xét bản ghi này có tồn tại hay không
        /// </summary>
        /// <param name="where">Điều kiện</param>
        /// <returns></returns>
        public Task<bool> getIsAny(Expression<Func<T, bool>> where);


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
        public Task<List<T>> getPageList<T>(int pageIndex, int pageSize, bool isWhere = false, Expression<Func<T, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


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
        public Task<dynamic> getJoinList<T1, T2>(Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


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
        public Task<dynamic> getJoinPageList<T1, T2>(int pageIndex, int pageSize, Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


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
        public Task<dynamic> getJoinList<T1, T2, T3>(Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


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
        public Task<dynamic> getJoinList<T1, T2, T3>(int pageIndex, int pageSize, Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// Thực thi truy vấnsqlcâu lệnh ,Trả về tập dữ liệu
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <returns></returns>
        public Task<List<T>> getListBySql(string sql);


        /// <summary>
        /// Thực thi câu lệnh không truy vấnsqlcâu lệnh,trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">Tham số hóa</param>
        /// <returns></returns>
        public Task<bool> ExecuteCommandSql(string sql, params SugarParameter[] parameters);


        /// <summary>
        /// Thực thi truy vấnsqlcâu lệnh,Trả về tập kết quả truy vấn
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">Tham số hóa</param>
        /// <returns></returns>
        public Task<List<T>> getListBySqlQuery(string sql, params SugarParameter[] parameters);


        /// <summary>
        /// Thực thi truy vấnsqlcâu lệnh,Trả về Hàng đầu tiên, cột đầu tiên
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">Tham số hóa</param>
        /// <returns></returns>
        public Task<object> getScalar(string sql, params SugarParameter[] parameters);



        #endregion

        #region Other


        /// <summary>
        /// Sử dụng stored procedure,Trả về tập kết quả
        /// </summary>
        /// <param name="procedureName">Tên stored procedure </param>
        /// <param name="parameters">Tham số,Hỗ trợ output</param>
        /// <returns></returns>
        public Task<DataTable> UseStoredProcedure(string procedureName, params SugarParameter[] parameters);


        /// <summary>
        /// Sử dụng transaction ,Không có giá trị trả về
        /// </summary>
        /// <param name="action">Hành động thực thi</param>
        /// <param name="errorCallBack">Callback lỗi</param>
        public DbResult<bool> UseTran(Action action, Action<Exception> errorCallBack);


        /// <summary>
        /// Sử dụng transaction,Có giá trị trả về
        /// </summary>
        /// <typeparam name="S">Kiểu giá trị trả về</typeparam>
        /// <param name="func">Hành động thực thi</param>
        /// <param name="errorCallBack">Callback lỗi</param>
        public DbResult<S> UseTran<S>(Func<S> func, Action<Exception> errorCallBack);


        #endregion
    }
}
