using Microsoft.AspNetCore.Mvc;

namespace Hostel_Management_System.Areas.MST_Payment.Controllers
{
    public class Stripe_PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}