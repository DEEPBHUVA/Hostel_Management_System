using Hostel_Management_System.Areas.MST_Employee.Models;
using Hostel_Management_System.Areas.MST_EmployeeSalary.Models;
using Hostel_Management_System.BAL;
using Hostel_Management_System.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Hostel_Management_System.Areas.MST_EmployeeSalary.Controllers
{
	[CheckAccess]
	[Area("MST_EmployeeSalary")]
    [Route("MST_EmployeeSalary/{Controller}/{action}")]
    public class MST_EmployeeSalaryController : Controller
    {
        MST_EmployeeSalary_DAL dalMST_EmployeeSalary = new MST_EmployeeSalary_DAL();

        #region Configuration
        public IConfiguration Configuration;
        public MST_EmployeeSalaryController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        #region SelectAllEmployeeSalary
        public IActionResult Index()
        {
            DataTable dt = dalMST_EmployeeSalary.PR_MST_EmployeeSalary_SelectAll();
            return View("MST_EmployeeSalary_List", dt);
        }
        #endregion

        #region EmployeeSalary
        public IActionResult EmployeeSalary(string EmployeeName, DateTime? SalaryDate ,string PaymentMode)
        {
            DataTable dt = dalMST_EmployeeSalary.PR_MST_EmployeeSalary_Filter(EmployeeName, SalaryDate, PaymentMode);
            return View("MST_EmployeeSalary_List", dt);
        }
        #endregion

        #region Add
        public IActionResult Add(int? EmployeeSalaryID)
        {

            #region Employee Dropdown
            string MyConnectionStr1 = this.Configuration.GetConnectionString("ConStr");
            SqlConnection connection2 = new SqlConnection(MyConnectionStr1);
            DataTable dt2 = new DataTable();
            connection2.Open();
            SqlCommand cmd2 = connection2.CreateCommand();
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.CommandText = "PR_MST_Employee_Dropdown";
            SqlDataReader reader2 = cmd2.ExecuteReader();
            dt2.Load(reader2);

            List<MST_Employee_DropdownModel> list1 = new List<MST_Employee_DropdownModel>();
            foreach (DataRow dr in dt2.Rows)
            {
                MST_Employee_DropdownModel dlist = new MST_Employee_DropdownModel();
                dlist.EmployeeID = Convert.ToInt32(dr["EmployeeID"]);
                dlist.EmployeeName = dr["EmployeeName"].ToString();
                list1.Add(dlist);
            }
            ViewBag.EmployeeList = list1;
            #endregion

            if (EmployeeSalaryID != null)
            {
                DataTable dt = dalMST_EmployeeSalary.PR_MST_EmployeeSalary_SelectByPK(EmployeeSalaryID);
                MST_EmployeeSalaryModel modelMST_EmployeeModel= new MST_EmployeeSalaryModel();

                foreach (DataRow row in dt.Rows)
                {
                    modelMST_EmployeeModel.EmployeeID = Convert.ToInt32(row["EmployeeID"]);
                    modelMST_EmployeeModel.Salary= Convert.ToDecimal(row["Salary"]);
                    modelMST_EmployeeModel.PaymentMode = row["PaymentMode"].ToString();
                    modelMST_EmployeeModel.Status = Convert.ToBoolean(row["Status"]);
                    modelMST_EmployeeModel.Remark = row["Remark"].ToString();
                    modelMST_EmployeeModel.BankName = row["BankName"].ToString();
                    modelMST_EmployeeModel.ChequeNo = row["ChequeNo"].ToString();
                    modelMST_EmployeeModel.SalaryDate = Convert.ToDateTime(row["SalaryDate"]);

                }
                return View("MST_EmployeeSalary_AddEdit", modelMST_EmployeeModel);
            }
            return View("MST_EmployeeSalary_AddEdit");
        }
        #endregion

        #region Save(Insert/Update)
        public IActionResult Save(MST_EmployeeSalaryModel modelMST_EmployeeModel)
        {
            if (modelMST_EmployeeModel.EmployeeSalaryID == null)
            {
                DataTable dt = dalMST_EmployeeSalary.PR_MST_EmployeeSalary_Insert(modelMST_EmployeeModel.EmployeeID, modelMST_EmployeeModel.SalaryDate, modelMST_EmployeeModel.Salary, modelMST_EmployeeModel.Remark, modelMST_EmployeeModel.PaymentMode, modelMST_EmployeeModel.BankName, modelMST_EmployeeModel.ChequeNo,modelMST_EmployeeModel.Status);
                TempData["SuccessMessage"] = "Record Inserted Successfully!!";
            }
            else
            {
                DataTable dt = dalMST_EmployeeSalary.PR_MST_EmployeeSalary_Update((int)modelMST_EmployeeModel.EmployeeSalaryID, modelMST_EmployeeModel.EmployeeID, modelMST_EmployeeModel.SalaryDate, modelMST_EmployeeModel.Salary, modelMST_EmployeeModel.Remark, modelMST_EmployeeModel.PaymentMode, modelMST_EmployeeModel.BankName, modelMST_EmployeeModel.ChequeNo, modelMST_EmployeeModel.Status);
                TempData["SuccessMessage"] = "Record Updated Successfully!!";
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int EmployeeSalaryID)
        {
            try
            {
                if (Convert.ToBoolean(dalMST_EmployeeSalary.PR_MST_EmployeeSalary_Delete(EmployeeSalaryID)))
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
