using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Hostel_Management_System.Areas.MST_Room.Models;
using Hostel_Management_System.Areas.Room_Allocate.Models;
using Hostel_Management_System.Areas.MST_Student.Models;
using Hostel_Management_System.DAL;
using Hostel_Management_System.BAL;

namespace Hostel_Management_System.Areas.Room_Allocate.Controllers
{
	[CheckAccess]
	[Area("Room_Allocate")]
    [Route("Room_Allocate/{Controller}/{action}")]
    public class Room_AllocateController : Controller
    {
        Room_Allocation_DAL dalRoom_Allocation = new Room_Allocation_DAL();
        #region Configuration
        public IConfiguration Configuration;
        public Room_AllocateController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        #region SelectAllRoom
        public IActionResult Index()
        {
            DataTable dt = dalRoom_Allocation.PR_Room_AllocationSelectAll();
            return View("Room_AllocateList", dt);
        }
        #endregion

        #region Add
        public IActionResult Add(int? RoomAllocateID)
        {
            #region Student Dropdown
            string MyConnectionStr1 = this.Configuration.GetConnectionString("ConStr");
            SqlConnection connection2 = new SqlConnection(MyConnectionStr1);
            DataTable dt2 = new DataTable();
            connection2.Open();
            SqlCommand cmd2 = connection2.CreateCommand();
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.CommandText = "PR_MST_StudentDropdown_FORRoom";
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

            #region Room Dropdown
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
                DataTable dt = dalRoom_Allocation.PR_Room_AllocationSelectByPk(RoomAllocateID);
                Room_AllocateModel modelRoom_Allocate = new Room_AllocateModel();

                foreach (DataRow row in dt.Rows)
                {
                    modelRoom_Allocate.RoomAllocateID = Convert.ToInt32(row["RoomAllocateID"]);
                    modelRoom_Allocate.StudentID = Convert.ToInt32(row["StudentID"]);
                    modelRoom_Allocate.RoomId = Convert.ToInt32(row["RoomId"]);
                }
                return View("Room_AllocateAddEdit", modelRoom_Allocate);
            }
            return View("Room_AllocateAddEdit");
        }
        #endregion

        #region Save(Insert/Update)
        public IActionResult Save(Room_AllocateModel modelRoom_Allocate)
        {
            if (modelRoom_Allocate.RoomAllocateID == null)
            {
                DataTable dt = dalRoom_Allocation.PR_RoomAllocationInsert(modelRoom_Allocate.StudentID,modelRoom_Allocate.RoomId);
                TempData["RoomAllocation_AlertMessage"] = "Record Inserted Successfully!!";
            }
            else
            {
                DataTable dt = dalRoom_Allocation.PR_RoomAllocationUpdate((int)modelRoom_Allocate.RoomAllocateID,modelRoom_Allocate.StudentID, modelRoom_Allocate.RoomId);
                TempData["RoomAllocation_AlertMessage"] = "Record Updated Successfully!!";
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int RoomAllocateID)
        {
            if (Convert.ToBoolean(dalRoom_Allocation.PR_RoomAllocationDelete(RoomAllocateID)))
            {
                TempData["RoomAllocation_Delete_AlertMessage"] = "Record Deleted Successfully!!";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        #endregion

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

        #region Cancle
        public IActionResult Cancle()
        {
            return RedirectToAction("Index");
        }
        #endregion
    }
}
