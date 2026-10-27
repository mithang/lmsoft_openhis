using Newtouch.CIS.Compat;

using Newtouch.Application.Interface;
using Newtouch.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.Application.Implementation
{
    /// <summary>
    /// Cách dùngliênđộng
    /// </summary>
    public class UsageLinkageApp : AppBase, IUsageLinkageApp
    {
        private readonly ISysUsageLinkageRepo sysUsageLinkageRepo;

        /// <summary>
        /// GửiCách dùnggắn
        /// </summary>
        /// <param name="sysUsageLinkageEntity"></param>
        /// <returns></returns>
        public string SubmitForm(SysUsageLinkageEntity sysUsageLinkageEntity)
        {
            if (sysUsageLinkageEntity == null) return "Lưunội dungkhông thểđể trống";
            if (string.IsNullOrWhiteSpace(sysUsageLinkageEntity.yfCode)) return "Cách dùngkhông thểđể trống";
            if (string.IsNullOrWhiteSpace(sysUsageLinkageEntity.sfxmCode)) return "Mục thu phíkhông thểđể trống";
            if (string.IsNullOrWhiteSpace(sysUsageLinkageEntity.Id))
            {
                sysUsageLinkageEntity.Create(true);
                return sysUsageLinkageRepo.Insert(sysUsageLinkageEntity) > 0 ? "" : "Thêm mớithất bại";
            }
            sysUsageLinkageEntity.Modify();
            return sysUsageLinkageRepo.Update(sysUsageLinkageEntity) > 0 ? "" : "Sửa thất bại";
        }
    }
}
