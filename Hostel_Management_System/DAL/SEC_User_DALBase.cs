using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace Hostel_Management_System.DAL
{
	public class SEC_User_DALBase : DAL_Helper
	{
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
	}
}
