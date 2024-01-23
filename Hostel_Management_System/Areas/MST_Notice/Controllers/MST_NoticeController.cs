using Hostel_Management_System.Areas.MST_BillType.Models;
using Hostel_Management_System.Areas.MST_Notice.Models;
using Hostel_Management_System.BAL;
using Hostel_Management_System.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Hostel_Management_System.Areas.MST_Notice.Controllers
{
	[CheckAccess]
	[Area("MST_Notice")]
    [Route("MST_Notice/{Controller}/{action}")]
    public class MST_NoticeController : Controller
    {
        MST_Notice_DAL dalMST_Notice = new MST_Notice_DAL();

        #region Configuration
        public IConfiguration Configuration;
        public MST_NoticeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        #region SelectAllNotice
        public IActionResult AllNotice()
        {
            DataTable dt = dalMST_Notice.PR_MST_Notice_SelectAll();
            return RedirectToAction("Index", "Home", new { dt });
        }
        #endregion

        #region Add
        public IActionResult Add(int? NoticeID)
        {
            #region SelectByPK
            if (NoticeID != null)
            {
                DataTable dt = dalMST_Notice.PR_MST_Notice_SelectByPK(NoticeID);

                MST_NoticeModel model_MST_Notice= new MST_NoticeModel();

                foreach (DataRow row in dt.Rows)
                {
                    model_MST_Notice.Title= row["Title"].ToString();
                    model_MST_Notice.Description = row["Description"].ToString();
                }
                return View("MST_NoticeAddEdit", model_MST_Notice);
            }

            return View("MST_NoticeAddEdit");
            #endregion
        }
        #endregion

        #region Save(Insert/Update)
        public IActionResult Save(MST_NoticeModel model_MSTNotice)
        {
            if (model_MSTNotice.NoticeID == null)
            {
                DataTable dt = dalMST_Notice.PR_MST_Notice_INSERT(model_MSTNotice.Title, model_MSTNotice.Description);
                TempData["MST_Notice_AlertMessage"] = "Record Inserted Successfully!!";
            }
            else
            {
                DataTable dt = dalMST_Notice.PR_MST_Notice_Update((int)model_MSTNotice.NoticeID, model_MSTNotice.Title, model_MSTNotice.Description);
                TempData["MST_Notice_AlertMessage"] = "Record Updated Successfully!!";
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int NoticeID)
        {
            if (Convert.ToBoolean(dalMST_Notice.PR_MST_Notice_Delete(NoticeID)))
            {
                TempData["MST_Notice_Delete_AlertMessage"] = "Record Deleted Successfully";
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Cancle
        public IActionResult Cancle()
        {
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
