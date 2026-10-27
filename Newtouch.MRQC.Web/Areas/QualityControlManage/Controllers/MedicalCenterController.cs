using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.MRQC.Domain;
using Newtouch.MRQC.Domain.ValueObjects;
using Newtouch.MRQC.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.MRQC.Web.Areas.QualityControlManage.Controllers
{
    [Area("QualityControlManage")]
    public class MedicalCenterController : OrgControllerBase
    {
        private readonly IQcItemDataRepo _qcitemdataRepo;
        private readonly IQcBlzkDmnService _qcblzkDmn;

        public MedicalCenterController(IQcItemDataRepo qcitemdataRepo, IQcBlzkDmnService qcblzkDmn)
        {
            _qcitemdataRepo = qcitemdataRepo;
            _qcblzkDmn = qcblzkDmn;
        }

        // GET: QualityControlManage/MedicalCenter
        // PORT NOTE: the original built a DCSoft.Writer ActiveX document engine
        // here (DCSoft.Writer.ForASPNET, Windows/IE-only, no .NET Core build).
        // The page renders without the editor control in this build.
        public override IActionResult Index()
        {
            ViewBag.WriterControlHtml = "";
            return View();
        }

        public IActionResult PreView()
        {
            return View();
        }

        public IActionResult MessageQuery()
        {
            return View();
        }

        public IActionResult ScoreFrom()
        {
            return View();
        }

        public IActionResult QcMessageManage()
        {
            return View();
        }

        #region DC病历文书

        // PORT STUB: the DCSoft writer service page has no .NET Core equivalent.
        // Kept as an explicit error instead of a 404 so callers get a clear message.
        public IActionResult MoreHandleDCWriterServicePage()
        {
            return Error("Trình soạn thảo tài liệu DCSoft.Writer tạm thời chưa được hỗ trợ trong phiên bản .NET Core");
        }

        #endregion

        #region 质控中心接口调用
        public IActionResult GetPatList(string brbz, DateTime ksrq, DateTime jsrq, string srz)
        {
            string token = AuthenManageHelper.GetToken();
            var reqObj = new
            {
                Data = new
                {
                    brbz = brbz,
                    ksrq = ksrq,
                    jsrq = jsrq,
                    srz = srz
                },
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            AuthenTokenHelper apiResp = JsonConvert.DeserializeObject<AuthenTokenHelper>(AuthenManageHelper.HttpPost(reqObj.ToJson(),
                AuthenManageHelper.SiteConmonAPIHost + "api/MedicalBl/MedicalCenterPatInfo", token));

            var data = JsonConvert.DeserializeObject<List<PatientDataVo>>(apiResp.BusData.data.ToString());
            return Content(data.ToJson());
        }

        /// <summary>
        /// 诊断列表
        /// </summary>
        public IActionResult GetMedicalCenterDiag(string zyh)
        {
            string token = AuthenManageHelper.GetToken();
            var reqObj = new
            {
                Data = new { zyh = zyh },
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            AuthenTokenHelper apiResp = JsonConvert.DeserializeObject<AuthenTokenHelper>(AuthenManageHelper.HttpPost(reqObj.ToJson(),
                AuthenManageHelper.SiteConmonAPIHost + "api/MedicalBl/MedicalCenterDiaglist", token));

            var data = JsonConvert.DeserializeObject<List<MedicalHomeDiagVo>>(apiResp.BusData.data.ToString());
            return Content(data.ToJson());
        }

        /// <summary>
        /// 病历文书tree
        /// </summary>
        public IActionResult GetMedicalCenterBlTree(string zyh)
        {
            string token = AuthenManageHelper.GetToken();
            var reqObj = new
            {
                Data = new { zyh = zyh },
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            AuthenTokenHelper apiResp = JsonConvert.DeserializeObject<AuthenTokenHelper>(AuthenManageHelper.HttpPost(reqObj.ToJson(),
                AuthenManageHelper.SiteConmonAPIHost + "api/MedicalBl/MedicalCenterBlwsTree", token));

            var data = JsonConvert.DeserializeObject<List<MedRecordTree>>(apiResp.BusData.data.ToString());
            var treeList = new List<TreeGridModel>();
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    TreeGridModel treeModel = new TreeGridModel();
                    bool hasChildren = data.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                    treeModel.id = item.Id.ToString();
                    treeModel.isLeaf = hasChildren;
                    treeModel.parentId = item.ParentId == null ? null : item.ParentId.ToString();
                    treeModel.expanded = hasChildren;
                    treeModel.entityJson = item.ToJson();
                    treeList.Add(treeModel);
                }
            }
            return Content(treeList.TreeGridJson(null));
        }

        public IActionResult GetDoctorsAdviceRecord(string zyh, string yzlx)
        {
            var reqObj = new
            {
                Data = new { zyh = zyh, yzlx = yzlx },
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            var outstr = AuthenManageHelper.HttpPost<List<DoctorAdviceRecordVo>>(reqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/MedicalDoctorsAdvice/GetDoctorAdviceRecore", this.UserIdentity);
            return Content(outstr.data.ToJson());
        }

        /// <summary>
        /// 门诊住院检验检查申请单
        /// </summary>
        public IActionResult GetInspectionExaminationRecore(string jzh, DateTime ksrq, DateTime jsrq, string reportType, string mzzybz)
        {
            var reqObj = new
            {
                Data = new { jzh = jzh, ksrq = ksrq, jsrq = jsrq, reportType = reportType, mzzybz = mzzybz },
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            var outstr = AuthenManageHelper.HttpPost<List<MedicalInspectionExaminationVo>>(reqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/MedicalDoctorsAdvice/GetInspectionExaminationRecore", this.UserIdentity);
            return Content(outstr.data.ToJson());
        }

        #endregion

        #region 质控消息
        public IActionResult GetTyzkxm(string bllx, string keyword)
        {
            var data = _qcitemdataRepo.IQueryable(p => p.BlmbId == bllx && p.OrganizeId == this.OrganizeId && p.zt == "1" && p.Name.Contains(keyword)).ToList();
            return Content(data.ToJson());
        }

        public IActionResult SendMsg(MsgSendVo sendvo, string zyysgh)
        {
            var blwszk = "ZKNoticeBl" + this.UserIdentity.UserCode;
            var defaultNoticeContent = "Họ tên:{0}, số nhập viện:{1}, số giường:{2}, khoa:{3}, bác sĩ:{4}, hồ sơ bệnh án có bất thường kiểm soát chất lượng, loại hồ sơ:{5}, văn bản hồ sơ:{6}, nội dung phản hồi:{7}, mức độ vấn đề:{8}, vui lòng xử lý kịp thời, thời hạn xử lý:{9}";
            var noticeGroup = new
            {
                GroupTag = blwszk,
                GroupName = "Tin nhắn nhóm kiểm soát chất lượng văn bản hồ sơ",
                GroupDesc = "Dành riêng cho văn bản hồ sơ của hệ thống kiểm soát chất lượng",
                GroupYwlx = (int)GroupYwlxEnum.None,
                NoticeRange = (int)MsgNoticeRangeEnum.User,
                DefaultTitle = "Bạn có tin nhắn kiểm soát chất lượng hồ sơ mới cần tiếp nhận",
                DefaultContent = defaultNoticeContent,
                DefaultContentData = "",
                CreatorCode = this.UserIdentity.UserCode
            };
            var reqObj = new
            {
                Data = noticeGroup,
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            var NoticeGroupId = "";
            var checknoticeGroup = AuthenManageHelper.HttpPost<List<MsgNoticeGroupVo>>(reqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/MsgQueue/GetNoticeGroup", this.UserIdentity);
            if (checknoticeGroup.data != null && checknoticeGroup.data.Count > 0)
            {
                NoticeGroupId = checknoticeGroup.data[0].Id;
            }
            else
            {
                var addnoticeGroup = AuthenManageHelper.HttpPost<string>(reqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/MsgQueue/NewNoticeGroup", this.UserIdentity);
                if (addnoticeGroup.code == 10000 && !string.IsNullOrWhiteSpace(addnoticeGroup.data))
                {
                    NoticeGroupId = addnoticeGroup.data;
                }
                else
                {
                    throw new FailedException("Gửi tin nhắn thất bại: " + addnoticeGroup.msg);
                }
            }
            defaultNoticeContent = "Họ tên:{xm}, số nhập viện:{jzh}, số giường:{ch}, khoa:{ks}, bác sĩ:{zyys}, hồ sơ bệnh án có bất thường kiểm soát chất lượng, ngày kiểm soát:{zkrq}, loại kiểm soát:{zklx}, văn bản hồ sơ:{hzwd}, nội dung phản hồi:{fknr}, mức độ vấn đề:{wtdj}, vui lòng xử lý kịp thời, thời hạn xử lý:{qxclsj}";
            var NoticeQueue = new
            {
                SendFrom = this.UserIdentity.UserCode,
                NoticeGroupId = NoticeGroupId,
                GroupYwlx = (int)GroupYwlxEnum.None,
                NoticeRange = (int)MsgNoticeRangeEnum.User,
                Title = "Bạn có tin nhắn kiểm soát chất lượng văn bản hồ sơ mới cần tiếp nhận",
                Content = defaultNoticeContent,
                ContentData = sendvo.ToJson(),
                CreatorCode = this.UserIdentity.UserCode,
                Recipient = zyysgh
            };
            var NoticereqObj = new
            {
                Data = NoticeQueue,
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            var NewNoticeResult = AuthenManageHelper.HttpPost<List<string>>(NoticereqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/MsgQueue/NewNotice", this.UserIdentity);

            return Success(NewNoticeResult.code == 10000 ? "Gửi tin nhắn kiểm soát chất lượng thành công" : NewNoticeResult.msg);
        }

        public IActionResult MessageSendQuery(string status, DateTime ksrq, DateTime jsrq)
        {
            var data = GetMessageSendData(status, ksrq, jsrq);
            return Content(data.ToJson());
        }

        public IActionResult GetDclMsgJson(string status, DateTime ksrq, DateTime jsrq)
        {
            var msgvo = GetMessageSendData(status, ksrq, jsrq);
            List<string> data = new List<string>();
            if (msgvo != null)
            {
                data = msgvo.Where(t => t.GroupTag.Contains("ZKNoticeBl")).Select(p => p.ContentData).ToList();
            }
            var treeList = new List<TreeGridModel>();
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    TreeGridModel treeModel = new TreeGridModel();
                    treeModel.id = Guid.NewGuid().ToString();
                    treeModel.isLeaf = false;
                    treeModel.parentId = null;
                    treeModel.expanded = false;
                    treeModel.entityJson = item;
                    treeList.Add(treeModel);
                }
            }
            return Content(treeList.TreeGridJson(null));
        }

        public List<MsgNoticeQueueVo> GetMessageSendData(string status, DateTime ksrq, DateTime jsrq)
        {
            var reqObj = new
            {
                Data = new { NoticeStu = status, ksrq = ksrq, jsrq = jsrq, appId = AuthenManageHelper.appId, },
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            var outstr = AuthenManageHelper.HttpPost<List<MsgNoticeQueueVo>>(reqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/MsgQueue/NoticeQuery", this.UserIdentity);
            var data = outstr.data != null ? outstr.data.OrderByDescending(p => p.CreateTime).ToList() : outstr.data;
            return data;
        }
        #endregion

        #region 评分
        public IActionResult Getybbxbldata(string bllx, string zyh)
        {
            var list = _qcblzkDmn.Getybbxbldata(bllx, this.OrganizeId, zyh);
            return Content(list.ToJson());
        }

        /// <summary>
        /// 保存评分
        /// </summary>
        public IActionResult SaveScoreDate(List<ScoreEntity> entity, string bllx, string blmc, string zyh)
        {
            _qcblzkDmn.SaveScoreDate(entity, this.OrganizeId, this.UserIdentity.UserCode, bllx, blmc, zyh);
            return Success();
        }
        #endregion
    }
}
