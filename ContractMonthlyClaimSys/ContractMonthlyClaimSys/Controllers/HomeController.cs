using Microsoft.AspNetCore.Mvc;

namespace ContractMonthlyClaimSys.Controllers
{
    // HomeController is responsible for managing the Home page views
    public class HomeController : Controller
    {
        // The Index action method loads the home page
        public IActionResult Index()
        {
            return View();
        }
    }
}
