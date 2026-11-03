

using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.EMR.Domain.IDomainServices;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.EMR.Domain.ValueObjects;
using Newtouch.EMR.DomainServices;
using Newtouch.EMR.Infrastructure;
using Newtouch.EMR.Repository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Attributes;

namespace Newtouch.EMR.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
   
    public class YzPreViewController : OrgControllerBase//Controller
    {
        private readonly ICommonDmnService _CommonDmnService;
        private readonly IBlmblbRepo _blmbRepo;

        public YzPreViewController(ICommonDmnService commonDmnService, IBlmblbRepo blmbRepo)
        {
            this._CommonDmnService = commonDmnService;
            this._blmbRepo = blmbRepo;
        }

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


        public IActionResult YzPreView()
        {
            //DCwriterServer dcControl = new DCwriterServer();
            //dcControl.Init();
            //WebWriterControlEngine eng = dcControl.DCWControl;//Đây là engine được tạo ở bước trước 
            //ViewBag.WriterControlHtml = eng.GetAllContentHtml();
            //eng.Dispose();
            return View();
        }

        #region In tiếp y lệnh
        public IActionResult GetZyCqYzData(string zyh, string orgId, string isSign)
        {
            var data = _CommonDmnService.GetPrintCqyzData(zyh, orgId, isSign);
            return Content(data.ToJson());
        }

        public IActionResult GetZyLsYzData(string zyh, string orgId, string isSign)
        {
            var data = _CommonDmnService.GetPrintLsyzData(zyh, orgId, isSign);
            return Content(data.ToJson());
        }

        public IActionResult GetInpatInfo(string zyh, string orgId)
        {
            orgId= orgId==null? this.OrganizeId:orgId;
            var data = _CommonDmnService.GetInpatInfo(zyh, orgId);
            return Content(data.ToJson());
        }
        #endregion

        #region In phiếu điều dưỡng
    
        public IActionResult GetHljldData(string zyh,string hljb)
        {
            var data = _CommonDmnService.GetPrintHljlData(zyh, this.OrganizeId, hljb);
            return Content(data.ToJson());
        }

        public IActionResult GetPrintTempleate(string templateName)
        {
            var data = _blmbRepo.FindEntity(p => p.mbmc == templateName && p.OrganizeId == this.OrganizeId && p.zt == "1");
            if (data == null || string.IsNullOrWhiteSpace(data.mblj))
            {
                return Error("Không tìm thấy mẫu in.");
            }
            string filename = data.mblj;
            string ml = AppDomain.CurrentDomain.BaseDirectory;

            ml = ml.Substring(0, ml.Length - 1);
            var readPath = $"{ml}{filename}";
            if (!IsTemplatePathWithinBase(readPath))
            {
                return Error("Đường dẫn mẫu không hợp lệ.");
            }
            string xml = System.IO.File.ReadAllText(readPath);
            BlTemplateVo vo = new BlTemplateVo();
            vo.xmldata = xml;
            return Content(vo.ToJson());
            //return Content(data.ToJson()); ;
        }
        #endregion

        // Fail-closed containment cho đường dẫn mẫu (vòng 179, cùng mẫu vòng 145):
        // mọi đường dẫn file suy ra từ mblj trong DB phải nằm trong thư mục gốc
        // của ứng dụng; đường dẫn thoát ra ngoài (../, tuyệt đối) bị từ chối
        // trước mọi thao tác đọc. Lưu ý: GetFullPath giữ nguyên dấu / cuối của
        // BaseDirectory nên phải trim trước khi nối thêm separator, nếu không
        // mọi đường dẫn trong thư mục gốc đều bị từ chối sai.
        [NonAction]
        private string TemplateBaseDir()
        {
            var baseDir = System.IO.Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);
            if (baseDir.Length > 1)
            {
                baseDir = baseDir.TrimEnd(System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar);
            }
            return baseDir;
        }

        [NonAction]
        private bool IsTemplatePathWithinBase(string candidatePath)
        {
            var baseDir = TemplateBaseDir();
            var fullPath = System.IO.Path.GetFullPath(candidatePath);
            if (fullPath.Length > 1)
            {
                fullPath = fullPath.TrimEnd(System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar);
            }
            return fullPath.Length > baseDir.Length
                && fullPath.StartsWith(baseDir + System.IO.Path.DirectorySeparatorChar, StringComparison.Ordinal);
        }
    }
}