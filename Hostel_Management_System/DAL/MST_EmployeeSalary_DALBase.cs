using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace Hostel_Management_System.DAL
{
    public class MST_EmployeeSalary_DALBase : DAL_Helper
    {
        #region PR_MST_EmployeeSalary_SelectAll
        public DataTable PR_MST_EmployeeSalary_SelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_EmployeeSalary_SelectAll");
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

        #region PR_MST_EmployeeSalary_Delete
        public bool? PR_MST_EmployeeSalary_Delete(int EmployeeSalaryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_EmployeeSalary_Delete");
                sqlDB.AddInParameter(dbCMD, "EmployeeSalaryID", SqlDbType.Int, EmployeeSalaryID);
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

        #region PR_MST_EmployeeSalary_SelectByPK
        public DataTable PR_MST_EmployeeSalary_SelectByPK(int? EmployeeSalaryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_EmployeeSalary_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "EmployeeSalaryID", SqlDbType.Int, EmployeeSalaryID);

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

        #region PR_MST_EmployeeSalary_Insert
        public DataTable PR_MST_EmployeeSalary_Insert(int EmployeeID, DateTime SalaryDate, decimal Salary, string Remark, string PaymentMode, string BankName, string ChequeNo, bool Status)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_EmployeeSalary_Insert");
                sqlDB.AddInParameter(dbCMD, "EmployeeID", SqlDbType.Int, EmployeeID);
                sqlDB.AddInParameter(dbCMD, "SalaryDate", SqlDbType.DateTime, SalaryDate);
                sqlDB.AddInParameter(dbCMD, "Remark", SqlDbType.VarChar, Remark);
                sqlDB.AddInParameter(dbCMD, "Salary", SqlDbType.Decimal, Salary);
                sqlDB.AddInParameter(dbCMD, "PaymentMode", SqlDbType.VarChar, PaymentMode);
                sqlDB.AddInParameter(dbCMD, "BankName", SqlDbType.VarChar, BankName);
                sqlDB.AddInParameter(dbCMD, "ChequeNo", SqlDbType.VarChar, ChequeNo);
                sqlDB.AddInParameter(dbCMD, "Status", SqlDbType.Bit, Status);

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

        #region PR_MST_EmployeeSalary_Update
        public DataTable PR_MST_EmployeeSalary_Update(int EmployeeSalaryID, int EmployeeID, DateTime SalaryDate, decimal Salary, string Remark, string PaymentMode, string BankName, string ChequeNo, bool Status)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_EmployeeSalary_Update");
                sqlDB.AddInParameter(dbCMD, "EmployeeSalaryID", SqlDbType.Int, EmployeeSalaryID);
                sqlDB.AddInParameter(dbCMD, "EmployeeID", SqlDbType.Int, EmployeeID);
                sqlDB.AddInParameter(dbCMD, "SalaryDate", SqlDbType.DateTime, SalaryDate);
                sqlDB.AddInParameter(dbCMD, "Remark", SqlDbType.VarChar, Remark);
                sqlDB.AddInParameter(dbCMD, "Salary", SqlDbType.Decimal, Salary);
                sqlDB.AddInParameter(dbCMD, "PaymentMode", SqlDbType.VarChar, PaymentMode);
                sqlDB.AddInParameter(dbCMD, "BankName", SqlDbType.VarChar, BankName);
                sqlDB.AddInParameter(dbCMD, "ChequeNo", SqlDbType.VarChar, ChequeNo);
                sqlDB.AddInParameter(dbCMD, "Status", SqlDbType.Bit, Status);

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

        #region PR_MST_EmployeeSalary_Filter
        public DataTable PR_MST_EmployeeSalary_Filter(string EmployeeName, DateTime? SalaryDate, string PaymentMode)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_EmployeeSalary_Filter");
                sqlDB.AddInParameter(dbCMD, "EmployeeName", SqlDbType.VarChar, EmployeeName);
                sqlDB.AddInParameter(dbCMD, "SalaryDate", SqlDbType.DateTime, SalaryDate);
                sqlDB.AddInParameter(dbCMD, "PaymentMode", SqlDbType.VarChar, PaymentMode);

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
