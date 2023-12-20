using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Hostel_Management_System.Areas.MST_Room.Models;
using Hostel_Management_System.Areas.Room_Allocate.Models;
using Hostel_Management_System.Areas.MST_Student.Models;

namespace Hostel_Management_System.Areas.Room_Allocate.Controllers
{
    [Area("Room_Allocate")]
    [Route("Room_Allocate/{Controller}/{action}")]
    public class Room_AllocateController : Controller
    {
        public IConfiguration Configuration;
        public Room_AllocateController(IConfiguration configuration)
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
            cmd.CommandText = "PR_Room_AllocationSelectAll";
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();
            return View("Room_AllocateList", dt);
        }

        public IActionResult Add(int? RoomAllocateID)
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

            //===============================

            #region Student Dropdown
            string MyConnectionStr2 = this.Configuration.GetConnectionString("ConStr");
            SqlConnection connection3 = new SqlConnection(MyConnectionStr2);
            DataTable dt3 = new DataTable();
            connection3.Open();
            SqlCommand cmd3 = connection3.CreateCommand();
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.CommandText = "PR_MST_RoomDropdown";
            SqlDataReader reader3 = cmd3.ExecuteReader();
            dt3.Load(reader3);

            List<MST_RoomDropdown> list2 = new List<MST_RoomDropdown>();
            foreach (DataRow dr in dt3.Rows)
            {
                MST_RoomDropdown dlist1 = new MST_RoomDropdown();
                dlist1.RoomId = Convert.ToInt32(dr["RoomId"]);
                dlist1.RoomNo = Convert.ToInt32(dr["RoomNo"]);
                list2.Add(dlist1);
            }
            ViewBag.RoomList = list2;
            #endregion


            if (RoomAllocateID != null)
            {
                string MyConnectionStr = this.Configuration.GetConnectionString("ConStr");
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(MyConnectionStr);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_Room_AllocationSelectByPk";
                cmd.Parameters.AddWithValue("@RoomAllocateID", RoomAllocateID);
                SqlDataReader objSDR = cmd.ExecuteReader();
                Room_AllocateModel modelRoom_Allocate = new Room_AllocateModel();

                if (objSDR.HasRows)
                {
                    while (objSDR.Read())
                    {
                        modelRoom_Allocate.RoomAllocateID = Convert.ToInt32(objSDR["RoomAllocateID"]);
                        modelRoom_Allocate.StudentID = Convert.ToInt32(objSDR["StudentID"]);
                        modelRoom_Allocate.RoomId = Convert.ToInt32(objSDR["RoomId"]);
                    }
                }
                return View("Room_AllocateAddEdit", modelRoom_Allocate);
            }
            return View("Room_AllocateAddEdit");
        }


        public IActionResult Save(Room_AllocateModel modelRoom_Allocate)
        {
            string MyConnectionStr = this.Configuration.GetConnectionString("ConStr");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(MyConnectionStr);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            if (modelRoom_Allocate.RoomAllocateID== null)
            {
                cmd.CommandText = "PR_RoomAllocationInsert";
            }
            else
            {
                cmd.CommandText = "PR_RoomAllocationUpdate";
                cmd.Parameters.AddWithValue("@RoomAllocateID", modelRoom_Allocate.RoomAllocateID);
            }
            cmd.Parameters.AddWithValue("@StudentID", modelRoom_Allocate.StudentID);
            cmd.Parameters.AddWithValue("@RoomId", modelRoom_Allocate.RoomId);
         
            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if (modelRoom_Allocate.RoomAllocateID == null)
                    TempData["RoomAllocation_AlertMessage"] = "Record Inserted Successfully!!";
                else
                {
                    TempData["RoomAllocation_AlertMessage"] = "Record Updated Successfully!!";
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int RoomAllocateID)
        {
            string MyConnectionStr = this.Configuration.GetConnectionString("ConStr");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(MyConnectionStr);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_RoomAllocationDelete";
            cmd.Parameters.AddWithValue("@RoomAllocateID", RoomAllocateID);
            cmd.ExecuteNonQuery();
            TempData["RoomAllocation_Delete_AlertMessage"] = "Record Deleted Successfully!!";
            return RedirectToAction("Index");
        }

        //public IActionResult Deallocate(int RoomAllocateID)
        //{
        //    string MyConnectionStr = this.Configuration.GetConnectionString("ConStr");
        //    DataTable dt = new DataTable();
        //    SqlConnection conn = new SqlConnection(MyConnectionStr);
        //    conn.Open();
        //    SqlCommand cmd = conn.CreateCommand();
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = "PR_RoomAllocationDelete";
        //    cmd.Parameters.AddWithValue("@RoomAllocateID", RoomAllocateID);
        //    cmd.ExecuteNonQuery();
        //    return RedirectToAction("Index");
        //}

        public IActionResult Cancle()
        {
            return RedirectToAction("Index");
        }
    }
}
