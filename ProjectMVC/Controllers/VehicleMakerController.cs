using Microsoft.AspNetCore.Mvc;

namespace ProjectMVC.Controllers
{
    public class VehicleMakerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
