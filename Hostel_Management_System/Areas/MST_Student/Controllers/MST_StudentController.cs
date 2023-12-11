using Hostel_Management_System.Areas.MST_Student.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Hostel_Management_System.Areas.MST_Student.Controllers
{
    [Area("MST_Student")]
    [Route("MST_Student/{Controller}/{action}")]
    public class MST_StudentController : Controller
    {
        public IConfiguration Configuration;
        public MST_StudentController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            string MyConnectionStr = this.Configuration.GetConnectionString("ConStr");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(MyConnectionStr);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_MST_Student_SelectAll";
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();
            return View("MST_StudentList", dt);
        }

        public IActionResult Add() { 
            return View("MST_StudentAddEdit");
        }
    }
}
