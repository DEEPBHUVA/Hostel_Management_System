using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace Hostel_Management_System.DAL
{
	public class MST_BillType_DALBase : DAL_Helper
	{
		#region PR_MST_BillType_SelectAll
		public DataTable PR_MST_BillType_SelectAll()
		{
			try
			{
				SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
				DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_BillType_SelectAll");
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

		#region PR_MST_BillType_Delete
		public bool? PR_MST_BillType_Delete(int BillTypeID)
		{
			try
			{
				SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
				DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_BillType_Delete");
				sqlDB.AddInParameter(dbCMD, "BillTypeID", SqlDbType.Int, BillTypeID);
				int delete = sqlDB.ExecuteNonQuery(dbCMD);
				return (delete == -1 ? false : true);
			}
			catch (Exception e)
			{
				return null;
			}
		}
		#endregion

		#region PR_MST_BillType_SelectByPk
		public DataTable PR_MST_BillType_SelectByPk(int? BillTypeID)
		{
			try
			{
				SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
				DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_BillType_SelectByPk");
				sqlDB.AddInParameter(dbCMD, "BillTypeID", SqlDbType.Int, BillTypeID);

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

		#region PR_MST_BillType_Insert
		public DataTable PR_MST_BillType_Insert(string BillType)
		{
			try
			{
				SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
				DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_BillType_Insert");
				sqlDB.AddInParameter(dbCMD, "BillType", SqlDbType.VarChar, BillType);

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

		#region PR_MST_BillType_Update
		public DataTable PR_MST_BillType_Update(int BillTypeID, string BillType)
		{
			try
			{
				SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
				DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_BillType_Update");
				sqlDB.AddInParameter(dbCMD, "BillTypeID", SqlDbType.Int, BillTypeID);
				sqlDB.AddInParameter(dbCMD, "BillType", SqlDbType.VarChar, BillType);

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
