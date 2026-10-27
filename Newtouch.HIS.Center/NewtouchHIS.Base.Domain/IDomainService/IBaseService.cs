
using Microsoft.Data.SqlClient;
using SqlSugar;
using System.Data.Common;
using System.Linq.Expressions;

namespace NewtouchHIS.Base.Domain
{
    public interface IBaseServices<TEntity> where TEntity : class
    {
        ///// <summary>
        ///// TheoIDXóa theo danh sách
        ///// </summary>
        ///// <param name="ids"></param>
        ///// <returns></returns>
        //Task<bool> DeleteByIds(object[] ids);

        ///// <summary>
        ///// TheoIDXóa
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //Task<bool> DeleteById(string id);

        ///// <summary>
        ///// Thêm thực thể
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //Task<long> Add(TEntity model);

        ///// <summary>
        ///// Cập nhật thực thể
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>

        //Task<bool> Update(TEntity model);


        Task<dynamic> GetJoinList<T1, T2, T3>(Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);
        Task<dynamic> GetJoinList<T1, T2, T3>(int pageIndex, int pageSize, Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);
        

        #region Ado
        /// <summary>
        /// Thực thi câu lệnh không truy vấnsqlcâu lệnh,trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">Tham số hóa</param>
        /// <returns></returns>
        Task<bool> ExecuteCommandSql(string sql, params SugarParameter[] parameters);
        /// <summary>
        /// Thực thi truy vấnsqlcâu lệnh,Trả về tập kết quả truy vấn
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">Tham số hóa</param>
        /// <returns></returns>
        Task<List<TEntity>> GetListBySqlQuery(string sql, params DbParameter[] parameters);
        /// <summary>
        /// Thực thi truy vấnsqlcâu lệnh,Trả về Hàng đầu tiên, cột đầu tiên
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">Tham số hóa</param>
        /// <returns></returns>
        //Task<object> GetScalar(string sql, params SugarParameter[] parameters);

        #endregion

        #region DbMuti Query
        #endregion
    }
}
