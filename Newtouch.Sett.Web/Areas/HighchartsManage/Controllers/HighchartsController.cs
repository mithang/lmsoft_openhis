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

namespace Newtouch.Sett.Web.Areas.HighchartsManage.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Area("HighchartsManage")]

    public class HighchartsController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IHighchartsDmnService _highchartsDmnService;
        public HighchartsController(IHighchartsDmnService highchartsDmnService)
        {
            _highchartsDmnService = highchartsDmnService;
        }



        #region Samples
        #region Biểu đồ đường
        /// <summary>
        /// Biểu đồ đường
        /// </summary>
        /// <returns></returns>
        public IActionResult Sample1()
        {
            return View("~/Areas/HighchartsManage/Views/Samples/Sample1.cshtml");
        }
        #endregion

        #region Biểu đồ đường cong
        /// <summary>
        /// Biểu đồ đường cong
        /// </summary>
        /// <returns></returns>
        public IActionResult Sample2()
        {
            return View("~/Areas/HighchartsManage/Views/Samples/Sample2.cshtml");
        }
        #endregion

        #region Biểu đồ diện tích
        /// <summary>
        /// Biểu đồ diện tích
        /// </summary>
        /// <returns></returns>
        public IActionResult Sample3()
        {
            return View("~/Areas/HighchartsManage/Views/Samples/Sample3.cshtml");
        }
        #endregion

        #region Biểu đồ thanh
        /// <summary>
        /// Biểu đồ thanh
        /// </summary>
        /// <returns></returns>
        public IActionResult Sample4()
        {
            return View("~/Areas/HighchartsManage/Views/Samples/Sample4.cshtml");
        }
        #endregion

        #region Biểu đồ cột
        /// <summary>
        /// Biểu đồ cột
        /// </summary>
        /// <returns></returns>
        public IActionResult Sample5()
        {
            return View("~/Areas/HighchartsManage/Views/Samples/Sample5.cshtml");
        }
        #endregion

        #region Biểu đồ tròn
        /// <summary>
        /// Biểu đồ tròn
        /// </summary>
        /// <returns></returns>
        public IActionResult Sample6()
        {
            return View("~/Areas/HighchartsManage/Views/Samples/Sample6.cshtml");
        }
        #endregion

        #region Biểu đồ phân tán
        /// <summary>
        /// Biểu đồ phân tán
        /// </summary>
        /// <returns></returns>
        public IActionResult Sample7()
        {
            return View("~/Areas/HighchartsManage/Views/Samples/Sample7.cshtml");
        }
        #endregion

        #region Biểu đồ bong bóng
        /// <summary>
        /// Biểu đồ bong bóng
        /// </summary>
        /// <returns></returns>
        public IActionResult Sample8()
        {
            return View("~/Areas/HighchartsManage/Views/Samples/Sample8.cshtml");
        }
        #endregion

        #region Đồng hồ mô phỏng
        /// <summary>
        /// Đồng hồ mô phỏng
        /// </summary>
        /// <returns></returns>
        public IActionResult Sample9()
        {
            return View("~/Areas/HighchartsManage/Views/Samples/Sample9.cshtml");
        }
        #endregion

        #region Bảng đồng hồ1
        /// <summary>
        /// Bảng đồng hồ1
        /// </summary>
        /// <returns></returns>
        public IActionResult Sample10()
        {
            return View("~/Areas/HighchartsManage/Views/Samples/Sample10.cshtml");
        }
        #endregion

        #region Biểu đồ radar
        /// <summary>
        /// Biểu đồ radar
        /// </summary>
        /// <returns></returns>
        public IActionResult Sample11()
        {
            return View("~/Areas/HighchartsManage/Views/Samples/Sample11.cshtml");
        }
        #endregion

        #region Biểu đồ mạng nhện
        /// <summary>
        /// Biểu đồ mạng nhện
        /// </summary>
        /// <returns></returns>
        public IActionResult Sample12()
        {
            return View("~/Areas/HighchartsManage/Views/Samples/Sample12.cshtml");
        }
        #endregion

        #region Biểu đồ hoa hồng
        /// <summary>
        /// Biểu đồ hoa hồng
        /// </summary>
        /// <returns></returns>
        public IActionResult Sample13()
        {
            return View("~/Areas/HighchartsManage/Views/Samples/Sample13.cshtml");
        }
        #endregion

        #region Biểu đồ phễu
        /// <summary>
        /// Biểu đồ phễu
        /// </summary>
        /// <returns></returns>
        public IActionResult Sample14()
        {
            return View("~/Areas/HighchartsManage/Views/Samples/Sample14.cshtml");
        }
        #endregion

        #region Biểu đồ nến
        /// <summary>
        /// Biểu đồ nến
        /// </summary>
        /// <returns></returns>
        public IActionResult Sample15()
        {
            return View("~/Areas/HighchartsManage/Views/Samples/Sample15.cshtml");
        }
        #endregion

        #region Lưu đồ
        /// <summary>
        /// Lưu đồ
        /// </summary>
        /// <returns></returns>
        public IActionResult Sample16()
        {
            return View("~/Areas/HighchartsManage/Views/Samples/Sample16.cshtml");
        }
        #endregion
        #endregion
    }
}
