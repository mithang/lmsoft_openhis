using System;
using FrameworkBase.MultiOrg.Application;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.OutpatientManage;
using Newtouch.HIS.Sett.Request;
using Newtouch.HIS.Sett.Request.OutPatientPharmacy;
using Newtouch.HIS.Sett.Request.Patient;
using Newtouch.Infrastructure;
//using Newtouch.PDS.Requset;
using Newtouch.Tools;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// nội bộ giao diện gọi nghiệp vụ 
    /// </summary>
    public class InsideGeneralApp : AppBase, IInsideGeneralApp
    {
        private readonly IGuiAnOutpatientXnhApp _guiAnOutpatientXnhApp;
        private readonly IPatientBasicInfoDmnService _patientBasic;
        private readonly IOutpatientPharmacyAPIDmnService _outpatientPharmacyAPIDmnService;

        /// <summary>
        /// cổng vào chung
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
                //case "xnhS27"://Tra cứu và lưu đ ịa S27(quyết toántra cứu)kết quả
                //    var pr = _guiAnOutpatientXnhApp.QueryAndRecordSettDetail(o.Content);
                //    if (!string.IsNullOrWhiteSpace(pr)) throw new FailedException(pr);
                //    break;
                //case "supplementPatientBaseInfo"://bổ sungbệnh nhân cơ bản thông tin
                //    var r = SupplementPatientBaseInfo(o.Content);
                //    if (!string.IsNullOrWhiteSpace(r)) throw new FailedException(r);
                //    break;
                case "OutpatientDrugWithdrawalNotify"://Ngoại trú trả thuốc 
                    var rd = OutpatientDrugWithdrawalNotify(o.Content);
                    if (!string.IsNullOrWhiteSpace(rd)) throw new FailedException(rd);
                    break;
                default:
                    throw new FailedException("未匹配到响应的处理流程，请查看OperationType赋值是否正确");
            }
        }

        /// <summary>
        /// Ngoại trú trả thuốc 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string OutpatientDrugWithdrawalNotify(string request)
        {
            var po = request.ToObject<OutpatientDrugWithdrawalNotifyRequest>();
            if (po == null) throw new Exception("请求参数序列化后为空，原始报文：" + request);
            _outpatientPharmacyAPIDmnService.OutpatientDrugWithdrawalNotify(po.OrganizeId, po.cfnm, po.yp, po.sl, po.czh);
            return "";
        }

        /// <summary>
        /// bổ sungbệnh nhân cơ bản thông tin
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //private string SupplementPatientBaseInfo(string request)
        //{
        //    var po = request.ToObject<PatientBaseInfoQueryRequest>();
        // if (po == null) throw new Exception(" yêu cầu tham số tuần tự hóa sau là trống, bản gốc bản tin:" + request);
        //    var brjbxx = _patientBasic.SelectPatientBasicInfoByCfh(po.cfh, po.OrganizeId);
        // if (brjbxx == null) return string.Format(" theo số đ ơn thuốc[{0}]và cơ quanid[{1}]chưatìm đ ến hệ thống thông tin cơ bản bệnh nhân！", po.cfh, po.Organizeid);
        //    var rp = new SupplementPatientBaseInfoRequest();
        //    brjbxx.MapperTo(rp);
        //    rp.cfh = po.cfh;
        //    rp.Timestamp = DateTime.Now;
        //    AppLogger.Instance.Info("SupplementPatientBaseInfo requestXml", rp);
        //    var rs = SitePDSAPIHelper.Request<APIRequestHelper.DefaultResponse>("api/Outpatient/SupplementPatientBaseInfo", rp, autoAppendToken: false);

        // return rs.code!= APIRequestHelper.ResponseResultCode.SUCCESS? string.Format("Cập nh ậtĐ ơn thuốc ngoại trú[{0}]bệnh nhân thông tin thất bại.Nguyên nhân thất bại:{1} {2}", po.cfh, rs.msg, rs.sub_msg): "";
        //}
    }
}