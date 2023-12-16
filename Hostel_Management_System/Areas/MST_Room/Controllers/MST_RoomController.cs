using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Hostel_Management_System.Areas.MST_Room.Models;

namespace Hostel_Management_System.Areas.MST_Room.Controllers
{
    [Area("MSt_Room")]
    [Route("MST_Room/{Controller}/{action}")]
    public class MST_RoomController : Controller
    { 
        public IConfiguration Configuration;
        public MST_RoomController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            string MyConnectionStr = this.Configuration.GetConnectionString("ConStr");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(MyConnectionStr);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_MST_Room_SelectAll";
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();
            return View("MST_RoomList", dt);
        }

        public IActionResult Add(int? RoomId)
        {
            if (RoomId != null)
            {
                string MyConnectionStr = this.Configuration.GetConnectionString("ConStr");
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(MyConnectionStr);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_MST_Room_SelectByPk";
                cmd.Parameters.AddWithValue("@RoomId", RoomId);
                SqlDataReader objSDR = cmd.ExecuteReader();
                MST_RoomModel modelMST_Room = new MST_RoomModel();

                if (objSDR.HasRows)
                {
                    while (objSDR.Read())
                    {
                        modelMST_Room.RoomNo = Convert.ToInt32(objSDR["RoomNo"]);
						modelMST_Room.SeatCount = Convert.ToInt32(objSDR["SeatCount"]);
						modelMST_Room.Capacity = Convert.ToInt32(objSDR["Capacity"]);
						modelMST_Room.Status = (bool)objSDR["Status"];
                    }
                }
                return View("MST_RoomAddEdit", modelMST_Room);
            }
            return View("MST_RoomAddEdit");
        }

        public IActionResult Save(MST_RoomModel modelMST_Room)
        {
            string MyConnectionStr = this.Configuration.GetConnectionString("ConStr");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(MyConnectionStr);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            if(modelMST_Room.RoomId == null)
            {
                cmd.CommandText = "PR_MST_Room_Insert";
            }
            else
            {
                cmd.CommandText = "PR_MST_Room_Update";
                cmd.Parameters.AddWithValue("@RoomId", modelMST_Room.RoomId);
            }
            cmd.Parameters.AddWithValue("@RoomNo", modelMST_Room.RoomNo);
            cmd.Parameters.AddWithValue("@Status", modelMST_Room.Status);
			cmd.Parameters.AddWithValue("@Capacity", modelMST_Room.Capacity);
			cmd.Parameters.AddWithValue("@SeatCount", modelMST_Room.SeatCount);
			if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if (modelMST_Room.RoomId == null)
                    TempData["MST_Room_AlertMessage"] = "Record Inserted Successfully!!";
                else
                {
                    TempData["MST_Room_AlertMessage"] = "Record Updated Successfully!!";
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int RoomId) 
        {
            string MyConnectionStr = this.Configuration.GetConnectionString("ConStr");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(MyConnectionStr);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_MST_Room_DeleteByRoomId";
            cmd.Parameters.AddWithValue("@RoomId", RoomId);
            cmd.ExecuteNonQuery();
            return RedirectToAction("Index");
        }

        public IActionResult Cancle()
        {
            return RedirectToAction("Index");
        }

    }
}
