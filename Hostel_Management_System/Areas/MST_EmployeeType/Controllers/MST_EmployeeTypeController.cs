using Hostel_Management_System.Areas.MST_Course.Models;
using Hostel_Management_System.Areas.MST_EmployeeType.Models;
using Hostel_Management_System.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Hostel_Management_System.Areas.MST_EmployeeType.Controllers
{
	[Area("MST_EmployeeType")]
	[Route("MST_EmployeeType/{Controller}/{action}")]
	public class MST_EmployeeTypeController : Controller
	{
		MST_EmployeeType_DAL dalMST_EmployeeType = new MST_EmployeeType_DAL();

		#region Configuration
		public IConfiguration Configuration;
		public MST_EmployeeTypeController(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		#endregion

		#region SelectAllEmployeeType
		public IActionResult Index()
		{
			DataTable dt = dalMST_EmployeeType.PR_MST_EmployeeType_SelectAll();
			return View("MST_EmployeeType_List", dt);

		}
		#endregion

		#region Add
		public IActionResult Add(int? EmployeeTypeID)
		{
			#region SelectByPK
			if (EmployeeTypeID != null)
			{
				DataTable dt = dalMST_EmployeeType.PR_MST_EmployeeType_SelectByPK(EmployeeTypeID);

				MST_EmployeeTypeModel modelMST_EmployeeType = new MST_EmployeeTypeModel();

				foreach (DataRow row in dt.Rows)
				{
					modelMST_EmployeeType.EmployeeType= row["EmployeeType"].ToString();
				}
				return View("MST_EmployeeType_AddEdit", modelMST_EmployeeType);
			}
			return View("MST_EmployeeType_AddEdit");
			#endregion
		}
		#endregion

		#region Save(Insert/Update)
		public IActionResult Save(MST_EmployeeTypeModel modelMST_EmployeeType)
		{
			if (modelMST_EmployeeType.EmployeeTypeID== null)
			{
				DataTable dt = dalMST_EmployeeType.PR_MST_EmployeeType_Insert(modelMST_EmployeeType.EmployeeType);
				TempData["MST_EmployeeType_AlertMessage"] = "Record Inserted Successfully!!";
			}
			else
			{
				DataTable dt = dalMST_EmployeeType.PR_MST_EmployeeType_Update((int)modelMST_EmployeeType.EmployeeTypeID, modelMST_EmployeeType.EmployeeType);
				TempData["MST_EmployeeType_AlertMessage"] = "Record Updated Successfully!!";
			}
			return RedirectToAction("Index");
		}
		#endregion

		#region Delete
		public IActionResult Delete(int EmployeeTypeID)
		{
			if (Convert.ToBoolean(dalMST_EmployeeType.PR_MST_EmployeeType_Delete(EmployeeTypeID)))
			{
				TempData["MST_EmployeeType_Delete_AlertMessage"] = "Record Deleted Successfully";
				return RedirectToAction("Index");
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
