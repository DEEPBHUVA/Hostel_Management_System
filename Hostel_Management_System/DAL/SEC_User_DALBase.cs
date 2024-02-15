using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Hostel_Management_System.DAL
{
	public class SEC_User_DALBase : DAL_Helper
	{
        #region PR_SEC_User_SelectBYUserNamePassword
        public DataTable PR_SEC_User_SelectBYUserNamePassword(string UserName, string Password)
		{
			try
			{
				SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
				DbCommand cmd = sqlDB.GetStoredProcCommand("PR_SEC_User_SelectBYUserNamePassword");
				sqlDB.AddInParameter(cmd, "@UserName", DbType.String, UserName);
				sqlDB.AddInParameter(cmd, "@Password", DbType.String, Password);
				DataTable dt = new DataTable();
				using (IDataReader reader = sqlDB.ExecuteReader(cmd))
				{
					dt.Load(reader);
				}
				return dt;
			}
			catch (Exception ex)
			{
				throw ex;
				return null;
			}
		}
        #endregion

       
        #region PR_SEC_User_SelectAll
        public DataTable PR_SEC_User_SelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SEC_User_SelectAll");
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

        #region PR_SEC_User_Delete
        public bool? PR_SEC_User_Delete(int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SEC_User_Delete");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                int delete = sqlDB.ExecuteNonQuery(dbCMD);
                return (delete == -1 ? false : true);
            }
            catch (Exception ex)
            {
                if (ex is SqlException sqlException && sqlException.Number == 547)
                {
                    throw new Exception("Cannot Delete Record!!. Related records exist.", ex);
                }
                else
                {
                    throw;
                }
            }
        }
        #endregion

        #region PR_SEC_User_SelectByPk
        public DataTable PR_SEC_User_SelectByPk(int? UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SEC_User_SelectByPk");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);

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

        #region PR_SEC_User_Insert
        public DataTable PR_SEC_User_Insert(int StudentID,string FristName,string LastName,string UserName,string UserRole)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SEC_User_Insert");
                sqlDB.AddInParameter(dbCMD, "StudentID", SqlDbType.Int, StudentID);
                sqlDB.AddInParameter(dbCMD, "FristName", SqlDbType.VarChar, FristName);
                sqlDB.AddInParameter(dbCMD, "LastName", SqlDbType.VarChar, LastName);
                sqlDB.AddInParameter(dbCMD, "UserName", SqlDbType.VarChar, UserName);
                sqlDB.AddInParameter(dbCMD, "UserRole", SqlDbType.VarChar, UserRole);


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

        #region PE_SEC_User_Edit
        public DataTable PE_SEC_User_Edit(int UserID, int StudentID, string FristName, string LastName, string UserName, string UserRole)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PE_SEC_User_Edit");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(dbCMD, "StudentID", SqlDbType.Int, StudentID);
                sqlDB.AddInParameter(dbCMD, "FristName", SqlDbType.VarChar, FristName);
                sqlDB.AddInParameter(dbCMD, "LastName", SqlDbType.VarChar, LastName);
                sqlDB.AddInParameter(dbCMD, "UserName", SqlDbType.VarChar, UserName);
                sqlDB.AddInParameter(dbCMD, "UserRole", SqlDbType.VarChar, UserRole);

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

        #region PR_Change_UserPassword
        public DataTable PR_Change_UserPassword(int UserID, string OldPassword, string NewPassword)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_Change_UserPassword");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(dbCMD, "OldPassword", SqlDbType.VarChar, OldPassword);
                sqlDB.AddInParameter(dbCMD, "NewPassword", SqlDbType.VarChar, NewPassword);

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
