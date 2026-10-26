using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtouch.PDS.API;
using Newtouch.PDS.API.Controllers;

namespace Newtouch.PDS.API.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Sắp xếp
            HomeController controller = new HomeController();

            // Thao tác
            ViewResult result = controller.Index() as ViewResult;

            // Khẳng định
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
