using Hostel_Management_System.Areas.MST_Student.Models;
using Hostel_Management_System.Areas.SEC_User.Models;
using Hostel_Management_System.BAL;
using Hostel_Management_System.DAL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Hostel_Management_System.Areas.SEC_User.Controllers
{
	[Area("SEC_User")]
	[Route("SEC_User/{Controller}/{action}")]
	public class SEC_UserController : Controller
	{
        SEC_User_DAL dal = new SEC_User_DAL();

        #region Configuration
        public IConfiguration Configuration;
		public SEC_UserController(IConfiguration configuration)
		{
			Configuration = configuration;
		}
        #endregion

        #region Index
        public IActionResult Index()
		{
			return View("SEC_USserLogin");
		}
        #endregion

        #region GetUserRole
        public string GetUserRole(string username, string password)
		{
			string connectionString = Configuration.GetConnectionString("ConStr");

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				using (SqlCommand command = new SqlCommand("[PR_SEC_User_GetRole]", connection))
				{
					command.CommandType = CommandType.StoredProcedure;

					// Add OUTPUT parameter
					SqlParameter userRoleParameter = new SqlParameter("@User_Role", SqlDbType.VarChar, 50);
					userRoleParameter.Direction = ParameterDirection.Output;
					command.Parameters.Add(userRoleParameter);

					// Add other parameters
					command.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar, 50) { Value = username });
					command.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar, 50) { Value = password });

					command.ExecuteNonQuery();

					// Retrieve the value of the OUTPUT parameter
					string userRole = userRoleParameter.Value.ToString();

					return userRole;
				}
			}
		}
        #endregion

        #region Login
        [HttpPost]
		public IActionResult Login(SEC_UserModel modelSEC_User)
		{
			string ErrorMsg = string.Empty;
			if (string.IsNullOrEmpty(modelSEC_User.UserName))
			{
				ErrorMsg += "User Name is Required";
			}
			if (string.IsNullOrEmpty(modelSEC_User.Password))
			{
				ErrorMsg += "<br/>Password is Required";
			}

			if (!string.IsNullOrEmpty(ErrorMsg))
			{
				TempData["Error"] = ErrorMsg;
				return RedirectToAction("Index");
			}
			else
			{
				DataTable dt = dal.PR_SEC_User_SelectBYUserNamePassword(modelSEC_User.UserName, modelSEC_User.Password);
				if (dt.Rows.Count > 0)
				{
					foreach (DataRow dr in dt.Rows)
					{
						HttpContext.Session.SetString("UserID", dr["UserID"].ToString());
						HttpContext.Session.SetString("UserName", dr["UserName"].ToString());
						HttpContext.Session.SetString("FristName", dr["FristName"].ToString()); // Corrected column name
						HttpContext.Session.SetString("LastName", dr["LastName"].ToString());
						HttpContext.Session.SetString("UserRole", dr["UserRole"].ToString());
						HttpContext.Session.SetString("Email", dr["Email"].ToString());
                        HttpContext.Session.SetString("StudentID", dr["StudentID"].ToString());
                        HttpContext.Session.SetString("Password", dr["Password"].ToString());
						HttpContext.Session.SetString("PhotoPath", dr["PhotoPath"].ToString());
                        HttpContext.Session.SetString("Created", dr["Created"].ToString());
                        break;	
					}
				}
				else
				{
					TempData["Error"] = "Invalid User Name or Password";
					return RedirectToAction("Index", "SEC_User");
				}

				if (HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetString("Password") != null)
				{
					string username = HttpContext.Session.GetString("UserName");
					string password = HttpContext.Session.GetString("Password");

					
					string userRole = GetUserRole(username, password);
					if (!string.IsNullOrEmpty(userRole))
					{
						// Check the user role and redirect accordingly
						if (userRole == "Admin")
						{
							return RedirectToAction("Index", "Home");
						}
						else if (userRole == "Student")
						{
							return RedirectToAction("Index", "User");
						}
                        else if (userRole == "Staff")
                        {   
                            return RedirectToAction("Index", "Staff");
                        }
                    }
					else
					{
						return RedirectToAction("Index");
					}
					
				}
			}
			return RedirectToAction("Index");
		}
		#endregion

        #region LogOut
        //Logout action to clear current session and redirect user to login page
        public IActionResult LOGOUT()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home"); // Redirect to the desired action and controller
        }
        #endregion

        #region SelectAllPayment
        public IActionResult GetallUser()
        {
            DataTable dt = dal.PR_SEC_User_SelectAll();
            return View("SEC_User_List", dt);
        }
        #endregion

        #region Add
        public IActionResult Add(int? UserID)
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

            if (UserID != null)
            {
                DataTable dt = dal.PR_SEC_User_SelectByPk(UserID);
                SEC_UserModel modelSEC_User= new SEC_UserModel();

                foreach (DataRow row in dt.Rows)
                {
                    modelSEC_User.UserID= Convert.ToInt32(row["UserID"]);
                    modelSEC_User.StudentID = Convert.ToInt32(row["StudentID"]);
                    modelSEC_User.FirstName= row["FristName"].ToString();
                    modelSEC_User.LastName= row["LastName"].ToString();
                    modelSEC_User.UserName= row["UserName"].ToString();
                    modelSEC_User.Password= row["Password"].ToString();
                    modelSEC_User.UserRole = row["UserRole"].ToString();
                }
                return View("SEC_User_AddForm", modelSEC_User);
            }
            return View("SEC_User_AddForm");
        }
        #endregion

        #region Save(Insert/Update)
        public IActionResult Save(SEC_UserModel modelSEC_user)
        {
            if (modelSEC_user.UserID == null)
            {
                string resultMessage = "An error occurred while processing your request.";

                DataTable dt = dal.PR_SEC_User_Insert((int)modelSEC_user.StudentID, modelSEC_user.FirstName, modelSEC_user.LastName, modelSEC_user.UserName, modelSEC_user.UserRole);

                if (dt != null && dt.Rows.Count > 0)
                {
                    string result = dt.Rows[0]["Result"].ToString();
                    if (result == "Success")
                    {
                        resultMessage = "Record Inserted Successfully!!";
                    }
                    else if (result == "UsernameExists")
                    {
                        resultMessage = "Username already exists.";
                    }
                }
                TempData["SEC_User_AlertMessage"] = resultMessage;
            }

            else
            {
                DataTable dt = dal.PE_SEC_User_Edit((int)modelSEC_user.UserID, (int)modelSEC_user.StudentID, modelSEC_user.FirstName, modelSEC_user.LastName, modelSEC_user.UserName, modelSEC_user.UserRole);
                TempData["SEC_User_AlertMessage"] = "Record Updated Successfully!!";
            }
            return RedirectToAction("GetallUser");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int UserID)
        {
            if (Convert.ToBoolean(dal.PR_SEC_User_Delete(UserID)))
            {
                TempData["SEC_User_Delete_AlertMessage"] = "Record Deleted Successfully";
                return RedirectToAction("GetallUser");
            }
            return RedirectToAction("GetallUser");
        }
        #endregion

        #region Cancle
        public IActionResult Cancle()
        {
            return RedirectToAction("GetallUser");
        }
        #endregion

        #region Change Password
        public IActionResult ChangePsw()
        {
            return View("ChangePassword");
        }
        #endregion

        #region UserProfile
        public IActionResult UserProfile()
        {
            return View("User_Profile");
        }
        #endregion

        #region PR_Change_UserPassword
        public IActionResult ChangePassword(string oldPassword, string newPassword)
        {
            int userId = (int)@CV.UserID();
            DataTable dt = dal.PR_Change_UserPassword(userId, oldPassword, newPassword);

            if (dt.Rows.Count > 0)
            {
                string? message = dt.Rows[0]["Message"].ToString();

                // Check the message and take appropriate actions
                if (message.Equals("Password changed successfully"))
                {
                    TempData["SEC_User_ChangePassword"] = "Password changed successfully!";
                }
                else if (message.Equals("Incorrect old password"))
                {
                    TempData["SEC_User_ChangePassword"] = "Incorrect old password. Please try again.";
                }
                else
                {
                    TempData["SEC_User_ChangePassword"] = "An error occurred while changing the password.";
                }
            }
            else
            {
                TempData["SEC_User_ChangePassword"] = "An unexpected error occurred.";
            }
            if (CV.UserRole() == "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            else if (CV.UserRole() == "Student")
            {
                return RedirectToAction("Index", "User");
            }
            else if (CV.UserRole() == "Staff")
            {
                return RedirectToAction("Index", "Staff");
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}
