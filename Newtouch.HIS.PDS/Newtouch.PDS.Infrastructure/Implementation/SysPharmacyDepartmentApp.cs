using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkBase.MultiOrg.Application;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.PDS.Requset.PharmacyDepartment;
using Newtouch.Tools;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class SysPharmacyDepartmentApp : AppBase, ISysPharmacyDepartmentApp
    {
        private readonly IPharmacyDrugStorageDmnService _pharmacyDrugStorageDmnService;
        private readonly ISysPharmacyDepartmentMedicineRepo _sysPharmacyDepartmentMedicineRepo;

        /// <summary>
        /// Lấy kho thuốc theo mã nhà thuốcList（Nhà thuốc có thể lĩnh từ kho thuốc đích）
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<SysPharmacyDepartmentVEntity> GetTheUpperYkbmCodeList(string keyword, string yfbmCode, string organizeId)
        {
            return _pharmacyDrugStorageDmnService.GetTheUpperYkbmCodeList(keyword, yfbmCode, organizeId);
        }

        /// <summary>
        /// Lấy nhà thuốc theo mã kho thuốcList（Kho thuốc có thể phát thuốc cho nhà thuốc đích）
        /// </summary>
        /// <param name="ykbmCode"></param>
        /// <returns></returns>
        public IList<SysPharmacyDepartmentVEntity> GetTheLowerYfbmCodeList(string ykbmCode)
        {
            return _pharmacyDrugStorageDmnService.GetTheLowerYfbmCodeList("", ykbmCode, OrganizeId);
        }

        /// <summary>
        /// Lấy khoa theo mã nhà thuốcList（Nhà thuốc có thể phát thuốc cho khoa đích）
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        public IList<SysDepartmentVEntity> GetTheLowerKsCodeList(string yfbmCode)
        {
            return _pharmacyDrugStorageDmnService.GetTheLowerKsCodeList(yfbmCode, OperatorProvider.GetCurrent().OrganizeId);
        }

        /// <summary>
        /// Bộ phận nhà thuốc được ủy quyền thuốc
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string EmpowermentPharmacyDepartment(EmpowermentPharmacyDepartmentRequestDto req)
        {
            if (req == null || string.IsNullOrWhiteSpace(req.bmypId)) return "Vui lòng truyền thông tin dược phẩm bộ phận này hiệu lực；";
            var oldData = _sysPharmacyDepartmentMedicineRepo.SelectData(req.Ypdm, req.OrganizeId, req.yfbmCode);
            if (oldData != null && oldData.Count > 0)
            {
                if (oldData.Any(p => p.zt == "1")) return "";
                var item = oldData.FirstOrDefault();
                if (item != null)
                {
                    var t = _sysPharmacyDepartmentMedicineRepo.UpdateZt(item.bmypId, "1", req.CreatorCode, req.CreateTime);
                    return t > 0 ? "" : string.Format("Sửa thuốc bộ phận ủy quyền thành【{0}】của thuốc【{1}】Thất bại；", req.yfbmCode, req.Ypdm);
                }
            }
            var bmypxx = new SysPharmacyDepartmentMedicineEntity
            {
                bmypId = req.bmypId,
                yfbmCode = req.yfbmCode,
                Ypdm = req.Ypdm,
                OrganizeId = req.OrganizeId,
                Ypkw = req.Ypkw,
                Zcxh = req.Zcxh,
                px = req.px,
                Pxfs1 = req.Pxfs1,
                Pxfs2 = req.Pxfs2,
                Kcsx = req.Kcsx,
                Kcxx = req.Kcxx,
                Jhd = req.Jhd,
                Jhl = req.Jhl,
                Ypsxdm = req.Ypsxdm,
                Sysx = req.Sysx,
                Ylsx = req.Ylsx,
                zt = req.zt,
                CreateTime = req.CreateTime,
                CreatorCode = req.CreatorCode,
                LastModifierCode = req.LastModifierCode,
                LastModifyTime = req.LastModifyTime
            };
            var rt = _sysPharmacyDepartmentMedicineRepo.Insert(bmypxx);
            return rt > 0 ? "" : string.Format("Thêm thuốc bộ phận ủy quyền thành【{0}】của thuốc【{1}】Thất bại；", req.yfbmCode, req.Ypdm);
        }

        /// <summary>
        /// Bộ phận nhà thuốc được ủy quyền thuốc vàEmpowermentPharmacyDepartmentKhác nhau，Phương thức này sẽ hủy trước ủy quyền tất cả nhà thuốc của thuốc này，trong việc cấp lại quyền
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string EmpowermentPharmacyDepartmentAndRemoveOld(EmpowermentPharmacyDepartmentAndRemoveOldRequestDto req)
        {
            var result = new StringBuilder();
            if (req == null || req.epds == null || req.epds.Count == 0) return "Vui lòng truyền thông tin dược phẩm bộ phận này hiệu lực；";
            req.epds.Select(p => p.Ypdm).Distinct().ToList().ForEach(o =>
            {
                _sysPharmacyDepartmentMedicineRepo.DeleteItem(o, req.epds[0].OrganizeId);
            });
            req.epds.ForEach(p =>
            {
                var tp = p.ToJson().ToObject<EmpowermentPharmacyDepartmentRequestDto>();
                result.Append(EmpowermentPharmacyDepartment(tp));
            });
            return result.ToString();
        }
    }
}
