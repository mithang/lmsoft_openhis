
using SqlSugar;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;

namespace NewtouchHIS.Base.Domain
{
    /// <summary>
    /// giao diện lớp cơ sở,các giao diện khác kế thừa giao diện này
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        #region TEntity 操作
        /// <summary>
        /// theoIDtruy vấn danh sách
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        Task<TEntity> FindKey(object pkValue);

        /// <summary>
        /// truy vấn tất cả dữ liệu
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        Task<List<TEntity>> FindAll();

        /// <summary>
        /// thêm
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<long> Add(TEntity model);

        /// <summary>
        /// sửa đổi
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> Update(TEntity model);

        /// <summary>
        /// theoidxóa danh sách
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> DeleteByIds(object[] ids);

        /// <summary>
        /// theoIDxóa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteById(object id);

        #region Query

        /// <summary>
        /// truy vấn tất cả dữ liệu
        /// </summary>
        /// <returns></returns>
        public Task<List<TEntity>> FindAll(bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// lấy đầu num bản ghi dữ liệu
        /// </summary>
        /// <param name="num">lấy vài bản ghi đầu</param>
        /// <returns></returns>
        public Task<List<TEntity>> getTakeList(int num, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// lấy một bảng dữ liệu phân trang
        /// </summary>
        /// <param name="skip">bỏ qua vài bản ghi</param>
        /// <param name="take">lấy vài bản ghi</param>
        /// <param name="whereExp">bỏ qua vài bản ghi</param>
        /// <param name="orderBy">điều kiện sắp xếp</param>
        /// <param name="orderByType">kiểu sắp xếp（Asc、Desc）</param>
        /// <returns></returns>
        public Task<List<TEntity>> getPageList(int skip, int take, Expression<Func<TEntity, bool>> whereExp, Expression<Func<TEntity, object>> orderBy, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// lấy các bản ghi đầu thỏa điều kiện num bản ghi dữ liệu
        /// </summary>
        /// <param name="where">điều kiện</param>
        /// <param name="num">lấy vài bản ghi đầu</param>
        /// <returns></returns> 
        public Task<List<TEntity>> getTakeList(Expression<Func<TEntity, bool>> where, int num, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// lấy theo điều kiện một bản ghi dữ liệu 
        /// </summary>
        /// <param name="where">điều kiện</param>
        /// <returns></returns>
        public Task<TEntity> getFirstOrDefault(Expression<Func<TEntity, bool>> where);


        /// <summary>
        /// theo khóa chính In  truy vấn
        /// </summary>
        /// <typeparam name="S">kiểu của khóa chính</typeparam>
        /// <param name="list">khóa chính In tập kết quả của thao tác</param>
        /// <returns></returns>
        public Task<List<TEntity>> getByIn<S>(List<S> list, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// theo cột chỉ định In truy vấn
        /// </summary>
        /// <typeparam name="S">kiểu của cột chỉ định</typeparam>
        /// <param name="column">cột chỉ định</param>
        /// <param name="list">cột chỉ định In thao tác tập kết quả của</param>
        /// <returns></returns>
        public Task<List<TEntity>> getByIn<S>(Expression<Func<TEntity, object>> column, List<S> list, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// theo cột chỉ định Not In (!Contain)truy vấn
        /// </summary>
        /// <typeparam name="S">kiểu cột chỉ định</typeparam>
        /// <param name="list">Not Intập kết quả của</param>
        /// <param name="field">cột chỉ định</param>
        /// <returns></returns>
        public Task<List<TEntity>> getByNotIn<S>(List<S> list, object field, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// theo điều kiện truy vấn
        /// </summary>
        /// <param name="where">điều kiện</param>
        /// <returns></returns>
        public Task<List<TEntity>> getByWhere(Expression<Func<TEntity, bool>> where, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// một điều kiện đơn theo isWhere phán đoán có sử dụng điều kiện này để truy vấn hay không
        /// </summary>
        /// <param name="isWhere">điều kiện phán đoán có sử dụng điều kiện truy vấn này hay không</param>
        /// <param name="where">điều kiện truy vấn</param>
        /// <returns></returns>
        public Task<List<TEntity>> getByWhereIF(bool isWhere, Expression<Func<TEntity, bool>> where, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// nhiều điều kiện theo wheres.value  phán đoán có sử dụng hay không này wheres.key điều kiện của
        /// </summary>
        /// <param name="wheres">điều kiện truy vấn</param>
        /// <returns></returns>
        public Task<List<TEntity>> getByWhereIF(Dictionary<Expression<Func<TEntity, bool>>, bool> wheres, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// truy vấn giá trị của cột chỉ định trong startđếnend dữ liệu giữa
        /// </summary>
        /// <param name="value">lớp chỉ định</param>
        /// <param name="start">bắt đầu</param>
        /// <param name="end">kết thúc</param>
        /// <returns></returns>
        public Task<List<TEntity>> getByBetween(object value, object start, object end, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// phán đoán bản ghi này có tồn tại hay không
        /// </summary>
        /// <param name="where">điều kiện</param>
        /// <returns></returns>
        public Task<bool> getIsAny(Expression<Func<TEntity, bool>> where);


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
        public Task<List<TEntity>> getPageList<TEntity>(int pageIndex, int pageSize, bool isWhere = false, Expression<Func<TEntity, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


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
        public Task<List<object>> getJoinList<T1, T2>(Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


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
        public Task<dynamic> getJoinPageList<T1, T2>(int pageIndex, int pageSize, Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


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
        public Task<dynamic> getJoinList<T1, T2, T3>(Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


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
        public Task<dynamic> getJoinList<T1, T2, T3>(int pageIndex, int pageSize, Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// thực thi truy vấnsqlcâu lệnh ，trả về tập dữ liệu
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <returns></returns>
        public Task<List<TEntity>> getListBySql(string sql);


        /// <summary>
        /// thực thi câu lệnh không phải truy vấnsqlcâu lệnh，trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">tham số hóa</param>
        /// <returns></returns>
        public Task<bool> ExecuteCommandSql(string sql, params SugarParameter[] parameters);


        /// <summary>
        /// thực thi truy vấnsqlcâu lệnh，trả về tập kết quả truy vấn
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">tham số hóa</param>
        /// <returns></returns>
        public Task<List<TEntity>> getListBySqlQuery(string sql, params SugarParameter[] parameters);


        /// <summary>
        /// thực thi truy vấnsqlcâu lệnh，trả về hàng đầu tiên, cột đầu tiên
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">tham số hóa</param>
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
        /// LINQtruy vấn liên kết hai bảng (choose db)
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

