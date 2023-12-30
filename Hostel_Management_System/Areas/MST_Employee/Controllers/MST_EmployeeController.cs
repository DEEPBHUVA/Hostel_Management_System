using Hostel_Management_System.Areas.MST_Employee.Models;
using Hostel_Management_System.Areas.MST_EmployeeType.Models;
using Hostel_Management_System.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Hostel_Management_System.Areas.MST_Employee.Controllers
{
    [Area("MST_Employee")]
    [Route("MST_Employee/{Controller}/{action}")]
    public class MST_EmployeeController : Controller
    {
        MST_Employee_DAL dalMST_Employee = new MST_Employee_DAL();

        #region Configuration
        public IConfiguration Configuration;
        public MST_EmployeeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        #region SelectAllEmployee
        public IActionResult Index()
        {
            DataTable dt = dalMST_Employee.PR_MST_Employee_SelectAll();
            return View("MST_EmployeeList", dt);

        }
        #endregion

        #region Add
        public IActionResult Add(int? EmployeeID)
        {
            #region EmployeeType Dropdown
            string MyConnectionStr1 = this.Configuration.GetConnectionString("ConStr");
            SqlConnection connection2 = new SqlConnection(MyConnectionStr1);
            DataTable dt2 = new DataTable();
            connection2.Open();
            SqlCommand cmd2 = connection2.CreateCommand();
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.CommandText = "PR_MST_EmployeeType_Dropdown";
            SqlDataReader reader2 = cmd2.ExecuteReader();
            dt2.Load(reader2);

            List<MST_EmployeeType_Dropdown> list1 = new List<MST_EmployeeType_Dropdown>();
            foreach (DataRow dr in dt2.Rows)
            {
                MST_EmployeeType_Dropdown dlist = new MST_EmployeeType_Dropdown();
                dlist.EmployeeTypeID = Convert.ToInt32(dr["EmployeeTypeID"]);
                dlist.EmployeeType = dr["EmployeeType"].ToString();
                list1.Add(dlist);
            }
            ViewBag.EmployeeTypeList = list1;
            #endregion

            if (EmployeeID != null)
            {
                DataTable dt = dalMST_Employee.PR_MST_Employee_SelectByPK(EmployeeID);  
                MST_EmployeeModel modelMST_Employee= new MST_EmployeeModel();

                foreach (DataRow row in dt.Rows)
                {
                    modelMST_Employee.EmployeeName = row["EmployeeName"].ToString();
                    modelMST_Employee.EmployeeTypeID= Convert.ToInt32(row["EmployeeTypeID"]);
                    modelMST_Employee.Email = row["Email"].ToString();
                    modelMST_Employee.Age = Convert.ToInt32(row["Age"]);
                    modelMST_Employee.BirthDate = Convert.ToDateTime(row["BirthDate"]);
                    modelMST_Employee.BloodGroup = row["BloodGroup"].ToString();
                    modelMST_Employee.Gender= row["Gender"].ToString();
                    modelMST_Employee.MobileNo = row["MobileNo"].ToString();
                    modelMST_Employee.JoiningDate= Convert.ToDateTime(row["JoiningDate"]);
                    modelMST_Employee.Address= row["Address"].ToString();
                    modelMST_Employee.Salary = Convert.ToDecimal(row["Salary"]);
                    modelMST_Employee.PhotoPath = row["PhotoPath"].ToString();
                    modelMST_Employee.isActive = Convert.ToBoolean(row["isActive"]);
                }

                return View("MST_Employee_AddEdit", modelMST_Employee);
            }
            return View("MST_Employee_AddEdit");
        }
        #endregion


        #region Save(Insert/Update)
        public IActionResult Save(MST_EmployeeModel modelMST_Employee)
        {
            #region PhotoPath
            if (modelMST_Employee.File != null)
            {
                string FilePath = "wwwroot\\Images";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string fileNameWithPath = Path.Combine(path, modelMST_Employee.File.FileName);
                modelMST_Employee.PhotoPath = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + modelMST_Employee.File.FileName;

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    modelMST_Employee.File.CopyTo(stream);
                }
            }
            #endregion

            if (modelMST_Employee.EmployeeID== null)
            {
                DataTable dt = dalMST_Employee.PR_MST_Employee_Insert(
                    modelMST_Employee.EmployeeName,
                    modelMST_Employee.Email,
                    modelMST_Employee.MobileNo,
                    modelMST_Employee.BloodGroup,
                    modelMST_Employee.BirthDate,
                    modelMST_Employee.Age,
                    modelMST_Employee.Gender,
                    modelMST_Employee.JoiningDate,
                    modelMST_Employee.Salary,
                    modelMST_Employee.Address,
                    modelMST_Employee.EmployeeTypeID,
                    modelMST_Employee.isActive,
                    modelMST_Employee.PhotoPath
                );
                TempData["MST_Employee_AlertMessage"] = "Record Inserted Successfully!!";
            }
            else
            {
                DataTable dt = dalMST_Employee.PR_MST_Employee_Update(
                    (int)modelMST_Employee.EmployeeID,
                    modelMST_Employee.EmployeeName,
                    modelMST_Employee.Email,
                    modelMST_Employee.MobileNo,
                    modelMST_Employee.BloodGroup,
                    modelMST_Employee.BirthDate,
                    modelMST_Employee.Age,
                    modelMST_Employee.Gender,
                    modelMST_Employee.JoiningDate,
                    modelMST_Employee.Salary,
                    modelMST_Employee.Address,
                    modelMST_Employee.EmployeeTypeID,
                    modelMST_Employee.isActive,
                    modelMST_Employee.PhotoPath
                );
                TempData["MST_Employee_AlertMessage"] = "Record Updated Successfully!!";
            }
            return RedirectToAction("Index");
        }
        #endregion



        #region Delete
        public IActionResult Delete(int EmployeeID)
        {
            if (Convert.ToBoolean(dalMST_Employee.PR_MST_Employee_Delete(EmployeeID)))
            {
                TempData["MST_EmployeeDelete_AlertMessage"] = "Record Deleted Successfully";
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
