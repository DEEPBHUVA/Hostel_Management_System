using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace Hostel_Management_System.DAL
{
    public class MST_Visitor_DALBase : DAL_Helper
    {
        #region PR_MST_Visitor_SelectAll
        public DataTable PR_MST_Visitor_SelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Visitor_SelectAll");
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

        #region PR_MST_Visitor_Delete
        public bool? PR_MST_Visitor_Delete(int VisitorID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Visitor_Delete");
                sqlDB.AddInParameter(dbCMD, "VisitorID", SqlDbType.Int, VisitorID);
                int delete = sqlDB.ExecuteNonQuery(dbCMD);
                return (delete == -1 ? false : true);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region PR_MST_Visitor_SelectByID
        public DataTable PR_MST_Visitor_SelectByID(int? VisitorID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Visitor_SelectByID");
                sqlDB.AddInParameter(dbCMD, "VisitorID", SqlDbType.Int, VisitorID);

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

        #region PR_MST_Visitor_Insert
        public DataTable PR_MST_Visitor_Insert(string VisitorName, string MobileNo, string Remark, DateTime DateIN, DateTime DateOUT)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Visitor_Insert");
                sqlDB.AddInParameter(dbCMD, "DateIN", SqlDbType.DateTime, DateIN);
                sqlDB.AddInParameter(dbCMD, "DateOUT", SqlDbType.DateTime, DateOUT);
                sqlDB.AddInParameter(dbCMD, "MobileNo", SqlDbType.VarChar, MobileNo);
                sqlDB.AddInParameter(dbCMD, "Remark", SqlDbType.VarChar, Remark);
                sqlDB.AddInParameter(dbCMD, "VisitorName", SqlDbType.VarChar, VisitorName);

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

        #region PR_MST_Visitor_Update
        public DataTable PR_MST_Visitor_Update(int VisitorID, string VisitorName, string MobileNo, string Remark, DateTime DateIN, DateTime DateOUT)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Visitor_Update");
                sqlDB.AddInParameter(dbCMD, "VisitorID", SqlDbType.Int, VisitorID);
                sqlDB.AddInParameter(dbCMD, "DateIN", SqlDbType.DateTime, DateIN);
                sqlDB.AddInParameter(dbCMD, "DateOUT", SqlDbType.DateTime, DateOUT);
                sqlDB.AddInParameter(dbCMD, "MobileNo", SqlDbType.VarChar, MobileNo);
                sqlDB.AddInParameter(dbCMD, "Remark", SqlDbType.VarChar, Remark);
                sqlDB.AddInParameter(dbCMD, "VisitorName", SqlDbType.VarChar, VisitorName);

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

        #region PR_MST_Visitor_MultipleDelete
        public bool? PR_MST_Visitor_MultipleDelete(string visitorIds)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Visitor_MultipleDelete");

                // Assuming you have a stored procedure that accepts a comma-separated list of IDs
                sqlDB.AddInParameter(dbCMD, "VisitorIDs", SqlDbType.VarChar, visitorIds);

                int delete = sqlDB.ExecuteNonQuery(dbCMD);
                return (delete >= 0);  // Return true if at least one record was deleted
            }
            catch (Exception e)
            {
                // Handle exception or log it
                return null;
            }
        }
        #endregion


    }
}
