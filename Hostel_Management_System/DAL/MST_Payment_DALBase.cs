using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

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
        public DataTable PR_MST_Payment_Insert(int StudentID, DateTime PaymentDate, string MobileNo, decimal Amount, string Remark, string PaidBY, string BankName, string ChequeNo)
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
                sqlDB.AddInParameter(dbCMD, "BankName", SqlDbType.VarChar, BankName);
                sqlDB.AddInParameter(dbCMD, "ChequeNo", SqlDbType.VarChar, ChequeNo);

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
        public DataTable PR_MST_Payment_Update(int PaymentID, int StudentID, DateTime PaymentDate, string MobileNo, decimal Amount, string Remark, string PaidBY, string BankName, string ChequeNo)
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
                sqlDB.AddInParameter(dbCMD, "BankName", SqlDbType.VarChar, BankName);
                sqlDB.AddInParameter(dbCMD, "ChequeNo", SqlDbType.VarChar, ChequeNo);

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

        #region PR_MST_Payment_Filter
        public DataTable PR_MST_Payment_Filter(string StudentName, DateTime? PaymentDate, string PaidBy)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Payment_Filter");
                sqlDB.AddInParameter(dbCMD, "StudentName", SqlDbType.VarChar, StudentName);
                sqlDB.AddInParameter(dbCMD, "PaymentDate", SqlDbType.DateTime, PaymentDate);
                sqlDB.AddInParameter(dbCMD, "PaidBY", SqlDbType.VarChar, PaidBy);

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
