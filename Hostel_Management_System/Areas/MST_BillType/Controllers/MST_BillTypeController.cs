using Hostel_Management_System.Areas.MST_BillType.Models;
using Hostel_Management_System.Areas.MST_Course.Models;
using Hostel_Management_System.BAL;
using Hostel_Management_System.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Hostel_Management_System.Areas.MST_BillType.Controllers
{
	[CheckAccess]
	[Area("MST_BillType")]
	[Route("MST_BillType/{Controller}/{action}")]
	public class MST_BillTypeController : Controller
	{
		MST_BillType_DAL dal_MST_BillType = new MST_BillType_DAL();

		#region Configuration
		public IConfiguration Configuration;
		public MST_BillTypeController(IConfiguration configuration)
		{
			Configuration = configuration;
		}
        #endregion

        #region SelectAllCourse
        public IActionResult Index()
        {
            DataTable dt = dal_MST_BillType.PR_MST_BillType_SelectAll();
            return View("MST_BillType", dt);
        }
        #endregion

        #region Add
        public IActionResult Add(int? BillTypeID)
		{
            #region SelectByPK
            if (BillTypeID != null)
            {
                DataTable dt = dal_MST_BillType.PR_MST_BillType_SelectByPk(BillTypeID);

				MST_BillTypeModel modelMST_BillType = new MST_BillTypeModel();

                foreach (DataRow row in dt.Rows)
                {
                    modelMST_BillType.BillType = row["BillType"].ToString();
                }
                return View("MST_BillType_AddEdit", modelMST_BillType);
            }

            return View("MST_BillType_AddEdit");
			#endregion
		}
		#endregion

		#region Save(Insert/Update)
		public IActionResult Save(MST_BillTypeModel modelMST_BillType)
		{
			if (modelMST_BillType.BillTypeID == null)
			{
				DataTable dt = dal_MST_BillType.PR_MST_BillType_Insert(modelMST_BillType.BillType);
				TempData["SuccessMessage"] = "Record Inserted Successfully!!";
			}
			else
			{
				DataTable dt = dal_MST_BillType.PR_MST_BillType_Update((int)modelMST_BillType.BillTypeID, modelMST_BillType.BillType);
				TempData["SuccessMessage"] = "Record Updated Successfully!!";
			}
			return RedirectToAction("Index");
		}
		#endregion

		#region Delete
		public IActionResult Delete(int BillTypeID)
		{
            try
            {
                if (Convert.ToBoolean(dal_MST_BillType.PR_MST_BillType_Delete(BillTypeID)))
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
