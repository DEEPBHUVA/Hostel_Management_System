using Hostel_Management_System.Areas.MST_Payment.Models;
using Hostel_Management_System.Areas.MST_Room.Models;
using Hostel_Management_System.Areas.MST_Student.Models;
using Hostel_Management_System.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Hostel_Management_System.Areas.MST_Payment.Controllers
{
    [Area("MST_Payment")]
    [Route("MST_Payment/{Controller}/{action}")]
    public class MST_PaymentController : Controller
    {
        MST_Payment_DAL dalMST_Payment = new MST_Payment_DAL();

        #region Configuration
        public IConfiguration Configuration;
        public MST_PaymentController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        #region SelectAllPayment
        public IActionResult Index()
        {
            DataTable dt = dalMST_Payment.PR_MST_Payment_SelectAll();
            return View("MST_PaymentList",dt);
        }
        #endregion

        #region Add
        public IActionResult Add(int? PaymentID)
        {

			#region Student Dropdown
			string MyConnectionStr1 = this.Configuration.GetConnectionString("ConStr");
			SqlConnection connection2 = new SqlConnection(MyConnectionStr1);
			DataTable dt2 = new DataTable();
			connection2.Open();
			SqlCommand cmd2 = connection2.CreateCommand();
			cmd2.CommandType = CommandType.StoredProcedure;
			cmd2.CommandText = "PR_MST_StudentDropdown";
			SqlDataReader reader2 = cmd2.ExecuteReader();
			dt2.Load(reader2);

			List<MST_StudentDropdown> list1 = new List<MST_StudentDropdown>();
			foreach (DataRow dr in dt2.Rows)
			{
				MST_StudentDropdown dlist = new MST_StudentDropdown();
				dlist.StudentID = Convert.ToInt32(dr["StudentID"]);
				dlist.StudentName = dr["StudentName"].ToString();
				list1.Add(dlist);
			}
			ViewBag.StudentList = list1;
			#endregion

			if (PaymentID != null)
            {
                DataTable dt = dalMST_Payment.PR_MST_Payment_SelectByPK(PaymentID);
                MST_PaymentModel modelMST_Payment = new MST_PaymentModel();

                foreach (DataRow row in dt.Rows)
                {
                    modelMST_Payment.StudentID = Convert.ToInt32(row["StudentID"]);
                    modelMST_Payment.Amount = Convert.ToDecimal(row["Amount"]);
                    modelMST_Payment.PaidBY = row["PaidBY"].ToString();
                    modelMST_Payment.MobileNo = row["MobileNo"].ToString();
                    modelMST_Payment.Remark = row["Remark"].ToString();
                    modelMST_Payment.BankName = row["BankName"].ToString();
                    modelMST_Payment.ChequeNo = row["ChequeNo"].ToString();
                    modelMST_Payment.PaymentDate = Convert.ToDateTime(row["PaymentDate"]);

                }
                return View("MST_PaymentAddEdit", modelMST_Payment);
            }
            return View("MST_PaymentAddEdit");
        }
        #endregion

        #region Save(Insert/Update)
        public IActionResult Save(MST_PaymentModel modelMST_Payment)
        {
            if (modelMST_Payment.PaymentID == null)
            {
                DataTable dt = dalMST_Payment.PR_MST_Payment_Insert(modelMST_Payment.StudentID, modelMST_Payment.PaymentDate, modelMST_Payment.MobileNo, modelMST_Payment.Amount,modelMST_Payment.Remark, modelMST_Payment.PaidBY,modelMST_Payment.BankName,modelMST_Payment.ChequeNo);
                TempData["MST_Payment_AlertMessage"] = "Record Inserted Successfully!!";
            }
            else
            {
                DataTable dt = dalMST_Payment.PR_MST_Payment_Update((int)modelMST_Payment.PaymentID, modelMST_Payment.StudentID, modelMST_Payment.PaymentDate, modelMST_Payment.MobileNo, modelMST_Payment.Amount, modelMST_Payment.Remark, modelMST_Payment.PaidBY, modelMST_Payment.BankName, modelMST_Payment.ChequeNo);
                TempData["MST_Payment_AlertMessage"] = "Record Updated Successfully!!";
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int PaymentID)
        {
            if (Convert.ToBoolean(dalMST_Payment.PR_MST_Payment_Delete(PaymentID)))
            {
                TempData["MST_Payment_Delete_AlertMessage"] = "Record Deleted Successfully";
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
