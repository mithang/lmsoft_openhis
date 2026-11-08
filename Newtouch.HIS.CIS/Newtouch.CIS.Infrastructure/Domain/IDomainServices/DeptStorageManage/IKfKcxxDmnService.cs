using System.Collections.Generic;
using Newtouch.Infrastructure.EF;
using Newtouch.Domain.ValueObjects.Storage;

namespace Newtouch.Domain.IDomainServices.DeptStorageManage
{
    /// <summary>
    /// Tồn khoThao tác
    /// </summary>
    public interface IKfKcxxDmnService
    {
        /// <summary>
        /// Sửa Tồn kho Số lượng
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="warehouseId"></param>
        /// <param name="sl">Đơn vị nhỏ nhất Số lượng</param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns>Trả về số dòng bị ảnh hưởng</returns>
        int UpdateKcsl(string productId, string pc, string ph, string warehouseId, int sl, string organizeId, string userCode);

        // /// <summary>
        // /// Sửa Tồn kho Số lượng
        // /// </summary>
        // /// <param name="updateKcslDto"></param>
        // /// <returns></returns>
        // string UpdateKcsl(List<UpdateKcslDTO> updateKcslDto);

        /// <summary>
        /// đuổi thêm Tồn kho,không Sửa đông kết Số lượng Quay lại hình hưởng dòng
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="warehouseId"></param>
        /// <param name="sl">Đơn vị nhỏ nhất Số lượng  phải cần là đang số</param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns>Quay lại hình hưởng dòng</returns>
        int AddKcsl(string productId, string pc, string ph, string warehouseId, int sl, string organizeId, string userCode);
        
        // // /// <summary>
        // // /// Sửa Tồn kho Số lượng,không Sửa đông kết Số lượng
        // // /// </summary>
        // // /// <param name="updateKcslDto"></param>
        // // /// <returns></returns>
        // // string JustSubtractKcsl(List<UpdateKcslDTO> updateKcslDto);
        // //
        // //
        // // // /// <summary>
        // // // /// Sửa Tồn kho Số lượng,không Sửa đông kết Số lượng
        // // // /// </summary>
        // // // /// <param name="updateKcslDto"></param>
        // // // /// <param name="db"></param>
        // // // /// <returns></returns>
        // // // string JustSubtractKcslNoTrans(List<UpdateKcslDTO> updateKcslDto, EFDbTransaction db = null);
        // // //
        // // // /// <summary>
        // // // /// Sửa Tồn kho Số lượng,không Sửa đông kết Số lượng
        // // // /// </summary>
        // // // /// <param name="updateKcslDto"></param>
        // // // /// <param name="db"></param>
        // // // /// <returns></returns>
        // // // string JustSubtractKcsl(UpdateKcslDTO updateKcslDto, EFDbTransaction db = null);
        //
        // /// <summary>
        // /// giảm Tồn kho Số lượng,đông kết Số lượng(Xuất kho có thể dùng)
        // /// </summary>
        // /// <param name="productId"></param>
        // /// <param name="pc"></param>
        // /// <param name="ph"></param>
        // /// <param name="warehouseId"></param>
        // /// <param name="sl"></param>
        // /// <param name="organizeId"></param>
        // /// <param name="userCode"></param>
        // /// <returns></returns>
        // string SubtractKcslAndDjsl(string productId, string pc, string ph, string warehouseId, int sl, string organizeId, string userCode);
        //
        // /// <summary>
        // /// Rã đông
        // /// </summary>
        // /// <param name="productId"></param>
        // /// <param name="pc"></param>
        // /// <param name="ph"></param>
        // /// <param name="warehouseId"></param>
        // /// <param name="sl">Đơn vị nhỏ nhất Số lượng</param>
        // /// <param name="organizeId"></param>
        // /// <param name="userCode"></param>
        // /// <returns></returns>
        // string Unfreeze(string productId, string pc, string ph, string warehouseId, int sl, string organizeId, string userCode);
        //
        // /// <summary>
        // /// đông kết Tồn kho
        // /// </summary>
        // /// <param name="sl">Đơn vị nhỏ nhất Số lượng</param>
        // /// <param name="pc"></param>
        // /// <param name="ph"></param>
        // /// <param name="productId"></param>
        // /// <param name="organizeId"></param>
        // /// <param name="warehouseId"></param>
        // /// <param name="userCode"></param>
        // /// <returns></returns>
        // string FrozenKc(int sl, string pc, string ph, string productId, string organizeId, string warehouseId, string userCode);
        //
        // /// <summary>
        // /// ngoài bộ Nhập kho khấu Tồn kho(Hủy bỏ)
        // /// </summary>
        // /// <param name="sl">Đơn vị nhỏ nhất Số lượng</param>
        // /// <param name="pc"></param>
        // /// <param name="ph"></param>
        // /// <param name="productId"></param>
        // /// <param name="organizeId"></param>
        // /// <param name="warehouseId"></param>
        // /// <param name="userCode"></param>
        // /// <returns></returns>
        // string WbrkKkc(int sl, string pc, string ph, string productId, string organizeId, string warehouseId, string userCode);
        //
        // /// <summary>
        // /// Thu hồi Nhập kho(trực tiếp Xuất kho Hủy bỏ)
        // /// </summary>
        // /// <param name="sl">Đơn vị nhỏ nhất Số lượng</param>
        // /// <param name="pc"></param>
        // /// <param name="ph"></param>
        // /// <param name="productId"></param>
        // /// <param name="organizeId"></param>
        // /// <param name="rkbm"></param>
        // /// <param name="ckbm"></param>
        // /// <param name="userCode"></param>
        // /// <returns></returns>
        // string RevokeInstorage(int sl, string pc, string ph, string productId, string organizeId, string rkbm, string ckbm, string userCode);

        /// <summary>
        /// Lấy Tồn kho thông tin kết chuyển dùng
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VKcxxEntity> GetKcxxForJz(string warehouseId, string organizeId);
    }
}
