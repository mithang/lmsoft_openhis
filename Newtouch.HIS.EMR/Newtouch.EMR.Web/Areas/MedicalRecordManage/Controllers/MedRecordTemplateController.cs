using NewtouchHIS.Framework.Web.Controllers;

using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IDomainServices;
using Newtouch.EMR.Domain.IRepository;

using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Attributes;
using Newtouch.EMR.Infrastructure;

using NewtouchHIS.Lib.Framework.Operator;

using System.IO;
using Newtouch.EMR.Domain.ValueObjects;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.Linq;

namespace Newtouch.EMR.Web.Areas.MedicalRecordManage
{
    [Area("MedicalRecordManage")]
    public class MedRecordTemplateController : OrgControllerBase
    {
    
        private readonly IBlmblbRepo _BlmblbRepo;
        private readonly IZybrjbxxDmnService _ZybrjbxxDmnService;
        private readonly ISysDepartmentRepo _SysDepartmentRepo;
        private readonly ISysStaffRepo _SysStaffRepo;
        private readonly IBlmblbDmnService _BlmblbDmnService;
        private readonly ICommonDmnService _CommonDmnService;

        public MedRecordTemplateController(IBlmblbRepo blmblbRepo, IZybrjbxxDmnService zybrjbxxDmnService, ISysDepartmentRepo sysDepartmentRepo, ISysStaffRepo sysStaffRepo, IBlmblbDmnService blmblbDmnService, ICommonDmnService commonDmnService)
        {
            this._BlmblbRepo = blmblbRepo;
            this._ZybrjbxxDmnService = zybrjbxxDmnService;
            this._SysDepartmentRepo = sysDepartmentRepo;
            this._SysStaffRepo = sysStaffRepo;
            this._BlmblbDmnService = blmblbDmnService;
            this._CommonDmnService = commonDmnService;
        }
        public static string BlTemplatePath = EmrConfig.GetAppConfigValue("BlTemplatePath");

        #region View
        public IActionResult FormQX()
        {
            return View();
        }
        //public IActionResult MedicalTemplate()
        //{
        //    return View();
        //}
        public IActionResult LeftMedicalTemplate()
        {
            return View();
        }
        public IActionResult RightMedicalTemplate()
        {
            return View();
        }
        public IActionResult BlTemplate()
        {
            return View();
        }
        public IActionResult testbl()
        {
            return View();
        }
        //openVer
        public IActionResult MedicalOpenEditor()
        {
            return View();
        }
        #endregion

