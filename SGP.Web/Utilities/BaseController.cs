using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SGP.Web.Utilities
{
    [Authorize]
    [AutoValidateAntiforgeryToken] 
    public class BaseController : Controller
    {
    }
}
