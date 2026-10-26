using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGzybCodeContrastRepo : IRepositoryBase<GzybCodeContrastEntity>
    {
        /// <summary>
        /// Xác định dữ liệu đó có tồn tại hay không
        /// </summary>
        /// <param name="aaa100">Danh mục mã</param>
        /// <param name="aaa102">Giá trị mã</param>
        /// <returns></returns>
        bool Exist(string aaa100, string aaa102);

        /// <summary>
        /// Ghi dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        bool InsertInfo(GzybCodeContrastEntity entity);
    }
}