        #region Quản lý mẫu bệnh án
        public IActionResult GetGridJson(Pagination pagination, string keyword)
        {
            OperatorModel user = this.UserIdentity;
            var data = new
            {
                rows = _BlmblbDmnService.GetBlmbList(pagination, this.OrganizeId, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }
        /// <summary>
        /// Đọc mẫu bệnh án
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="blId"></param>
        /// <param name="bllx"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(string keyValue, string blId,string bllx)
        {
            if (!string.IsNullOrWhiteSpace(blId)) //Lưu mẫu cá nhân
            {
                var mod = _BlmblbDmnService.BlConvertToTemplate(OrganizeId, blId, bllx, 1);
                var bllxDic = _CommonDmnService.GetBllxListDetail(OrganizeId, bllx).FirstOrDefault();
                BlmblbEntity data = new BlmblbEntity();
                data.bllxId = bllxDic.Id;
                data.bllx = mod.bllx;
                data.bllxmc = bllxDic.bllxmc;
                data.Isempty = mod.Isempty;
                data.mbqx = mod.mbqx;
                data.Memo = mod.blxtml + mod.blxtmc;
                data.zt = "true";
                data.mblj= EmrConfig.GetAppConfigValue("BlTemplatePath");
                data.LoadWay = Convert.ToInt32(EnummbqxTempLoadWay.DcWriter);
                if (data.mbqx == Convert.ToInt32( Enummbqx.prv))
                {
                    data.ysgh = this.UserIdentity.rygh;
                }
                return Content(data.ToJson());
            }
            else
            {
                var data = _BlmblbRepo.FindEntity(keyValue);
                return Content(data.ToJson());
            }
        }
        /// <summary>
        /// Sửa/Thêm mới mẫu
        /// </summary>
        /// <param name="blmb"></param>
        /// <param name="keyValue"></param>
        /// <param name="mbly">Nguồn mẫu</param>
        /// <returns></returns>
        public IActionResult submitForm(BlmblbEntity blmb,string keyValue,int? mbly)
        {
            blmb.OrganizeId = this.OrganizeId;
            if (blmb.Isempty == null)
            {
                blmb.Isempty = 0;
            }
            else
            {
                blmb.Isempty = 1;
            }
            if (string.IsNullOrWhiteSpace(blmb.IsYB) || blmb.IsYB == "false")
            {
                blmb.IsYB = "0";
            }
            if (string.IsNullOrWhiteSpace(blmb.EnableDataLoad) || blmb.EnableDataLoad == "false")
            {
                blmb.EnableDataLoad = "0";
            }
            if (mbly != null && mbly == 0)
            {
                blmb.mblj = blmb.mblj + blmb.mbmc+".xml";
            }
            var bllxDic = _CommonDmnService.GetBllxListDetail( OrganizeId,blmb.bllx).FirstOrDefault();
            blmb.bllxId = bllxDic.Id;
            blmb.bllxmc = bllxDic.bllxmc;

            _BlmblbRepo.SubmitForm(blmb, keyValue);

            if (mbly != null && mbly == 0)  //Dấu hiệu chuyển bệnh án thành mẫu,Thao tác di chuyển tệp
            {
                //Di chuyển tệp
                if (!string.IsNullOrWhiteSpace(blmb.Memo))
                {
                    blmb.mblj = WebPath.Map(blmb.mblj);
                    blmb.Memo = WebPath.Map(blmb.Memo);
                    // Fail-closed containment (vòng 193, cùng họ guard vòng
                    // 145/183): nguồn và đích của thao tác chuyển mẫu phải
                    // nằm trong cây thư mục ứng dụng; đường dẫn tuyệt đối
                    // ngoài cây ứng dụng hoặc ../ thoát ra ngoài bị từ chối
                    // trước khi sao chép.
                    if (!IsPathWithinAppRoot(blmb.mblj) || !IsPathWithinAppRoot(blmb.Memo))
                    {
                        return Error("Đường dẫn mẫu không hợp lệ.");
                    }
                    _BlmblbDmnService.BlConvertToTemplateProcess(blmb);
                }
            }
            return Success();
        }

        public IActionResult DeleteForm(string keyValue)
        {
            var ety = _BlmblbRepo.FindEntity(keyValue);
            ety.zt = "0";
            _BlmblbRepo.SubmitForm(ety, keyValue);
            return Success();
            ////Di chuyển tệp
            //var delpath = WebPath.Map(EmrConfig.GetAppConfigValue("BlTemplateDelPath"));
            //var cutpath = WebPath.Map(ety.mblj);
            //var flag = _BlmblbDmnService.CopyDir(cutpath, delpath, "",1);
            //if (flag == true)
            //{
            //    return Success();
            //}
            //else
            //    return Error("Xóa mẫu bất thường,Vui lòng liên hệ quản trị viên");
        }

        public IActionResult GetSysDept()
        {
            var list = _SysDepartmentRepo.GetList(OrganizeId, "1", null);
            var data = new List<object>();
            foreach (var item in list)
            {
                var obj = new
                {
                    id = item.Code,
                    text = item.Name
                };
                data.Add(obj);
            }

            return Content(data.ToJson());
        }

        public IActionResult GetSysStaff()
        {
            var data = _SysStaffRepo.GetValidStaffListByOrganizeId(OrganizeId);
            return Content(data.ToJson());
        }
        #endregion

        #region Kiểm soát quyền
        public IActionResult GetblmbDuty(string mbId)
        {
            OperatorModel user = this.UserIdentity;
            var data = _CommonDmnService.GetblmbDuty(mbId,user);
            return Content(data.ToJson());
        }
        
        public IActionResult SubmitAssigned(string list,string mbId,string mbbm)
        {
            OperatorModel user = this.UserIdentity;
           // submitForm(mbInfo, mbId, mbly);

            //Khi thêm mới lấy theo mãmbId 
            if (string.IsNullOrWhiteSpace(mbId))
            {
                mbId = _BlmblbRepo.FindEntity(p => p.mbbm == mbbm && p.OrganizeId == OrganizeId && p.zt == "1").Id;
            }
            if (!string.IsNullOrWhiteSpace(mbId))
            {
                _BlmblbDmnService.TempCtrlAssigned(list, mbId, user);
            }
            return Success();
        }
        #endregion

        public IActionResult TemplateEdit()
        {
            //TheoidLấy mẫu
            //Nếu không tìm thấy xml để tải thì mặc định là xml trống
            string currentFileName = WebPath.Map(BlTemplatePath + "newFile.xml");
            // Tải tệp
            WebWriterControlEngine eng = GetControlEngine();
            if (Request.Method != "GET")
            {
                string sd = this.Request.Form["saveDocument"];
                bool loaded = false;
                if ((sd == "Lưu tài liệu" || sd == "Lưu tài liệu") && eng.Options.ContentRenderMode == WebWriterControlRenderMode.NormalHtmlEditable)
                {
                    // Người dùng đã nhấn“Lưu tài liệu”Nút,
                    // thì thửWEBtải tài liệu từ yêu cầu rồi lưu
                    loaded = eng.LoadDocumentFromRequestFormData();
                    if (loaded)
                    {
                        string path = BlTemplatePath + DateTime.Now.ToString("yyyy/MM/dd/");
                        IsExistDir(path);
                        string BLMC = DateTime.Now.ToString("ddHHmm");

                        currentFileName = WebPath.Map(path)+ BLMC + ".xml";
                        eng.SaveDocument(currentFileName , null);              
                    }
                }
            }
            eng.LoadDocument(currentFileName, null);
            ViewBag.WriterControlHtml = eng.GetAllContentHtml();
            eng.Dispose();
            return View();
        }
        private WebWriterControlEngine GetControlEngine()
        {
            return new WebWriterControlEngine();
        }

        //Thiết lập đường dẫn tham chiếu của trình soạn thảo
        // DCSoft.Writer has no .NET Core build: this service endpoint reports the
        // limitation explicitly instead of 404. A replacement web editor is the
        // remaining EMR item.
        public IActionResult HandelDCWriterServicePage()
        {
            return Content(new AjaxResult
            {
                state = ResultType.error.ToString(),
                message = "Trình soạn thảo văn bản DCSoft chưa khả dụng trên .NET Core (không có bản build .NET Core). Cần thay bằng trình soạn thảo web khác."
            }.ToJson());
        }


        //Xử lý kết quả thao tác để tạo trình soạn thảo có nội dung tài liệu
        [NonAction]
        public void IsExistDir(string path)
        {
            if (Directory.Exists(WebPath.Map(path)) == false)//Nếu không tồn tại thì tạo thư mục file
            {
                Directory.CreateDirectory(WebPath.Map(path));
            }
        }

        #region  Phiên bản dc
        // DCSoft.Writer has no .NET Core build: this service endpoint reports the
        // limitation explicitly instead of 404. A replacement web editor is the
        // remaining EMR item.
        public IActionResult MoreHandleDCWriterServicePage()
        {
            return Content(new AjaxResult
            {
                state = ResultType.error.ToString(),
                message = "Trình soạn thảo văn bản DCSoft chưa khả dụng trên .NET Core (không có bản build .NET Core). Cần thay bằng trình soạn thảo web khác."
            }.ToJson());
        }


        public IActionResult MedicalTemplate()
        {
            //DCwriterServer dcControl = new DCwriterServer();
            //dcControl.MedicalTemplate();
            //WebWriterControlEngine eng = dcControl.DCWControl;//Đây là engine được tạo ở bước trước 
            //eng.Options.ClientContextMenuType = WebClientContextMenuType.Custom;
            //ViewBag.WriterControlHtml = eng.GetAllContentHtml();
            //eng.Dispose();
            return View();
        }

        #region  dc5.0 Cách thực hiện
        // Fail-closed containment cho đường dẫn mẫu (vòng 145): mọi đường dẫn
        // file suy ra từ templateUrl của request phải nằm trong thư mục gốc
        // của ứng dụng; đường dẫn thoát ra ngoài (../, tuyệt đối) bị từ chối
        // trước mọi thao tác đọc/ghi. Cùng pattern SettFileHelper (vòng 114).
        // Lưu ý: GetFullPath giữ nguyên dấu / cuối của BaseDirectory nên
        // phải trim trước khi nối thêm separator, nếu không mọi đường dẫn
        // trong thư mục gốc đều bị từ chối sai.
        [NonAction]
        private string TemplateBaseDir()
        {
            var baseDir = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);
            if (baseDir.Length > 1)
            {
                baseDir = baseDir.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            }
            return baseDir;
        }

        [NonAction]
        private bool IsTemplatePathWithinBase(string candidatePath)
        {
            var baseDir = TemplateBaseDir();
            var fullPath = Path.GetFullPath(candidatePath);
            if (fullPath.Length > 1)
            {
                fullPath = fullPath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            }
            return fullPath.Length > baseDir.Length
                && fullPath.StartsWith(baseDir + Path.DirectorySeparatorChar, StringComparison.Ordinal);
        }

        [NonAction]
        private bool IsTemplatePathWithinBaseOrBase(string candidatePath)
        {
            var baseDir = TemplateBaseDir();
            var fullPath = Path.GetFullPath(candidatePath);
            if (fullPath.Length > 1)
            {
                fullPath = fullPath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            }
            return string.Equals(fullPath, baseDir, StringComparison.Ordinal)
                || fullPath.StartsWith(baseDir + Path.DirectorySeparatorChar, StringComparison.Ordinal);
        }

        // Fail-closed containment theo thư mục gốc ứng dụng (vòng 193):
        // WebPath.Map giải "~/..." tương đối với thư mục làm việc hiện tại,
        // nên mọi đường dẫn "~/..." hợp lệ đều nằm trong cây này; đường dẫn
        // tuyệt đối ngoài cây ứng dụng hoặc ../ thoát ra ngoài bị từ chối.
        [NonAction]
        private bool IsPathWithinAppRoot(string candidatePath)
        {
            try
            {
                var baseDir = Path.GetFullPath(Directory.GetCurrentDirectory()).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                var fullPath = Path.GetFullPath(candidatePath);
                return fullPath.StartsWith(baseDir + Path.DirectorySeparatorChar, StringComparison.Ordinal);
            }
            catch
            {
                return false;
            }
        }

        public IActionResult GetTemplateXml(string templateUrl)
        {
            if (string.IsNullOrWhiteSpace(templateUrl))
            {
                return Error("Đường dẫn mẫu không hợp lệ.");
            }
            string[] arr = templateUrl.Split('\\');
            string filename = arr.LastOrDefault();
            string ml = AppDomain.CurrentDomain.BaseDirectory;
            var tempaleUrl = "";
            for (var item = 1; item < arr.Length; item++)
            {
                if (arr[item].Contains(".xml"))
                {
                    var xmlfile = ml + tempaleUrl + arr[item];
                    if (!System.IO.File.Exists((xmlfile)))
                    {
                        var createPath = $"{ml}{tempaleUrl}{filename}";
                        if (!IsTemplatePathWithinBase(createPath))
                        {
                            return Error("Đường dẫn mẫu không hợp lệ.");
                        }
                        System.IO.File.WriteAllText(createPath, "");
                    }
                    tempaleUrl += arr[item];
                }
                else
                {
                    var dirPath = ml + tempaleUrl + arr[item];
                    if (!System.IO.Directory.Exists(dirPath))
                    {
                        if (!IsTemplatePathWithinBaseOrBase(dirPath))
                        {
                            return Error("Đường dẫn mẫu không hợp lệ.");
                        }
                        System.IO.Directory.CreateDirectory(dirPath);
                    }
                    tempaleUrl += arr[item] + "\\";

                }
            }
            //string filename = args.FileName + ".xml";
            var readPath = $"{ml}{tempaleUrl}";
            if (!IsTemplatePathWithinBase(readPath))
            {
                return Error("Đường dẫn mẫu không hợp lệ.");
            }
            string xml = System.IO.File.ReadAllText(readPath);
            BlTemplateVo vo = new BlTemplateVo();
            vo.xmldata = xml;
            return Content(vo.ToJson());
            #region
            //string filename = templateName+".xml";
            //string ml = AppDomain.CurrentDomain.BaseDirectory;
            //if (!System.IO.Directory.Exists(ml + "File"))
            //{
            //    System.IO.Directory.CreateDirectory(ml + "File");
            //}
            //if (!System.IO.Directory.Exists(ml + "File\\YzContinueTem"))
            //{
            //    System.IO.Directory.CreateDirectory(ml + "File\\YzContinueTem");
            //}
            ////var d = ml + "File\\YzContinueTem\\" + filename;
            ////var dd = System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(d));
            //if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(ml + "File\\YzContinueTem\\"+ filename)))
            //{
            //    System.IO.Directory.CreateDirectory(ml + "File\\YzContinueTem\\" + filename);
            //    return null;
            //}
            //BlTemplateVo vo = new BlTemplateVo();
            //string xml = System.IO.File.ReadAllText($"{ml}File\\YzContinueTem\\{filename}");
            //vo.xmldata = xml;
            //return  Content(vo.ToJson()); 
            #endregion
        }

        public IActionResult SaveTemplateXml(string templateUrl,string templateData)
        {
            if (string.IsNullOrWhiteSpace(templateUrl) || !templateUrl.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
            {
                return Error("Đường dẫn mẫu không hợp lệ.");
            }
            string filename = templateUrl;
            byte[] decodedBytes = Convert.FromBase64String(templateData);
            string xmlString = Encoding.UTF8.GetString(decodedBytes);
            string ml = AppDomain.CurrentDomain.BaseDirectory;
            ml = ml.Substring(0, ml.Length - 1);
            if (!System.IO.Directory.Exists(ml + "File"))
            {
                System.IO.Directory.CreateDirectory(ml + "File");
            }
            var d = $"{ml}{filename}";
            if (!IsTemplatePathWithinBase(d))
            {
                return Error("Đường dẫn mẫu không hợp lệ.");
            }
            System.IO.File.WriteAllText(d, xmlString);
            return Success();
        }
        #endregion

        #endregion

        #region openeditor
        public IActionResult GetTemplateHtml(string templateUrl)
        {
            if (string.IsNullOrWhiteSpace(templateUrl))
            {
                return Error("Đường dẫn mẫu không hợp lệ.");
            }
            templateUrl = templateUrl.Replace("xml","html");
            string[] arr = templateUrl.Split('\\');
            string filename = arr.LastOrDefault();
            string ml = AppDomain.CurrentDomain.BaseDirectory;
            var tempaleUrl = "";
            for (var item = 1; item < arr.Length; item++)
            {
                if (arr[item].Contains(".html"))
                {
                    var xmlfile = ml + tempaleUrl + arr[item];
                    if (!System.IO.File.Exists((xmlfile)))
                    {
                        var createPath = $"{ml}{tempaleUrl}{filename}";
                        if (!IsTemplatePathWithinBase(createPath))
                        {
                            return Error("Đường dẫn mẫu không hợp lệ.");
                        }
                        System.IO.File.WriteAllText(createPath, "<body data-hm-papersize=\"A4_portrait$top:5mm#right:5mm#bottom:5mm#left:5mm\" meta_json=\"\" style=\"\" class=\"\"><table class=\"dashed-border font-normal\" style=\"width: 100%; table-layout: auto;\" data-hm-table=\"true\" hm-table-id=\"ef00d3b996d222f2e8e56ef5e19a40486\" _paperheader=\"true\"><colgroup><col style=\"width: 17.1897%;\"><col style=\"width: 37.9707%;\"><col style=\"width: 19.8396%;\"><col style=\"width: 25%;\"></colgroup><tbody><tr><td style=\"width: 100%; text-align: center;\" colspan=\"4\"><p><br></p></td></tr><tr><td style=\"width: 100%; text-align: center;\" colspan=\"4\">Tiêu đề trang</td></tr><tr><td style=\"width: 17.1897%; border-bottom: 1px solid black;\"><br></td><td style=\"width: 37.9707%; border-bottom: 1px solid black;\"><br></td><td style=\"width: 19.8396%; border-bottom: 1px solid black;\"><br></td><td style=\"width: 25%; border-bottom: 1px solid black;\"><br></td></tr></tbody></table><p><br><span data-cke-bookmark=\"1\" style=\"display: none;\">&nbsp;</span>　　</p><p><br></p><p><br></p><p><br></p><p><br></p><p><br></p><p><br></p><p><br></p><p><br></p><p><br></p><p><br></p><p><br></p><p><br></p><p><br></p><p><br></p><p><br></p><p><br></p><p><br></p><p><br></p><p><br></p><p><br></p><table class=\"dashed-border font-normal\" style=\"width: 100%; table-layout: auto;\" data-hm-table=\"true\" hm-table-id=\"e5765dc780bff98a90954d12e022e605d\" _paperfooter=\"true\"><colgroup><col style=\"width: 100%;\"></colgroup><tbody><tr><td style=\"width: 100%; text-align: center; border-top-width: 1px; border-top-style: solid;\">Trang <span contenteditable=\"false\" class=\"page\"> </span></td></tr></tbody></table></body>");
                    }
                    tempaleUrl += arr[item];
                }
                else
                {
                    var dirPath = ml + tempaleUrl + arr[item];
                    if (!System.IO.Directory.Exists(dirPath))
                    {
                        if (!IsTemplatePathWithinBaseOrBase(dirPath))
                        {
                            return Error("Đường dẫn mẫu không hợp lệ.");
                        }
                        System.IO.Directory.CreateDirectory(dirPath);
                    }
                    tempaleUrl += arr[item] + "\\";

                }
            }
            var readPath = $"{ml}{tempaleUrl}";
            if (!IsTemplatePathWithinBase(readPath))
            {
                return Error("Đường dẫn mẫu không hợp lệ.");
            }
            string xml = System.IO.File.ReadAllText(readPath);
            BlTemplateVo vo = new BlTemplateVo();
            vo.xmldata = xml;
            return Content(vo.ToJson());
        }
        public IActionResult SaveTemplateHtml(string templateUrl, string templateData)
        {
            string filename = string.IsNullOrWhiteSpace(templateUrl) ? "" : templateUrl.Replace("xml", "html");
            if (string.IsNullOrWhiteSpace(filename) || !filename.EndsWith(".html", StringComparison.OrdinalIgnoreCase))
            {
                return Error("Đường dẫn mẫu không hợp lệ.");
            }
            byte[] decodedBytes = Convert.FromBase64String(templateData);
            string xmlString = Encoding.UTF8.GetString(decodedBytes);
            string ml = AppDomain.CurrentDomain.BaseDirectory;
            ml = ml.Substring(0, ml.Length - 1);
            if (!System.IO.Directory.Exists(ml + "File"))
            {
                System.IO.Directory.CreateDirectory(ml + "File");
            }
            var d = $"{ml}{filename}";
            if (!IsTemplatePathWithinBase(d))
            {
                return Error("Đường dẫn mẫu không hợp lệ.");
            }
            System.IO.File.WriteAllText(d, xmlString);
            return Success();
        }
        #endregion
    }
}