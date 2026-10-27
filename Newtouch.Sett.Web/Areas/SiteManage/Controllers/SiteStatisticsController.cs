using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using Newtouch.Sett.Web.Controllers;
using Newtouch.Sett.Web.Helpers;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Newtouch.Sett.Web.Areas.SiteManage.Controllers
{

    /// <summary>
    /// Thống kê trạm
    /// </summary>
    [Area("SiteManage")]

    public class SiteStatisticsController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IGRSCostEarningDmnService _jgssCostEarningDmnService;
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly Ijgss_EarningInfoRepo _jgss_EarningInfoRepo;
        private readonly Ijgss_AttachmentInfoRepo _jgss_AttachmentInfoRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        public SiteStatisticsController(IGRSCostEarningDmnService jgssCostEarningDmnService, ISysOrganizeDmnService sysOrganizeDmnService, Ijgss_EarningInfoRepo jgss_EarningInfoRepo, Ijgss_AttachmentInfoRepo jgss_AttachmentInfoRepo, ISysConfigRepo sysConfigRepo)
        {
            _jgssCostEarningDmnService = jgssCostEarningDmnService;
            _sysOrganizeDmnService = sysOrganizeDmnService;
            _jgss_EarningInfoRepo = jgss_EarningInfoRepo;
            _jgss_AttachmentInfoRepo = jgss_AttachmentInfoRepo;
            _sysConfigRepo = sysConfigRepo;
        }


        #region thu chi Thống kê Phê duyệt

        /// <summary>
        /// đượclấychờxét duyệtthuchithốngtínhsốdữ liệu
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAwaitingTrialSiteStatisticsGridJson()
        {
            return Content("");
        }

        /// <summary>
        /// Phê duyệt Thống kê khung nhìn
        /// </summary>
        /// <returns></returns>
        public IActionResult VerifySiteStatistics()
        {
            return View();
        }

        /// <summary>
        /// Phê duyệt thốngtínhrõchi tiết khung nhìn
        /// </summary>
        /// <returns></returns>
        public IActionResult VerifySiteStatisticsDetail()
        {
            ViewBag.currentUserName = UserIdentity.UserName;
            return View();
        }

        #endregion

        #region thu chi Thống kê Nhập

        /// <summary>
        /// đượclấychínhtrangsốdữ liệu
        /// </summary>
        /// <returns></returns>
        public IActionResult GetSiteStatisticsGridJson()
        {
            return Content("");
        }

        /// <summary>
        /// Thống kê nhập liệu khung nhìn
        /// </summary>
        /// <returns></returns>
        public override IActionResult Form()
        {
            var maxSize = SysConfigReader.String("jgzsMaxSize");
            maxSize = string.IsNullOrWhiteSpace(maxSize) ? 5.ToString() : maxSize;
            ViewData["maxSize"] = Convert.ToInt32(maxSize);
            var fcbl = _sysConfigRepo.GetIntValueByCode("jgssfdbl", this.OrganizeId);
            ViewData["jgssfdbl"] = fcbl;
            return View();
        }

        /// <summary>
        /// Thống kê nhập liệu khung nhìn
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult RevenueForm(string keyValue)
        {
            return View();
        }

        /// <summary>
        /// máycơ quanthuchiđiềnbáo cáo
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public IActionResult GetIndexGridJson(Pagination pagination, string siteId, string year, string month, string shzt, bool verify = false)
        {
            if (string.IsNullOrWhiteSpace(siteId))
            {
                throw new FailedException("Thiếu cơ sở");
            }
            if (string.IsNullOrWhiteSpace(year))
            {
                throw new FailedException("Thiếu Năm");
            }
            if (string.IsNullOrWhiteSpace(month))
            {
                throw new FailedException("Thiếu Tháng");
            }
            var list = new
            {
                rows = _jgssCostEarningDmnService.GetjgszInfoList(pagination, siteId, year, month, shzt, verify),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        public IActionResult ViewChargeMoneyDetail()
        {
            return View();
        }

        public IActionResult GetMoneyDetailGridJson(string siteId, string year, string month, string type, string dlcode)
        {
            if (string.IsNullOrWhiteSpace(siteId))
            {
                throw new FailedException("Thiếu trạm");
            }
            if (string.IsNullOrWhiteSpace(year))
            {
                throw new FailedException("Thiếu Năm");
            }
            if (string.IsNullOrWhiteSpace(month))
            {
                throw new FailedException("Thiếu Năm");
            }
            if (string.IsNullOrWhiteSpace(type))
            {
                throw new FailedException("Thiếu Loại");
            }
            if (string.IsNullOrWhiteSpace(dlcode))
            {
                throw new FailedException("Thiếu nhóm lớn");
            }
            //var list = new
            //{
            //    rows = _jgssCostEarningDmnService.GetMoneyDetailListV2(pagination, siteId, year, month, type, dlcode),
            //    total = pagination.total,
            //    page = pagination.page,
            //    records = pagination.records
            //};
            var list = _jgssCostEarningDmnService.GetMoneyDetailList(siteId, year, month, type, dlcode);
            return Content(list.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="site"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public IActionResult GetGridJson(string site, string year, string month)
        {
            var data = _jgssCostEarningDmnService.GetCosttable(site, year, month);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="srxx"></param>
        /// <param name="srxxList"></param>
        /// <param name="cbxxList"></param>
        /// <param name="keyvalue"></param>
        /// <param name="fjxx"></param>
        /// <returns></returns>
        
        public IActionResult savefile(string srxx, string srxxList, string cbxxList, string keyvalue, string fjxx)
        {
                        var vo = new SiteCostEarningVO
            {
                srxx = JsonConvert.DeserializeObject<jgssEarningVO>(srxx),
                srxxList = JsonConvert.DeserializeObject<List<jgssEarningGridVO>>(srxxList),
                cbxxList = JsonConvert.DeserializeObject<List<jgssCostVO>>(cbxxList),
                fjxx = JsonConvert.DeserializeObject<List<jgssAttachmentVO>>(fjxx),
            };

            if (Request.Form.Files.Count > 0)
            {
                for (int i = 0; i < Request.Form.Files.Count; i++)
                {

                    string path = "";
                    _jgssCostEarningDmnService.UploadFile(Request.Form.Files[i].FileName, out path);

                    jgssAttachmentVO javo = new jgssAttachmentVO()
                    {
                        ContentType = Request.Form.Files[i].ContentType,
                        fjmc = Request.Form.Files[i].FileName,
                        fjPath = path
                    };
                    vo.fjxx.Add(javo);

                }
            }
            _jgssCostEarningDmnService.submitEarningInfo(vo, keyvalue, OrganizeId);


            return Success();
        }

        public IActionResult Download(string id)
        {

            var earn = _jgss_AttachmentInfoRepo.FindEntity(p => p.Id == id && p.zt == "1");
            if (earn == null)
            {
                throw new FailedException("Thiếu thông tin thu chi cơ sở");
            }
            if (string.IsNullOrWhiteSpace(earn.fjPath))
            {
                throw new FailedException("Thiếu đường dẫn tệp đính kèm");
            }
            if (string.IsNullOrWhiteSpace(earn.ContentType))
            {
                throw new FailedException("Thiếu loại tệp đính kèm");
            }
            //var configFileExportBaseDir = SysConfigReader.String("LocalFileBaseDir");
            //if (string.IsNullOrWhiteSpace(configFileExportBaseDir))
            //{
            //    configFileExportBaseDir = "D:\\";
            //}

            //1.chỉđịnhcầnviếtđếncủavănhạngmụcghivàtêngọi
            byte[] buffer = System.IO.File.ReadAllBytes(earn.fjPath);

            return File(buffer, earn.ContentType, System.IO.Path.GetFileName(earn.fjPath));

        }


        /// <summary>
        /// submit
        /// </summary>
        /// <param name="keyWord">SiteCostEarningVO </param>
        /// <returns></returns>

        public IActionResult savedata(jgssEarningVO srxx, List<jgssEarningGridVO> srxxList, List<jgssCostVO> cbxxList, string keyvalue)
        {

            SiteCostEarningVO vo = new SiteCostEarningVO();
            vo.srxx = srxx;
            vo.srxxList = srxxList;
            vo.cbxxList = cbxxList;

            _jgssCostEarningDmnService.submitEarningInfo(vo, keyvalue, OrganizeId);
            return Success();
        }

        /// <summary>
        /// get form json
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(string keyWord)
        {
            var data = _jgssCostEarningDmnService.GetEarningInfo(keyWord, this.OrganizeId);
            if (!string.IsNullOrWhiteSpace(data.srxx.siteId))
            {
                data.srxx.zdmc = _sysOrganizeDmnService.GetNameByOrgId(data.srxx.siteId);
            }
            return Success(data.ToJson());
        }

        /// <summary>
        /// delete data by keyValue
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(string keyValue)
        {
            _jgssCostEarningDmnService.deletess(keyValue);
            return Success();
        }

        #endregion

        #region thu chi Thống kê  highCharts

        /// <summary>
        /// Biểu đồ thống kê thu chi trạm khung nhìn
        /// </summary>
        /// <returns></returns>
        public IActionResult SiteStatisticsHighCharts()
        {
            return View();
        }

        #endregion

        /// <summary>
        /// xét duyệtkiểm trathao táclàm
        /// </summary>
        /// <param name="id"></param>
        /// <param name="shzt"></param>
        /// <returns></returns>
        public IActionResult updateshzt(string id, string shzt)
        {
            string account = UserIdentity.UserCode;
            _jgss_EarningInfoRepo.updateshzt(id, shzt, account);
            return Success();

        }

        /// <summary>
        /// khitrướcđăngghingườicủanhómtổ chứcmáycơ quan
        /// </summary>
        /// <returns></returns>
        public IActionResult getCurrentOrgIds()
        {
            var mediOrgList = _sysOrganizeDmnService.GetMedicalOrganizeListByUserId(UserIdentity.UserId);
            return Content(mediOrgList.ToJson());
        }

        /// <summary>
        /// khitrướcđăngghingườicủacâynhómtổ chứcmáycơ quan
        /// </summary>
        /// <returns></returns>
        public IActionResult getCurrentTreeOrgIds()
        {
            var mediOrgList = _sysOrganizeDmnService.GetMedicalOrganizeListByUserId(UserIdentity.UserId);
            var treeList = new List<TreeSelectModel>();
            foreach (var item in mediOrgList)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.Id;
                treeModel.text = item.Name;
                treeModel.parentId = null;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson(null));
        }



        public IActionResult getAllOrgIds()
        {

            var data = _sysOrganizeDmnService.GetValidListByParentOrg(OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// khitrướcnhómtổ chứcmáycơ quankhitrướcthời giangiữalàkhôngđãlưutạiqua
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public IActionResult isAlreadyCollect(string site, string year, string month)
        {
            var earn = _jgss_EarningInfoRepo.FindEntity(p => p.siteId == site && p.year == year && p.month == month && p.zt == "1");
            if (earn != null)
            {
                return Content(true.ToJson());
            }
            return Content(false.ToJson());

        }

        /// <summary>
        /// đượclấytrêncáithángcủathànhnàythông tintin
        /// </summary>
        /// <param name="site"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public IActionResult loadLastMonthcbxx(string site, string year, string month)
        {
            var data = _jgssCostEarningDmnService.getLastcbxx(site, year, month);
            return Content(data.ToJson());
        }
    }
}
