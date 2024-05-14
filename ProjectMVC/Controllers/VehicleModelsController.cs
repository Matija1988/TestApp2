using Microsoft.AspNetCore.Mvc;

namespace ProjectMVC.Controllers
{
    public class VehicleModelsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
