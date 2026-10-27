using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.Rehabilitation;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices.Rehabilitation
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRehabilitationDmnService
    {
        /// <summary>
        /// Lấy tất cả danh sách
        /// </summary>
        List<RehabChargeItemVO> GetRehabChargeItemList(string keyword, string OrganizeId);

        /// <summary>
        /// Sửaform
        /// </summary>
        RehabChargeItemVO GetRehabChargeItemEntity(string sfxmId, string OrganizeId);

        /// <summary>
        /// Lấy tất cả danh sách(Đối chiếu hạng mục thu phí phục hồi chức năng)
        /// </summary>
        List<RehabChargeItemComparisonVO> GetRehabChargeItemComparisonList(string keyword, string OrganizeId);

        /// <summary>
        /// Sửaform(Đối chiếu hạng mục thu phí phục hồi chức năng)
        /// </summary>
        RehabChargeItemComparisonVO GetRehabChargeItemComparisonEntity(string sfxmdzId,string OrganizeId);

        /// <summary>
        /// Tìm kiếm hạng mục thu phí
        /// </summary>
        IList<SysChargeItemVO> GetHisBindSelect(string keyword, string orgId);
    }
}
