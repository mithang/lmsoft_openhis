

using NewtouchHIS.Framework.Web.Controllers;
using Newtonsoft.Json;

using NewtouchHIS.Lib.Framework.Operator;

using NewtouchHIS.Lib.Base;

using Newtouch.EMR.APIRequest.Bljgh.Request;
using Newtouch.EMR.Domain.BusinessObjects;
using Newtouch.EMR.Domain.DTO;
using Newtouch.EMR.Domain.DTO.OutputDto.MRUpload;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IDomainServices;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.EMR.Domain.ValueObjects;
using Newtouch.EMR.Domain.ValueObjects.MedicalRecord;

using NewtouchHIS.Lib.Base.Model;
using Newtouch.EMR.Web.Helpers;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Attributes;
using System.Xml;

namespace Newtouch.EMR.Web.Areas.MedicalRecordManage
{
    [Area("MedicalRecordManage")]
    public class MedicalRecordController : OrgControllerBase
    {
        #region private
        private readonly IMedicalRecordDmnService _medicalRecordDmnService;
        private readonly IBlmblbRepo _bl_mblbRepo;
        private readonly IZymeddocsrelationRepo _ZymeddocsrelationRepo;
        private readonly IZybrjbxxRepo _ZybrjbxxRepo;
        private readonly IBabasyRepo _BabasyRepo;
        private readonly ISysDepartmentRepo _SysDepartmentRepo;
        private readonly ISysStaffRepo _SysStaffRepo;
        private readonly IZybrjbxxDmnService _ZybrjbxxDmnService;
        private readonly IMrbasyRepo _MrbasyRepo;
        private readonly IBlmblbDmnService _blmblbDmnService;
        private readonly IYBInpPatRegInfoRepo _yBInpPatRegInfoRepo;
        private readonly IYBInpOutHosSummariesDiagRepo _yBInpOutHosSummariesDiagRepo;
        private readonly IYBInpOutHosSummariesRepo _yBInpOutHosSummariesRepo;
        private readonly IYBInpCourseDiseaseRepo _yBInpCourseDiseaseRepo;
        private readonly IYBInpPatRegInfoDiagRepo _yBInpPatRegInfoDiagRepo;
        private readonly IYBInterfaceDmnService _yBInterfaceDmnService;
        private readonly IOutpatientDmnService _outpatientDmnService;
        private readonly IMzmeddocsrelationRepo _mzmeddocsrelationRepo;
        private readonly Ibl_ElementDomainRepo _bljsRepo;

        public static string BlTemplatePath = EmrConfig.GetAppConfigValue("BlTemplatePath");
        public static string BlBackupFilePath = EmrConfig.GetAppConfigValue("BlBackupFilePath");
        /// <summary>
        /// Số ngày giữ lại sao lưu lịch sử
        /// </summary>
        public static string KeepBackupFileDays = EmrConfig.GetAppConfigValue("KeepBackupFileDays");
        public static string BlFilePath = EmrConfig.GetAppConfigValue("BlFilePath");
        public static string EditorRegCode = EmrConfig.GetAppConfigValue("EditorRegCode");
        private readonly string IsEnableWebEditor = EmrConfig.GetAppConfigValue("EnableWebHomePage");
        private readonly string YBOrgCode = EmrConfig.GetAppConfigValue("YBOrgCode");

        private readonly ICommonDmnService _CommonDmnService;

        public MedicalRecordController(IMedicalRecordDmnService medicalRecordDmnService, IBlmblbRepo bl_mblbRepo, IZymeddocsrelationRepo zymeddocsrelationRepo, IZybrjbxxRepo zybrjbxxRepo, IBabasyRepo babasyRepo, ISysDepartmentRepo sysDepartmentRepo, ISysStaffRepo sysStaffRepo, IZybrjbxxDmnService zybrjbxxDmnService, IMrbasyRepo mrbasyRepo, IBlmblbDmnService blmblbDmnService, IYBInpPatRegInfoRepo yBInpPatRegInfoRepo, IYBInpOutHosSummariesDiagRepo yBInpOutHosSummariesDiagRepo, IYBInpOutHosSummariesRepo yBInpOutHosSummariesRepo, IYBInpCourseDiseaseRepo yBInpCourseDiseaseRepo, IYBInpPatRegInfoDiagRepo yBInpPatRegInfoDiagRepo, IYBInterfaceDmnService yBInterfaceDmnService, IOutpatientDmnService outpatientDmnService, IMzmeddocsrelationRepo mzmeddocsrelationRepo, Ibl_ElementDomainRepo bljsRepo, ICommonDmnService commonDmnService)
        {
            this._medicalRecordDmnService = medicalRecordDmnService;
            this._bl_mblbRepo = bl_mblbRepo;
            this._ZymeddocsrelationRepo = zymeddocsrelationRepo;
            this._ZybrjbxxRepo = zybrjbxxRepo;
            this._BabasyRepo = babasyRepo;
            this._SysDepartmentRepo = sysDepartmentRepo;
            this._SysStaffRepo = sysStaffRepo;
            this._ZybrjbxxDmnService = zybrjbxxDmnService;
            this._MrbasyRepo = mrbasyRepo;
            this._blmblbDmnService = blmblbDmnService;
            this._yBInpPatRegInfoRepo = yBInpPatRegInfoRepo;
            this._yBInpOutHosSummariesDiagRepo = yBInpOutHosSummariesDiagRepo;
            this._yBInpOutHosSummariesRepo = yBInpOutHosSummariesRepo;
            this._yBInpCourseDiseaseRepo = yBInpCourseDiseaseRepo;
            this._yBInpPatRegInfoDiagRepo = yBInpPatRegInfoDiagRepo;
            this._yBInterfaceDmnService = yBInterfaceDmnService;
            this._outpatientDmnService = outpatientDmnService;
            this._mzmeddocsrelationRepo = mzmeddocsrelationRepo;
            this._bljsRepo = bljsRepo;
            this._CommonDmnService = commonDmnService;
        }
        private static WebWriterControlEngine engine;
        private static string UploadURL = EmrConfig.GetAppConfigValue("MRUploadURL");
        #endregion
        
        #region DCwriter

        private WebWriterControlEngine GetControlEngine()
        {
            return new WebWriterControlEngine();
        }

        //Thiết lập đường dẫn tham chiếu của trình soạn thảo
        // DCSoft.Writer has no .NET Core build: this service endpoint reports the
        // limitation explicitly instead of 404. A replacement web editor is the
        // remaining EMR item.
        public IActionResult HandleDCWriterServicePage()
        {
            return Content(new AjaxResult
            {
                state = ResultType.error.ToString(),
                message = "Trình soạn thảo văn bản DCSoft chưa khả dụng trên .NET Core (không có bản build .NET Core). Cần thay bằng trình soạn thảo web khác."
            }.ToJson());
        }


        //Xử lý kết quả thao tác để tạo trình soạn thảo có nội dung tài liệu

        #endregion

