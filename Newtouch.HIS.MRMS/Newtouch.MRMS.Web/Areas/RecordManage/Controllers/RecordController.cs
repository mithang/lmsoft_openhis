using Newtouch.MR.ManageSystem.Domain.IDomainServices;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Init;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.MRMS.Web.Models;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using Newtouch.MR.ManageSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace Newtouch.MRMS.Web.Areas.RecordManage.Controllers
{
    [Area("RecordManage")]
    public class RecordController : OrgControllerBase
    {
        public RecordController(IEMRDmnService EMRDmnService, IRecordDmnService RecordDmnService, ICommonDmnService CommonDmnService, IZybrjbxxDmnService ZybrjbxxDmnService)
        {
            this._EMRDmnService = EMRDmnService;
            this._RecordDmnService = RecordDmnService;
            this._CommonDmnService = CommonDmnService;
            this._ZybrjbxxDmnService = ZybrjbxxDmnService;
        }

#pragma warning disable CS0649 // Field 'RecordController._EMRDmnService' is never assigned to, and will always have its default value null
        private readonly IEMRDmnService _EMRDmnService;
#pragma warning restore CS0649 // Field 'RecordController._EMRDmnService' is never assigned to, and will always have its default value null
#pragma warning disable CS0649 // Field 'RecordController._RecordDmnService' is never assigned to, and will always have its default value null
        private readonly IRecordDmnService _RecordDmnService;
#pragma warning restore CS0649 // Field 'RecordController._RecordDmnService' is never assigned to, and will always have its default value null
        public static string BlTemplatePath = AppSettings.GetConfig("BlTemplatePath");
        public static string BlFilePath = AppSettings.GetConfig("BlFilePath");
        public static string EditorRegCode = AppSettings.GetConfig("EditorRegCode");
#pragma warning disable CS0169 // The field 'RecordController._CommonDmnService' is never used
        private readonly ICommonDmnService _CommonDmnService;
#pragma warning restore CS0169 // The field 'RecordController._CommonDmnService' is never used
#pragma warning disable CS0649 // Field 'RecordController._ZybrjbxxDmnService' is never assigned to, and will always have its default value null
        private readonly IZybrjbxxDmnService _ZybrjbxxDmnService;
#pragma warning restore CS0649 // Field 'RecordController._ZybrjbxxDmnService' is never assigned to, and will always have its default value null

        // GET: RecordManage/Record
        public IActionResult Receive()
        {
             EMRBasyPrintReportCom();

            return View();
        }

        #region Trang chức năng ký nhận bệnh án
        public IActionResult PatList(Pagination pagination, string keyword, string zyh, string type, string cyts, string blzt,string kssj,string jssj)
        {
            var chargeQueryList = new
            {
                rows = _EMRDmnService.GetPagintionList(pagination, keyword, zyh, null, blzt, this.UserIdentity.rygh, this.OrganizeId, Convert.ToInt32(type),kssj,jssj),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            kssj = "";jssj = "";
            return Content(chargeQueryList.ToJson());

        }

        /// <summary>
        /// 获取Bệnh án树
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult GetMedRecordTree(string zyh)
        {
            OperatorModel user = this.UserIdentity;
            var data = _EMRDmnService.GetPatMedRecordTree(this.OrganizeId, zyh, user.rygh);
            var treeList = new List<TreeGridModel>();
            foreach (var item in data)
			{
				bool hasChildren = data.Count(t => t.parentId == item.Id) != 0;
				var treeModel = new TreeGridModel
				{
					id = item.Id,
					isLeaf = hasChildren,
					parentId = item.parentId == null ? null : item.parentId.ToString(),
					expanded = hasChildren,
					entityJson = item.ToJson()
				};

				treeList.Add(treeModel);

            }
            return Content(treeList.TreeGridJson(null));
        }


        #endregion

        #region  开源版Sửa器
        public IActionResult PreViewV2()
        {
            return View();
        }

        public IActionResult LoadMedicalRecordHtml(string blid, string bllx, string zyh)
        {
            string currentFileName = AppSettings.GetConfig("DcWriterFileRoter") + "\\File\\Template\\" + "newFile.html";
            string bllj = "";
            string blxtmc_yj = "";
            var MedicalRecord = _RecordDmnService.GetMedicalRecord(blid, bllx, OrganizeId);
            bllj = MedicalRecord.blxtml;
            blxtmc_yj = MedicalRecord.blxtmc_yj;
            if (string.IsNullOrWhiteSpace(MedicalRecord.blxtmc_yj))
            {
                blxtmc_yj = _RecordDmnService.GetZymeddocsrelation(blid, OrganizeId).blmc;
            }
            currentFileName = AppSettings.GetConfig("DcWriterFileRoter") + bllj + blxtmc_yj.Trim() + ".html";
            currentFileName = currentFileName.Replace("~", "");
            currentFileName = currentFileName.Replace("/", "\\");
            if(System.IO.File.Exists(currentFileName) == false)//如果不存在就打开默认Mẫu
            {
                currentFileName = AppSettings.GetConfig("DcWriterFileRoter") + "\\File\\Template\\" + "newFile.html";
            }
            // 加载Mẫu
            string htmldata = System.IO.File.ReadAllText(currentFileName);
            BlTemplateVo vo = new BlTemplateVo();
            vo.xmldata = htmldata;
            vo.templateId = blid;
            vo.templateName = blxtmc_yj;
            return Content(vo.ToJson());
        }

        #endregion

        public IActionResult PreView(string blid, string bllx, string zyh)
        {
            // DCSoft Writer ActiveX editor has no .NET Core build (same vendor
            // blocker as the EMR editor): the viewer area is an explicit error.
            // Use PreViewV2 (open-source HTML editor path) instead.
            return Content(new { state = "error", message = "Trình xem tài liệu DCSoft Writer (ActiveX, chỉ chạy trên .NET Framework/IE) không khả dụng trên .NET Core/Linux. Vui lòng dùng PreViewV2." }.ToJson());
        }


        #region 数据初始化
        /// <summary>
        /// TheoSố nội trú获取病 người基本信息
        /// 实体必须是可序列化的才能在Bệnh án控件里绑定上数据所以使用ZybrjbxxVO
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        private ZybrjbxxVO GetEMRZybrjbxxByZYH(string zyh)
        {
            return _EMRDmnService.GetZyPatInfobyzyh(OrganizeId, zyh);
        }
        #endregion 

        /// <summary>
        /// TheoSố nội trú获取病 người基本信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public BabasyVO GetBasyByZYH(string zyh)
        {

            var BabasyVO = _ZybrjbxxDmnService.GetPatBasicInfo_basy(OrganizeId, zyh); //_ZybrjbxxRepo.GetZybrjbxx(zyh, OrganizeId);

            //var BabasyVO = new BabasyVO();
            //BabasyVO.zyh = ZybrjbxxEntity.zyh;
            //BabasyVO.blh = ZybrjbxxEntity.blh;
            //BabasyVO.brxm = ZybrjbxxEntity.;
            //BabasyVO.sfzhm = ZybrjbxxEntity.sfzh;
            //BabasyVO.xb = ZybrjbxxEntity.sex;
            //BabasyVO.csny = ZybrjbxxEntity.birth;
            //BabasyVO.hljb = ZybrjbxxEntity.hljb;
            //BabasyVO.ryrq = ZybrjbxxEntity.ryrq;
            ////BabasyVO.rqrq = ZybrjbxxEntity.rqrq;
            ////BabasyVO.cqrq = ZybrjbxxEntity.cqrq;
            ////BabasyVO.wzjb = ZybrjbxxEntity.wzjb;

            //BabasyVO.rytj = ZybrjbxxEntity.ryfs;
            ////BabasyVO.cyfs = ZybrjbxxEntity.cyfs;
            ////BabasyVO.gdxmzxrq = ZybrjbxxEntity.gdxmzxrq;
            ////BabasyVO.brxzdm = ZybrjbxxEntity.brxzdm;
            ////BabasyVO.brxzmc = ZybrjbxxEntity.brxzmc;
            ////BabasyVO.cardno = ZybrjbxxEntity.cardno;
            ////BabasyVO.cardtype = ZybrjbxxEntity.cardtype;
            ////BabasyVO.zybz = ZybrjbxxEntity.zybz;
            ////BabasyVO.sfqj = ZybrjbxxEntity.sfqj;
            ////BabasyVO.DeptCode = ZybrjbxxEntity.DeptCode;
            ////BabasyVO.WardCode = ZybrjbxxEntity.WardCode;
            ////BabasyVO.ysgh = ZybrjbxxEntity.ysgh;
            ////BabasyVO.BedCode = ZybrjbxxEntity.BedCode;
            //BabasyVO.lxr = ZybrjbxxEntity.lxr;
            //// BabasyVO.lxrgx = ZybrjbxxEntity.lxrgx;
            //BabasyVO.lxdh = ZybrjbxxEntity.lxrdh;

            //BabasyVO.cyzdmc = ZybrjbxxEntity.cyzdmc;

            return BabasyVO;
        }



        //设置Sửa器的引用路径
        public IActionResult HandleDCWriterServicePage()
        {
            // DCSoft Writer service page: no .NET Core build of DCSoft.Writer.
            return Content(new { state = "error", message = "Trang dịch vụ DCSoft Writer không khả dụng trên .NET Core/Linux." }.ToJson());
        }


        //处理Thao tác结果生成一个带文档内容的Sửa器


        /// <summary>
        /// 
        /// </summary>
        /// <param name="blid"></param>
        /// <param name="bllx"></param>
        /// <param name="isLock">1请求SửaBệnh án2请求Mở khóaBệnh án</param>
        /// <returns></returns>
        public IActionResult LockRecord(string blid, string bllx, int isToLock)
        {
            var MedicalRecord = _RecordDmnService.GetMedicalRecord(blid, bllx, OrganizeId);
            if (isToLock == 1)
            {
                if (MedicalRecord.IsLock != 1)
                {
                    _RecordDmnService.LockRecord(blid, bllx, OrganizeId, this.UserIdentity.UserCode, isToLock);
                }
            }
            else
            {
                if (MedicalRecord.IsLock == 1)
                {
                    if (MedicalRecord.LastModifierCode == this.UserIdentity.UserCode)
                    {
                        _RecordDmnService.LockRecord(blid, bllx, OrganizeId, this.UserIdentity.UserCode, isToLock);
                        return Success("Mở khóa thành công");
                    }
                    else
                    {
                        throw new FailedException("Mở khóa thất bại，请" + MedicalRecord.LastModifierCode + "Mở khóa");
                    }
                }
                else
                {
                    return Success("Chưa bị khóa");
                }

            }
            return Success("Mở khóa thành công");
        }



        public IActionResult updateRecordStu(string Id,string blzt) {
            var LastModifierCode = UserIdentity.UserName;
            var LastModifyTime = DateTime.Now;
            _RecordDmnService.updateRecordStu(Id,OrganizeId, LastModifierCode, LastModifyTime,blzt);
            return Success("Thao tác thành công。");
        }




        private void EMRBasyPrintReportCom()
        {
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.topOrgId = Constants.TopOrganizeId;
            ViewBag.ReportServerHOST = AppSettings.GetConfig("ReportServer.HOST");
            ViewBag.IsHospAdministrator = this.UserIdentity.IsHospAdministrator.ToString().ToLower();  //是否是医院管理员
            ViewBag.CurUserCode = this.UserIdentity.UserCode;
            ViewBag.curUsergh = UserIdentity.rygh;
        }








    }
}
