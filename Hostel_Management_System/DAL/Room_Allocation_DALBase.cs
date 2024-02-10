using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace Hostel_Management_System.DAL
{
    public class Room_Allocation_DALBase : DAL_Helper
    {
        #region PR_Room_AllocationSelectAll
        public DataTable PR_Room_AllocationSelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_Room_AllocationSelectAll");
                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region PR_RoomAllocationDelete
        public bool? PR_RoomAllocationDelete(int RoomAllocateID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_RoomAllocationDelete");
                sqlDB.AddInParameter(dbCMD, "RoomAllocateID", SqlDbType.Int, RoomAllocateID);
                int delete = sqlDB.ExecuteNonQuery(dbCMD);
                return (delete == -1 ? false : true);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region PR_Room_AllocationSelectByPk
        public DataTable PR_Room_AllocationSelectByPk(int? RoomAllocateID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_Room_AllocationSelectByPk");
                sqlDB.AddInParameter(dbCMD, "RoomAllocateID", SqlDbType.Int, RoomAllocateID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;

            }
            catch (Exception e)
            {
                throw e;
                return null;
            }
        }
        #endregion

        #region PR_RoomAllocationInsert
        public DataTable PR_RoomAllocationInsert(int StudentID,int RoomId)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_RoomAllocationInsert");
                sqlDB.AddInParameter(dbCMD, "StudentID", SqlDbType.Int, StudentID);
                sqlDB.AddInParameter(dbCMD, "RoomId", SqlDbType.Int, RoomId);

                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region PR_RoomAllocationUpdate
        public DataTable PR_RoomAllocationUpdate(int StudentID, int RoomId, int RoomAllocateID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_RoomAllocationUpdate");
                sqlDB.AddInParameter(dbCMD, "RoomId", SqlDbType.Int, RoomId);
                sqlDB.AddInParameter(dbCMD, "StudentID", SqlDbType.Int, StudentID);
                sqlDB.AddInParameter(dbCMD, "RoomAllocateID", SqlDbType.Int, RoomAllocateID);

                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region PR_Room_Allocation_Filter
        public DataTable PR_Room_Allocation_Filter(string StudentName, int RoomNo)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_Room_Allocation_Filter");
                sqlDB.AddInParameter(dbCMD, "RoomNo", SqlDbType.Int, RoomNo);
                sqlDB.AddInParameter(dbCMD, "StudentName", SqlDbType.VarChar, StudentName);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;

            }
            catch (Exception e)
            {
                throw e;
                return null;
            }
        }
        #endregion
    }
}
