using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Hostel_Management_System.DAL
{
    public class MST_Course_DALBase : DAL_Helper
    {
        #region PR_MST_Course_SelectAll
        public DataTable PR_MST_Course_SelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Course_SelectAll");
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

        #region PR_MST_Course_Delete
        public bool? PR_City_DeleteByPK(int CourseID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Course_Delete");
                sqlDB.AddInParameter(dbCMD, "CourseID", SqlDbType.Int, CourseID);
                int delete = sqlDB.ExecuteNonQuery(dbCMD);
                return (delete == -1 ? false : true);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region PR_MST_Course_SelectByPk
        public DataTable PR_MST_Course_SelectByPk(int? CourseID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Course_SelectByPk");
                sqlDB.AddInParameter(dbCMD, "CourseID", SqlDbType.Int, CourseID);

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

        #region PR_MST_Course_Insert
        public DataTable PR_MST_Course_Insert(string CourseName)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Course_Insert");
                sqlDB.AddInParameter(dbCMD, "CourseName", SqlDbType.VarChar, CourseName);

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

        #region PR_MST_COURSE_UPDATE
        public DataTable PR_MST_COURSE_UPDATE(int CourseID, string CourseName)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_COURSE_UPDATE");
                sqlDB.AddInParameter(dbCMD, "CourseID", SqlDbType.Int, CourseID);
                sqlDB.AddInParameter(dbCMD, "CourseName", SqlDbType.VarChar, CourseName);

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
