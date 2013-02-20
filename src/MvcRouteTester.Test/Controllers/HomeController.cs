//
using System.Web.Mvc;
//
using MvcRouteTester.Test.Models;

namespace MvcRouteTester.Test.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index(int id)
		{
			return new EmptyResult();
		}

        [HttpPost]
        public ActionResult Test(string url)
        {
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Test(LoginModel model, string returnUrl)
        {
            return new ViewResult();
        }
	}
}
