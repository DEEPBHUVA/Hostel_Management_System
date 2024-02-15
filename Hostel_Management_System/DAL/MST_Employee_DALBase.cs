using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace Hostel_Management_System.DAL
{
    public class MST_Employee_DALBase : DAL_Helper
    {
        #region PR_MST_Employee_SelectAll
        public DataTable PR_MST_Employee_SelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Employee_SelectAll");
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

        #region PR_MST_Employee_Delete
        public bool? PR_MST_Employee_Delete(int EmployeeID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Employee_Delete");
                sqlDB.AddInParameter(dbCMD, "EmployeeID", SqlDbType.Int, EmployeeID);
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


        #region PR_MST_Employee_SelectByPK
        public DataTable PR_MST_Employee_SelectByPK(int? EmployeeID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Employee_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "EmployeeID", SqlDbType.Int, EmployeeID);

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

        #region PR_MST_Employee_Insert
        public DataTable PR_MST_Employee_Insert
        (
            string EmployeeName,
            string Email,
            string MobileNo,
            string BloodGroup,
            DateTime BirthDate,
            int Age,
            string Gender,
            DateTime JoiningDate,
            decimal Salary,
            string Address,
            int EmployeeTypeID,
            bool isActive,
            string? PhotoPath
        )
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Employee_Insert");
                sqlDB.AddInParameter(dbCMD, "EmployeeName", SqlDbType.VarChar, EmployeeName);
                sqlDB.AddInParameter(dbCMD, "Email", SqlDbType.VarChar, Email);
                sqlDB.AddInParameter(dbCMD, "MobileNo", SqlDbType.VarChar, MobileNo);
                sqlDB.AddInParameter(dbCMD, "BloodGroup", SqlDbType.VarChar, BloodGroup);
                sqlDB.AddInParameter(dbCMD, "BirthDate", SqlDbType.DateTime, BirthDate);
                sqlDB.AddInParameter(dbCMD, "Age", SqlDbType.Int, Age);
                sqlDB.AddInParameter(dbCMD, "Gender", SqlDbType.VarChar, Gender);
                sqlDB.AddInParameter(dbCMD, "JoiningDate", SqlDbType.DateTime, JoiningDate);
                sqlDB.AddInParameter(dbCMD, "Address", SqlDbType.VarChar, Address);
                sqlDB.AddInParameter(dbCMD, "Salary", SqlDbType.Decimal, Salary);
                sqlDB.AddInParameter(dbCMD, "isActive", SqlDbType.VarChar, isActive);
                sqlDB.AddInParameter(dbCMD, "EmployeeTypeID", SqlDbType.Int, EmployeeTypeID);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.NVarChar, PhotoPath);


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

        #region PR_MST_Employee_Update
        public DataTable PR_MST_Employee_Update
        (
            int EmployeeID,
            string EmployeeName,
            string Email,
            string MobileNo,
            string BloodGroup,
            DateTime BirthDate,
            int Age,
            string Gender,
            DateTime JoiningDate,
            decimal Salary,
            string Address,
            int EmployeeTypeID,
            bool isActive,
            string? PhotoPath
        )
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Employee_Update");
                sqlDB.AddInParameter(dbCMD, "EmployeeID", SqlDbType.Int, EmployeeID);
                sqlDB.AddInParameter(dbCMD, "EmployeeName", SqlDbType.VarChar, EmployeeName);
                sqlDB.AddInParameter(dbCMD, "Email", SqlDbType.VarChar, Email);
                sqlDB.AddInParameter(dbCMD, "MobileNo", SqlDbType.VarChar, MobileNo);
                sqlDB.AddInParameter(dbCMD, "BloodGroup", SqlDbType.VarChar, BloodGroup);
                sqlDB.AddInParameter(dbCMD, "BirthDate", SqlDbType.DateTime, BirthDate);
                sqlDB.AddInParameter(dbCMD, "Age", SqlDbType.Int, Age);
                sqlDB.AddInParameter(dbCMD, "Gender", SqlDbType.VarChar, Gender);
                sqlDB.AddInParameter(dbCMD, "JoiningDate", SqlDbType.DateTime, JoiningDate);
                sqlDB.AddInParameter(dbCMD, "Address", SqlDbType.VarChar, Address);
                sqlDB.AddInParameter(dbCMD, "Salary", SqlDbType.Decimal, Salary);
                sqlDB.AddInParameter(dbCMD, "isActive", SqlDbType.VarChar, isActive);
                sqlDB.AddInParameter(dbCMD, "EmployeeTypeID", SqlDbType.Int, EmployeeTypeID);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.NVarChar, PhotoPath);

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

        #region PR_MST_Employee_Filter
        public DataTable PR_MST_Employee_Filter(string EmployeeName, DateTime? JoiningDate, string EmployeeType)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Employee_Filter");
                sqlDB.AddInParameter(dbCMD, "EmployeeName", SqlDbType.VarChar, EmployeeName);
                sqlDB.AddInParameter(dbCMD, "JoiningDate", SqlDbType.DateTime, JoiningDate);
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
                throw e;
                return null;
            }
        }
        #endregion
    }
}
