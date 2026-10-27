
using SqlSugar;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;

namespace NewtouchHIS.Base.Domain
{
    /// <summary>
    /// Giao diện lớp cơ sở,Các giao diện khác kế thừa giao diện này
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        #region TEntity Thao tác
        /// <summary>
        /// TheoIDTruy vấn danh sách
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        Task<TEntity> FindKey(object pkValue);

        /// <summary>
        /// Truy vấn tất cả dữ liệu
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        Task<List<TEntity>> FindAll();

        /// <summary>
        /// Thêm
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<long> Add(TEntity model);

        /// <summary>
        /// Sửa đổi
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> Update(TEntity model);

        /// <summary>
        /// TheoidXóa theo danh sách
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> DeleteByIds(object[] ids);

        /// <summary>
        /// TheoIDXóa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteById(object id);

        #region Query

        /// <summary>
        /// Truy vấn tất cả dữ liệu
        /// </summary>
        /// <returns></returns>
        public Task<List<TEntity>> FindAll(bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// Lấy đầu num bản ghi
        /// </summary>
        /// <param name="num">Lấy mấy bản ghi đầu</param>
        /// <returns></returns>
        public Task<List<TEntity>> getTakeList(int num, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// Lấy dữ liệu một bảng Dữ liệu phân trang
        /// </summary>
        /// <param name="skip">Bỏ qua mấy bản ghi</param>
        /// <param name="take">Lấy mấy bản ghi</param>
        /// <param name="whereExp">Bỏ qua mấy bản ghi</param>
        /// <param name="orderBy">Điều kiện sắp xếp</param>
        /// <param name="orderByType">Kiểu sắp xếp(Asc,Desc)</param>
        /// <returns></returns>
        public Task<List<TEntity>> getPageList(int skip, int take, Expression<Func<TEntity, bool>> whereExp, Expression<Func<TEntity, object>> orderBy, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// Lấy các bản ghi đầu thỏa điều kiện num bản ghi
        /// </summary>
        /// <param name="where">Điều kiện</param>
        /// <param name="num">Lấy mấy bản ghi đầu</param>
        /// <returns></returns> 
        public Task<List<TEntity>> getTakeList(Expression<Func<TEntity, bool>> where, int num, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// Lấy theo điều kiện Một bản ghi 
        /// </summary>
        /// <param name="where">Điều kiện</param>
        /// <returns></returns>
        public Task<TEntity> getFirstOrDefault(Expression<Func<TEntity, bool>> where);


        /// <summary>
        /// TheoKhóa chính In  Truy vấn
        /// </summary>
        /// <typeparam name="S">Khóa chínhkiểu</typeparam>
        /// <param name="list">Khóa chính In Tập kết quả của thao tác</param>
        /// <returns></returns>
        public Task<List<TEntity>> getByIn<S>(List<S> list, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// Theo cột chỉ định In Truy vấn
        /// </summary>
        /// <typeparam name="S">Kiểu của cột chỉ định</typeparam>
        /// <param name="column">Cột chỉ định</param>
        /// <param name="list">Cột chỉ định In Thao tác tập kết quả</param>
        /// <returns></returns>
        public Task<List<TEntity>> getByIn<S>(Expression<Func<TEntity, object>> column, List<S> list, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// Theo cột chỉ định Not In (!Contain)Truy vấn
        /// </summary>
        /// <typeparam name="S">Kiểu cột chỉ định</typeparam>
        /// <param name="list">Not Intập kết quả</param>
        /// <param name="field">Cột chỉ định</param>
        /// <returns></returns>
        public Task<List<TEntity>> getByNotIn<S>(List<S> list, object field, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// Theo điều kiện Truy vấn
        /// </summary>
        /// <param name="where">Điều kiện</param>
        /// <returns></returns>
        public Task<List<TEntity>> getByWhere(Expression<Func<TEntity, bool>> where, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// Một điều kiện Theo isWhere Xét Có sử dụng điều kiện này để truy vấn hay không
        /// </summary>
        /// <param name="isWhere">Điều kiện xét có sử dụng điều kiện truy vấn này hay không</param>
        /// <param name="where">Điều kiện truy vấn</param>
        /// <returns></returns>
        public Task<List<TEntity>> getByWhereIF(bool isWhere, Expression<Func<TEntity, bool>> where, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// Nhiều điều kiện Theo wheres.value  Xét có sử dụng hay không này wheres.key điều kiện
        /// </summary>
        /// <param name="wheres">Điều kiện truy vấn</param>
        /// <returns></returns>
        public Task<List<TEntity>> getByWhereIF(Dictionary<Expression<Func<TEntity, bool>>, bool> wheres, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// Truy vấn Giá trị của cột chỉ định tại startđếnend dữ liệu trong khoảng
        /// </summary>
        /// <param name="value">Lớp chỉ định</param>
        /// <param name="start">Bắt đầu</param>
        /// <param name="end">Kết thúc</param>
        /// <returns></returns>
        public Task<List<TEntity>> getByBetween(object value, object start, object end, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// Xét bản ghi này có tồn tại hay không
        /// </summary>
        /// <param name="where">Điều kiện</param>
        /// <returns></returns>
        public Task<bool> getIsAny(Expression<Func<TEntity, bool>> where);


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
        public Task<List<TEntity>> getPageList<TEntity>(int pageIndex, int pageSize, bool isWhere = false, Expression<Func<TEntity, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


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
        public Task<List<object>> getJoinList<T1, T2>(Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


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
        public Task<List<TEntity>> getListBySql(string sql);


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
        public Task<List<TEntity>> getListBySqlQuery(string sql, params SugarParameter[] parameters);


        /// <summary>
        /// Thực thi truy vấnsqlcâu lệnh,Trả về Hàng đầu tiên, cột đầu tiên
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">Tham số hóa</param>
        /// <returns></returns>
        public Task<object> getScalar(string sql, params SugarParameter[] parameters);



        #endregion
        #endregion

        #region DbMuti Query
        /// <summary>
        /// LINQ Where (choose db)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="where"></param>
        /// <param name="isOrderBy"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        Task<List<T>> getByWhere<T>(string db, Expression<Func<T, bool>> where, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);
        #region ADO 
        /// <summary>
        /// SQL (choose db,choose T)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<List<T>> getListBySqlQuery<T>(string db, string sql, List<DbParameter> parameters);
        #endregion
        /// <summary>
        /// LINQTruy vấn nối hai bảng (choose db)
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="db"></param>
        /// <param name="joinExp"></param>
        /// <param name="selectExp"></param>
        /// <param name="isWhere"></param>
        /// <param name="whereExp"></param>
        /// <param name="isOrderBy"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        Task<List<object>> getJoinList<T1, T2>(string db, Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);
        #endregion
    }
}