        #region  Thao tác văn bản bệnh án của trình soạn thảo dc
        /// <summary>
        /// Theo mẫuIDThêm mới bệnh án
        /// </summary>
        /// <param name="mbbh"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult MedicalRecordEdiForAdd(string mbbh, string zyh, string mzh = null,string submitXml=null)
        {
            //TheoidLấy mẫu
            //Nếu không tìm thấy xml để tải thì mặc định là xml trống
            string currentFileName = WebPath.Map(BlTemplatePath + "newFile.xml");
            var mb = _bl_mblbRepo.GetTemplateById(mbbh);
            string errormsg = "";
            string bllxmc = mb.bllxmc; // _CommonDmnService.GetBllxTB(mb.bllx);
                                       // Tải tệp
            WebWriterControlEngine eng;
            if (engine == null)
            {
                eng = GetControlEngine();
            }
            else
            {
                eng = engine;
            }
            string ywid = zyh.Trim();
            if (!string.IsNullOrWhiteSpace(mzh))
            {
                ywid = mzh.Trim();
            }
            if (Request.Method == "GET")
            {
                if (mb != null && !string.IsNullOrWhiteSpace(mb.mblj))
                {
                    currentFileName = WebPath.Map(mb.mblj);
                    // Tải mẫu
                    if (System.IO.File.Exists(currentFileName) == false)//Nếu không tồn tại thì mở mẫu mặc định
                    {
                        currentFileName = WebPath.Map(BlTemplatePath + "newFile.xml");
                    }
                    eng.LoadDocument(currentFileName, null);
                    if (mb.bllx == "5")
                    {
                        var BabasyVO = GetBasyByZYH(zyh);
                        //eng.Document.SetDocumentParameterValue("ba_basy", BabasyVO);
                        XMLDataBind(BabasyVO, eng);
                    }
                    else if (!string.IsNullOrWhiteSpace(mzh))
                    {
                        var mzpat = GetOutpatbyMzh(mzh);
                        XMLDataBind(mzpat, eng);
                    }
                    else
                    {
                        var Zybrjbxx = getBlZybrjbxx(zyh,false);
                        //var Zybrjbxx = GetZybrjbxxByZYH(zyh);
                        //engHTML = XMLDataBind(Zybrjbxx, currentFileName);

                        //Lấy phần tử diễn biến bệnh của mẫuname
                        XTextElementList xTextElements = eng.Document.GetElementsByName("emr_bcrq");
                        if (xTextElements.Count > 0)
                        {
                            XTextInputFieldElement xTextInput = xTextElements[0] as XTextInputFieldElement;
                            //Khi tạo mới mẫu diễn biến bệnh，Sử dụng ngày hiện tại
                            xTextInput.InnerText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        XMLDataBind(Zybrjbxx, eng);
                        // eng.Document.SetDocumentParameterValue("zy_brjbxx", Zybrjbxx);
                    }
                    //eng.Document.WriteDataFromDataSourceToDocument();// .UpdateViewForDataSource();               
                }
                else
                {
                    eng.Options.ContentRenderMode = WebWriterControlRenderMode.PagePreviewHtml;
                    eng.LoadDocument(currentFileName, null);
                }
            }
            else
            {
                string sd = this.Request.Form["saveDocument"];
                bool loaded = false;
                if ((sd == "Lưu tài liệu" || sd == "Lưu tài liệu" || sd == "Tải lên bảo hiểm y tế" || sd == "Tải lên bảo hiểm y tế") && eng.Options.ContentRenderMode == WebWriterControlRenderMode.NormalHtmlEditable)
                {
                    try
                    {
                        // Người dùng đã nhấn“Lưu tài liệu”Nút，
                        // thì thửWEBtải tài liệu từ yêu cầu rồi lưu
                        //loaded = eng.LoadDocumentFromRequestFormData();
                        //Trình soạn thảo bệnh án 5.0
                        loaded = string.IsNullOrWhiteSpace(submitXml) ? false : true;

                        if (loaded)
                        {
                            string DataSource = "";
                            var mbety = _bl_mblbRepo.FindEntity(mbbh);
                            if (mbety != null && mbety.EnableDataLoad == "1" && !string.IsNullOrWhiteSpace(mbety.DataSource))
                            {
                                DataSource = mbety.DataSource;
                            }
                            string path = BlFilePath + DateTime.Now.ToString("yyyy/MM/dd/") + ywid + "/";//win10máy tính có vấn đề
                            path = $"{BlFilePath}{DateTime.Now.ToString("yyyy")}/{DateTime.Now.ToString("MM")}/{DateTime.Now.ToString("dd")}/{ywid}/";
                            IsExistDir(path);
                            string BLMC = DateTime.Now.ToString("ddHHmm") + mb.mbmc.Replace("模板", "");
                            BLMC = BLMC.Trim();
                            byte[] decodedBytes = Convert.FromBase64String(submitXml);
                            string xmlConten = Encoding.UTF8.GetString(decodedBytes);
                            //var xmlConten1 = eng.Document.InnerText;
                            currentFileName = WebPath.Map(path);
                            System.IO.File.WriteAllText(currentFileName + BLMC + ".xml", xmlConten);
                            //eng.SaveDocument(currentFileName + BLMC + ".xml", null);
                            //eng.LoadDocument(currentFileName + BLMC + ".xml", null);
                            string BLID = "";
                            if (!string.IsNullOrWhiteSpace(mzh))
                            {
                                BLID = _medicalRecordDmnService.BL_Save(OrganizeId, mb.bllx, mbbh, path, BLMC, this.UserIdentity, null, mzh,".xml");
                            }
                            else
                            {
                                BLID = BL_Save(mb.bllx, mbbh, zyh, path, BLMC, DataSource, xmlConten,".xml");
                                eng.LoadDocument(currentFileName + BLMC + ".xml", null);
                                XTextElementList xTextElements = eng.Document.GetAllElements();
                                List<XTextElement> xTextElements_input = xTextElements.FindAll(n => n.GetType() == typeof(XTextInputFieldElement));
                                _medicalRecordDmnService.BLJG_Delete(BLID, OrganizeId);
                                List<XTextInputFieldElement> dd = new List<XTextInputFieldElement>();
                                foreach (var item in xTextElements_input)
                                {
                                    XTextInputFieldElement res = (XTextInputFieldElement)item;
                                    dd.Add(res);
                                }
                                List<byysjg> codeList = new List<byysjg>();
                                foreach (var item in dd)
                                {
                                    byysjg jg = new byysjg();
                                    jg.ysid = item.DisplayName;
                                    jg.ysvalue = item.Text;
                                    codeList.Add(jg);
                                }
                                List<XTextInputFieldElement> ss = new List<XTextInputFieldElement>();

                                foreach (var item in dd)
                                {
                                    if (!ss.Exists(p => p.DisplayName == item.DisplayName))
                                    {
                                        var jj = codeList.Where(p => p.ysid == item.DisplayName).GroupBy(i => new { i.ysid, i.ysvalue }).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).ToList().First();
                                        var distinct = dd.Where(p => p.Text == jj.ysvalue && p.DisplayName == jj.ysid).ToList().First();
                                        ss.Add(distinct);
                                    }
                                }
                                List<bl_ysjgnrEntity> bljgnrEntity = new List<bl_ysjgnrEntity>();
                                foreach (var res in ss)
                                {
                                    if (res.Name == null)
                                    {
                                        continue;
                                    }
                                    bl_ysjgnrEntity jget = new bl_ysjgnrEntity();
                                    jget.blid = BLID;
                                    jget.zyh = zyh;
                                    jget.bllx = mb.bllx;
                                    jget.yscode = res.Name;
                                    jget.ysvalue = res.Text;
                                    jget.ystext = res.InnerValue;
                                    jget.ysmc = res.BackgroundText;
                                    jget.ysid = res.DisplayName;
                                    jget.OrganizeId = this.OrganizeId;
                                    bljgnrEntity.Add(jget);
                                }
                                if(bljgnrEntity.Count>0)
                                    _medicalRecordDmnService.BLJG_Save(bljgnrEntity);
                                if (mb.bllx == "5")
                                {
                                    BabasyVO basy = GetBasyInfo(eng);//(BabasyVO)eng.Document.GetParameterValue("ba_basy");
                                    if (basy != null)
                                    {
                                        basy.zyh = zyh;
                                        basy.zid = BLID;
                                        basy.OrganizeId = OrganizeId;
                                        basy.CreatorCode = this.UserIdentity.UserCode;
                                        _BabasyRepo.SubmitForm(basy, "");
                                    }
                                }
                            }
                            BljghReq bljshentity = new BljghReq();
                            bljshentity.bllx = mb.bllx;
                            bljshentity.blmc = BLMC;
                            bljshentity.czr = this.UserIdentity.UserCode;
                            bljshentity.blId = BLID;
                            bljshentity.zyh = zyh;
                            bljshentity.organizeId = this.OrganizeId;
                            _bljsRepo.BljghDataDealwith(bljshentity);
                            //if (!string.IsNullOrWhiteSpace(mb.Ybbm))
                            //{
                            //    errormsg = FieldDataSave(eng.Document.Fields, mb.Ybbm, BLID, sd);
                            //}
                            if (!string.IsNullOrWhiteSpace(errormsg))
                            {
                                return RedirectToAction("MedicalRecordEdit", new { blid = BLID, bllx = mb.bllx, zyh = zyh, mzh = mzh, message = errormsg });
                            }
                            else
                            {
                                return RedirectToAction("PreView", new { blid = BLID, bllx = mb.bllx, zyh = zyh, mzh = mzh, message = errormsg });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        errormsg = ex.Message;
                    }

                }

                if (sd == "保存为pdf" && eng.Options.ContentRenderMode == WebWriterControlRenderMode.NormalHtmlEditable)
                {
                    // Người dùng đã nhấn“Lưu tài liệu”Nút，
                    // thì thửWEBtải tài liệu từ yêu cầu rồi lưu
                    loaded = eng.LoadDocumentFromRequestFormData();
                    if (loaded)
                    {
                        string path = BlFilePath + DateTime.Now.ToString("yyyy/MM/dd/") + ywid + "/";
                        IsExistDir(path);
                        string BLMC = ywid + bllxmc;

                        currentFileName = WebPath.Map(path);
                        eng.SaveDocument(currentFileName + BLMC + ".pdf", "pdf");
                        eng.LoadDocument(currentFileName + BLMC + ".pdf", "pdf");
                    }
                }

                if (sd == "保存为html" && eng.Options.ContentRenderMode == WebWriterControlRenderMode.NormalHtmlEditable)
                {
                    // Người dùng đã nhấn“Lưu tài liệu”Nút，
                    // thì thửWEBtải tài liệu từ yêu cầu rồi lưu
                    loaded = eng.LoadDocumentFromRequestFormData();
                    if (loaded)
                    {
                        string path = BlFilePath + DateTime.Now.ToString("yyyy/MM/dd/") + ywid + "/";
                        IsExistDir(path);
                        string BLMC = ywid + bllxmc;
                        currentFileName = WebPath.Map(path);
                        eng.SaveDocument(currentFileName + BLMC + ".html", "html");
                        eng.LoadDocument(currentFileName + BLMC + ".html", "html");
                    }
                }
            }
            ViewBag.mbbh = mbbh;
            ViewBag.zyh = zyh;
            ViewBag.mzh = mzh;
            //ViewBag.WriterControlHtml = eng.GetAllContentHtml();
            //string oldstr = "span style=&quot;color:black;font-size:9pt;background-color:white";
            //string htmlstr = eng.GetAllContentHtml();
            //string tip = "style=\"position:relative;left:4px;top:1px";
            //ViewBag.WriterControlHtml = eng.GetAllContentHtml();
            byte[] bytes = Encoding.UTF8.GetBytes(eng.Document.InnerXML);
            ViewBag.WriterControlXmlData = Convert.ToBase64String(bytes);
            ViewBag.message = errormsg;
            ViewBag.userId = this.UserIdentity.UserCode;
            ViewBag.userName = this.UserIdentity.UserName;
            eng.Dispose();
            return View();
        }

        /// <summary>
        /// Theo bệnh ánIDC hỉnh sửa ID--Sửa bệnh án
        /// </summary>
        /// <param name="blid"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult MedicalRecordEdit(string blid, string bllx, string zyh, string message = null, string mzh = null,string submitXml=null)
        {
            //Nếu không tìm thấy xml để tải thì mặc định là xml trống
            string currentFileName = WebPath.Map(BlTemplatePath + "newFile.xml");
            string bllj = "";
            string blxtmc_yj = "";
            string errormsg = "";
            string mbId = "";
            int isupload = 0;

            //Sao lưu lịch sử
            string nameblxtmc = "";
            string lujing = "";
            string Getaddress = WebPath.Map(BlBackupFilePath);

            // Tải tệp
            WebWriterControlEngine eng;
            if (engine == null)
            {
                eng = GetControlEngine();
            }
            else
            {
                eng = engine;
            }
            if (Request.Method == "GET")
            {
                if (!string.IsNullOrWhiteSpace(bllx))
                {
                    medicalRecordVO mr = new medicalRecordVO();
                    if (!string.IsNullOrWhiteSpace(mzh))
                    {
                        mr = _medicalRecordDmnService.GetMedicalRecordbyId(blid, bllx);
                    }
                    else
                    {
                        mr = _medicalRecordDmnService.GetMedicalRecord(blid, bllx);
                    }
                    bllj = mr.blxtml;
                    blxtmc_yj = mr.blxtmc_yj;

                    //Tên tệp sao lưu
                    nameblxtmc = DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + zyh + "-" + mr.blxtmc_yj;
                    lujing = mr.blxtml;

                    if (mr.IsLock == 1 && mr.LastModifierCode != this.UserIdentity.UserCode)
                    {
                        return RedirectToAction("PreView", new { blid = blid, bllx = bllx, zyh = zyh, mzh = mzh });
                    }

                    currentFileName = WebPath.Map(bllj + blxtmc_yj.Trim() + ".xml");
                    if (System.IO.File.Exists(currentFileName) == false)//Nếu không tồn tại thì mở mẫu mặc định
                    {
                        currentFileName = WebPath.Map(BlTemplatePath + "newFile.xml");
                    }

                    // Tải mẫu
                    eng.LoadDocument(currentFileName, null);
                    LockRecord(blid, bllx, 1);
                }
                else
                {
                    eng.LoadDocument(currentFileName, null);
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(blid))
                {
                    blid = this.Request.Form["blid"];
                }
                if (string.IsNullOrWhiteSpace(zyh))
                {
                    zyh = this.Request.Form["zyh"];
                }
                if (string.IsNullOrWhiteSpace(mzh))
                {
                    mzh = this.Request.Form["mzh"];
                }

                blid = blid.Trim();
                zyh = zyh.Trim();
                mzh = mzh.Trim();
                bllx = this.Request.Form["bllx"].ToString().Trim();//== null ? bllx : Convert.ToInt32(this.Request.Form["bllx"]);  
                if (string.IsNullOrWhiteSpace(bllj))
                {
                    bllj = this.Request.Form["bllj"];
                }
                if (string.IsNullOrWhiteSpace(blxtmc_yj))
                {
                    blxtmc_yj = this.Request.Form["blxtmc_yj"].ToString().Trim();
                }
                string sd = this.Request.Form["saveDocument"];
                bool loaded = false;

                string blxtmc_hj = blxtmc_yj;
                currentFileName = WebPath.Map(bllj);
                if ((sd == "Lưu tài liệu" || sd == "Lưu tài liệu" || sd == "Tải lên bảo hiểm y tế" || sd == "Tải lên bảo hiểm y tế") && eng.Options.ContentRenderMode == WebWriterControlRenderMode.NormalHtmlEditable)
                {

                    // Người dùng đã nhấn“Lưu tài liệu”Nút，
                    // thì thửWEBtải tài liệu từ yêu cầu rồi lưu
                    try {
                        //loaded = eng.LoadDocumentFromRequestFormData();
                        //Trình soạn thảo bệnh án 5.0
                        loaded = string.IsNullOrWhiteSpace(submitXml) ? false : true;
                    }
                    catch (Exception er) {
                    }
                    if (loaded)
                    {
                        byte[] decodedBytes = Convert.FromBase64String(submitXml);
                        string xmldata = Encoding.UTF8.GetString(decodedBytes);
                        System.IO.File.WriteAllText(currentFileName + blxtmc_hj.Trim() + ".xml", xmldata);

                        //eng.SaveDocument(currentFileName + blxtmc_hj.Trim() + ".xml", null);
                        //eng.LoadDocument(currentFileName + blxtmc_hj.Trim() + ".xml", null);
                        var medicalRecord = new medicalRecordVO();
                        medicalRecord.ID = blid;
                        medicalRecord.bllx = bllx;
                        medicalRecord.blxtmc_hj = blxtmc_hj;
                        medicalRecord.blxtmc_yj = blxtmc_hj;
                        medicalRecord.LastModifierCode = this.UserIdentity.UserCode;
                        if (!string.IsNullOrWhiteSpace(mzh))
                        {
                            _medicalRecordDmnService.MedicalRecordSaveMz(medicalRecord, null);
                        }
                        else
                        {
                            _medicalRecordDmnService.MedicalRecordSave(medicalRecord, null);
                            if (bllx == "5")
                            {
                                BabasyVO basy = GetBasyInfo(eng);
                                if (basy != null)
                                {
                                    basy.zyh = zyh;
                                    _BabasyRepo.SubmitForm(basy, blid);
                                }
                            }
                            var xmlConten = xmldata;//eng.Document.InnerText;
                            var relEty = _ZymeddocsrelationRepo.FindEntity(p => p.blId == blid && p.OrganizeId == this.OrganizeId && p.zyh == zyh);
                            relEty.LastModifyTime = DateTime.Now;
                            relEty.LastModifierCode = this.UserIdentity.rygh;
                            relEty.OldXmlConten = relEty.XmlConten;
                            relEty.XmlConten = xmlConten;
                            eng.LoadDocument(currentFileName + blxtmc_hj.Trim() + ".xml", null);
                            XTextElementList xTextElements = eng.Document.GetAllElements();
                            List<XTextElement> xTextElements_input = xTextElements.FindAll(n => n.GetType() == typeof(XTextInputFieldElement));
                            _medicalRecordDmnService.BLJG_Delete(blid, OrganizeId);
                            List<XTextInputFieldElement> dd = new List<XTextInputFieldElement>();
                            foreach (var item in xTextElements_input)
                            {
                                XTextInputFieldElement res = (XTextInputFieldElement)item;
                                dd.Add(res);
                            }
                            List<byysjg> codeList = new List<byysjg>();
                            foreach (var item in dd)
                            {
                                byysjg jg = new byysjg();
                                jg.ysid = item.DisplayName;
                                jg.ysvalue = item.Text;
                                codeList.Add(jg);
                            }
                            List<XTextInputFieldElement> ss = new List<XTextInputFieldElement>();

                            foreach (var item in dd)
                            {
                                if (!ss.Exists(p => p.DisplayName == item.DisplayName))
                                {
                                    var jj = codeList.Where(p => p.ysid == item.DisplayName).GroupBy(i => new { i.ysid, i.ysvalue }).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).ToList().First();
                                    var distinct = dd.Where(p => p.Text == jj.ysvalue && p.DisplayName == jj.ysid).ToList().First();
                                    ss.Add(distinct);
                                }
                            }
                            List<bl_ysjgnrEntity> bljgnrEntity = new List<bl_ysjgnrEntity>();
                            foreach (var res in ss)
                            {
                                if (res.Name == null)
                                {
                                    continue;
                                }
                                bl_ysjgnrEntity jget = new bl_ysjgnrEntity();
                                jget.blid = blid;
                                jget.zyh = zyh;
                                jget.bllx = bllx;
                                jget.yscode = res.Name;
                                jget.ystext = res.TextValue;
                                jget.ysvalue = res.Text;
                                jget.ysmc = res.BackgroundText;
                                jget.ysid = res.DisplayName;
                                jget.OrganizeId = this.OrganizeId;
                                bljgnrEntity.Add(jget);
                            }
                            if(bljgnrEntity.Count>0)
                                _medicalRecordDmnService.BLJG_Save(bljgnrEntity);
                            _ZymeddocsrelationRepo.Update(relEty);
                            mbId = relEty.mbId;

                            BljghReq bljshentity = new BljghReq();
                            bljshentity.bllx = bllx;
                            bljshentity.blmc = blxtmc_hj;
                            bljshentity.czr = this.UserIdentity.UserCode;
                            bljshentity.blId = blid;
                            bljshentity.zyh = zyh;
                            bljshentity.organizeId = this.OrganizeId;
                            _bljsRepo.BljghDataDealwith(bljshentity);
                            #region Tải lên bảo hiểm y tế
                            //if (sd == "Tải lên bảo hiểm y tế")
                            //{
                                string jydm = "";
                                if (sd == "Tải lên bảo hiểm y tế" || sd == "Tải lên bảo hiểm y tế")
                                {
                                    isupload = 1;
                                }
                                var blmb = _bl_mblbRepo.FindEntity(p => p.Id == relEty.mbId && p.zt == "1" && p.OrganizeId == OrganizeId);
                                if (blmb != null && !string.IsNullOrWhiteSpace(blmb.Ybbm))
                                {
                                    jydm = blmb.Ybbm;
                                    errormsg = FieldDataSaveJS(eng.Document.Fields, blmb.Ybbm, blid, isupload);
                                }

                                if (isupload == 1)
                                {
                                    return RedirectToAction("MedicalRecordEdit", new { blid = blid, bllx = bllx, zyh = zyh, message = errormsg, isupload = isupload, jydm = jydm });
                                }
                                else
                                {
                                    return RedirectToAction("PreView", new { blid = blid, bllx = bllx, zyh = zyh, message = errormsg, isupload = isupload, jydm = jydm });
                                }
                           // }
                            #endregion
                        }
                    }

                    return RedirectToAction("PreView", new { blid = blid, bllx = bllx, zyh = zyh, mzh = mzh });
                }

                if (sd == "保存为pdf" && eng.Options.ContentRenderMode == WebWriterControlRenderMode.NormalHtmlEditable)
                {
                    // Người dùng đã nhấn“Lưu tài liệu”Nút，
                    // thì thửWEBtải tài liệu từ yêu cầu rồi lưu
                    loaded = eng.LoadDocumentFromRequestFormData();
                    if (loaded)
                    {
                        eng.SaveDocument(currentFileName + blxtmc_hj.Trim() + ".pdf", "pdf");
                        eng.LoadDocument(currentFileName + blxtmc_hj.Trim() + ".pdf", "pdf");
                    }
                }
                if (sd == "保存为html" && eng.Options.ContentRenderMode == WebWriterControlRenderMode.NormalHtmlEditable)
                {
                    // Người dùng đã nhấn“Lưu tài liệu”Nút，
                    // thì thửWEBtải tài liệu từ yêu cầu rồi lưu
                    loaded = eng.LoadDocumentFromRequestFormData();
                    if (loaded)
                    {
                        eng.SaveDocument(currentFileName + blxtmc_hj.Trim() + ".html", "html");
                        eng.LoadDocument(currentFileName + blxtmc_hj.Trim() + ".html", "html");
                    }
                }
                //Tìm và tải tệp sao lưu
                if (sd == "history")
                {
                    if (!string.IsNullOrWhiteSpace(this.Request.Form["blid"]) && this.Request.Form["blid"].Contains("|"))
                    {
                        nameblxtmc = this.Request.Form["blid"].ToString().Split('|')[0];
                        blid = this.Request.Form["blid"].ToString().Split('|')[1];
                        //Tìm bản sao lưu
                        string backuplj = WebPath.Map(BlBackupFilePath + zyh + "/" + nameblxtmc.Trim() + ".xml");
                        if (System.IO.File.Exists(backuplj) == false)//Nếu không tồn tại thì mở mẫu mặc định
                        {
                            backuplj = WebPath.Map(BlTemplatePath + "newFile.xml");
                        }
                        // Tải mẫu
                        eng.LoadDocument(backuplj, null);
                    }
                    else
                    {
                        message = "Bản sao lưu lịch sử không hợp lệ";
                    }

                }
            }
            ViewBag.bllj = bllj;
            ViewBag.blxtmc_yj = blxtmc_yj;
            ViewBag.blid = blid;
            ViewBag.bllx = bllx;
            ViewBag.zyh = zyh;
            ViewBag.mzh = mzh;
            ViewBag.blxtmc_yj = blxtmc_yj;
            //ViewBag.userid = this.UserIdentity.UserId;
            //ViewBag.username = this.UserIdentity.UserName;//Không được gán lại người dùng, sẽ khiến lưu văn bản điều dưỡng thất bại
            //string oldstr = "span style=&quot;color:black;font-size:9pt;background-color:white";
            //string tip = "style=\"position:relative;left:4px;top:1px";
            eng.Options.LogUserEditTrack = true;
            eng.Options.CurrentUserID = this.UserIdentity.UserCode;
            eng.Options.CurrentUserName = this.UserIdentity.UserName;
            eng.Options.CurrentUserPermissionLevel = 0;
            eng.Options.ClientMachineName = "123";
            eng.Options.ExcludeLogicDeleted = true;
            eng.Options.OutputOriginUserTrackInfos = true; //Lấy danh sách dấu vết
            eng.DocumentOptions.SecurityOptions.EnablePermission = true;
            eng.DocumentOptions.SecurityOptions.EnableLogicDelete = true;
            eng.DocumentOptions.SecurityOptions.ShowLogicDeletedContent = false;
            eng.DocumentOptions.SecurityOptions.ShowPermissionMark = false;
            eng.DocumentOptions.SecurityOptions.ShowPermissionTip = false;
            eng.DocumentOptions.SecurityOptions.TrackVisibleLevel0.DeleteLineNum = 2;
            eng.DocumentOptions.SecurityOptions.TrackVisibleLevel0.DeleteLineColorString = "Black";
            eng.DocumentOptions.SecurityOptions.TrackVisibleLevel0.UnderLineColorString = "Yellow";
            eng.DocumentOptions.SecurityOptions.TrackVisibleLevel0.UnderLineColorNum = 2;
            eng.DocumentOptions.SecurityOptions.TrackVisibleLevel0.BackgroundColorString = "LightGrey";
            //ViewBag.WriterControlHtml = eng.GetAllContentHtml();
            byte[] bytes = Encoding.UTF8.GetBytes(eng.Document.InnerXML);
            ViewBag.WriterControlXmlData = Convert.ToBase64String(bytes);
            ViewBag.user = this.UserIdentity.UserCode;
            ViewBag.userName = this.UserIdentity.UserName;
            if (!string.IsNullOrWhiteSpace(nameblxtmc.Trim()))
            {
                //Phán đoán đường dẫn có tồn tại hay không，Tạo thư mục
                string path = Getaddress + zyh;
                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }
                path = path + "\\";
                //Lưu địa chỉ tệp sao lưu
                eng.SaveDocument(path + nameblxtmc.Trim() + ".xml", null);
            }

            eng.Dispose();
            ViewBag.message = message;

            return View();
        }
        /// <summary>
        /// Khóa văn bản bệnh án- Mở khóa，không nhiều người được viết cùng một văn bản
        /// </summary>
        /// <param name="blid"></param>
        /// <param name="bllx"></param>
        /// <param name="isLock">1 Yêu cầu chỉnh sửa bệnh án2 Yêu cầu mở khóa bệnh án</param>
        /// <returns></returns>
        public IActionResult LockRecord(string blid, string bllx, int isToLock)
        {
            medicalRecordVO MedicalRecord = new medicalRecordVO();
            if (IsEnableWebEditor == "true" && bllx == "5")
            {
                var basy = _MrbasyRepo.FindEntity(p => p.Id == blid && p.OrganizeId == OrganizeId);
                if (basy != null)
                {
                    MedicalRecord.IsLock = basy.IsLock;
                    MedicalRecord.LastModifierCode = basy.LastModifierCode == null ? basy.CreatorCode : basy.LastModifierCode;
                }
            }
            else
            {
                MedicalRecord = _medicalRecordDmnService.GetMedicalRecord(blid, bllx);
            }
            if (isToLock == 1)
            {
                if (MedicalRecord.IsLock != 1)
                {
                    _medicalRecordDmnService.LockRecord(blid, bllx, OrganizeId, this.UserIdentity.UserCode, isToLock);
                }
            }
            else
            {
                if (MedicalRecord.IsLock == 1)
                {
                    if (MedicalRecord.LastModifierCode == this.UserIdentity.UserCode)
                    {
                        _medicalRecordDmnService.LockRecord(blid, bllx, OrganizeId, this.UserIdentity.UserCode, isToLock);
                        return Success("Mở khóa thành công");
                    }
                    else
                    {
                        throw new FailedException("Mở khóa thất bại, vui lòng nhờ " + MedicalRecord.LastModifierCode + " mở khóa");
                    }
                }
                else
                {
                    return Success("Chưa bị khóa");
                }

            }
            return Success("Mở khóa thành công");
        }
        /// <summary>
        /// Xem trước văn bản bệnh án
        /// </summary>
        /// <param name="jzbl"></param>
        /// <param name="blid"></param>
        /// <param name="bllx"></param>
        /// <param name="blid2"></param>
        /// <param name="bllx2"></param>
        /// <param name="blxtmc_yj"></param>
        /// <param name="zyh"></param>
        /// <param name="message"></param>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public IActionResult PreView(string jzbl, string blid, string bllx, string blid2, string bllx2, string blxtmc_yj, string zyh, string message, string mzh = null)
        {

            WebWriterControlEngine eng;
            if (jzbl != "" && jzbl != null)
            {
                if (engine == null)
                {
                    eng = GetControlEngine();
                }
                else
                {
                    eng = engine;
                }
                ViewBag.mbqx = (int)EnummbqxFp.edit;
                string Getaddress = WebPath.Map("~/File/BackupsBL/" + zyh + "/" + blid + ".xml");
                eng.LoadDocument(Getaddress, null);
                eng.Options.ContentRenderMode = WebWriterControlRenderMode.PagePreviewHtml;
                //ViewBag.WriterControlHtml = eng.GetAllContentHtml();
                byte[] bytes = Encoding.UTF8.GetBytes(eng.Document.InnerXML);
                ViewBag.WriterControlXmlData = Convert.ToBase64String(bytes);
                eng.Dispose();
                ViewBag.message = message;
                ViewBag.blid = blid2;
                ViewBag.bllx = bllx2;
                ViewBag.zyh = zyh;
                ViewBag.mzh = mzh;
                ViewBag.blxtmc_yj = blxtmc_yj;
                ViewBag.ContentRenderMode = eng.Options.ContentRenderMode;
            }
            else
            {
                //Nếu không tìm thấy xml để tải thì mặc định là xml trống
                ViewBag.blid = blid;
                ViewBag.bllx = bllx;
                ViewBag.zyh = zyh;
                ViewBag.mzh = mzh;
                ViewBag.mbqx = (int)EnummbqxFp.non;
                string currentFileName = WebPath.Map(BlTemplatePath + "newFile.xml");
                string bllj = "";
                blxtmc_yj = "";
                // Tải tệp
                //WebWriterControlEngine eng ;
                if (engine == null)
                {
                    eng = GetControlEngine();
                }
                else
                {
                    eng = engine;
                }
                medicalRecordVO mr = new medicalRecordVO();
                if (!string.IsNullOrWhiteSpace(mzh))
                {
                    mr = _medicalRecordDmnService.GetMedicalRecordbyId(blid, bllx);
                }
                else
                {
                    mr = _medicalRecordDmnService.GetMedicalRecord(blid, bllx);
                }
                bllj = mr.blxtml;
                blxtmc_yj = mr.blxtmc_yj;

                ViewBag.mbqx = (int)EnummbqxFp.non;
                var qx = _blmblbDmnService.Getqxkz(this.UserIdentity.StaffId, mr.mbbh, bllx);
                if (qx != null && qx.Count > 0)
                {
                    ViewBag.mbqx = qx.FirstOrDefault().ctrlLevel;
                }
                if (string.IsNullOrWhiteSpace(mr.blxtmc_yj) && !string.IsNullOrWhiteSpace(mzh))
                {
                    blxtmc_yj = _mzmeddocsrelationRepo.FindEntity(p => p.blId == blid).blmc;
                }
                else if (string.IsNullOrWhiteSpace(mr.blxtmc_yj))
                {
                    blxtmc_yj = _ZymeddocsrelationRepo.FindEntity(p => p.blId == blid).blmc;
                }

                int isLock = Convert.ToInt32(mr.IsLock);
                ViewBag.isLocker = "";
                if (isLock == 1)
                {
                    if (mr.LastModifierCode != this.UserIdentity.UserCode)
                    {
                        ViewBag.isLocker = mr.LastModifierCode + " đang sửa";
                    }
                    else
                    {
                        LockRecord(blid, bllx, 2);
                    }
                }
                currentFileName = WebPath.Map(bllj + blxtmc_yj.Trim() + ".xml");
                if (System.IO.File.Exists(currentFileName) == false)//Nếu không tồn tại thì mở mẫu mặc định
                {
                    currentFileName = WebPath.Map(BlTemplatePath + "newFile.xml");
                }
                // Tải mẫu
                eng.LoadDocument(currentFileName, null);
                eng.Options.ContentRenderMode = WebWriterControlRenderMode.PagePreviewHtml;

                //string oldstr = "span style=&quot;color:black;font-size:9pt;background-color:white";
                //string tip = "style=\"position:relative;left:4px;top:1px";
                eng.Options.LogUserEditTrack = true;
                eng.Options.CurrentUserID = this.UserIdentity.UserCode;
                eng.Options.CurrentUserName = this.UserIdentity.UserName;
                eng.Options.CurrentUserPermissionLevel = 0;
                eng.Options.ClientMachineName = "123";
                eng.Options.ExcludeLogicDeleted = true;
                eng.DocumentOptions.SecurityOptions.EnablePermission = true;
                eng.DocumentOptions.SecurityOptions.EnableLogicDelete = true;
                eng.DocumentOptions.SecurityOptions.ShowLogicDeletedContent = false;
                eng.DocumentOptions.SecurityOptions.ShowPermissionMark = false;
                eng.DocumentOptions.SecurityOptions.ShowPermissionTip = false;
                eng.DocumentOptions.SecurityOptions.TrackVisibleLevel0.DeleteLineNum = 2;
                eng.DocumentOptions.SecurityOptions.TrackVisibleLevel0.DeleteLineColorString = "Black";
                eng.DocumentOptions.SecurityOptions.TrackVisibleLevel0.UnderLineColorString = "Yellow";
                eng.DocumentOptions.SecurityOptions.TrackVisibleLevel0.UnderLineColorNum = 2;
                eng.DocumentOptions.SecurityOptions.TrackVisibleLevel0.BackgroundColorString = "LightGrey";
                //ViewBag.WriterControlHtml = eng.GetAllContentHtml();
                byte[] bytes = Encoding.UTF8.GetBytes(eng.Document.InnerXML);
                ViewBag.WriterControlXmlData = Convert.ToBase64String(bytes);
                eng.Dispose();
                ViewBag.isLock = isLock;
                ViewBag.message = message;
                ViewBag.blxtmc_yj = blxtmc_yj;
                ViewBag.ContentRenderMode = eng.Options.ContentRenderMode;
            }
            return View();
        }
        public IActionResult PreViewEdit(string blid, string bllx)
        {
            //Nếu không tìm thấy xml để tải thì mặc định là xml trống
            string currentFileName = WebPath.Map(BlTemplatePath + "newFile.xml");
            string bllj = "";
            string blxtmc_yj = "";
            // Tải tệp
            WebWriterControlEngine eng;
            if (engine == null)
            {
                eng = GetControlEngine();
            }
            else
            {
                eng = engine;
            }
            var MedicalRecord = _medicalRecordDmnService.GetMedicalRecord(blid, bllx);
            bllj = MedicalRecord.blxtml;
            blxtmc_yj = MedicalRecord.blxtmc_yj;

            currentFileName = WebPath.Map(bllj + blxtmc_yj + ".xml");
            if (System.IO.File.Exists(currentFileName) == false)//Nếu không tồn tại thì mở mẫu mặc định
            {
                currentFileName = WebPath.Map(BlTemplatePath + "newFile.xml");
            }
            // Tải mẫu
            eng.LoadDocument(currentFileName, null);
            eng.Options.ContentRenderMode = WebWriterControlRenderMode.PagePreviewHtml;
            string oldstr = "span style=&quot;color:black;font-size:9pt;background-color:white";
            ViewBag.WriterControlHtml = eng.GetAllContentHtml().Replace(oldstr + "&quot;&gt; 常", oldstr + ";display:none&quot;&gt; 常").Replace(oldstr + "&quot;&gt; &amp;#x5E38", oldstr + ";display:none&quot;&gt; &amp;#x5E38");
            ViewBag.ContentRenderMode = eng.Options.ContentRenderMode;
            eng.Dispose();
            return View();
        }

