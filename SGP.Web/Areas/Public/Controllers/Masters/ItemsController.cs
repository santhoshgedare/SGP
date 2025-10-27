using Microsoft.AspNetCore.Mvc;
using SGP.Web.Utilities;

namespace SGP.Web.Areas.Public.Controllers.Masters
{
    public class ItemsController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
