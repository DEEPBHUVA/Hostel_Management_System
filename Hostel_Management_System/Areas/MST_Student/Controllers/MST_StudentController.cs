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

        public IActionResult Save(MST_StudentModel modelMST_Student)
        {
            string MyConnectionStr = this.Configuration.GetConnectionString("ConStr");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(MyConnectionStr);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_MST_Student_Insert";

            cmd.Parameters.AddWithValue("@FullName", modelMST_Student.FullName);
            cmd.Parameters.AddWithValue("@Address", modelMST_Student.Address);
            cmd.Parameters.AddWithValue("@Age", modelMST_Student.Age);
            cmd.Parameters.AddWithValue("@ContactNo", modelMST_Student.ContactNo);
            cmd.Parameters.AddWithValue("@Dob", modelMST_Student.Dob);
            cmd.Parameters.AddWithValue("@Email", modelMST_Student.Email);
            cmd.Parameters.AddWithValue("@FatherContactNo", modelMST_Student.FatherContactNo);
            cmd.Parameters.AddWithValue("@FatherName", modelMST_Student.FatherName);
            cmd.Parameters.AddWithValue("@MotherContatcNo", modelMST_Student.MotherContactNo);
            cmd.Parameters.AddWithValue("@MotherName", modelMST_Student.MotherName);
            cmd.Parameters.AddWithValue("@Photopath", modelMST_Student.Photopath);
            cmd.Parameters.AddWithValue("@Status", modelMST_Student.Status);

            cmd.ExecuteNonQuery();
            TempData["MST_Student_AlertMessage"] = "Record Inserted Successfully!!";
            return RedirectToAction("Index");
        }

		public IActionResult Cancle()
		{
			return RedirectToAction("Index");
		}
	}
}
