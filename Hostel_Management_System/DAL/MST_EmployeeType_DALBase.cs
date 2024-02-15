using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace Hostel_Management_System.DAL
{
	public class MST_EmployeeType_DALBase : DAL_Helper
	{
		#region PR_MST_EmployeeType_SelectAll
		public DataTable PR_MST_EmployeeType_SelectAll()
		{
			try
			{
				SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
				DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_EmployeeType_SelectAll");
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

		#region PR_MST_EmployeeType_Delete
		public bool? PR_MST_EmployeeType_Delete(int EmployeeTypeID)
		{
			try
			{
				SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
				DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_EmployeeType_Delete");
				sqlDB.AddInParameter(dbCMD, "EmployeeTypeID", SqlDbType.Int, EmployeeTypeID);
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

		#region PR_MST_EmployeeType_SelectByPK
		public DataTable PR_MST_EmployeeType_SelectByPK(int? EmployeeTypeID)
		{
			try
			{
				SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
				DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_EmployeeType_SelectByPK");
				sqlDB.AddInParameter(dbCMD, "EmployeeTypeID", SqlDbType.Int, EmployeeTypeID);

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

		#region PR_MST_EmployeeType_Insert
		public DataTable PR_MST_EmployeeType_Insert(string EmployeeType)
		{
			try
			{
				SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
				DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_EmployeeType_Insert");
				sqlDB.AddInParameter(dbCMD, "EmployeeType", SqlDbType.VarChar, EmployeeType);

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

        #region PR_MST_EmployeeType_Update
        public DataTable PR_MST_EmployeeType_Update(int EmployeeTypeID, string EmployeeType)
		{
			try
			{
				SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
				DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_EmployeeType_Update");
				sqlDB.AddInParameter(dbCMD, "EmployeeTypeID", SqlDbType.Int, EmployeeTypeID);
				sqlDB.AddInParameter(dbCMD, "EmployeeType", SqlDbType.VarChar, EmployeeType);

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
