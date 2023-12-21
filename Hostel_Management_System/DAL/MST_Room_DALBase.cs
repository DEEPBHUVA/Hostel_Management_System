using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace Hostel_Management_System.DAL
{
    public class MST_Room_DALBase : DAL_Helper
    {
        #region PR_MST_Room_SelectAll
        public DataTable PR_MST_Room_SelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Room_SelectAll");
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

        #region PR_MST_Room_DeleteByRoomId
        public bool? PR_MST_Room_DeleteByRoomId(int RoomId)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Room_DeleteByRoomId");
                sqlDB.AddInParameter(dbCMD, "RoomId", SqlDbType.Int, RoomId);
                int delete = sqlDB.ExecuteNonQuery(dbCMD);
                return (delete == -1 ? false : true);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region PR_MST_Room_SelectByPk
        public DataTable PR_MST_Room_SelectByPk(int? RoomId)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Room_SelectByPk");
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
                throw e;
                return null;
            }
        }
        #endregion

        #region PR_MST_Room_Insert
        public DataTable PR_MST_Room_Insert(int RoomNo, bool Status,int Capacity,int SeatCount)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Room_Insert");
                sqlDB.AddInParameter(dbCMD, "RoomNo", SqlDbType.Int, RoomNo);
                sqlDB.AddInParameter(dbCMD, "Status", SqlDbType.Bit, Status);
                sqlDB.AddInParameter(dbCMD, "Capacity", SqlDbType.Int, Capacity);
                sqlDB.AddInParameter(dbCMD, "SeatCount", SqlDbType.Int, SeatCount);

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

        #region PR_MST_Room_Update
        public DataTable PR_MST_Room_Update(int RoomId, int RoomNo, bool Status, int Capacity, int SeatCount)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Room_Update");
                sqlDB.AddInParameter(dbCMD, "RoomId", SqlDbType.Int, RoomId);
                sqlDB.AddInParameter(dbCMD, "RoomNo", SqlDbType.Int, RoomNo);
                sqlDB.AddInParameter(dbCMD, "Status", SqlDbType.Bit, Status);
                sqlDB.AddInParameter(dbCMD, "Capacity", SqlDbType.Int, Capacity);
                sqlDB.AddInParameter(dbCMD, "SeatCount", SqlDbType.Int, SeatCount);

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
    }
}
