using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Hostel_Management_System.Areas.MST_Room.Models;
using Hostel_Management_System.DAL;
using Hostel_Management_System.BAL;

namespace Hostel_Management_System.Areas.MST_Room.Controllers
{
	[CheckAccess]
	[Area("MSt_Room")]
    [Route("MST_Room/{Controller}/{action}")]
    public class MST_RoomController : Controller
    {
        MST_Room_DAL dalMST_Room = new MST_Room_DAL();
        #region Configuration
        public IConfiguration Configuration;
        public MST_RoomController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        #region SelectAllRooms
        public IActionResult Index()
        {
            DataTable dt = dalMST_Room.PR_MST_Room_SelectAll();
            return View("MST_RoomList", dt);
        }
        #endregion

        #region Add
        public IActionResult Add(int? RoomId)
        {
            if (RoomId != null)
            {
                DataTable dt = dalMST_Room.PR_MST_Room_SelectByPk(RoomId);
                MST_RoomModel modelMST_Room = new MST_RoomModel();

                foreach (DataRow row in dt.Rows)
                {
                    modelMST_Room.RoomNo = Convert.ToInt32(row["RoomNo"]);
                    modelMST_Room.SeatCount = Convert.ToInt32(row["SeatCount"]);
                    modelMST_Room.Capacity = Convert.ToInt32(row["Capacity"]);
                    modelMST_Room.Status = (bool)row["Status"];
                }
                return View("MST_RoomAddEdit", modelMST_Room);
            }
            return View("MST_RoomAddEdit");
        }
        #endregion

        #region Save(Insert/Update)
        public IActionResult Save(MST_RoomModel modelMST_Room)
        {
            if (modelMST_Room.RoomId== null)
            {
                DataTable dt = dalMST_Room.PR_MST_Room_Insert(modelMST_Room.RoomNo,modelMST_Room.Status,modelMST_Room.Capacity,modelMST_Room.SeatCount);
                TempData["MST_Room_AlertMessage"] = "Record Inserted Successfully!!";
            }
            else
            {
                DataTable dt = dalMST_Room.PR_MST_Room_Update((int)modelMST_Room.RoomId,modelMST_Room.RoomNo, modelMST_Room.Status, modelMST_Room.Capacity, modelMST_Room.SeatCount);
                TempData["MST_Room_AlertMessage"] = "Record Updated Successfully!!";
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int RoomId) 
        {
            if (Convert.ToBoolean(dalMST_Room.PR_MST_Room_DeleteByRoomId(RoomId)))
            {
                TempData["MST_Room_DeleteAlertMessage"] = "Record Deleted Successfully";
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
