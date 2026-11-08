using Newtouch.CIS.Compat;

using Newtouch.Application.Interface;
using Newtouch.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.Application.Implementation
{
    /// <summary>
    /// Cách dùng liên động
    /// </summary>
    public class UsageLinkageApp : AppBase, IUsageLinkageApp
    {
        private readonly ISysUsageLinkageRepo sysUsageLinkageRepo;

        /// <summary>
        /// Gửi Cách dùng gắn
        /// </summary>
        /// <param name="sysUsageLinkageEntity"></param>
        /// <returns></returns>
        public string SubmitForm(SysUsageLinkageEntity sysUsageLinkageEntity)
        {
            if (sysUsageLinkageEntity == null) return "Lưu nội dung không thể để trống";
            if (string.IsNullOrWhiteSpace(sysUsageLinkageEntity.yfCode)) return "Cách dùng không thể để trống";
            if (string.IsNullOrWhiteSpace(sysUsageLinkageEntity.sfxmCode)) return "Mục thu phí không thể để trống";
            if (string.IsNullOrWhiteSpace(sysUsageLinkageEntity.Id))
            {
                sysUsageLinkageEntity.Create(true);
                return sysUsageLinkageRepo.Insert(sysUsageLinkageEntity) > 0 ? "" : "Thêm mới thất bại";
            }
            sysUsageLinkageEntity.Modify();
            return sysUsageLinkageRepo.Update(sysUsageLinkageEntity) > 0 ? "" : "Sửa thất bại";
        }
    }
}
