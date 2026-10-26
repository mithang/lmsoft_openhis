using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Hạng mục vật tư tổng hợp
    /// </summary>
    public interface IIntegratedMaterialDmnService
    {
        /// <summary>
        /// Lấy theo hạng mục thu phí Mã tiêu chuẩn thanh toán vật tư dùng một lần
        /// </summary>
        /// <param name="sfxm"></param>
        /// <returns></returns>
        SysChargeMaterialItemSynthesisEntity GetSfclxmzhEntity(string sfxm, string orgId);

    }

}
