using Hostel_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using Hostel_Management_System.BAL;
using Hostel_Management_System.DAL;

namespace Hostel_Management_System.Controllers
{
	[CheckAccess]
	public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public IConfiguration Configuration;

        MST_Notice_DAL dalMST_Notice = new MST_Notice_DAL();

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            string connectionString = this.Configuration.GetConnectionString("ConStr");
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("PR_CountFor_DashBoard", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();
                    dataTable.Load(reader);
                }
            }

            // Convert DataTable to Dictionary for simplicity
            Dictionary<string, int> dataDictionary = new Dictionary<string, int>
            {
                { "StudentCount", Convert.ToInt32(dataTable.Rows[0]["StudentCount"]) },
                { "RoomCount", Convert.ToInt32(dataTable.Rows[0]["RoomCount"]) },
                { "BedCount", Convert.ToInt32(dataTable.Rows[0]["BedCount"]) },
                { "AvailableBedCount", Convert.ToInt32(dataTable.Rows[0]["AvailableBedCount"]) },
                { "EmployeeCount", Convert.ToInt32(dataTable.Rows[0]["EmployeeCount"]) },

            };

            // Pass data to view using ViewBag or ViewData
            ViewBag.DashboardData = dataDictionary;

            DataTable dt = dalMST_Notice.PR_MST_Notice_SelectAll();
            return View(dt);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
