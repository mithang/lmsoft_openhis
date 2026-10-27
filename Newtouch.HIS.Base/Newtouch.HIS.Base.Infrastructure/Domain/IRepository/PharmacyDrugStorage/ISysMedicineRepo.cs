using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Repository;
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
        /// Lấy thuốc từ kho từ điển BHYTList
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<GzybybItemCodeVO> GetYbMedicineList(string OrganizeId, string keyword = null);
        /// <summary>
        /// Tra cứu thông tin bảng tên BHYT
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<GzybNameCodeVO> GetYbName(string OrganizeId ,string lx, string keyword=null);

        IList<GzxnhybItemCodeVO> GetYbXNHMedicineList(string keyword = null);
    }
}
