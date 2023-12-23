using Hostel_Management_System.Areas.MST_Course.Models;
using Hostel_Management_System.Areas.MST_Student.Models;
using Hostel_Management_System.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace Hostel_Management_System.Areas.MST_Student.Controllers
{
    [Area("MST_Student")]
    [Route("MST_Student/{Controller}/{action}")]
    public class MST_StudentController : Controller
    {
        MST_Student_DAL dalMST_Student = new MST_Student_DAL();

        #region Configuration
        public IConfiguration Configuration;
        public MST_StudentController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        #region SelectOnlyActiveStudent
        public IActionResult Index()
        {
            DataTable dt = dalMST_Student.PR_MST_Student_SelectAll();
            return View("MST_StudentList", dt);
        }
        #endregion

        #region SelectAllStudent
        public IActionResult GetAllStudent()
        {
            DataTable dt = dalMST_Student.PR_MST_Student_GetAllStudent();
            return View("MST_AllStudentList", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int StudentID)
        {
            if (Convert.ToBoolean(dalMST_Student.PR_MST_Student_DeleteByPk(StudentID)))
            {
                TempData["MST_Student_Delete_AlertMessage"] = "Record Deleted Successfully";
                return RedirectToAction("GetAllStudent");
            }
            return RedirectToAction("GetAllStudent");
        }
        #endregion

        #region UpdateStatus
        public IActionResult UpdateStatus(int StudentID)
        {
            if (Convert.ToBoolean(dalMST_Student.PR_Mst_StudentUpdateStatus(StudentID)))
            {
                TempData["MST_Student_Remove_AlertMessage"] = "Record removed form this list successfully!!";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Add
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
                DataTable dt = dalMST_Student.PR_MST_Student_SelectByPk(StudentID);
                MST_StudentModel modelMST_Student= new MST_StudentModel();

                foreach (DataRow row in dt.Rows)
                {
                    //modelMST_Student.StudentID = Convert.ToInt32(row["StudentID"]);
                    modelMST_Student.StudentName = row["StudentName"].ToString();
                    modelMST_Student.MobileNo = row["MobileNo"].ToString();
                    modelMST_Student.Email = row["Email"].ToString();
                    modelMST_Student.Age = Convert.ToInt32(row["Age"]);
                    modelMST_Student.BirthDate = Convert.ToDateTime(row["BirthDate"]);
                    modelMST_Student.BloodGroup = row["BloodGroup"].ToString();
                    modelMST_Student.FatherMobileNo = row["FatherMobileNo"].ToString();
                    modelMST_Student.FatherName = row["FatherName"].ToString();
                    modelMST_Student.MotherMobileNo = row["MotherMobileNo"].ToString();
                    modelMST_Student.MotherName = row["MotherName"].ToString();
                    modelMST_Student.LocalGurdianName = row["LocalGurdianName"].ToString();
                    modelMST_Student.LocalGurdianNo = row["LocalGurdianNo"].ToString();
                    modelMST_Student.Nationlity = row["Nationlity"].ToString();
                    modelMST_Student.AadharCardNo = row["AadharCardNo"].ToString();
                    modelMST_Student.PermentAddress = row["PermentAddress"].ToString();
                    modelMST_Student.PresentAddress = row["PresentAddress"].ToString();
                    modelMST_Student.PhotoPath = row["PhotoPath"].ToString();
                    modelMST_Student.isActive = row["isActive"].ToString();
                    modelMST_Student.CourseID = Convert.ToInt32(row["CourseID"]);
					modelMST_Student.Remarks = row["Remarks"].ToString();
				}
                
                return View("MST_StudentAddEdit", modelMST_Student);
            }
            return View("MST_StudentAddEdit");
        }
        #endregion

        #region Save(Insert/Update)
        public IActionResult Save(MST_StudentModel modelMST_Student)
        {
            #region PhotoPath
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
            #endregion

            if (modelMST_Student.StudentID == null)
            {
                DataTable dt = dalMST_Student.PR_MST_Student_Insert(
                    modelMST_Student.StudentName,
                    modelMST_Student.Email,
                    modelMST_Student.MobileNo,
                    modelMST_Student.BloodGroup,
                    modelMST_Student.BirthDate,
                    modelMST_Student.Age,
                    modelMST_Student.FatherName,
                    modelMST_Student.FatherMobileNo,
                    modelMST_Student.MotherName,
                    modelMST_Student.MotherMobileNo,
                    modelMST_Student.LocalGurdianName,
                    modelMST_Student.LocalGurdianNo,
                    modelMST_Student.Nationlity,
                    modelMST_Student.AadharCardNo,
                    modelMST_Student.PresentAddress,
                    modelMST_Student.PermentAddress,
                    modelMST_Student.isActive,
                    modelMST_Student.CourseID,
                    modelMST_Student.Remarks,
                    modelMST_Student.PhotoPath
                );
                TempData["MST_Student_AlertMessage"] = "Record Inserted Successfully!!";
            }
            else
            {
                DataTable dt = dalMST_Student.PR_MST_Student_Update(
                    (int)modelMST_Student.StudentID,
                    modelMST_Student.StudentName,
                    modelMST_Student.Email,
                    modelMST_Student.MobileNo,
                    modelMST_Student.BloodGroup,
                    modelMST_Student.BirthDate,
                    modelMST_Student.Age,
                    modelMST_Student.FatherName,
                    modelMST_Student.FatherMobileNo,
                    modelMST_Student.MotherName,
                    modelMST_Student.MotherMobileNo,
                    modelMST_Student.LocalGurdianName,
                    modelMST_Student.LocalGurdianNo,
                    modelMST_Student.Nationlity,
                    modelMST_Student.AadharCardNo,
                    modelMST_Student.PresentAddress,
                    modelMST_Student.PermentAddress,
                    modelMST_Student.isActive,
                    modelMST_Student.CourseID,
                    modelMST_Student.Remarks,
                    modelMST_Student.PhotoPath
                );
                TempData["MST_Student_AlertMessage"] = "Record Updated Successfully!!";
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Cancle
        public IActionResult Cancle()
		{
			return RedirectToAction("Index");
		}
        #endregion

        #region ViewProfile
        public IActionResult ViewProfile(int StudentID)
        {
            DataTable dt = dalMST_Student.PR_MST_Student_SelectByPk(StudentID);
            return View("MST_Student_ViewProfile", dt);
        }
        #endregion

    }
}