        /// <summary>
        /// Khung nhìn in bệnh án
        /// </summary>
        /// <param name="jzbl"></param>
        /// <param name="blid"></param>
        /// <param name="bllx"></param>
        /// <param name="blid2"></param>
        /// <param name="bllx2"></param>
        /// <param name="blxtmc_yj"></param>
        /// <param name="zyh"></param>
        /// <param name="message"></param>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public IActionResult PrintView(string jzbl, string blid, string bllx, string blid2, string bllx2, string blxtmc_yj, string zyh, string message, string mzh = null)
        {
            WebWriterControlEngine eng;
            if (jzbl != "" && jzbl != null)
            {
                if (engine == null)
                {
                    eng = GetControlEngine();
                }
                else
                {
                    eng = engine;
                }
                ViewBag.mbqx = (int)EnummbqxFp.edit;
                string Getaddress = WebPath.Map("~/File/BackupsBL/" + zyh + "/" + blid + ".xml");
                eng.LoadDocument(Getaddress, null);
                eng.Options.ContentRenderMode = WebWriterControlRenderMode.PagePreviewHtml;
                //ViewBag.WriterControlHtml = eng.GetAllContentHtml();
                byte[] bytes = Encoding.UTF8.GetBytes(eng.Document.InnerXML);
                ViewBag.WriterControlXmlData = Convert.ToBase64String(bytes);
                eng.Dispose();
                ViewBag.message = message;
                ViewBag.blid = blid2;
                ViewBag.bllx = bllx2;
                ViewBag.zyh = zyh;
                ViewBag.mzh = mzh;
                ViewBag.blxtmc_yj = blxtmc_yj;
                ViewBag.ContentRenderMode = eng.Options.ContentRenderMode;
            }
            else
            {

                //Nếu không tìm thấy xml để tải thì mặc định là xml trống
                ViewBag.blid = blid;
                ViewBag.bllx = bllx;
                ViewBag.zyh = zyh;
                ViewBag.mzh = mzh;
                ViewBag.mbqx = (int)EnummbqxFp.non;

                string currentFileName = WebPath.Map(BlTemplatePath + "newFile.xml");
                string bllj = "";
                blxtmc_yj = "";
                // Tải tệp
                //WebWriterControlEngine eng ;
                if (engine == null)
                {
                    eng = GetControlEngine();
                }
                else
                {
                    eng = engine;
                }
                medicalRecordVO mr = new medicalRecordVO();
                if (!string.IsNullOrWhiteSpace(mzh))
                {
                    mr = _medicalRecordDmnService.GetMedicalRecordbyId(blid, bllx);
                }
                else
                {
                    mr = _medicalRecordDmnService.GetMedicalRecord(blid, bllx);
                }
                bllj = mr.blxtml;
                blxtmc_yj = mr.blxtmc_yj;

                ViewBag.mbqx = (int)EnummbqxFp.non;
                var qx = _blmblbDmnService.Getqxkz(this.UserIdentity.StaffId, mr.mbbh, bllx);
                if (qx != null && qx.Count > 0)
                {
                    ViewBag.mbqx = qx.FirstOrDefault().ctrlLevel;
                }
                if (string.IsNullOrWhiteSpace(mr.blxtmc_yj) && !string.IsNullOrWhiteSpace(mzh))
                {
                    blxtmc_yj = _mzmeddocsrelationRepo.FindEntity(p => p.blId == blid).blmc;
                }
                else if (string.IsNullOrWhiteSpace(mr.blxtmc_yj))
                {
                    blxtmc_yj = _ZymeddocsrelationRepo.FindEntity(p => p.blId == blid).blmc;
                }

                int isLock = Convert.ToInt32(mr.IsLock);
                ViewBag.isLocker = "";
                if (isLock == 1)
                {
                    if (mr.LastModifierCode != this.UserIdentity.UserCode)
                    {
                        ViewBag.isLocker = mr.LastModifierCode + " đang sửa";
                    }
                    else
                    {
                        LockRecord(blid, bllx, 2);
                    }
                }
                currentFileName = WebPath.Map(bllj + blxtmc_yj.Trim() + ".xml");
                if (System.IO.File.Exists(currentFileName) == false)//Nếu không tồn tại thì mở mẫu mặc định
                {
                    currentFileName = WebPath.Map(BlTemplatePath + "newFile.xml");
                }
                // Tải mẫu
                eng.LoadDocument(currentFileName, null);
                eng.Options.ContentRenderMode = WebWriterControlRenderMode.PagePreviewHtml;

                //string oldstr = "span style=&quot;color:black;font-size:9pt;background-color:white";
                //string tip = "style=\"position:relative;left:4px;top:1px";
                eng.Options.LogUserEditTrack = true;
                eng.Options.CurrentUserID = this.UserIdentity.UserCode;
                eng.Options.CurrentUserName = this.UserIdentity.UserName;
                eng.Options.CurrentUserPermissionLevel = 0;
                eng.Options.ClientMachineName = "123";
                eng.Options.ExcludeLogicDeleted = true;
                eng.DocumentOptions.SecurityOptions.EnablePermission = true;
                eng.DocumentOptions.SecurityOptions.EnableLogicDelete = true;
                eng.DocumentOptions.SecurityOptions.ShowLogicDeletedContent = false;
                eng.DocumentOptions.SecurityOptions.ShowPermissionMark = false;
                eng.DocumentOptions.SecurityOptions.ShowPermissionTip = false;
                eng.DocumentOptions.SecurityOptions.TrackVisibleLevel0.DeleteLineNum = 2;
                eng.DocumentOptions.SecurityOptions.TrackVisibleLevel0.DeleteLineColorString = "Black";
                eng.DocumentOptions.SecurityOptions.TrackVisibleLevel0.UnderLineColorString = "Yellow";
                eng.DocumentOptions.SecurityOptions.TrackVisibleLevel0.UnderLineColorNum = 2;
                eng.DocumentOptions.SecurityOptions.TrackVisibleLevel0.BackgroundColorString = "LightGrey";
                //ViewBag.WriterControlHtml = eng.GetAllContentHtml();
                byte[] bytes = Encoding.UTF8.GetBytes(eng.Document.InnerXML);
                ViewBag.WriterControlXmlData = Convert.ToBase64String(bytes);
                eng.Dispose();
                ViewBag.isLock = isLock;
                ViewBag.message = message;
                ViewBag.blxtmc_yj = blxtmc_yj;
                ViewBag.ContentRenderMode = eng.Options.ContentRenderMode;
            }
            return View();
        }

