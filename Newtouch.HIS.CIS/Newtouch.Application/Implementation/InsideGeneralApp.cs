using FrameworkBase.MultiOrg.Application;
using Newtouch.Application.Interface;
using Newtouch.CIS.APIRequest.Dto;
using Newtouch.CIS.APIRequest.Prescription;
using Newtouch.Common.Operator;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;

namespace Newtouch.Application.Implementation
{
    /// <summary>
    /// Nghiệp vụ gọi giao diện nội bộ
    /// </summary>
    public class InsideGeneralApp : AppBase, IInsideGeneralApp
    {
        private readonly IPrescriptionRepo _prescriptionRepo;

        /// <summary>
        /// Điểm vào chung
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public void PublicPortal(MqGeneralTaskRequestDto requestDto)
        {
            if (requestDto == null)
            {
                throw new FailedException("请求报文并不能为空");
            }
            if (string.IsNullOrWhiteSpace(requestDto.body))
            {
                throw new FailedException("报文body不能为空");
            }
            var o = requestDto.body.ToObject<MqGeneralTaskBody>();
            if (o == null)
            {
                throw new FailedException("body反序列化失败，结果为null");
            }

            switch (o.OperationType)
            {
                case "UpdateChargeStatus"://Tra cứu và lưu kết quả S27 (tra cứu thanh toán)
                    var pr = UpdateChargeStatus(o.Content);
                    if (!string.IsNullOrWhiteSpace(pr)) throw new FailedException(pr);
                    break;
                default:
                    throw new FailedException("未匹配到响应的处理流程，请查看OperationType赋值是否正确");
            }
        }

        /// <summary>
        /// Sửa trạng thái thu phí
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        private string UpdateChargeStatus(string body)
        {
            var o = body.ToObject<PrescriptionChargeRequest>();
            if (o == null) return "解析请求参数失败";
            var op = OperatorProvider.GetCurrent();
            _prescriptionRepo.UpdateChargeStatus(o, op.OrganizeId, op.UserCode);
            return "";
        }
    }
}