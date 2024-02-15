using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Hostel_Management_System.DAL
{
    public class MST_BillCalculation_DALBase : DAL_Helper
    {
        #region PR_MST_BillCalculation_SelectAll
        public DataTable PR_MST_BillCalculation_SelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_BillCalculation_SelectAll");
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

        #region GetBillsByDateRange
        public DataTable GetBillsByDateRange(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("GetBillsByDateRange");
                sqlDB.AddInParameter(dbCMD, "FromDate", SqlDbType.DateTime, FromDate);
                sqlDB.AddInParameter(dbCMD, "ToDate", SqlDbType.DateTime, ToDate);
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

        #region PR_MST_BillCalculation_Delete
        public bool? PR_MST_BillCalculation_Delete(int BillID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_BillCalculation_Delete");
                sqlDB.AddInParameter(dbCMD, "BillID", SqlDbType.Int, BillID);
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
                };
            }
        }
        #endregion

        #region PR_MST_BillCalculation_SelectByPk
        public DataTable PR_MST_BillCalculation_SelectByPk(int? BillID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_BillCalculation_SelectByPk");
                sqlDB.AddInParameter(dbCMD, "BillID", SqlDbType.Int, BillID);

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

        #region PR_MST_BillCalculation_Insert
        public DataTable PR_MST_BillCalculation_Insert(int BillTypeID, DateTime BillDate, string Description, decimal Amount)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_BillCalculation_Insert");
                sqlDB.AddInParameter(dbCMD, "BillTypeID", SqlDbType.Int, BillTypeID);
                sqlDB.AddInParameter(dbCMD, "BillDate", SqlDbType.DateTime, BillDate);
                sqlDB.AddInParameter(dbCMD, "Description", SqlDbType.VarChar, Description);
                sqlDB.AddInParameter(dbCMD, "Amount", SqlDbType.Decimal, Amount);

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

        #region PR_MST_BillCalculation_Update
        public DataTable PR_MST_BillCalculation_Update(int BillID,int BillTypeID, DateTime BillDate, string Description, decimal Amount)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_BillCalculation_Update");
                sqlDB.AddInParameter(dbCMD, "BillID", SqlDbType.Int, BillID);
                sqlDB.AddInParameter(dbCMD, "BillTypeID", SqlDbType.Int, BillTypeID);
                sqlDB.AddInParameter(dbCMD, "BillDate", SqlDbType.DateTime, BillDate);
                sqlDB.AddInParameter(dbCMD, "Description", SqlDbType.VarChar, Description);
                sqlDB.AddInParameter(dbCMD, "Amount", SqlDbType.Decimal, Amount);

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
