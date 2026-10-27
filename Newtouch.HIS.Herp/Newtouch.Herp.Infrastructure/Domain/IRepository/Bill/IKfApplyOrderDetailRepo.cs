using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// Chi tiết phiếu lĩnh
    /// </summary>
    public interface IKfApplyOrderDetailRepo : IRepositoryBase<KfApplyOrderDetailEntity>
    {

        /// <summary>
        /// Sửa số lượng đã phát
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sl">Số lượng đơn vị khoa phòng，với bảng kf_applyOrderDetail.zhyz*sl=Số lượng đơn vị nhỏ nhất</param>
        /// <returns></returns>
        int UpdateYfsl(long id, int sl);

        /// <summary>
        /// Sửa số lượng đã phát，và trả về bản ghi thay đổi
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sl">Số lượng đơn vị khoa phòng，với bảng kf_applyOrderDetail.zhyz*sl=Số lượng đơn vị nhỏ nhất</param>
        /// <returns></returns>
        KfApplyOrderDetailEntity UpdateYfslAndSelectChangeRecord(long id, int sl);

        /// <summary>
        /// Lấy thông tin chi tiết phiếu lĩnh
        /// </summary>
        /// <param name="sldh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<KfApplyOrderDetailEntity> SelectData(string sldh, string organizeId);
    }
}