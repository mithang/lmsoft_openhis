using System;
using System.Linq;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.DomainServices.AccessManage
{
    /// <summary>
    /// Port tay từ DomainServices/AccessManage/AccessAuthDmnService.cs gốc: impl này
    /// KHÔNG có trong bản port Infra (thư mục AccessManage thiếu) nhưng
    /// HOSP.API AuthController.GetTokenbyProxy cần nó (bước Authorized).
    /// DRIFT-FIX (ghi rõ): source gốc gán accesskey = "" rồi Convert.ToDateTime("")
    /// (luôn ném FormatException trên nhánh này); bản port dùng ety.accesskey đúng
    /// ý đồ gốc (đo cửa sổ hiệu lực từ ngày của accesskey). Thông điệp trả về
    /// đã dịch sang tiếng Việt (chúng hiện lên sub_msg của API).
    /// </summary>
    public class AccessAuthDmnService : DmnServiceBase, IAccessAuthDmnService
    {
        private readonly IAccessAuthRepo _accessAuthRepo;
        private readonly ISysStaffRepo _sysStaffRepo;

        public AccessAuthDmnService(IAccessAuthRepo accessAuthRepo, ISysStaffRepo sysStaffRepo)
        {
            _accessAuthRepo = accessAuthRepo;
            _sysStaffRepo = sysStaffRepo;
        }

        public bool Authorized(AccessIdentity ident, out string message)
        {
            message = "";
            if (!string.IsNullOrWhiteSpace(ident.OrganizeId) && !string.IsNullOrWhiteSpace(ident.Account) && !string.IsNullOrWhiteSpace(ident.AppId))
            {
                var list = _accessAuthRepo.FindList(ident.Account, ident.OrganizeId);
                if (list != null && list.Count > 0)
                {
                    var ety = list.FirstOrDefault();
                    if (ety.AuthorizedLev == ((int)EnumAuthorizedLev.imitatelogin))
                    {
                        if (string.IsNullOrWhiteSpace(ident.UserCode) || string.IsNullOrWhiteSpace(ident.UserName))
                        {
                            message = "Thông tin đăng nhập mô phỏng chưa đầy đủ, vui lòng bổ sung.";
                            return false;
                        }
                        var user = _sysStaffRepo.FindEntity(p => p.gh == ident.UserCode && p.Name == ident.UserName && p.OrganizeId == ident.OrganizeId && p.zt == "1");
                        if (user == null)
                        {
                            message = "Xác thực thông tin đăng nhập mô phỏng thất bại, vui lòng kiểm tra.";
                            return false;
                        }
                    }
                    else if (ety.AuthorizedLev == ((int)EnumAuthorizedLev.saltaccess))
                    {
                        if (string.IsNullOrWhiteSpace(ident.TokenType))
                        {
                            try
                            {
                                //AuthProvider.GetAuthDecrypt(ident.TokenType);
                            }
                            catch
                            {
                                message = "Xác thực thông tin ủy quyền thất bại, vui lòng kiểm tra.";
                                return false;
                            }
                        }
                    }
                    else if (ety.AuthorizedLev == ((int)EnumAuthorizedLev.tokenaccess))
                    {
                    }

                    int? authlev = ety.AuthorizedPeriod;
                    string accesskey = ety.accesskey;
                    if (authlev != null && !string.IsNullOrWhiteSpace(accesskey))
                    {
                        TimeSpan timesp;
                        try
                        {
                            timesp = DateTime.Now.Subtract(Convert.ToDateTime(accesskey)).Duration();
                        }
                        catch
                        {
                            message = "Thông tin ủy quyền bất thường, vui lòng ủy quyền lại";
                            return false;
                        }
                        if (ety.AuthorizedPeriod == ((int)EnumAuthorizedPeriod.longtime))
                        {
                            return true;
                        }
                        else if (ety.AuthorizedPeriod == ((int)EnumAuthorizedPeriod.month) && timesp.TotalDays <= 31)
                        {
                            message = "Số ngày ủy quyền còn lại: " + timesp.TotalDays.ToString();
                            return true;
                        }
                        else if (ety.AuthorizedPeriod == ((int)EnumAuthorizedPeriod.partyear) && timesp.TotalDays <= 180)
                        {
                            message = "Số ngày ủy quyền còn lại: " + timesp.TotalDays.ToString();
                            return true;
                        }
                        else if (ety.AuthorizedPeriod == ((int)EnumAuthorizedPeriod.year) && timesp.TotalDays <= 365)
                        {
                            message = "Số ngày ủy quyền còn lại: " + timesp.TotalDays.ToString();
                            return true;
                        }
                        message = "Thông tin ủy quyền đã hết hạn, vui lòng ủy quyền lại";
                    }
                    else
                    {
                        message = "Thông tin ủy quyền bất thường, vui lòng ủy quyền lại";
                    }
                }
                else
                {
                    message = "Truy cập trái phép, ứng dụng chưa được ủy quyền";
                }
            }
            else
            {
                message = "Thông tin định danh quan trọng không được để trống";
            }
            return false;
        }
    }
}
