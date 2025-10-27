using Microsoft.AspNetCore.Mvc;
using SGP.Core.Entities.Items;
using SGP.Core.Interfaces;
using SGP.Web.Utilities;

namespace SGP.Web.Areas.Public.Controllers.Masters
{
    public class ItemCategoriesController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork; 

        public ItemCategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }
    }
}
