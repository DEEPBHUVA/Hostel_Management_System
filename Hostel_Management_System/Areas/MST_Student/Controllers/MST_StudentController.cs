using Hostel_Management_System.Areas.MST_Course.Models;
using Hostel_Management_System.Areas.MST_Room.Models;
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

        public IActionResult Delete(int StudentID)
        {
            string MyConnectionStr = this.Configuration.GetConnectionString("ConStr");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(MyConnectionStr);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_MST_Student_DeleteByPk";
            cmd.Parameters.AddWithValue("@StudentID", StudentID);
            cmd.ExecuteNonQuery();
            TempData["MST_Student_Delete_AlertMessage"] = "Record Deleted Successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Add(int? StudentID) 
        {
			#region Course Dropdown
			string MyConnectionStr1 = this.Configuration.GetConnectionString("ConStr");
			SqlConnection connection2 = new SqlConnection(MyConnectionStr1);
            DataTable dt2 = new DataTable();
            connection2.Open();
            SqlCommand cmd2 = connection2.CreateCommand();
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.CommandText = "MST_Course_Dropdown";
            SqlDataReader reader2 = cmd2.ExecuteReader();
            dt2.Load(reader2);

            List<MST_Course_DropdownModel> list1 = new List<MST_Course_DropdownModel>();
            foreach (DataRow dr in dt2.Rows)
            {
                MST_Course_DropdownModel dlist = new MST_Course_DropdownModel();
                dlist.CourseID = Convert.ToInt32(dr["CourseID"]);
                dlist.CourseName = dr["CourseName"].ToString();
                list1.Add(dlist);
            }
            ViewBag.CourseList = list1;
            #endregion

            if (StudentID != null)
            {
                string MyConnectionStr = this.Configuration.GetConnectionString("ConStr");
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(MyConnectionStr);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_MST_Student_SelectByPk";
                cmd.Parameters.AddWithValue("@StudentID", StudentID);
                SqlDataReader objSDR = cmd.ExecuteReader();
                MST_StudentModel modelMST_Student= new MST_StudentModel();

                if (objSDR.HasRows)
                {
                    while (objSDR.Read())
                    {
                        //modelMST_Student.StudentID = Convert.ToInt32(objSDR["StudentID"]);
                        modelMST_Student.StudentName = objSDR["StudentName"].ToString();
                        modelMST_Student.MobileNo = objSDR["MobileNo"].ToString();
                        modelMST_Student.Email = objSDR["Email"].ToString();
                        modelMST_Student.Age = Convert.ToInt32(objSDR["Age"]);
                        modelMST_Student.BirthDate = Convert.ToDateTime(objSDR["BirthDate"]);
                        modelMST_Student.BloodGroup = objSDR["BloodGroup"].ToString();
                        modelMST_Student.FatherMobileNo = objSDR["FatherMobileNo"].ToString();
                        modelMST_Student.FatherName = objSDR["FatherName"].ToString();
                        modelMST_Student.MotherMobileNo = objSDR["MotherMobileNo"].ToString();
                        modelMST_Student.MotherName = objSDR["MotherName"].ToString();
                        modelMST_Student.LocalGurdianName = objSDR["LocalGurdianName"].ToString();
                        modelMST_Student.LocalGurdianNo = objSDR["LocalGurdianNo"].ToString();
                        modelMST_Student.Nationlity = objSDR["Nationlity"].ToString();
                        modelMST_Student.AadharCardNo = objSDR["AadharCardNo"].ToString();
                        modelMST_Student.PermentAddress = objSDR["PermentAddress"].ToString();
                        modelMST_Student.PresentAddress = objSDR["PresentAddress"].ToString();
                        modelMST_Student.PhotoPath = objSDR["PhotoPath"].ToString();
                        modelMST_Student.isActive = objSDR["isActive"].ToString();
                        modelMST_Student.CourseID = Convert.ToInt32(objSDR["CourseID"]);
                    }
                }
                return View("MST_StudentAddEdit", modelMST_Student);
            }
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

            if (modelMST_Student.StudentID == null)
            {
                cmd.CommandText = "PR_MST_Student_Insert";
            }
            else
            {
                cmd.CommandText = "PR_MST_Student_Update";
                cmd.Parameters.AddWithValue("@StudentID", modelMST_Student.StudentID);
            }
            
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

            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if (modelMST_Student.StudentID == null)
                    TempData["MST_Student_AlertMessage"] = "Record Inserted Successfully!!";
                else
                {
                    TempData["MST_Student_AlertMessage"] = "Record Updated Successfully!!";
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Cancle()
		{
			return RedirectToAction("Index");
		}


	}
}
