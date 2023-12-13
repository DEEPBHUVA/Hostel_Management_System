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
            if (modelMST_Student.File != null)
            {
                string FilePath = "wwwroot\\Images";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string fileNameWithPath = Path.Combine(path, modelMST_Student.File.FileName);
                modelMST_Student.PhotoPath = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + modelMST_Student.File.FileName;

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    modelMST_Student.File.CopyTo(stream);
                }
            }

            string MyConnectionStr = this.Configuration.GetConnectionString("ConStr");
            SqlConnection conn = new SqlConnection(MyConnectionStr);
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_MST_Student_Insert";

            cmd.Parameters.AddWithValue("@StudentName", modelMST_Student.StudentName);
            cmd.Parameters.AddWithValue("@Email", modelMST_Student.Email);
            cmd.Parameters.AddWithValue("@MobileNo", modelMST_Student.MobileNo);
            cmd.Parameters.AddWithValue("@BloodGroup", modelMST_Student.BloodGroup);
            cmd.Parameters.AddWithValue("@BirthDate", modelMST_Student.BirthDate);
            cmd.Parameters.AddWithValue("@Age", modelMST_Student.Age);
			cmd.Parameters.AddWithValue("@CourseID", modelMST_Student.CourseID);
			cmd.Parameters.AddWithValue("@FatherName", modelMST_Student.FatherName);
            cmd.Parameters.AddWithValue("@FatherMobileNo", modelMST_Student.FatherMobileNo);
            cmd.Parameters.AddWithValue("@MotherName", modelMST_Student.MotherName);
            cmd.Parameters.AddWithValue("@MotherMobileNo", modelMST_Student.MotherMobileNo);
            cmd.Parameters.AddWithValue("@LocalGurdianName", modelMST_Student.LocalGurdianName);
            cmd.Parameters.AddWithValue("@LocalGurdianNo", modelMST_Student.LocalGurdianNo);
            cmd.Parameters.AddWithValue("@Nationlity", modelMST_Student.Nationlity);
			cmd.Parameters.AddWithValue("@AadharCardNo", modelMST_Student.AadharCardNo);
			cmd.Parameters.AddWithValue("@PresentAddress", modelMST_Student.PresentAddress);
			cmd.Parameters.AddWithValue("@PermentAddress", modelMST_Student.PermentAddress);
			cmd.Parameters.AddWithValue("@isActive", modelMST_Student.isActive);
			cmd.Parameters.AddWithValue("@PhotoPath", modelMST_Student.PhotoPath);

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
