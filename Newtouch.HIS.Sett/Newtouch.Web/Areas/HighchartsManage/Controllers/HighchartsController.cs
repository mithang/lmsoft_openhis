using Newtouch.HIS.Domain.IDomainServices.ReportManage;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.HighchartsManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HighchartsController : ControllerBase
    {
        private readonly IHighchartsDmnService _highchartsDmnService;


        #region Samples
        #region 折线图
        /// <summary>
        /// Biểu đồ đường gấp khúc
        /// </summary>
        /// <returns></returns>
        public ActionResult Sample1()
        {
            return View();
        }
        #endregion

        #region 曲线图
        /// <summary>
        /// Biểu đồ đường cong
        /// </summary>
        /// <returns></returns>
        public ActionResult Sample2()
        {
            return View();
        }
        #endregion

        #region 面积图
        /// <summary>
        /// Biểu đồ diện tích
        /// </summary>
        /// <returns></returns>
        public ActionResult Sample3()
        {
            return View();
        }
        #endregion

        #region 条形图
        /// <summary>
        /// Biểu đồ thanh ngang
        /// </summary>
        /// <returns></returns>
        public ActionResult Sample4()
        {
            return View();
        }
        #endregion

        #region 柱状图
        /// <summary>
        /// Biểu đồ cột
        /// </summary>
        /// <returns></returns>
        public ActionResult Sample5()
        {
            return View();
        }
        #endregion

        #region 饼图
        /// <summary>
        /// Biểu đồ tròn
        /// </summary>
        /// <returns></returns>
        public ActionResult Sample6()
        {
            return View();
        }
        #endregion

        #region 散点图
        /// <summary>
        /// Biểu đồ phân tán
        /// </summary>
        /// <returns></returns>
        public ActionResult Sample7()
        {
            return View();
        }
        #endregion

        #region 气泡图
        /// <summary>
        /// Biểu đồ bong bóng
        /// </summary>
        /// <returns></returns>
        public ActionResult Sample8()
        {
            return View();
        }
        #endregion

        #region 模拟时钟
        /// <summary>
        /// Đồng hồ mô phỏng
        /// </summary>
        /// <returns></returns>
        public ActionResult Sample9()
        {
            return View();
        }
        #endregion

        #region 仪表盘1
        /// <summary>
        /// Bảng đồng hồ 1
        /// </summary>
        /// <returns></returns>
        public ActionResult Sample10()
        {
            return View();
        }
        #endregion

        #region 雷达图
        /// <summary>
        /// Biểu đồ radar
        /// </summary>
        /// <returns></returns>
        public ActionResult Sample11()
        {
            return View();
        }
        #endregion

        #region 蛛网图
        /// <summary>
        /// Biểu đồ mạng nhện
        /// </summary>
        /// <returns></returns>
        public ActionResult Sample12()
        {
            return View();
        }
        #endregion

        #region 玫瑰图
        /// <summary>
        /// Biểu đồ hoa hồng
        /// </summary>
        /// <returns></returns>
        public ActionResult Sample13()
        {
            return View();
        }
        #endregion

        #region 漏斗图
        /// <summary>
        /// Biểu đồ phễu
        /// </summary>
        /// <returns></returns>
        public ActionResult Sample14()
        {
            return View();
        }
        #endregion

        #region 蜡烛图
        /// <summary>
        /// Biểu đồ nến
        /// </summary>
        /// <returns></returns>
        public ActionResult Sample15()
        {
            return View();
        }
        #endregion

        #region 流程图
        /// <summary>
        /// Sơ đồ quy trình
        /// </summary>
        /// <returns></returns>
        public ActionResult Sample16()
        {
            return View();
        }
        #endregion
        #endregion
    }
}
