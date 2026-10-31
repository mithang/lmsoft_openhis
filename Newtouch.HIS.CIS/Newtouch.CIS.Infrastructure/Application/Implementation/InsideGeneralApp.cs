
using Newtouch.Application.Interface;
using Newtouch.CIS.APIRequest.Dto;
using Newtouch.CIS.APIRequest.Prescription;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.CIS.Compat;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Extension;

namespace Newtouch.Application.Implementation
{
    /// <summary>
    /// trongbộgiao diệngọinghiệp vụ
    /// </summary>
    public class InsideGeneralApp : AppBase, IInsideGeneralApp
    {
        private readonly IPrescriptionRepo _prescriptionRepo;

        /// <summary>
        /// công cộngvàomiệng
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public void PublicPortal(MqGeneralTaskRequestDto requestDto)
        {
            if (requestDto == null)
            {
                throw new FailedException("Yêu cầu (báo văn) không được để trống"); // QA PHASE 2 (API-08): chuỗi gốc bị mojibake
            }
            if (string.IsNullOrWhiteSpace(requestDto.body))
            {
                throw new FailedException("Body của báo văn không được để trống"); // QA PHASE 2 (API-08): chuỗi gốc bị mojibake
            }
            var o = requestDto.body.ToObject<MqGeneralTaskBody>();
            if (o == null)
            {
                throw new FailedException("Phân tích body thất bại (kết quả null)"); // QA PHASE 2 (API-08): chuỗi gốc bị mojibake
            }

            switch (o.OperationType)
            {
                case "UpdateChargeStatus"://Tra cứuvàrơiđịaS27(quyết toánTra cứu)kết quả
                    var pr = UpdateChargeStatus(o.Content);
                    if (!string.IsNullOrWhiteSpace(pr)) throw new FailedException(pr);
                    break;
                default:
                    throw new FailedException("Chưa khớp được quy trình xử lý tương ứng, vui lòng kiểm tra OperationType có đúng không"); // QA PHASE 2 (API-08): chuỗi gốc bị mojibake
            }
        }

        /// <summary>
        /// SửaThu phíTrạng thái
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        private string UpdateChargeStatus(string body)
        {
            var o = body.ToObject<PrescriptionChargeRequest>();
            if (o == null) return "Phân tích tham số yêu cầu thất bại";
            var op = OperatorProvider.GetCurrent();
            _prescriptionRepo.UpdateChargeStatus(o, op.OrganizeId, op.UserCode);
            return "";
        }
    }
}