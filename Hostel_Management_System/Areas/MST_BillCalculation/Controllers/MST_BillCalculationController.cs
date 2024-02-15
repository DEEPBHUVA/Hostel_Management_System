using Hostel_Management_System.Areas.MST_BillCalculation.Models;
using Hostel_Management_System.Areas.MST_BillType.Models;
using Hostel_Management_System.BAL;
using Hostel_Management_System.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Hostel_Management_System.Areas.MST_BillCalculation.Controllers
{
    [CheckAccess]
    [Area("MST_BillCalculation")]
    [Route("MST_BillCalculation/{Controller}/{action}")]
    public class MST_BillCalculationController : Controller
    {
        MST_BillCalculation_DAL dal_MstBillCalculation = new MST_BillCalculation_DAL();

        #region Configuration
        public IConfiguration Configuration;
        public MST_BillCalculationController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        #region SelectAllBill
        public IActionResult Index()
        {
            DataTable dt = dal_MstBillCalculation.PR_MST_BillCalculation_SelectAll();
            return View("MST_Billcalculation_List", dt);
        }
        #endregion

        #region BillFilter
        public IActionResult BillFilter(DateTime FromDate, DateTime ToDate)
        {
            DataTable dt = dal_MstBillCalculation.GetBillsByDateRange(FromDate, ToDate);
            return View("MST_Billcalculation_List", dt);
        }
        #endregion

        #region Add
        public IActionResult Add(int? BillID)
        {
            #region BillType Dropdown
            string MyConnectionStr1 = this.Configuration.GetConnectionString("ConStr");
            SqlConnection connection2 = new SqlConnection(MyConnectionStr1);
            DataTable dt2 = new DataTable();
            connection2.Open();
            SqlCommand cmd2 = connection2.CreateCommand();
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.CommandText = "PR_MST_BillType_Dropdown";
            SqlDataReader reader2 = cmd2.ExecuteReader();
            dt2.Load(reader2);

            List<MST_BillType_DropDown> list1 = new List<MST_BillType_DropDown>();
            foreach (DataRow dr in dt2.Rows)
            {
                MST_BillType_DropDown dlist = new MST_BillType_DropDown();
                dlist.BillTypeID = Convert.ToInt32(dr["BillTypeID"]);
                dlist.BillType = dr["BillType"].ToString();
                list1.Add(dlist);
            }
            ViewBag.BillTypeList = list1;
            #endregion

            if (BillID != null)
            {
                DataTable dt = dal_MstBillCalculation.PR_MST_BillCalculation_SelectByPk(BillID);
                MST_BillCalculationModel model_BillCalculation = new MST_BillCalculationModel();

                foreach (DataRow row in dt.Rows)
                {
                    model_BillCalculation.BillID= Convert.ToInt32(row["BillID"]);
                    model_BillCalculation.Amount = Convert.ToDecimal(row["Amount"]);
                    model_BillCalculation.Description= row["Description"].ToString();
                    model_BillCalculation.BillDate = Convert.ToDateTime(row["BillDate"]);
                    model_BillCalculation.BillTypeID = Convert.ToInt32(row["BillTypeID"]);
                }
                return View("MST_BillCalculation_AddEdit", model_BillCalculation);
            }
            return View("MST_BillCalculation_AddEdit");
        }
        #endregion

        #region Save(Insert/Update)
        public IActionResult Save(MST_BillCalculationModel model_MSTBillCalculation)
        {
            if (model_MSTBillCalculation.BillID == null)
            {
                DataTable dt = dal_MstBillCalculation.PR_MST_BillCalculation_Insert(model_MSTBillCalculation.BillTypeID, model_MSTBillCalculation.BillDate, model_MSTBillCalculation.Description, model_MSTBillCalculation.Amount);
                TempData["SuccessMessage"] = "Record Inserted Successfully!!";
            }
            else
            {
                DataTable dt = dal_MstBillCalculation.PR_MST_BillCalculation_Update((int)model_MSTBillCalculation.BillID, model_MSTBillCalculation.BillTypeID, model_MSTBillCalculation.BillDate, model_MSTBillCalculation.Description, model_MSTBillCalculation.Amount);
                TempData["SuccessMessage"] = "Record Updated Successfully!!";
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int BillID)
        {
            try
            {
                if (Convert.ToBoolean(dal_MstBillCalculation.PR_MST_BillCalculation_Delete(BillID)))
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
