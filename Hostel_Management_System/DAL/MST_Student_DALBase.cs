using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace Hostel_Management_System.DAL
{
    public class MST_Student_DALBase : DAL_Helper
    {
        #region PR_MST_Student_SelectAll
        public DataTable PR_MST_Student_SelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Student_SelectAll");
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

        #region PR_MST_Student_GetAllStudent
        public DataTable PR_MST_Student_GetAllStudent()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Student_GetAllStudent");
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

        #region PR_MST_Student_DeleteByPk
        public bool? PR_MST_Student_DeleteByPk(int StudentID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Student_DeleteByPk");
                sqlDB.AddInParameter(dbCMD, "StudentID", SqlDbType.Int, StudentID);
                int delete = sqlDB.ExecuteNonQuery(dbCMD);
                return (delete == -1 ? false : true);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region PR_Mst_StudentUpdateStatus
        public bool? PR_Mst_StudentUpdateStatus(int StudentID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_Mst_StudentUpdateStatus");
                sqlDB.AddInParameter(dbCMD, "StudentID", SqlDbType.Int, StudentID);
                int delete = sqlDB.ExecuteNonQuery(dbCMD);
                return (delete == -1 ? false : true);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region PR_MST_Student_SelectByPk
        public DataTable PR_MST_Student_SelectByPk(int? StudentID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Student_SelectByPk");
                sqlDB.AddInParameter(dbCMD, "StudentID", SqlDbType.Int, StudentID);

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

        #region PR_MST_Student_Insert
        public DataTable PR_MST_Student_Insert
        (
            string StudentName,
            string Email,
            string MobileNo,
            string BloodGroup,
            DateTime BirthDate,
            int Age,
            string FatherName,
            string FatherMobileNo,
            string MotherName,
            string MotherMobileNo,
            string LocalGurdianName,
            string LocalGurdianNo,
            string Nationlity,
            string AadharCardNo,
            string PresentAddress,
            string PermentAddress,
            string isActive,
            int CourseID,
            string Remarks,
            string? PhotoPath
        )
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Student_Insert");
                sqlDB.AddInParameter(dbCMD, "StudentName", SqlDbType.VarChar, StudentName);
                sqlDB.AddInParameter(dbCMD, "Email", SqlDbType.VarChar, Email);
                sqlDB.AddInParameter(dbCMD, "MobileNo", SqlDbType.VarChar, MobileNo);
                sqlDB.AddInParameter(dbCMD, "BloodGroup", SqlDbType.VarChar, BloodGroup);
                sqlDB.AddInParameter(dbCMD, "BirthDate", SqlDbType.DateTime, BirthDate);
                sqlDB.AddInParameter(dbCMD, "Age", SqlDbType.Int, Age);
                sqlDB.AddInParameter(dbCMD, "FatherName", SqlDbType.VarChar, FatherName);
                sqlDB.AddInParameter(dbCMD, "FatherMobileNo", SqlDbType.VarChar, FatherMobileNo);
                sqlDB.AddInParameter(dbCMD, "MotherName", SqlDbType.VarChar, MotherName);
                sqlDB.AddInParameter(dbCMD, "MotherMobileNo", SqlDbType.VarChar, MotherMobileNo);
                sqlDB.AddInParameter(dbCMD, "LocalGurdianName", SqlDbType.VarChar, LocalGurdianName);
                sqlDB.AddInParameter(dbCMD, "LocalGurdianNo", SqlDbType.VarChar, LocalGurdianNo);
                sqlDB.AddInParameter(dbCMD, "Nationlity", SqlDbType.VarChar, Nationlity);
                sqlDB.AddInParameter(dbCMD, "AadharCardNo", SqlDbType.VarChar, AadharCardNo);
                sqlDB.AddInParameter(dbCMD, "PresentAddress", SqlDbType.VarChar, PresentAddress);
                sqlDB.AddInParameter(dbCMD, "PermentAddress", SqlDbType.VarChar, PermentAddress);
                sqlDB.AddInParameter(dbCMD, "isActive", SqlDbType.VarChar, isActive);
                sqlDB.AddInParameter(dbCMD, "CourseID", SqlDbType.Int, CourseID);
                sqlDB.AddInParameter(dbCMD, "Remarks", SqlDbType.VarChar, Remarks);
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

        #region PR_MST_Student_Update
        public DataTable PR_MST_Student_Update
        (
            int StudentID,
            string StudentName,
            string Email,
            string MobileNo,
            string BloodGroup,
            DateTime BirthDate,
            int Age,
            string FatherName,
            string FatherMobileNo,
            string MotherName,
            string MotherMobileNo,
            string LocalGurdianName,
            string LocalGurdianNo,
            string Nationlity,
            string AadharCardNo,
            string PresentAddress,
            string PermentAddress,
            string isActive,
            int CourseID,
            string Remarks,
            string? PhotoPath
        )
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Student_Update");
                sqlDB.AddInParameter(dbCMD, "StudentID", SqlDbType.Int, StudentID);
                sqlDB.AddInParameter(dbCMD, "StudentName", SqlDbType.VarChar, StudentName);
                sqlDB.AddInParameter(dbCMD, "Email", SqlDbType.VarChar, Email);
                sqlDB.AddInParameter(dbCMD, "MobileNo", SqlDbType.VarChar, MobileNo);
                sqlDB.AddInParameter(dbCMD, "BloodGroup", SqlDbType.VarChar, BloodGroup);
                sqlDB.AddInParameter(dbCMD, "BirthDate", SqlDbType.DateTime, BirthDate);
                sqlDB.AddInParameter(dbCMD, "Age", SqlDbType.Int, Age);
                sqlDB.AddInParameter(dbCMD, "FatherName", SqlDbType.VarChar, FatherName);
                sqlDB.AddInParameter(dbCMD, "FatherMobileNo", SqlDbType.VarChar, FatherMobileNo);
                sqlDB.AddInParameter(dbCMD, "MotherName", SqlDbType.VarChar, MotherName);
                sqlDB.AddInParameter(dbCMD, "MotherMobileNo", SqlDbType.VarChar, MotherMobileNo);
                sqlDB.AddInParameter(dbCMD, "LocalGurdianName", SqlDbType.VarChar, LocalGurdianName);
                sqlDB.AddInParameter(dbCMD, "LocalGurdianNo", SqlDbType.VarChar, LocalGurdianNo);
                sqlDB.AddInParameter(dbCMD, "Nationlity", SqlDbType.VarChar, Nationlity);
                sqlDB.AddInParameter(dbCMD, "AadharCardNo", SqlDbType.VarChar, AadharCardNo);
                sqlDB.AddInParameter(dbCMD, "PresentAddress", SqlDbType.VarChar, PresentAddress);
                sqlDB.AddInParameter(dbCMD, "PermentAddress", SqlDbType.VarChar, PermentAddress);
                sqlDB.AddInParameter(dbCMD, "isActive", SqlDbType.VarChar, isActive);
                sqlDB.AddInParameter(dbCMD, "CourseID", SqlDbType.Int, CourseID);
                sqlDB.AddInParameter(dbCMD, "Remarks", SqlDbType.VarChar, Remarks);
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

        #region PR_MST_Payment_SelectStudentID
        public DataTable PR_MST_Payment_SelectStudentID(int? StudentID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Payment_SelectStudentID");
                sqlDB.AddInParameter(dbCMD, "SudentID", SqlDbType.Int, StudentID);

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


        #region PR_MST_Student_SeleckbyPkWithAllData
        public DataTable PR_MST_Student_SeleckbyPkWithAllData(int? StudentID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Student_SeleckbyPkWithAllData");
                sqlDB.AddInParameter(dbCMD, "StudentID", SqlDbType.Int, StudentID);

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
