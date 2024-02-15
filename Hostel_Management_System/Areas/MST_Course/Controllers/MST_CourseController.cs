using Microsoft.AspNetCore.Mvc;
using System.Data;
using Hostel_Management_System.Areas.MST_Course.Models;
using Hostel_Management_System.DAL;
using Hostel_Management_System.BAL;

namespace Hostel_Management_System.Areas.MST_Course.Controllers
{
	[CheckAccess]
	[Area("MST_Course")]
    [Route("MST_Course/{Controller}/{action}")]
    public class MST_CourseController : Controller
    {
        MST_Course_DAL dalMST_Course = new MST_Course_DAL();

        #region Configuration
        public IConfiguration Configuration;
        public MST_CourseController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        #region SelectAllCourse
        public IActionResult Index()
        {
            DataTable dt = dalMST_Course.PR_MST_Course_SelectAll();
            return View("MST_CourseLIst", dt);

        }
        #endregion

        #region Add
        public IActionResult Add(int? CourseID) 
        {
            #region SelectByPK
            if (CourseID != null)
            {
                DataTable dt = dalMST_Course.PR_MST_Course_SelectByPk(CourseID);

                MST_CourseModel modelMST_Course = new MST_CourseModel();

                foreach (DataRow row in dt.Rows)
                {
                    modelMST_Course.CourseName = row["CourseName"].ToString();
                }
                return View("MST_CourseAddEdit", modelMST_Course);
            }
            return View("MST_CourseAddEdit");
            #endregion
        }
        #endregion

        #region Save(Insert/Update)
        public IActionResult Save(MST_CourseModel modelMST_Course)
        {
            if (modelMST_Course.CourseID == null)
            {
                DataTable dt = dalMST_Course.PR_MST_Course_Insert(modelMST_Course.CourseName);
                TempData["SuccessMessage"] = "Record Inserted Successfully!!";
            }
            else
            {
                DataTable dt = dalMST_Course.PR_MST_COURSE_UPDATE((int)modelMST_Course.CourseID,modelMST_Course.CourseName);
                TempData["SuccessMessage"] = "Record Updated Successfully!!";
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int CourseID)
        {
            try
            {
                if (Convert.ToBoolean(dalMST_Course.PR_City_DeleteByPK(CourseID)))
                {
                    TempData["DeleteSuccess"] = "Record Deleted Successfully";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["InfoMessage"] = ex.Message;
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
    }
}