        public IActionResult RecordList(string zyh, string mzh)
        {
            ViewBag.zyh = zyh;
            ViewBag.mzh = mzh;
            return View();
        }
        /// <summary>
        /// Văn bản bệnh án lịch sửtreeV1
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public IActionResult GetWSTreeList(string zyh, string mzh)
        {
            var treeList = new List<TreeViewModel>();
            var BLLX = Enum.GetValues(typeof(EnumBllx));
            foreach (var lx in BLLX)
            {
                FieldInfo field = lx.GetType().GetField(lx.ToString());
                object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);    //Lấy thuộc tính mô tả         
                DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
                treeList.Add(new TreeViewModel()
                {

                    id = lx.GetHashCode().ToString(),
                    value = "parent",
                    text = descriptionAttribute.Description,
                    parentId = null,
                    hasChildren = true,
                    isexpand = true,
                });
            }
            if (!string.IsNullOrWhiteSpace(mzh))
            {
                var dataChildmz = _mzmeddocsrelationRepo.GetTreeList(OrganizeId, mzh);
                foreach (var c in dataChildmz)
                {
                    treeList.Add(new TreeViewModel()
                    {
                        id = c.blId,
                        value = c.bllx.ToString(),
                        text = c.blmc,
                        parentId = c.bllx.ToString(),
                        hasChildren = false,
                        isexpand = false,
                    });
                }
            }
            else
            {
                var dataChildren = _ZymeddocsrelationRepo.GetTreeList(OrganizeId, zyh);
                foreach (var child in dataChildren)
                {
                    treeList.Add(new TreeViewModel()
                    {
                        id = child.blId,
                        value = child.bllx.ToString(),
                        text = child.blmc,
                        parentId = child.bllx.ToString(),
                        hasChildren = false,
                        isexpand = false,
                    });
                }
            }

            var show = treeList.TreeViewJson(null);
            return Content(treeList.TreeViewJson(null));

        }
        /// <summary>
        /// Văn bản bệnh án lịch sửtreeV2
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public IActionResult GetWSTreeListIndex(string zyh, string mzh)
        {
            var treeList = new List<TreeViewModel>();
            IList<PatMedRecordTreeVO> list = new List<PatMedRecordTreeVO>();
            if (!string.IsNullOrWhiteSpace(mzh))
            {
                list = _outpatientDmnService.GetOutPatMedRecordTree(OrganizeId, mzh, this.UserIdentity.rygh);
            }
            else if (!string.IsNullOrWhiteSpace(zyh))
            {
                list = _ZybrjbxxDmnService.GetPatMedRecordTree(OrganizeId, mzh, this.UserIdentity.rygh);
            }

            if (list != null && list.Count > 0)
            {
                foreach (var child in list)
                {
                    bool hasChildren = list.Count(t => t.parentId == child.Id) == 0 ? false : true;
                    if (string.IsNullOrWhiteSpace(child.parentId))
                    {
                        treeList.Add(new TreeViewModel()
                        {
                            id = child.bllx,
                            value = "parent",
                            text = child.Name,
                            parentId = null,
                            hasChildren = hasChildren,
                            isexpand = true,
                        });
                    }
                    else
                    {
                        treeList.Add(new TreeViewModel()
                        {
                            id = string.IsNullOrWhiteSpace(child.BlId) ? child.bllx : child.BlId,
                            value = string.IsNullOrWhiteSpace(child.BlId) ? "parent" : child.bllx.ToString(),
                            text = child.Name,
                            parentId = string.IsNullOrWhiteSpace(child.BlId) ? child.parentbllx : child.bllx.ToString(),
                            hasChildren = hasChildren,
                            isexpand = string.IsNullOrWhiteSpace(child.BlId) ? true : false,
                        });
                    }
                }
            }
            return Content(treeList.TreeViewJson(null));
        }

        public IActionResult Doctor()
        {
            return View();
        }
        
        /// <summary>
        /// Sao lưu bản ghi bệnh án lịch sử
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="blxtmc_yj"></param>
        /// <returns></returns>
        public IActionResult GetBFTreeListIndex(string zyh,string mzh, string blxtmc_yj,string doctype=null)
        {
            if (!string.IsNullOrWhiteSpace(mzh)&&mzh!="null")
            {
                zyh = mzh.Trim();
            }
            int strlength =!string.IsNullOrWhiteSpace(doctype)? doctype.Length:4;
            DateTime limitdate = DateTime.Today;
            var treeList = new List<TreeViewModel>();
            string Getaddress = WebPath.Map(BlBackupFilePath);
            Getaddress = Getaddress + zyh;
            DirectoryInfo dir = new DirectoryInfo(Getaddress);
            if (!dir.Exists)
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(KeepBackupFileDays))
            {
                KeepBackupFileDays = "0"; //Mặc định chỉ giữ lại trong ngày
            }
            else
            {
                limitdate = DateTime.Today.AddDays(-KeepBackupFileDays.ToInt());
            }
            FileInfo[] finfo = dir.GetFiles();
            List<BackuptimetreeVO> listtree = new List<BackuptimetreeVO>();

            Array.Sort<FileInfo>(finfo, delegate (FileInfo x, FileInfo y) { return y.Name.CompareTo(x.Name); });
            for (int i = 0; i < finfo.Length; i++)
            {
                BackuptimetreeVO treelist = new BackuptimetreeVO();
                string str = finfo[i].Name.Substring(0, finfo[i].Name.Length - strlength);
                string[] sArray = str.Split(new char[2] { 'j', '-' });

                if (sArray != null && sArray.Length >= 3 && sArray[2] == blxtmc_yj)
                {
                    string sj = sArray[0].Substring(0, 4) + "-" + sArray[0].Substring(4, 2) + "-" + sArray[0].Substring(6, 2);

                    if (sj.ToDate() <= limitdate)
                    {
                        try
                        {
                            System.IO.File.Delete(Getaddress + str);
                        }
                        catch
                        {

                        }
                    }
                    else
                    {
                        treelist.id = str;
                        treelist.name = "【" + sArray[0].Substring(8, 2) + ":" + sArray[0].Substring(10, 2) + "】" + sArray[2].Trim();
                        treelist.parentid = sArray[0].Substring(0, 8);
                        listtree.Add(treelist);
                    }
                }
            }

            var patlist = listtree.GroupBy(p => new { id = p.parentid });
            if (patlist.Count() > 0)
            {
                foreach (var item in patlist)
                {
                    TreeViewModel treepat = new TreeViewModel();
                    treepat.id = item.Key.id;
                    treepat.text = item.Key.id;
                    treepat.value = item.Key.id;
                    treepat.parentId = null;
                    treepat.isexpand = false;
                    treepat.complete = true;
                    treepat.hasChildren = true;
                    treepat.Ex1 = "p";
                    treeList.Add(treepat);
                }
            }

            foreach (BackuptimetreeVO itempat in listtree)
            {
                TreeViewModel treepat = new TreeViewModel();
                treepat.id = itempat.id;
                treepat.text = itempat.name;
                treepat.value = itempat.parentid;
                treepat.parentId = itempat.parentid;
                treepat.isexpand = false;
                treepat.complete = true;
                treepat.hasChildren = false;
                treepat.Ex1 = "c";
                treeList.Add(treepat);
            }

