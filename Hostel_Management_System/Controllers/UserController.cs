using Hostel_Management_System.BAL;
using Hostel_Management_System.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Hostel_Management_System.Controllers
{
    public class UserController : Controller
    {
        MST_Student_DAL dalMST_Student = new MST_Student_DAL();
        public IActionResult Index()
        {
            // Retrieve StudentID from session or any other source
            int studentID = (int)@CV.StudentID();

            DataTable dt = dalMST_Student.PR_MST_Student_SelectByPk(studentID);
            return View("~/Views/Home/User.cshtml", dt);
        }

    }
}
