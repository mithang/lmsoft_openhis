using System.Collections.Generic;
using Newtouch.Domain.ValueObjects.Storage;

namespace Newtouch.Domain.IDomainServices.DeptStorageManage
{
    /// <summary>
    /// Thao tác tồn kho
    /// </summary>
    public interface IKfKcxxDmnService
    {
        /// <summary>
        /// Sửa đổi số lượng tồn kho
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="warehouseId"></param>
        /// <param name="sl">Số lượng đơn vị nhỏ nhất</param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns>Trả về số dòng bị ảnh hưởng</returns>
        int UpdateKcsl(string productId, string pc, string ph, string warehouseId, int sl, string organizeId, string userCode);

        // /// <summary>
        // /// Sửa đổi số lượng tồn kho
        // /// </summary>
        // /// <param name="updateKcslDto"></param>
        // /// <returns></returns>
        // string UpdateKcsl(List<UpdateKcslDTO> updateKcslDto);

        /// <summary>
        /// Thêm tồn kho,Không sửa đổi số lượng đóng băng Trả về số dòng bị ảnh hưởng
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="warehouseId"></param>
        /// <param name="sl">Số lượng đơn vị nhỏ nhất  Phải là số dương</param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns>Trả về số dòng bị ảnh hưởng</returns>
        int AddKcsl(string productId, string pc, string ph, string warehouseId, int sl, string organizeId, string userCode);
        
        // // /// <summary>
        // // /// Sửa đổi số lượng tồn kho,Không sửa đổi số lượng đóng băng
        // // /// </summary>
        // // /// <param name="updateKcslDto"></param>
        // // /// <returns></returns>
        // // string JustSubtractKcsl(List<UpdateKcslDTO> updateKcslDto);
        // //
        // //
        // // // /// <summary>
        // // // /// Sửa đổi số lượng tồn kho,Không sửa đổi số lượng đóng băng
        // // // /// </summary>
        // // // /// <param name="updateKcslDto"></param>
        // // // /// <param name="db"></param>
        // // // /// <returns></returns>
        // // // string JustSubtractKcslNoTrans(List<UpdateKcslDTO> updateKcslDto, EFDbTransaction db = null);
        // // //
        // // // /// <summary>
        // // // /// Sửa đổi số lượng tồn kho,Không sửa đổi số lượng đóng băng
        // // // /// </summary>
        // // // /// <param name="updateKcslDto"></param>
        // // // /// <param name="db"></param>
        // // // /// <returns></returns>
        // // // string JustSubtractKcsl(UpdateKcslDTO updateKcslDto, EFDbTransaction db = null);
        //
        // /// <summary>
        // /// Giảm số lượng tồn kho,Số lượng đóng băng(Xuất kho khả dụng)
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
        // /// Giải đóng băng
        // /// </summary>
        // /// <param name="productId"></param>
        // /// <param name="pc"></param>
        // /// <param name="ph"></param>
        // /// <param name="warehouseId"></param>
        // /// <param name="sl">Số lượng đơn vị nhỏ nhất</param>
        // /// <param name="organizeId"></param>
        // /// <param name="userCode"></param>
        // /// <returns></returns>
        // string Unfreeze(string productId, string pc, string ph, string warehouseId, int sl, string organizeId, string userCode);
        //
        // /// <summary>
        // /// Đóng băng tồn kho
        // /// </summary>
        // /// <param name="sl">Số lượng đơn vị nhỏ nhất</param>
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
        // /// Nhập kho từ bên ngoài Trừ tồn kho(Hủy bỏ)
        // /// </summary>
        // /// <param name="sl">Số lượng đơn vị nhỏ nhất</param>
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
        // /// Hủy nhập kho(Hủy bỏ xuất kho trực tiếp)
        // /// </summary>
        // /// <param name="sl">Số lượng đơn vị nhỏ nhất</param>
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
        /// Lấy thông tin tồn kho Dùng để kết chuyển
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VKcxxEntity> GetKcxxForJz(string warehouseId, string organizeId);
    }
}