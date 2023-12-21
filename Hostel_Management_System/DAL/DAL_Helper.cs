namespace Hostel_Management_System.DAL
{
    public class DAL_Helper
    {
        #region MyConnectionStr
        public static string MyConnectionStr = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("ConStr");
        #endregion
    }
}
