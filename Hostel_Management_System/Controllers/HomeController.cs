using Hostel_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace Hostel_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public IConfiguration Configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }
       
        public IActionResult DashBoard()
        {
            string MyConnection = this.Configuration.GetConnectionString("ConnectionStrings");
            List<DashBoardModel> modelDashBoard = new List<DashBoardModel>();

            SqlConnection sqlConnection = new SqlConnection(MyConnection);
            sqlConnection.Open();
            SqlCommand cmd = sqlConnection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_CountFor_DashBoard";
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                DashBoardModel data = new DashBoardModel
                {
                    StudentCount = Convert.ToInt32(reader["StudentCount"]),
                    RoomCount = Convert.ToInt32(reader["RoomCount"])
                };

                modelDashBoard.Add(data);
            }
          
            return View("Index",modelDashBoard);
        }

        public IActionResult Index()
        {
            return View();
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
