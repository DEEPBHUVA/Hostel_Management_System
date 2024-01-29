using Hostel_Management_System.BAL;
using Hostel_Management_System.DAL;
using Hostel_Management_System.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Hostel_Management_System.Controllers
{
    public class UserController : Controller
    {
        MST_Student_DAL dalMST_Student = new MST_Student_DAL();
        MST_Notice_DAL dalMST_Notice = new MST_Notice_DAL();
        public IActionResult Index()
        {
            int studentID = (int)@CV.StudentID();

            DataTable studentData = dalMST_Student.PR_MST_Student_SelectByPk(studentID);
            DataTable noticeData = dalMST_Notice.PR_MST_Notice_SelectAll();

            // Create an instance of UserViewModel and populate its properties
            var viewModel = new UserViewModel
            {
                StudentData = studentData,
                NoticeData = noticeData
            };

            // Pass the view model to the view
            return View("~/Views/Home/User.cshtml", viewModel);
        }

        public IActionResult Rules()
        {
            return View("Rules_List");
        }

    }
}
