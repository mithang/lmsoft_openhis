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
        /// SửaTồn khoSố lượng
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="warehouseId"></param>
        /// <param name="sl">Đơn vị nhỏ nhấtSố lượng</param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns>Trả về số dòng bị ảnh hưởng</returns>
        int UpdateKcsl(string productId, string pc, string ph, string warehouseId, int sl, string organizeId, string userCode);

        // /// <summary>
        // /// SửaTồn khoSố lượng
        // /// </summary>
        // /// <param name="updateKcslDto"></param>
        // /// <returns></returns>
        // string UpdateKcsl(List<UpdateKcslDTO> updateKcslDto);

        /// <summary>
        /// đuổithêmTồn kho,khôngSửađôngkếtSố lượng Quay lạihìnhhưởngdòng
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="warehouseId"></param>
        /// <param name="sl">Đơn vị nhỏ nhấtSố lượng  phảicầnlàđangsố</param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns>Quay lạihìnhhưởngdòng</returns>
        int AddKcsl(string productId, string pc, string ph, string warehouseId, int sl, string organizeId, string userCode);
        
        // // /// <summary>
        // // /// SửaTồn khoSố lượng,khôngSửađôngkếtSố lượng
        // // /// </summary>
        // // /// <param name="updateKcslDto"></param>
        // // /// <returns></returns>
        // // string JustSubtractKcsl(List<UpdateKcslDTO> updateKcslDto);
        // //
        // //
        // // // /// <summary>
        // // // /// SửaTồn khoSố lượng,khôngSửađôngkếtSố lượng
        // // // /// </summary>
        // // // /// <param name="updateKcslDto"></param>
        // // // /// <param name="db"></param>
        // // // /// <returns></returns>
        // // // string JustSubtractKcslNoTrans(List<UpdateKcslDTO> updateKcslDto, EFDbTransaction db = null);
        // // //
        // // // /// <summary>
        // // // /// SửaTồn khoSố lượng,khôngSửađôngkếtSố lượng
        // // // /// </summary>
        // // // /// <param name="updateKcslDto"></param>
        // // // /// <param name="db"></param>
        // // // /// <returns></returns>
        // // // string JustSubtractKcsl(UpdateKcslDTO updateKcslDto, EFDbTransaction db = null);
        //
        // /// <summary>
        // /// giảmTồn khoSố lượng,đôngkếtSố lượng(Xuất khocó thểdùng)
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
        // /// <param name="sl">Đơn vị nhỏ nhấtSố lượng</param>
        // /// <param name="organizeId"></param>
        // /// <param name="userCode"></param>
        // /// <returns></returns>
        // string Unfreeze(string productId, string pc, string ph, string warehouseId, int sl, string organizeId, string userCode);
        //
        // /// <summary>
        // /// đôngkếtTồn kho
        // /// </summary>
        // /// <param name="sl">Đơn vị nhỏ nhấtSố lượng</param>
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
        // /// ngoàibộNhập kho khấuTồn kho(Hủy bỏ)
        // /// </summary>
        // /// <param name="sl">Đơn vị nhỏ nhấtSố lượng</param>
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
        // /// Thu hồiNhập kho(trực tiếpXuất khoHủy bỏ)
        // /// </summary>
        // /// <param name="sl">Đơn vị nhỏ nhấtSố lượng</param>
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
        /// LấyTồn khothông tin kết chuyểndùng
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VKcxxEntity> GetKcxxForJz(string warehouseId, string organizeId);
    }
}