            ViewBag.blxtmc_yj = blxtmc_yj;
            var a = treeList.TreeViewJson(null);
            return Content(treeList.TreeViewJson(null));
        }
        
        /// <summary>
        /// Thao tác bảng tương ứng khi lưu bệnh án，Lưu trữ dữ liệu
        /// </summary>
        /// <param name="bllx"></param>
        /// <param name="mbbh"></param>
        /// <param name="zyh"></param>
        /// <param name="path"></param>
        /// <param name="BLMC"></param>
        /// <param name="DataSource"></param>
        /// <param name="xmlConten"></param>
        /// <returns></returns>
        public string BL_Save(string bllx, string mbbh, string zyh, string path, string BLMC, string DataSource = null, string xmlConten = null,string doctype=null)
        {
            var medicalRecord = new medicalRecordVO();
            medicalRecord.ID = Guid.NewGuid().ToString();
            medicalRecord.blxtml = path;
            medicalRecord.mbbh = mbbh;
            medicalRecord.zyh = zyh;
            medicalRecord.blxtmc_yj = BLMC;
            medicalRecord.CreatorCode = this.UserIdentity.UserCode;
            medicalRecord.ksdm = this.UserIdentity.DepartmentCode;
            medicalRecord.ksmc = this.UserIdentity.DepartmentName;
            medicalRecord.OrganizeId = OrganizeId;
            medicalRecord.bllx = bllx;
            medicalRecord.dzbl_id = "R" + FieldValueHelper.GetNewFieldUniqueValue("YB_Inp_PatRegInfo.dzbl_id", OrganizeId, "{0:D10}", false);
            //Chèn vào bảng quan hệ bệnh án của bệnh nhân
            var Entity = new ZymeddocsrelationEntity();
            Entity.Id = Guid.NewGuid().ToString();
            Entity.zyh = zyh;
            Entity.blmc = BLMC;
            Entity.bllx = bllx;
            Entity.blzt = 0;
            Entity.mbId = mbbh;
            Entity.IsParent = 0;
            Entity.blId = medicalRecord.ID;
            Entity.OrganizeId = OrganizeId;
            Entity.blrq = DateTime.Now;
            Entity.CreatorCode = this.UserIdentity.UserCode;
            Entity.ysxm = this.UserIdentity.UserName;
            Entity.ysgh = this.UserIdentity.rygh;
            Entity.CreateTime = DateTime.Now;
            Entity.zt = "1";
            Entity.DataSource = DataSource;
            Entity.XmlConten = xmlConten;
            Entity.doctype = doctype;
            _medicalRecordDmnService.MedicalRecordSave(medicalRecord, Entity);
            return medicalRecord.ID;
        }
        
        /// <summary>
        /// Đường dẫn lưu trữ bệnh án có tồn tại hay không
        /// </summary>
        /// <param name="path"></param>
        public void IsExistDir(string path)
        {
            if (Directory.Exists(WebPath.Map(path)) == false)//Nếu không tồn tại thì tạo thư mục file
            {
                Directory.CreateDirectory(WebPath.Map(path));
            }
        }

        #endregion

        #region Trình soạn thảo bệnh án-Khởi tạo nguồn dữ liệu
        /// <summary>
        /// Lấy thông tin cơ bản của bệnh nhân theo số nhập viện
        /// Thực thể phải có thể tuần tự hóa mới có thể liên kết dữ liệu trong control bệnh án, vì vậy sử dụngZybrjbxxVO
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        private ZybrjbxxVO GetZybrjbxxByZYH(string zyh)
        {
            var ZybrjbxxEntity = _ZybrjbxxRepo.GetZybrjbxx(zyh, OrganizeId);

            var Zybrjbxx = new ZybrjbxxVO();
            Zybrjbxx.zyh = ZybrjbxxEntity.zyh;
            Zybrjbxx.blh = ZybrjbxxEntity.blh;
            Zybrjbxx.xm = ZybrjbxxEntity.xm;
            Zybrjbxx.sfzh = ZybrjbxxEntity.sfzh;

            if (ZybrjbxxEntity.sex == EnumSex.F.GetHashCode().ToString())
            {
                Zybrjbxx.sex = EnumSex.F.GetDescription();
            }
            else if (ZybrjbxxEntity.sex == EnumSex.M.GetHashCode().ToString())
            {
                Zybrjbxx.sex = EnumSex.M.GetDescription();
            }
            else
            {
                Zybrjbxx.sex = ZybrjbxxEntity.sex;
            }
            Zybrjbxx.birth = ZybrjbxxEntity.birth;
            if (Zybrjbxx.birth != null)
            {
                Zybrjbxx.nl = CommmHelper.CalculateAgeCorrect(ZybrjbxxEntity.birth ?? DateTime.Now, DateTime.Now);
            }
            Zybrjbxx.zybz = ZybrjbxxEntity.zybz;
            Zybrjbxx.sfqj = ZybrjbxxEntity.sfqj;
            Zybrjbxx.DeptCode = ZybrjbxxEntity.DeptCode;
            if (!string.IsNullOrWhiteSpace(Zybrjbxx.DeptCode))
            {
                Zybrjbxx.DeptName = _SysDepartmentRepo.GetEntityByCode(ZybrjbxxEntity.DeptCode, OrganizeId).Name;
            }

            Zybrjbxx.WardCode = ZybrjbxxEntity.WardCode;
            if (!string.IsNullOrWhiteSpace(Zybrjbxx.WardCode))
            {
                var ward = _CommonDmnService.GetWardList(OrganizeId, Zybrjbxx.WardCode).FirstOrDefault();
                if (ward != null)
                {
                    Zybrjbxx.WardName = ward.Name;
                }
            }
            Zybrjbxx.ysgh = ZybrjbxxEntity.ysgh;

            if (!string.IsNullOrWhiteSpace(Zybrjbxx.ysgh))
            {
                var ys = _SysStaffRepo.GetValidStaffByGh(ZybrjbxxEntity.ysgh, OrganizeId);
                if (ys != null)
                {
                    Zybrjbxx.ysxm = ys.Name;
                }
            }

            Zybrjbxx.BedCode = ZybrjbxxEntity.BedCode;
            Zybrjbxx.ryrq = ZybrjbxxEntity.ryrq;
            Zybrjbxx.rqrq = ZybrjbxxEntity.rqrq;
            Zybrjbxx.cqrq = ZybrjbxxEntity.cqrq;
            Zybrjbxx.wzjb = ZybrjbxxEntity.wzjb;
            Zybrjbxx.hljb = ZybrjbxxEntity.hljb;
            Zybrjbxx.ryfs = ZybrjbxxEntity.ryfs;
            Zybrjbxx.cyfs = ZybrjbxxEntity.cyfs;
            Zybrjbxx.gdxmzxrq = ZybrjbxxEntity.gdxmzxrq;
            Zybrjbxx.brxzdm = ZybrjbxxEntity.brxzdm;
            Zybrjbxx.brxzmc = ZybrjbxxEntity.brxzmc;
            Zybrjbxx.cardno = ZybrjbxxEntity.cardno;
            Zybrjbxx.cardtype = ZybrjbxxEntity.cardtype;
            Zybrjbxx.lxr = ZybrjbxxEntity.lxr;
            Zybrjbxx.lxrgx = ZybrjbxxEntity.lxrgx;
            Zybrjbxx.lxrdh = ZybrjbxxEntity.lxrdh;
            Zybrjbxx.zddm = ZybrjbxxEntity.zddm;
            Zybrjbxx.zdmc = ZybrjbxxEntity.zdmc;
            Zybrjbxx.cyzddm = ZybrjbxxEntity.cyzddm;
            Zybrjbxx.cyzdmc = ZybrjbxxEntity.cyzdmc;
            Zybrjbxx.bfNo = ZybrjbxxEntity.bfNo;
            Zybrjbxx.gj = ZybrjbxxEntity.gj;
            Zybrjbxx.mz = ZybrjbxxEntity.mz;
            Zybrjbxx.zy = ZybrjbxxEntity.zy;
            System.TimeSpan ts;
            if (Zybrjbxx.cqrq != null)
            {
                ts = ZybrjbxxEntity.cqrq.ToDate().Date - ZybrjbxxEntity.rqrq.ToDate().Date;
            }
            else
            {
                ts = DateTime.Now.ToDate().Date - ZybrjbxxEntity.rqrq.ToDate().Date;
            }

            Zybrjbxx.zyts = ts.Days;
            Zybrjbxx.nlshow = ZybrjbxxEntity.nlshow;
            Zybrjbxx.BedName = _ZybrjbxxDmnService.GetCwNameByCode(ZybrjbxxEntity.BedCode, OrganizeId, ZybrjbxxEntity.WardCode);
            return Zybrjbxx;
        }
        /// <summary>
        /// Lấy thông tin cơ bản của bệnh nhân theo số nhập viện-Trang đầu bệnh án
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        private BabasyVO GetBasyByZYH(string zyh)
        {

            var BabasyVO = _ZybrjbxxDmnService.GetPatBasicInfo_basy(OrganizeId, zyh); //_ZybrjbxxRepo.GetZybrjbxx(zyh, OrganizeId);

            return BabasyVO;
        }

        private BabasyVO GetBasyInfo(WebWriterControlEngine eng)
        {
            string dataBind = eng.Document.GetDataSourceBindingDescriptionsXML();
            BabasyVO ba = new BabasyVO();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(dataBind);

            XmlNodeList list = doc.SelectNodes(@"/DCDataSourceBindingDescriptions/Binding");
            if (list != null && list.Count > 0)
            {
                foreach (XmlNode item in list)
                {
                    try
                    {
                        if (item.Attributes["BindingPath"].Value == "ba_basy/brxm")
                        {
                            ba.brxm = eng.Document.GetElementById(item.Attributes["ElementID"].Value.ToString())
                                .InnerText;
                        }

                        if (item.Attributes["BindingPath"].Value == "ba_basy/xb")
                        {
                            ba.xb = eng.Document.GetElementById(item.Attributes["ElementID"].Value.ToString())
                                .InnerText;
                        }

                        if (item.Attributes["BindingPath"].Value == "ba_basy/csny")
                        {
                            ba.csny = eng.Document.GetElementById(item.Attributes["ElementID"].Value.ToString()).InnerText;
                        }
                        if (item.Attributes["BindingPath"].Value == "ba_basy/sfzhm")
                        {
                            ba.sfzhm = eng.Document.GetElementById(item.Attributes["ElementID"].Value.ToString()).InnerText;
                        }
                        if (item.Attributes["BindingPath"].Value == "ba_basy/ryrq")
                        {
                            ba.ryrq = Convert.ToDateTime(eng.Document.GetElementById(item.Attributes["ElementID"].Value.ToString()).InnerText);
                        }
                        if (item.Attributes["BindingPath"].Value == "ba_basy/rytj")
                        {
                            ba.rytj = eng.Document.GetElementById(item.Attributes["ElementID"].Value.ToString()).InnerText;
                        }
                    }
                    catch (Exception ex)
                    {
                    }

                }
            }

            return ba;
        }
        /// <summary>
        /// Lấy thông tin bệnh nhân ngoại trú
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        private TreatEntityVO GetOutpatbyMzh(string mzh)
        {
            return _outpatientDmnService.GetPatMzbymzh(OrganizeId, mzh, this.UserIdentity.rygh);
        }

        /// <summary>
        /// Bệnh án nội trú tải thông tin cơ bản của bệnh nhân
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        private blzybrjbxxVO getBlZybrjbxx(string zyh,bool newbz)
        {
            var data = _medicalRecordDmnService.GetBlZybrjbxx(this.OrganizeId, zyh, this.UserIdentity.UserName,newbz);
            return data;
        }
        #endregion

        #region  Gán nguồn dữ liệu cho văn bản bệnh án
        private string XMLDataBind<T>(T data, string currentFileName)
        {
            WebWriterControlEngine eng;
            if (engine == null)
            {
                eng = GetControlEngine();
            }
            else
            {
                eng = engine;
            }
            eng.LoadDocument(currentFileName, null);
            string dataBind = eng.Document.GetDataSourceBindingDescriptionsXML();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(dataBind);

            DataSet xmlDS = new DataSet();
            var stream = new StringReader(dataBind);
            var reader = new XmlTextReader(stream);
            xmlDS.ReadXml(reader);

            if (data != null)
            {
                PropertyInfo[] properties = data.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

                if (properties.Length <= 0)
                {
                    return eng.GetAllContentHtml();
                }

                if (xmlDS != null && xmlDS.Tables.Count > 0)
                {
                    foreach (DataRow item in xmlDS.Tables[0].Rows)
                    {
                        var ElementID = item["ElementID"].ToString();
                        var BindingPath = item["BindingPath"].ToString();
                        var DataSource = item["DataSource"].ToString();
                        var bdp = BindingPath.ToString().Split('/');

                        var eledata = properties.Where(p => p.Name == bdp[1]).FirstOrDefault();
                        if (eledata != null)
                        {
                            object value = eledata.GetValue(data, null);
                            if (value != null)
                            {
                                XTextElement ele = eng.Document.GetElementById(ElementID);
                                ele.InnerText = value.ToString();
                            }
                            //else if (BindingPath.Contains("ba_basy"))
                            //{
                            //    XTextElement ele = eng.Document.GetElementById(ElementID);
                            //    ele.InnerText = "[-]";
                            //}
                        }
                        if (eledata == null)
                        {
                            eledata = properties.Where(p => p.Name == ElementID).FirstOrDefault();
                            if (eledata != null)
                            {
                                object value = eledata.GetValue(data, null);
                                if (value != null)
                                {
                                    XTextElement ele = eng.Document.GetElementById(ElementID);
                                    ele.InnerText = value.ToString();
                                }
                            }
                        }

                    }
                }

                return eng.GetAllContentHtml();
            }

            return eng.GetAllContentHtml();
        }

        private WebWriterControlEngine XMLDataBind<T>(T data, WebWriterControlEngine eng)
        {
            string dataBind = eng.Document.GetDataSourceBindingDescriptionsXML();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(dataBind);

            DataSet xmlDS = new DataSet();
            var stream = new StringReader(dataBind);
            var reader = new XmlTextReader(stream);
            xmlDS.ReadXml(reader);

            if (data != null)
            {
                PropertyInfo[] properties = data.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

                if (properties.Length <= 0)
                {
                    return eng;
                }

                if (xmlDS != null && xmlDS.Tables.Count > 0)
                {
                    foreach (DataRow item in xmlDS.Tables[0].Rows)
                    {
                        var ElementID = item["ElementID"].ToString();
                        var BindingPath = item["BindingPath"].ToString();
                        var DataSource = item["DataSource"].ToString();
                        var bdp = BindingPath.ToString().Split('/');

                        if (bdp.Length > 1)
                        {
                            var eledata = properties.Where(p => p.Name == bdp[1]).FirstOrDefault();
                            if (eledata != null)
                            {
                                object value = eledata.GetValue(data, null);
                                if (value != null && !string.IsNullOrWhiteSpace(ElementID))
                                {
                                    XTextElement ele = eng.Document.GetElementById(ElementID);
                                    ele.InnerText = value.ToString();
                                }
                            }
                            if (eledata == null)
                            {
                                eledata = properties.Where(p => p.Name == ElementID).FirstOrDefault();
                                if (eledata != null)
                                {
                                    object value = eledata.GetValue(data, null);
                                    if (value != null && !string.IsNullOrWhiteSpace(ElementID))
                                    {
                                        XTextElement ele = eng.Document.GetElementById(ElementID);
                                        ele.InnerText = value.ToString();
                                    }
                                }
                            }
                        }
                    }
                }
                return eng;
            }
            return eng;
        }

        public string editorPross(string str)
        {
            string oldstr = "span style=&quot;color:black;font-size:9pt;background-color:white";
            string tip = "style=\"position:relative;left:4px;top:1px";
            return str.Replace(oldstr + "&quot;&gt; 常", oldstr + ";display:none&quot;&gt; 常")
                                        .Replace(oldstr + "&quot;&gt; &amp;#x5E38", oldstr + ";display:none&quot;&gt; &amp;#x5E38")
                                        .Replace(tip, tip + ";display:none ");
        }

        #endregion

        #region Tải lên bảo hiểm y tế
        /// <summary>
        /// Lưu và tải lên dữ liệu vùng văn bản
        /// </summary>
        /// <param name="list"></param>
        private string FieldDataSave(XTextElementList list, string ybbm, string dzblId, string sd, string zyh = null)
        {
            string msg = "";
            int isupload = 0;
            if (sd == "Tải lên bảo hiểm y tế" || sd == "Tải lên bảo hiểm y tế")
            {
                isupload = 1;
                if (string.IsNullOrWhiteSpace(UploadURL))
                {
                    return "Không lấy được địa chỉ giao diện";
                }
            }
            QHDSmartCheckInput rootinput = new QHDSmartCheckInput();
            switch (ybbm)
            {
                case "7100"://Hồ sơ nhập viện   
                    rootinput.jydm = "7100";
                    YBInpPatRegInfoEntity _ryjl = _yBInpPatRegInfoRepo.FindEntity(dzblId);
                    if (_ryjl == null)
                    {
                        _ryjl = new YBInpPatRegInfoEntity();
                        _ryjl.Id = dzblId;
                        _ryjl.OrganizeId = OrganizeId;
                    }
                    _ryjl = FieldDataCheck(_ryjl, list, isupload, out msg);
                    if (isupload == 1 && !string.IsNullOrWhiteSpace(msg))
                    {
                        return msg;
                    }
                    else if (isupload == 1)
                    {
                        YB_7100 yb_7100 = new YB_7100();
                        yb_7100 = _yBInterfaceDmnService.GetRyblInfo(OrganizeId, _ryjl.Id);
                        YBRequestBase<YB_7100> YBJKDATA = new YBRequestBase<YB_7100>();
                        YBJKDATA.REQUESTDATA = new YBRequestSingle<YB_7100>
                        {
                            MSGNO = ybbm,
                            AKB020 = YBOrgCode,
                            OPERID = this.UserIdentity.rygh,
                            OPERNAME = UserIdentity.UserName,
                            PERIODNO = "",
                            MSGID = YBOrgCode + DateTime.Now.ToString("yyyyMMddHHmmss"),
                            GRANTID = "",
                            INPUT = yb_7100
                        };
                        rootinput.jsondata = JsonConvert.SerializeObject(YBJKDATA);
                    }
                    _yBInpPatRegInfoRepo.SubmitRegInfo(_ryjl);
                    zyh = _ryjl.AKC190;
                    #region Chẩn đoán nhập viện
                    var zdlist = GetDocumentDiag(list); //Lấy danh sách chẩn đoán trong thông tin nhập viện
                    msg = _yBInterfaceDmnService.YBInhosDiagInfoSave(zdlist, OrganizeId, dzblId, zyh);
                    #endregion
                    if (isupload == 1 && !string.IsNullOrWhiteSpace(msg))
                    {
                        return msg;
                    }
                    break;
                case "7110"://Chẩn đoán bản ghi nhập viện
                    rootinput.jydm = "7110";
                    if (isupload == 1)
                    {
                        var ybzd = _yBInterfaceDmnService.GetRyblZdInfo(OrganizeId, dzblId);
                        YBRequestBase<YB_7110> YBJKDATA = new YBRequestBase<YB_7110>();
                        YBJKDATA.REQUESTDATA = new YBRequestRows<YB_7110>
                        {
                            MSGNO = ybbm,
                            AKB020 = YBOrgCode,
                            OPERID = this.UserIdentity.rygh,
                            OPERNAME = UserIdentity.UserName,
                            PERIODNO = "",
                            MSGID = YBOrgCode + DateTime.Now.ToString("yyyyMMddHHmmss"),
                            GRANTID = "",
                            INPUT = ybzd.ToList()
                        };
                        rootinput.jsondata = JsonConvert.SerializeObject(YBJKDATA);
                    }
                    break;
                case "7200"://Bản ghi diễn biến bệnh lần đầu
                    rootinput.jydm = "7200";
                    YBInpCourseDiseaseEntity _bcjl = _yBInpCourseDiseaseRepo.FindEntity(dzblId);
                    if (_bcjl == null)
                    {
                        _bcjl = new YBInpCourseDiseaseEntity();
                        _bcjl.Id = dzblId;
                        _bcjl.OrganizeId = OrganizeId;
                    }
                    _bcjl = FieldDataCheck(_bcjl, list, isupload, out msg);
                    if (isupload == 1 && !string.IsNullOrWhiteSpace(msg))
                    {
                        return msg;
                    }
                    else if (isupload == 1)
                    {
                        YB_7200 yb_7200 = new YB_7200();
                        yb_7200 = _yBInterfaceDmnService.GetBcjlInfo(OrganizeId, _bcjl.Id);
                        YBRequestBase<YB_7200> YBJKDATA = new YBRequestBase<YB_7200>();
                        YBJKDATA.REQUESTDATA = new YBRequestSingle<YB_7200>
                        {
                            MSGNO = ybbm,
                            AKB020 = YBOrgCode,
                            OPERID = this.UserIdentity.rygh,
                            OPERNAME = UserIdentity.UserName,
                            PERIODNO = "",
                            MSGID = YBOrgCode + DateTime.Now.ToString("yyyyMMddHHmmss"),
                            GRANTID = "",
                            INPUT = yb_7200
                        };
                        rootinput.jsondata = JsonConvert.SerializeObject(YBJKDATA);
                    }
                    _yBInpCourseDiseaseRepo.Submit(_bcjl);
                    break;
                case "7500"://Thông tin tóm tắt ra viện
                    rootinput.jydm = "7500";
                    YBInpOutHosSummariesEntity _cyxj = _yBInpOutHosSummariesRepo.FindEntity(dzblId);
                    if (_cyxj == null)
                    {
                        _cyxj = new YBInpOutHosSummariesEntity();
                        _cyxj.Id = dzblId;
                        _cyxj.OrganizeId = OrganizeId;
                    }
                    _cyxj = FieldDataCheck(_cyxj, list, isupload, out msg);
                    if (isupload == 1 && !string.IsNullOrWhiteSpace(msg))
                    {
                        return msg;
                    }
                    else if (isupload == 1)
                    {
                        YB_7500 yb_7500 = new YB_7500();
                        yb_7500 = _yBInterfaceDmnService.GetCyxjInfo(OrganizeId, _cyxj.Id);
                        YBRequestBase<YB_7500> YBJKDATA = new YBRequestBase<YB_7500>();
                        YBJKDATA.REQUESTDATA = new YBRequestSingle<YB_7500>
                        {
                            MSGNO = ybbm,
                            AKB020 = YBOrgCode,
                            OPERID = this.UserIdentity.rygh,
                            OPERNAME = UserIdentity.UserName,
                            PERIODNO = "",
                            MSGID = YBOrgCode + DateTime.Now.ToString("yyyyMMddHHmmss"),
                            GRANTID = "",
                            INPUT = yb_7500
                        };
                        rootinput.jsondata = JsonConvert.SerializeObject(YBJKDATA);
                    }
                    _yBInpOutHosSummariesRepo.Submit(_cyxj);
                    zyh = _cyxj.AKC190;
                    #region Chẩn đoán ra viện
                    var cyzdlist = GetDocumentDiag(list); //Lấy danh sách chẩn đoán trong thông tin nhập viện
                    msg = _yBInterfaceDmnService.YBOuthosDiagInfoSave(cyzdlist, OrganizeId, dzblId, zyh);
                    #endregion
                    if (isupload == 1 && !string.IsNullOrWhiteSpace(msg))
                    {
                        return msg;
                    }
                    break;
                case "7510"://Chẩn đoán tóm tắt ra viện
                    rootinput.jydm = "7510";
                    if (isupload == 1)
                    {
                        var cyzd = _yBInterfaceDmnService.GetCyxjZdInfo(OrganizeId, dzblId);
                        YBRequestBase<YB_7510> YBJKDATA = new YBRequestBase<YB_7510>();
                        YBJKDATA.REQUESTDATA = new YBRequestRows<YB_7510>
                        {
                            MSGNO = ybbm,
                            AKB020 = YBOrgCode,
                            OPERID = this.UserIdentity.rygh,
                            OPERNAME = UserIdentity.UserName,
                            PERIODNO = "",
                            MSGID = YBOrgCode + DateTime.Now.ToString("yyyyMMddHHmmss"),
                            GRANTID = "",
                            INPUT = cyzd.ToList()
                        };
                        rootinput.jsondata = JsonConvert.SerializeObject(YBJKDATA);
                    }
                    break;
            }

            if (isupload == 1)
            {
                try
                {
                    var respstr = EmrHttpHelper.PostJsonString(UploadURL + "api/QHDSmartCheck/MedicalRecordUpload", JsonConvert.SerializeObject(rootinput));
                    var resp = JsonConvert.DeserializeObject<Newtouch.EMR.Domain.DTO.OutputDto.MRUpload.ResponseBase>(respstr);
                    if (resp.code == "-1")
                    {
                        msg = resp.message;
                    }
                    else
                    {
                        if (ybbm == "7100")
                        {
                            msg = FieldDataSave(list, "7110", dzblId, sd, zyh);
                        }
                        else if (ybbm == "7500")
                        {
                            msg = FieldDataSave(list, "7510", dzblId, sd, zyh);
                        }
                        else
                        {
                            var rel = _ZymeddocsrelationRepo.FindEntity(p => p.OrganizeId == OrganizeId && p.zt == "1" && p.blId == dzblId);
                            if (rel != null)
                            {
                                rel.YbUploadFlag = "1";
                                _ZymeddocsrelationRepo.Update(rel);
                            }
                            msg = "SUCCESS";
                        }
                    }
                }
                catch (Exception ex)
                {
                    msg = "Tải lên thất bại！" + ex.Message;
                }
            }

            return msg;
        }

        private string FieldDataSaveJS(XTextElementList list, string ybbm, string dzblId, int isupload, string zyh = null)
        {
            string msg = "";
            //int isupload = 0;
            QHDSmartCheckInput rootinput = new QHDSmartCheckInput();
            rootinput.jydm = ybbm;
            try
            {
                switch (ybbm)
                {
                    case "7100"://Hồ sơ nhập viện   
                        YBInpPatRegInfoEntity _ryjl = _yBInpPatRegInfoRepo.FindEntity(dzblId);
                        if (_ryjl == null)
                        {
                            _ryjl = new YBInpPatRegInfoEntity();
                            _ryjl.Id = dzblId;
                            _ryjl.OrganizeId = OrganizeId;
                        }
                        _ryjl = FieldDataCheck(_ryjl, list, isupload, out msg);
                        if (isupload == 1 && !string.IsNullOrWhiteSpace(msg))
                        {
                            return msg;
                        }
                        _yBInpPatRegInfoRepo.SubmitRegInfo(_ryjl);
                        zyh = _ryjl.AKC190;
                        #region Chẩn đoán nhập viện
                        var zdlist = GetDocumentDiag(list); //Lấy danh sách chẩn đoán trong thông tin nhập viện
                        msg = _yBInterfaceDmnService.YBInhosDiagInfoSave(zdlist, OrganizeId, dzblId, zyh);
                        #endregion
                        if (isupload == 1 && !string.IsNullOrWhiteSpace(msg))
                        {
                            return msg;
                        }
                        break;
                    case "7200"://Bản ghi diễn biến bệnh lần đầu
                        YBInpCourseDiseaseEntity _bcjl = _yBInpCourseDiseaseRepo.FindEntity(dzblId);
                        if (_bcjl == null)
                        {
                            _bcjl = new YBInpCourseDiseaseEntity();
                            _bcjl.Id = dzblId;
                            _bcjl.OrganizeId = OrganizeId;
                        }
                        _bcjl = FieldDataCheck(_bcjl, list, isupload, out msg);
                        if (isupload == 1 && !string.IsNullOrWhiteSpace(msg))
                        {
                            return msg;
                        }
                        _yBInpCourseDiseaseRepo.Submit(_bcjl);
                        break;
                    case "7500"://Thông tin tóm tắt ra viện
                        YBInpOutHosSummariesEntity _cyxj = _yBInpOutHosSummariesRepo.FindEntity(dzblId);
                        if (_cyxj == null)
                        {
                            _cyxj = new YBInpOutHosSummariesEntity();
                            _cyxj.Id = dzblId;
                            _cyxj.OrganizeId = OrganizeId;
                        }
                        _cyxj = FieldDataCheck(_cyxj, list, isupload, out msg);
                        if (isupload == 1 && !string.IsNullOrWhiteSpace(msg))
                        {
                            return msg;
                        }
                        _yBInpOutHosSummariesRepo.Submit(_cyxj);
                        zyh = _cyxj.AKC190;
                        #region Chẩn đoán ra viện
                        var cyzdlist = GetDocumentDiag(list); //Lấy danh sách chẩn đoán trong thông tin nhập viện
                        msg = _yBInterfaceDmnService.YBOuthosDiagInfoSave(cyzdlist, OrganizeId, dzblId, zyh);
                        #endregion
                        if (isupload == 1 && !string.IsNullOrWhiteSpace(msg))
                        {
                            return msg;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (isupload == 1)
            {
                return "";
            }

            return msg;
        }
        /// <summary>
        /// Lấy thông tin bệnh án tương ứng theo tên phương thức
        /// </summary>
        /// <param name="jydm"></param>
        /// <param name="blId"></param>
        /// <returns></returns>
        public QHDSmartCheckInput GetBlxxbyJydmData(string jydm, string blId)
        {
            QHDSmartCheckInput rootinput = new QHDSmartCheckInput();
            rootinput.jydm = jydm;
            switch (jydm)
            {
                case "7100":
                    YB_7100 yb_7100 = new YB_7100();
                    yb_7100 = _yBInterfaceDmnService.GetRyblInfo(OrganizeId, blId);
                    if (yb_7100 != null)
                    {
                        YBRequestBase<YB_7100> YBJKDATA = new YBRequestBase<YB_7100>();
                        YBJKDATA.REQUESTDATA = new YBRequestSingle<YB_7100>
                        {
                            MSGNO = jydm,
                            AKB020 = YBOrgCode,
                            OPERID = this.UserIdentity.rygh,
                            OPERNAME = UserIdentity.UserName,
                            PERIODNO = "",
                            MSGID = YBOrgCode + DateTime.Now.ToString("yyyyMMddHHmmss"),
                            GRANTID = "",
                            INPUT = yb_7100
                        };
                        rootinput.jsondata = JsonConvert.SerializeObject(YBJKDATA);
                    }

                    break;
                case "7110":
                    var ybzd = _yBInterfaceDmnService.GetRyblZdInfo(OrganizeId, blId);
                    if (ybzd != null)
                    {
                        YBRequestBase<YB_7110> YBJKDATA = new YBRequestBase<YB_7110>();
                        YBJKDATA.REQUESTDATA = new YBRequestRows<YB_7110>
                        {
                            MSGNO = jydm,
                            AKB020 = YBOrgCode,
                            OPERID = this.UserIdentity.rygh,
                            OPERNAME = UserIdentity.UserName,
                            PERIODNO = "",
                            MSGID = YBOrgCode + DateTime.Now.ToString("yyyyMMddHHmmss"),
                            GRANTID = "",
                            INPUT = ybzd.ToList()
                        };
                        rootinput.jsondata = JsonConvert.SerializeObject(YBJKDATA);
                    }
                    break;
                case "7200":
                    var yb_7200 = _yBInterfaceDmnService.GetBcjlInfo(OrganizeId, blId);
                    if (yb_7200 != null)
                    {
                        YBRequestBase<YB_7200> YBJKDATA = new YBRequestBase<YB_7200>();
                        YBJKDATA.REQUESTDATA = new YBRequestSingle<YB_7200>
                        {
                            MSGNO = jydm,
                            AKB020 = YBOrgCode,
                            OPERID = this.UserIdentity.rygh,
                            OPERNAME = UserIdentity.UserName,
                            PERIODNO = "",
                            MSGID = YBOrgCode + DateTime.Now.ToString("yyyyMMddHHmmss"),
                            GRANTID = "",
                            INPUT = yb_7200
                        };
                        rootinput.jsondata = JsonConvert.SerializeObject(YBJKDATA);
                    }
                    break;
                case "7500":
                    YB_7500 yb_7500 = new YB_7500();
                    yb_7500 = _yBInterfaceDmnService.GetCyxjInfo(OrganizeId, blId);
                    if (yb_7500 != null)
                    {
                        YBRequestBase<YB_7500> YBJKDATA = new YBRequestBase<YB_7500>();
                        YBJKDATA.REQUESTDATA = new YBRequestSingle<YB_7500>
                        {
                            MSGNO = jydm,
                            AKB020 = YBOrgCode,
                            OPERID = this.UserIdentity.rygh,
                            OPERNAME = UserIdentity.UserName,
                            PERIODNO = "",
                            MSGID = YBOrgCode + DateTime.Now.ToString("yyyyMMddHHmmss"),
                            GRANTID = "",
                            INPUT = yb_7500
                        };
                        rootinput.jsondata = JsonConvert.SerializeObject(YBJKDATA);
                    }
                    break;
                case "7510":
                    var cyzd = _yBInterfaceDmnService.GetCyxjZdInfo(OrganizeId, blId);
                    if (cyzd != null)
                    {
                        YBRequestBase<YB_7510> YBJKDATA = new YBRequestBase<YB_7510>();
                        YBJKDATA.REQUESTDATA = new YBRequestRows<YB_7510>
                        {
                            MSGNO = jydm,
                            AKB020 = YBOrgCode,
                            OPERID = this.UserIdentity.rygh,
                            OPERNAME = UserIdentity.UserName,
                            PERIODNO = "",
                            MSGID = YBOrgCode + DateTime.Now.ToString("yyyyMMddHHmmss"),
                            GRANTID = "",
                            INPUT = cyzd.ToList()
                        };
                        rootinput.jsondata = JsonConvert.SerializeObject(YBJKDATA);
                    }
                    break;

            }

            return (rootinput);
        }

        public IActionResult GetBlxxbyJydm(string jydm, string blId)
        {
            QHDSmartCheckInput data = new QHDSmartCheckInput();
            if (!string.IsNullOrWhiteSpace(jydm) && !string.IsNullOrWhiteSpace(blId))
            {
                data = GetBlxxbyJydmData(jydm, blId);
            }
            return Content(data.ToJson());
        }

        /// <summary>
        /// Cập nhật trạng thái tải lên
        /// </summary>
        /// <returns></returns>
        public IActionResult UpdateYBUploadStu(string blId)
        {
            var ety = _ZymeddocsrelationRepo.FindEntity(p => p.blId == blId && p.zt == "1" && p.OrganizeId == OrganizeId);
            if (ety != null)
            {
                ety.YbUploadFlag = "1";
                _ZymeddocsrelationRepo.Update(ety);
                return Success("Tải lên thành công");
            }
            return Error("Tải lên thất bại，Vui lòng kiểm traBệnh ánthông tin。");
        }
        public T FieldDataCheck<T>(T entity, XTextElementList list, int isupload, out string errmsg)
        {
            errmsg = "";
            PropertyInfo[] properties = entity.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo p in properties)
            {
                XTextElement x = list.FirstOrDefault(r => r.ID == p.Name);
                if (x != null)
                {
                    if (!string.IsNullOrWhiteSpace(x.FormulaValue) && !string.IsNullOrWhiteSpace(x.InnerText))
                    {
                        p.SetValue(entity, x.FormulaValue);
                    }
                    else if (!string.IsNullOrWhiteSpace(x.InnerText))
                    {
                        p.SetValue(entity, x.InnerText);
                    }
                    else
                    {
                        PropertyInfo[] propxml = x.GetType().GetProperties();
                        PropertyInfo px = propxml.FirstOrDefault(pp => pp.Name == "ValueExpression");
                        if (px != null)
                        {
                            var relobj = px.GetValue(x);
                            if (relobj != null)
                            {
                                string relcode = relobj.ToString().Replace("[", "").Replace("]", "");
                                XTextElement rel_x = list.FirstOrDefault(r => r.ID == relcode);
                                if (rel_x != null)
                                {
                                    p.SetValue(entity, rel_x.InnerText);
                                }
                            }
                        }

                    }
                }

                var val = p.GetValue(entity);
                if (isupload == 1 && val == null)
                {
                    var required = (RequiredAttribute[])p.GetCustomAttributes(typeof(RequiredAttribute), false);
                    var desc = (DescriptionAttribute[])p.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (required != null && required.Length > 0 && desc != null && desc.Length > 0)
                    {
                        if (string.IsNullOrWhiteSpace(errmsg))
                        {
                            errmsg = "Tải lên dữ liệu quan trọng của bảo hiểm y tế！";
                        }
                        errmsg += "[" + desc[0].Description + "] không được để trống;";
                    }

                }
            }

            return entity;
        }

        #endregion

        #region Trích xuất dữ liệu văn bản bệnh án
        /// <summary>
        /// Lấy danh sách chẩn đoán của tài liệu
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<DiagnoseDocDTO> GetDocumentDiag(XTextElementList list)
        {
            List<DiagnoseDocDTO> diaglist = new List<DiagnoseDocDTO>();
            if (list != null && list.Count > 0)
            {
                int px = 0;
                //Số lượng chẩn đoán
                var docdiag = list.FindAll(p => p.ID.Contains("zyzd_") && p.ID.Contains("_zyzd_") == false && p.ID.Contains("_name") == false);
                if (docdiag != null && docdiag.Count > 0)
                {
                    foreach (var item in docdiag)
                    {
                        if (!string.IsNullOrWhiteSpace(item.FormulaValue) || !string.IsNullOrWhiteSpace(item.InnerText))
                        {
                            px += 1;
                            //Chi tiết chẩn đoán
                            DiagnoseDocDTO dto = new DiagnoseDocDTO();
                            dto.icd10 = item.FormulaValue ?? item.InnerText;
                            dto.zdCode = item.FormulaValue ?? item.InnerText;
                            dto.px = px;
                            var zdinfo = list.FindAll(p => p.ID.Contains(item.ID));
                            if (zdinfo != null && zdinfo.Count > 0)
                            {
                                foreach (var i in zdinfo)
                                {
                                    if (i.ID.Contains("name") && string.IsNullOrWhiteSpace(dto.zdName))
                                    {
                                        dto.zdName = i.InnerText.Trim();
                                    }
                                    if (i.ID.Contains("zdlx") && string.IsNullOrWhiteSpace(dto.zdlx))
                                    {
                                        dto.zdlx = i.FormulaValue ?? i.InnerText;
                                    }
                                    else if (i.ID.Contains("zdfl") && string.IsNullOrWhiteSpace(dto.zdfl))
                                    {
                                        dto.zdfl = i.FormulaValue ?? i.InnerText;
                                    }
                                }
                            }
                            diaglist.Add(dto);
                        }
                    }
                }
            }

            return diaglist;
        }

        public void TbDataSavebyWriter(string DataSource, WebWriterControlEngine eng, string blId)
        {
            var tables = eng.Document.Tables;
            if (tables != null && tables.Count > 0)
            {
                if (DataSource == "bl_hljldata")
                {
                    IList<BlHljlData> hllist = new List<BlHljlData>();
                    foreach (var tb in tables)
                    {
                        if (tb.ID.Contains(DataSource))
                        {
                            foreach (var row in tb.Elements)
                            {
                                BlHljlData hl = new BlHljlData();
                                hl.Id = System.Guid.NewGuid().ToString();
                                hl.jlrq = row.Elements.Find(x => x.ID == "tb_jlrq").Text;
                                hl.tw = row.Elements.Find(x => x.ID == "tb_tw").Text;
                                hl.mb = row.Elements.Find(x => x.ID == "tb_mb").Text;
                                hl.hx = row.Elements.Find(x => x.ID == "tb_hx").Text;
                                hl.xy = row.Elements.Find(x => x.ID == "tb_xy").Text;
                                hl.ybhd = row.Elements.Find(x => x.ID == "tb_ybhd").Text;
                                hl.cxxdjc = row.Elements.Find(x => x.ID == "tb_cxxdjc").Text;
                                hl.xroyx = row.Elements.Find(x => x.ID == "tb_xroyx").Text;
                                hl.hljb = row.Elements.Find(x => x.ID == "tb_hljb").Text;
                                hl.xzjs = row.Elements.Find(x => x.ID == "tb_xzjs").Text;
                                hl.pbjyxkt = row.Elements.Find(x => x.ID == "tb_pbjyxkt").Text;
                                hl.ycyf = row.Elements.Find(x => x.ID == "tb_ycyf").Text;
                                hl.ddyf = row.Elements.Find(x => x.ID == "tb_ddyf").Text;
                                hl.qtjh = row.Elements.Find(x => x.ID == "tb_qtjh").Text;
                                hl.zkhl = row.Elements.Find(x => x.ID == "tb_zkhl").Text;
                                hl.dglb = row.Elements.Find(x => x.ID == "tb_dglb").Text;
                                hl.hlzd = row.Elements.Find(x => x.ID == "tb_hlzd").Text;
                                hl.nl = row.Elements.Find(x => x.ID == "tb_nl").Text;
                                hl.wy = row.Elements.Find(x => x.ID == "tb_wy").Text;
                                hl.bqhlcontent = row.Elements.Find(x => x.ID == "tb_bqhlcontent").Text;
                                hl.hsqm = row.Elements.Find(x => x.ID == "tb_hsqm").Text;
                                hl.CreatorCode = UserIdentity.rygh;
                                hl.OrganizeId = this.OrganizeId;
                                hl.blId = blId;
                                hllist.Add(hl);
                            }

                        }
                    }

                    if (hllist.Count > 0)
                    {
                        _medicalRecordDmnService.TbDataSavebyWriter(hllist, "", "");
                    }
                }
            }
        }

        #endregion

        #region  Lớp nổi bên cạnh trình soạn thảo bệnh án
        public IActionResult GetLisSqdhGridJson(string mzzyh, string type, string ztmc, string kssj, string jssj)
        {
            var data = _medicalRecordDmnService.GetLisSqdhData(this.OrganizeId, mzzyh, type, ztmc, kssj, jssj);
            return Content(data.ToJson());
        }

        public IActionResult GetPacsSqdhGridJson(string mzzyh, string type, string ztmc, string kssj, string jssj)
        {
            var data = _medicalRecordDmnService.GetPacsSqdhData(this.OrganizeId, mzzyh, type, ztmc, kssj, jssj);
            return Content(data.ToJson());
        }

        public IActionResult GetLisSqdhMxGridJson(string zyh, string lissqdh)
        {
            var data = _medicalRecordDmnService.GetLisSqdhMxData(zyh, lissqdh, this.OrganizeId);
            return Content(data.ToJson());
        }

        public IActionResult GetPacsSqdhMxGridJson(string sqdh)
        {
            var data = _medicalRecordDmnService.GetPacsSqdhMxData(sqdh, this.OrganizeId);
            return Content(data.ToJson());
        }

        public IActionResult YZForm()
        {
            return View();
        }

        /// <summary>
        /// Truy vấn y lệnh
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public IActionResult AdviceGridView(Pagination pagination, AdviceListRequestVO req)
        {
            req.orgId = OrganizeId;
            var data = new
            {
                rows = _medicalRecordDmnService.AdviceGridView(pagination, req),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        #endregion

        #region  Hợp nhất nhiều bệnh án
        //Hợp nhất diễn biến bệnh
        public IActionResult MergeView(string jzbl, string blid, string bllx, string blid2, string bllx2, string blxtmc_yj, string zyh, string message, string mzh = null)
        {

            //1.Theozyh Đọc tất cả bản ghi diễn biến bệnh
            OperatorModel user = this.UserIdentity;
            var list = _ZybrjbxxDmnService.GetPatMedRecordTree(this.OrganizeId, zyh, user.rygh).OrderBy(p => p.Blrq).ToList();//Cây danh sách bản ghi diễn biến bệnh
            var urlList = new List<string>();//Diễn biến bệnhurl Địa chỉ

            //Thiết lậpxml Đường dẫn
            string currentFileName = "";//Đường dẫn đọc bản ghi diễn biến bệnh đã hợp nhất
            string fileUrl = WebPath.Map("~/file/住院病历/Merge/" + zyh);//文件夹路径
            if (!Directory.Exists(fileUrl))
            {//Tạo thư mục
                Directory.CreateDirectory(fileUrl);
            }
            string saveUrl = fileUrl + "/" + zyh + "病程记录合并_.xml";

            // Tạo một ... đã hợp nhất document
            XmlDocument result = new XmlDocument();
            // Tạo phần tử gốc
            XmlElement root = result.CreateElement("root");
            result.AppendChild(root);

            //Lấy loại bệnh án
            var bllxList = _CommonDmnService.GetBllxListDetail(OrganizeId).Where(p => p.bllxmc == "Ho so dien bien benh" && p.ParentId != null).Select(p => p.bllx).ToList();

            foreach (var obj in list)
            {

                //if (bllxList.IndexOf(obj.bllx) != -1 && obj.ctrlLevel != -1 && bllxList.IndexOf(obj.parentbllx) == -1)
                if ((obj.bllx == "2" || obj.bllx == "2019") && obj.ctrlLevel != -1 && (obj.parentbllx != "2011" || obj.parentbllx != "2012"))
                {
                    //Ghi chép diễn biến bệnh
                    var date = obj.Blrq.ToDate();
                    string _month = "00" + date.Month.ToString();
                    _month = _month.Substring(_month.Length - 2);
                    string _day = "00" + date.Day.ToString();
                    _day = _day.Substring(_day.Length - 2);
                    var url = WebPath.Map("~/file/住院病历/" + date.Year + "/" + _month + "/" + _day + "/" + obj.zyh + "/" + obj.Name + ".xml");
                    urlList.Add(url);
                    XmlDocument doc = new XmlDocument();
                    doc.Load(url);

                    XmlElement rootA = doc.DocumentElement;
                    foreach (XmlNode node in rootA.ChildNodes)
                    {
                        // Nhập nút trước
                        XmlNode n = result.ImportNode(node, true);
                        // Sau đó，chèn vào vị trí đã chỉ định
                        root.AppendChild(n);
                    }

                }
            }

            //XTextDocument xTextDocument = new XTextDocument();
            //var xml = System.IO.File.ReadAllText(urlList[0]);
            //xTextDocument.LoadFromString(xml, "xml");
            ////XTextDocument doc_ch = new XTextDocument();
            //for (int i = 1; i < urlList.Count; i++)
            //{
            //	xTextDocument.LoadUseAppendModeFromString(System.IO.File.ReadAllText(urlList[i]), "xml");
            //	//xTextDocument.Body.AppendContentElement(doc_ch);

            //}

            //xTextDocument.RefreshDocument();
            //xTextDocument.SaveToFile(saveUrl, "xml");

            //2.Hợp nhất nhiều bản ghi diễn biến bệnh và lưu tại máy cục bộ
            switch (urlList.Count)
            {
                case 0:
                    break;
                case 1://Đọc trực tiếp địa chỉ bản ghi diễn biến bệnh
                    currentFileName = urlList[0];
                    break;
                case 2://Hợp nhất2bản ghi diễn biến bệnh
                    MergeXML(urlList[0], urlList[1], saveUrl);
                    break;
                default://Hợp nhất hai bản ghi diễn biến bệnh đầu tiên，lấy bản ghi diễn biến bệnh đã lưu hợp nhất lần lượt với các bản ghi còn lại
                    MergeXML(urlList[0], urlList[1], saveUrl);
                    for (var i = 2; i < urlList.Count; i++)
                    {
                        MergeXML(saveUrl, urlList[i], saveUrl);
                    }
                    break;
            }

            //3.Mở bản ghi diễn biến bệnh
            WebWriterControlEngine eng;
            if (jzbl != "" && jzbl != null)
            {
                if (engine == null)
                {
                    eng = GetControlEngine();
                }
                else
                {
                    eng = engine;
                }
                ViewBag.mbqx = (int)EnummbqxFp.edit;
                string Getaddress = WebPath.Map("~/File/BackupsBL/" + zyh + "/" + blid + ".xml");
                eng.LoadDocument(Getaddress, null);
                eng.Options.ContentRenderMode = WebWriterControlRenderMode.PagePreviewHtml;
                ViewBag.WriterControlHtml = eng.GetAllContentHtml();
                eng.Dispose();
                ViewBag.message = message;
                ViewBag.blid = blid2;
                ViewBag.bllx = bllx2;
                ViewBag.zyh = zyh;
                ViewBag.mzh = mzh;
                ViewBag.blxtmc_yj = blxtmc_yj;
                ViewBag.ContentRenderMode = eng.Options.ContentRenderMode;
            }
            else
            {

                if (engine == null)
                {
                    eng = GetControlEngine();
                }
                else
                {
                    eng = engine;
                }

                currentFileName = currentFileName == "" ? saveUrl : currentFileName;
                //string currentFileName = "D:/Code/emr/Code/Newtouch.EMR/Newtouch.EMR.Web/File/Nội trú病历/Merge/03192/03192病程记录_合并.xml";
                if (System.IO.File.Exists(currentFileName) == false)//Nếu không tồn tại thì mở mẫu mặc định
                {
                    currentFileName = WebPath.Map(BlTemplatePath + "newFile.xml");
                }
                // Tải mẫu
                eng.LoadDocument(currentFileName, null);
                eng.Options.ContentRenderMode = WebWriterControlRenderMode.PagePreviewHtml;

                ViewBag.WriterControlHtml = eng.GetAllContentHtml();

                eng.Dispose();
                ViewBag.message = message;
                ViewBag.blxtmc_yj = blxtmc_yj;
                ViewBag.ContentRenderMode = eng.Options.ContentRenderMode;
            }
            return View();
        }

        // Merge two record XML documents by appending B's content nodes into A.
        // (Original used DCSoft XTextDocument append mode; System.Xml gives the
        // same result for these plain record documents.)
        public void MergeXML(string readUrlA, string readUrlB, string saveUrl)
        {
            var docA = new System.Xml.XmlDocument();
            docA.Load(readUrlA);
            var docB = new System.Xml.XmlDocument();
            docB.Load(readUrlB);
            if (docA.DocumentElement != null && docB.DocumentElement != null)
            {
                foreach (System.Xml.XmlNode child in docB.DocumentElement.ChildNodes)
                {
                    docA.DocumentElement.AppendChild(docA.ImportNode(child, true));
                }
            }
            docA.Save(saveUrl);
        }
        

        public IActionResult UpdateLockzt(string blid)
        {
            var data = _medicalRecordDmnService.updateLock(this.OrganizeId, blid, this.UserIdentity.UserName);
            //if (data != 0)
            //{
            return Success();
            //}
            //else
            //{
            //return Error("Sửa trạng thái khóa thất bại");chỉ áp dụng cho phiếu bản ghi điều dưỡng
            //}
        }
        #endregion

        #region  Trình soạn thảo phiên bản mới
        public IActionResult MedicalRecordEdiForAddV2()
        {
            return View();
        }
        public IActionResult MedicalRecordEditV2()
        {
            return View();
        }
        //Tải mẫu bệnh án
        public IActionResult LoadTemplateHtml(string mbbh, string zyh, string mzh = null)
        {
            //TheoidLấy mẫu
            //Nếu không tìm thấy xml để tải thì mặc định là xml trống
            BlTemplateVo vo = new BlTemplateVo();
            string currentFileName = WebPath.Map(BlTemplatePath + "newFile.html");
            var mb = _bl_mblbRepo.GetTemplateById(mbbh);
            if (mb != null && !string.IsNullOrWhiteSpace(mb.mblj)) {
                mb.mblj = mb.mblj.Replace("xml", "html");
                currentFileName = WebPath.Map(mb.mblj);
                // Tải mẫu
                if (System.IO.File.Exists(currentFileName) == false)//Nếu không tồn tại thì mở mẫu mặc định
                {
                    currentFileName = WebPath.Map(BlTemplatePath + "newFile.html");
                }
                if (mb.bllx == "5")
                {
                    vo.basyVo= GetBasyByZYH(zyh);
                }
                else if (!string.IsNullOrWhiteSpace(mzh))
                {
                    vo.mzjbxx= GetOutpatbyMzh(mzh);
                }
                else
                {
                    vo.zybrxx= getBlZybrjbxx(zyh, true);
                }
            }
            string xml = System.IO.File.ReadAllText(currentFileName);
            vo.xmldata = xml;
            vo.templateId = mb.Id;
            vo.templateName = mb.mbmc;
            return Content(vo.ToJson());
        }
        //Lưu bệnh án
        public IActionResult SavePatientBl(List<Blyyjgh> docData, string mbbh, string zyh, string mzh = null, string submitData=null )
        {
            string ywid = zyh.Trim();
            if (!string.IsNullOrWhiteSpace(mzh))
            {
                ywid = mzh.Trim();
            }
            string DataSource = "";
            var mbety = _bl_mblbRepo.FindEntity(mbbh);
            if (mbety != null && mbety.EnableDataLoad == "1" && !string.IsNullOrWhiteSpace(mbety.DataSource))
            {
                DataSource = mbety.DataSource;
            }
            string path = BlFilePath + DateTime.Now.ToString("yyyy/MM/dd/") + ywid + "/";//win10máy tính có vấn đề
            path = $"{BlFilePath}{DateTime.Now.ToString("yyyy")}/{DateTime.Now.ToString("MM")}/{DateTime.Now.ToString("dd")}/{ywid}/";
            IsExistDir(path);
            string BLMC = DateTime.Now.ToString("ddHHmm") + mbety.mbmc.Replace("模板", "").Trim();
            byte[] decodedBytes = Convert.FromBase64String(submitData);
            string xmlConten = Encoding.UTF8.GetString(decodedBytes);
            string currentFileName = WebPath.Map(path);
            System.IO.File.WriteAllText(currentFileName + BLMC + ".html", xmlConten);
            string BLID = "";
            if (!string.IsNullOrWhiteSpace(mzh))
            {
                BLID = _medicalRecordDmnService.BL_Save(OrganizeId, mbety.bllx, mbbh, path, BLMC, this.UserIdentity, null, mzh,".html");
            }
            else
            {
                BLID = BL_Save(mbety.bllx, mbbh, zyh, path, BLMC, DataSource, xmlConten,".html");
                _medicalRecordDmnService.BLJG_Delete(BLID, OrganizeId);
                List<bl_ysjgnrEntity> bljgnrEntity = new List<bl_ysjgnrEntity>();
                if (docData!=null) {
                    foreach (var res in docData)
                    {
                        bl_ysjgnrEntity jget = new bl_ysjgnrEntity();
                        jget.blid = BLID;
                        jget.zyh = zyh;
                        jget.bllx = mbety.bllx;
                        jget.yscode = res.keyCode;
                        jget.ysvalue = res.keyValue ?? "";
                        jget.ysmc = res.keyName;
                        jget.ysid = res.keyId;
                        jget.OrganizeId = this.OrganizeId;
                        bljgnrEntity.Add(jget);
                    }
                }
                if (bljgnrEntity.Count > 0)
                    _medicalRecordDmnService.BLJG_Save(bljgnrEntity);
            }
            var data = new
            {
                BLID = BLID,
                BLLX = mbety.bllx,
                BLLJ= path,
                blxtmc_yj= BLMC
            };
            return Content(data.ToJson());
        }
        //Tải bệnh án của bệnh nhân
        public IActionResult LoadMedicalRecordHtml(string blid, string bllx, string zyh, string mzh = null, string submitData = null)
        {
            //Nếu không tìm thấy mẫu html để tải thì mặc định là html trống
            string currentFileName = WebPath.Map(BlTemplatePath + "newFile.html");
            string bllj = "";
            string blxtmc_yj = "";
            bool lockd = false; //Khóa chiếm dụng khi viết bệnh án
            //Sao lưu lịch sử
            string nameblxtmc = "";
            string lujing = "";
            string Getaddress = WebPath.Map(BlBackupFilePath);

            medicalRecordVO mr = new medicalRecordVO();
            if (!string.IsNullOrWhiteSpace(mzh))
            {
                mr = _medicalRecordDmnService.GetMedicalRecordbyId(blid, bllx);
            }
            else
            {
                mr = _medicalRecordDmnService.GetMedicalRecord(blid, bllx);
            }
            bllj = mr.blxtml;
            blxtmc_yj = mr.blxtmc_yj;
            //Tên tệp sao lưu
            nameblxtmc = DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + zyh + "-" + mr.blxtmc_yj;
            lujing = mr.blxtml;
            if (mr.IsLock == 1 && mr.LastModifierCode != this.UserIdentity.UserCode)
                lockd = true;
            currentFileName = WebPath.Map(mr.blxtml + mr.blxtmc_yj.Trim() + ".html");
            if (System.IO.File.Exists(currentFileName) == false)//Nếu không tồn tại thì mở mẫu mặc định
            {
                currentFileName = WebPath.Map(BlTemplatePath + "newFile.xml");
            }
            // Tải mẫu
            string htmldata = System.IO.File.ReadAllText(currentFileName);
            LockRecord(blid, bllx, 1);
            if (!string.IsNullOrWhiteSpace(nameblxtmc.Trim()))
            {
                //Phán đoán đường dẫn có tồn tại hay không，Tạo thư mục
                string path = Getaddress + zyh;
                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }
                path = path + "\\";
                //Lưu địa chỉ tệp sao lưu
                System.IO.File.WriteAllText(path + nameblxtmc.Trim() + ".html", htmldata);
            }
            BlTemplateVo vo = new BlTemplateVo();
            vo.xmldata = htmldata;
            vo.templateId = blid;
            vo.templateName = mr.blxtmc_yj;
            vo.lockStatus= lockd;
            vo.bllj = bllj;
            return Content(vo.ToJson());
        }
        //Cập nhật bệnh án
        public IActionResult UpdatePatientBl(List<Blyyjgh> docData, string blid,string bllx,string bllj,string blxtmc_yj, string zyh, string mzh = null, string submitData = null)
        {
            string currentFileName = WebPath.Map(bllj);
            byte[] decodedBytes = Convert.FromBase64String(submitData);
            string xmldata = Encoding.UTF8.GetString(decodedBytes);
            blxtmc_yj = blxtmc_yj.Trim();
            System.IO.File.WriteAllText(currentFileName + blxtmc_yj.Trim() + ".html", xmldata);
            var medicalRecord = new medicalRecordVO();
            medicalRecord.ID = blid;
            medicalRecord.bllx = bllx;
            medicalRecord.blxtmc_hj = blxtmc_yj;
            medicalRecord.blxtmc_yj = blxtmc_yj;
            medicalRecord.LastModifierCode = this.UserIdentity.UserCode;
            if (!string.IsNullOrWhiteSpace(mzh))
            {
                _medicalRecordDmnService.MedicalRecordSaveMz(medicalRecord, null);
            }
            else {
                _medicalRecordDmnService.MedicalRecordSave(medicalRecord, null);
                var relEty = _ZymeddocsrelationRepo.FindEntity(p => p.blId == blid && p.OrganizeId == this.OrganizeId && p.zyh == zyh);
                relEty.LastModifyTime = DateTime.Now;
                relEty.LastModifierCode = this.UserIdentity.rygh;
                relEty.OldXmlConten = relEty.XmlConten;
                relEty.XmlConten = xmldata;

                _medicalRecordDmnService.BLJG_Delete(blid, OrganizeId);
                List<bl_ysjgnrEntity> bljgnrEntity = new List<bl_ysjgnrEntity>();
                if (docData != null) {
                    foreach (var res in docData)
                    {
                        bl_ysjgnrEntity jget = new bl_ysjgnrEntity();
                        jget.blid = blid;
                        jget.zyh = zyh;
                        jget.bllx = bllx;
                        jget.yscode = res.keyCode;
                        jget.ysvalue = res.keyValue ?? "";
                        jget.ysmc = res.keyName;
                        jget.ysid = res.keyId;
                        jget.OrganizeId = this.OrganizeId;
                        bljgnrEntity.Add(jget);
                    }
                }
                if (bljgnrEntity.Count > 0)
                    _medicalRecordDmnService.BLJG_Save(bljgnrEntity);
                _ZymeddocsrelationRepo.Update(relEty);
                //string mbId = relEty.mbId;
            }
            return Success();
        }

        public IActionResult LoadHistoryBackBl(string zyh,string backValue,string doctype)
        {
            if (!string.IsNullOrWhiteSpace(backValue) && backValue.Contains("|"))
            {
                string nameblxtmc = backValue.Split('|')[0];
                string blid = backValue.Split('|')[1];
                //Tìm bản sao lưu
                string backuplj = WebPath.Map(BlBackupFilePath + zyh + "/" + nameblxtmc.Trim() + doctype);
                if (System.IO.File.Exists(backuplj) == false)//Nếu không tồn tại thì mở mẫu mặc định
                {
                    backuplj = WebPath.Map(BlTemplatePath + "newFile"+ doctype);
                }
                // Tải mẫu
                string htmldata = System.IO.File.ReadAllText(backuplj);
                var data = new
                {
                    htmldata = htmldata
                };
                return Content(data.ToJson());
            }
            else
            {
                return Error("Bản sao lưu lịch sử không hợp lệ");
            }
        }
        #endregion
    }
}