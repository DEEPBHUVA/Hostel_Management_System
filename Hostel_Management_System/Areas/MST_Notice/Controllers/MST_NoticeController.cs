using Hostel_Management_System.Areas.MST_Notice.Models;
using Hostel_Management_System.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Hostel_Management_System.Areas.MST_Notice.Controllers
{
    [Area("MST_Notice")]
    [Route("MST_Notice/{Controller}/{action}")]
    public class MST_NoticeController : Controller
    {
        MST_Notice_DAL dalMST_Notice = new MST_Notice_DAL();

        #region Configuration
        public IConfiguration Configuration;
        public MST_NoticeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        #region SelectAllNotice
        public IActionResult Index()
        {
            DataTable dt = dalMST_Notice.PR_MST_Notice_SelectAll();
            ViewBag.NoticeData = dt;
            return View("Index");

        }
        #endregion
    }
}
