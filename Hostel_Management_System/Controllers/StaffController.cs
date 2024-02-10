using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Hostel_Management_System.DAL;

namespace Hostel_Management_System.Controllers
{
    public class StaffController : Controller
    {
       
        public IConfiguration Configuration;

        MST_Notice_DAL dalMST_Notice = new MST_Notice_DAL();

        public StaffController (IConfiguration configuration)
        {
            
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
                { "VisitorCount", Convert.ToInt32(dataTable.Rows[0]["VisitorCount"]) }

            };

            ViewBag.DashboardData = dataDictionary;

            DataTable dt = dalMST_Notice.PR_MST_Notice_SelectAll();
            return View("~/Views/Home/Staff.cshtml", dt);
        }
    }
}
