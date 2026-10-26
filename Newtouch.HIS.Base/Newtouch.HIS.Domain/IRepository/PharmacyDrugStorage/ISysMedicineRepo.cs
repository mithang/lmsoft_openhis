using Newtouch.Common.Model;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicineRepo : IRepositoryBase<SysMedicineEntity>
    {
        /// <summary>
        /// Lấy danh sách thông tin thuốc theo tổ chức
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        IList<SysMedicineEntity> GetValidListByOrg(string OrganizeId, string keyword = null);

        /// <summary>
        /// Lấy List thuốc trong kho từ điển bảo hiểm y tế
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<GzybybItemCodeVO> GetYbMedicineList(string OrganizeId, string keyword = null);
        /// <summary>
        /// Truy vấn thông tin bảng tên bảo hiểm y tế
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<GzybNameCodeVO> GetYbName(string OrganizeId ,string lx, string keyword=null);

        IList<GzxnhybItemCodeVO> GetYbXNHMedicineList(string keyword = null);
    }
}
