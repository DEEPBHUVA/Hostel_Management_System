using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Hostel_Management_System.Areas.MST_Course.Models;
using Hostel_Management_System.Areas.MST_Room.Models;

namespace Hostel_Management_System.Areas.MST_Course.Controllers
{
    [Area("MST_Course")]
    [Route("MST_Course/{Controller}/{action}")]
    public class MST_CourseController : Controller
    {
        public IConfiguration Configuration;
        public MST_CourseController(IConfiguration configuration)
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
            cmd.CommandText = "PR_MST_Course_SelectAll";
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();
            return View("MST_CourseLIst", dt);
        }

        public IActionResult Add(int? CourseID) 
        {
            if (CourseID != null)
            {
                string MyConnectionStr = this.Configuration.GetConnectionString("ConStr");
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(MyConnectionStr);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_MST_Course_SelectByPk";
                cmd.Parameters.AddWithValue("@CourseID", CourseID);
                SqlDataReader objSDR = cmd.ExecuteReader();
                MST_CourseModel modelMST_Course = new MST_CourseModel();

                if (objSDR.HasRows)
                {
                    while (objSDR.Read())
                    {
                        modelMST_Course.CourseName = objSDR["CourseName"].ToString();
                        
                    }
                }
                return View("MST_CourseAddEdit", modelMST_Course);
            }
            return View("MST_CourseAddEdit");
        }

        public IActionResult Save(MST_CourseModel modelMST_Course)
        {
            string MyConnectionStr = this.Configuration.GetConnectionString("ConStr");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(MyConnectionStr);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            if (modelMST_Course.CourseID == null)
            {
                cmd.CommandText = "PR_MST_Course_Insert";
            }
            else
            {
                cmd.CommandText = "PR_MST_COURSE_UPDATE";
                cmd.Parameters.AddWithValue("@CourseID", modelMST_Course.CourseID);

            }
            cmd.Parameters.AddWithValue("@CourseName", modelMST_Course.CourseName);

            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if (modelMST_Course.CourseID== null)
                    TempData["MST_Course_AlertMessage"] = "Record Inserted Successfully!!";
                else
                {
                    TempData["MST_Course_AlertMessage"] = "Record Updated Successfully!!";
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int CourseID)
        {
            string MyConnectionStr = this.Configuration.GetConnectionString("ConStr");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(MyConnectionStr);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_MST_Course_Delete";
            cmd.Parameters.AddWithValue("@CourseID", CourseID);
            cmd.ExecuteNonQuery();
            TempData["MST_Course_Delete_AlertMessage"] = "Record Deleted Successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Cancle()
        {
            return RedirectToAction("Index");
        }
    }
}
