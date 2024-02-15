using Hostel_Management_System.Areas.MST_Visitor.Models;
using Hostel_Management_System.BAL;
using Hostel_Management_System.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Hostel_Management_System.Areas.MST_Visitor.Controllers
{
    [CheckAccess]
    [Area("MST_Visitor")]
    [Route("MST_Visitor/{Controller}/{action}")]
    public class MST_VisitorController : Controller
    {
        MST_Visitor_DAL dalMST_Visitor = new MST_Visitor_DAL();

        #region Configuration
        public IConfiguration Configuration;
        public MST_VisitorController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        public IActionResult Index()
        {
            DataTable dt = dalMST_Visitor.PR_MST_Visitor_SelectAll();
            List<MST_VisitorModel> modelList = ConvertDataTableToList(dt);

            // Implement logic to select all visitors
            foreach (var visitor in modelList)
            {
                visitor.IsSelected = false;
            }
            return View("MST_Visitor_List", modelList);
        }

        // Helper method to convert DataTable to List<YourModel>
        private List<MST_VisitorModel> ConvertDataTableToList(DataTable dt)
        {
            List<MST_VisitorModel> modelList = new List<MST_VisitorModel>();

            foreach (DataRow row in dt.Rows)
            {
                MST_VisitorModel model = new MST_VisitorModel
                {
                    VisitorID = row["VisitorID"] != DBNull.Value ? (int?)row["VisitorID"] : null,
                    VisitorName = row["VisitorName"].ToString(),
                    MobileNo = row["MobileNo"].ToString(),
                    Remark = row["Remark"].ToString(),
                    IsSelected = true, // Default to false initially
                    DateIN = Convert.ToDateTime(row["DateIN"]),
                    DateOUT = Convert.ToDateTime(row["DateOUT"]),
                    Created = Convert.ToDateTime(row["Created"]),
                    Modified = Convert.ToDateTime(row["Modified"])
                };

                modelList.Add(model);
            }
            return modelList;
        }

        [HttpPost]
        public IActionResult MultipleDelete(List<MST_VisitorModel> model)
        {
            var selectedIds = model.Where(m => m.IsSelected).Select(m => m.VisitorID.ToString()).ToList();
            string commaSeparatedIds = string.Join(",", selectedIds);

            if (!string.IsNullOrEmpty(commaSeparatedIds))
            {
                if (Convert.ToBoolean(dalMST_Visitor.PR_MST_Visitor_MultipleDelete(commaSeparatedIds)))
                {
                    TempData["DeleteSuccess"] = "Records Deleted Successfully";
                }
                else
                {
                    TempData["DeleteSuccess"] = "Error deleting records";
                }
            }
            else
            {
                TempData["InfoMessage"] = "No records selected for deletion";
            }

            return RedirectToAction("Index");
        }



        #region Add
        public IActionResult Add(int? VisitorID)
        {
            #region SelectByPK
            if (VisitorID != null)
            {
                DataTable dt = dalMST_Visitor.PR_MST_Visitor_SelectByID(VisitorID);

                MST_VisitorModel modelMST_Visitor = new MST_VisitorModel();

                foreach (DataRow row in dt.Rows)
                {
                    modelMST_Visitor.VisitorName = row["VisitorName"].ToString();
                    modelMST_Visitor.Remark= row["Remark"].ToString();
                    modelMST_Visitor.MobileNo = row["MobileNo"].ToString();
                    modelMST_Visitor.DateIN = Convert.ToDateTime(row["DateIN"]);
                    modelMST_Visitor.DateOUT = Convert.ToDateTime(row["DateOUT"]);
                }
                return View("MST_VisitorAddEdit", modelMST_Visitor);
            }

            return View("MST_VisitorAddEdit");
            #endregion
        }
        #endregion

        #region Save(Insert/Update)
        public IActionResult Save(MST_VisitorModel modelMST_Visitor)
        {
            if (modelMST_Visitor.VisitorID == null)
            {
                DataTable dt = dalMST_Visitor.PR_MST_Visitor_Insert(modelMST_Visitor.VisitorName, modelMST_Visitor.MobileNo, modelMST_Visitor.Remark, modelMST_Visitor.DateIN, modelMST_Visitor.DateOUT);
                TempData["SuccessMessage"] = "Record Inserted Successfully!!";
            }
            else
            {
                DataTable dt = dalMST_Visitor.PR_MST_Visitor_Update((int)modelMST_Visitor.VisitorID, modelMST_Visitor.VisitorName, modelMST_Visitor.MobileNo, modelMST_Visitor.Remark, modelMST_Visitor.DateIN, modelMST_Visitor.DateOUT);
                TempData["SuccessMessage"] = "Record Updated Successfully!!";
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int VisitorID)
        {
            try
            {
                if (Convert.ToBoolean(dalMST_Visitor.PR_MST_Visitor_Delete(VisitorID)))
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
