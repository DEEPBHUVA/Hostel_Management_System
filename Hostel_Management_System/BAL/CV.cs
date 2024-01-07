namespace Hostel_Management_System.BAL
{
	public class CV
	{
		private static IHttpContextAccessor _contextAccessor;
		static CV()
		{
			_contextAccessor = new HttpContextAccessor();
		}

		public static string? UserName()
		{
			string? UserName = null;
			if (_contextAccessor.HttpContext.Session.GetString("UserName") != null)
			{
				UserName = _contextAccessor.HttpContext.Session.GetString("UserName").ToString();
			}
			return UserName;
		}

		public static int? UserID()
		{
			int? UserID = null;
			if (_contextAccessor.HttpContext.Session.GetString("UserID") != null)
			{
				UserID = Convert.ToInt32(_contextAccessor.HttpContext.Session.GetString("UserID"));
			}
			return UserID;
		}
	}
}
