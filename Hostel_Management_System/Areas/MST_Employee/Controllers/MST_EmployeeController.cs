using Hostel_Management_System.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Hostel_Management_System.Areas.MST_Employee.Controllers
{
    [Area("MST_Employee")]
    [Route("MST_Employee/{Controller}/{action}")]
    public class MST_EmployeeController : Controller
    {
        MST_Employee_DAL dalMST_Employee = new MST_Employee_DAL();

        #region Configuration
        public IConfiguration Configuration;
        public MST_EmployeeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion


        public IActionResult Index()
        {
            return View();
        }
    }
}
