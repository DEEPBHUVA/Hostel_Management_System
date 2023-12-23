using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace Hostel_Management_System.DAL
{
    public class MST_Payment_DALBase : DAL_Helper
    {
        #region PR_MST_Payment_SelectAll
        public DataTable PR_MST_Payment_SelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Payment_SelectAll");
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

        #region PR_MST_Payment_Delete
        public bool? PR_MST_Payment_Delete(int PaymentID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Payment_Delete");
                sqlDB.AddInParameter(dbCMD, "PaymentID", SqlDbType.Int, PaymentID);
                int delete = sqlDB.ExecuteNonQuery(dbCMD);
                return (delete == -1 ? false : true);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region PR_MST_Payment_SelectByPK
        public DataTable PR_MST_Payment_SelectByPK(int? PaymentID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Payment_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "PaymentID", SqlDbType.Int, PaymentID);

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

        #region PR_MST_Payment_Insert
        public DataTable PR_MST_Payment_Insert(int StudentID, DateTime PaymentDate, string MobileNo, decimal Amount, string Remark, string PaidBY)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Payment_Insert");
                sqlDB.AddInParameter(dbCMD, "StudentID", SqlDbType.Int, StudentID);
                sqlDB.AddInParameter(dbCMD, "PaymentDate", SqlDbType.DateTime, PaymentDate);
                sqlDB.AddInParameter(dbCMD, "MobileNo", SqlDbType.VarChar, MobileNo);
                sqlDB.AddInParameter(dbCMD, "Amount", SqlDbType.Decimal, Amount);
                sqlDB.AddInParameter(dbCMD, "Remark", SqlDbType.VarChar, Remark);
                sqlDB.AddInParameter(dbCMD, "PaidBY", SqlDbType.VarChar, PaidBY);

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

        #region PR_MST_Payment_Update
        public DataTable PR_MST_Payment_Update(int PaymentID, int StudentID, DateTime PaymentDate, string MobileNo, decimal Amount, string Remark, string PaidBY)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Payment_Update");
                sqlDB.AddInParameter(dbCMD, "PaymentID", SqlDbType.Int, PaymentID);
                sqlDB.AddInParameter(dbCMD, "StudentID", SqlDbType.Int, StudentID);
                sqlDB.AddInParameter(dbCMD, "PaymentDate", SqlDbType.DateTime, PaymentDate);
                sqlDB.AddInParameter(dbCMD, "MobileNo", SqlDbType.VarChar, MobileNo);
                sqlDB.AddInParameter(dbCMD, "Amount", SqlDbType.Decimal, Amount);
                sqlDB.AddInParameter(dbCMD, "Remark", SqlDbType.VarChar, Remark);
                sqlDB.AddInParameter(dbCMD, "PaidBY", SqlDbType.VarChar, PaidBY);

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
