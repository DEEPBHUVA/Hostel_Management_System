using Hostel_Management_System.Areas.SEC_User.Models;
using Hostel_Management_System.DAL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Hostel_Management_System.Areas.SEC_User.Controllers
{
	[Area("SEC_User")]
	[Route("SEC_User/{Controller}/{action}")]
	public class SEC_UserController : Controller
	{
		public IActionResult Index()
		{
			return View("SEC_USserLogin");
		}

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
				SEC_User_DAL dal = new SEC_User_DAL();
				DataTable dt = dal.PR_SEC_User_SelectBYUserNamePassword(modelSEC_User.UserName, modelSEC_User.Password);
				if (dt.Rows.Count > 0)
				{
					foreach (DataRow dr in dt.Rows)
					{
						HttpContext.Session.SetString("UserID", dr["UserID"].ToString());
						HttpContext.Session.SetString("UserName", dr["UserName"].ToString());
						HttpContext.Session.SetString("Email", dr["Email"].ToString());
						HttpContext.Session.SetString("Password", dr["Password"].ToString());
						HttpContext.Session.SetString("PhotoPath", dr["PhotoPath"].ToString());
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
					return RedirectToAction("Index", "Home");
				}
			}
			return RedirectToAction("Index");
		}
		//Logout action to clear current session and redirect user to login page
		[HttpPost]
        public async Task<IActionResult> LOGOUT()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
