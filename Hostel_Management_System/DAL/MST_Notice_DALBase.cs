using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace Hostel_Management_System.DAL
{
    public class MST_Notice_DALBase : DAL_Helper
    {
        #region PR_MST_Notice_SelectAll
        public DataTable PR_MST_Notice_SelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Notice_SelectAll");
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

        #region PR_MST_Notice_Delete
        public bool? PR_MST_Notice_Delete(int NoticeID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Notice_Delete");
                sqlDB.AddInParameter(dbCMD, "NoticeID", SqlDbType.Int, NoticeID);
                int delete = sqlDB.ExecuteNonQuery(dbCMD);
                return (delete == -1 ? false : true);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region PR_MST_Notice_SelectByPK
        public DataTable PR_MST_Notice_SelectByPK(int? NoticeID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Notice_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "NoticeID", SqlDbType.Int, NoticeID);

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

        #region PR_MST_Notice_INSERT
        public DataTable PR_MST_Notice_INSERT(string Title, string Description)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Notice_INSERT");
                sqlDB.AddInParameter(dbCMD, "Title", SqlDbType.VarChar, Title);
                sqlDB.AddInParameter(dbCMD, "Description", SqlDbType.VarChar, Description);

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

        #region PR_MST_Notice_Update
        public DataTable PR_MST_Notice_Update(int NoticeID, string Title, string Description)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Notice_Update");
                sqlDB.AddInParameter(dbCMD, "NoticeID", SqlDbType.Int, NoticeID);
                sqlDB.AddInParameter(dbCMD, "Title", SqlDbType.VarChar, Title);
                sqlDB.AddInParameter(dbCMD, "Description", SqlDbType.VarChar, Description);

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
