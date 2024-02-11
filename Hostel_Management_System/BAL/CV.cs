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

        public static string? Email()
        {
            string? Email = null;
            if (_contextAccessor.HttpContext.Session.GetString("Email") != null)
            {
                Email = _contextAccessor.HttpContext.Session.GetString("Email").ToString();
            }
            return Email;
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

		public static string? FristName()
		{
			string? FristName = null;
			if (_contextAccessor.HttpContext.Session.GetString("FristName") != null)
			{
				FristName = _contextAccessor.HttpContext.Session.GetString("FristName").ToString();
			}
			return FristName;
		}

		public static string? LastName()
		{
			string? LastName = null;
			if (_contextAccessor.HttpContext.Session.GetString("LastName") != null)
			{
				LastName = _contextAccessor.HttpContext.Session.GetString("LastName").ToString();
			}
			return LastName;
		}

        public static string? UserRole()
        {
            string? UserRole = null;
            if (_contextAccessor.HttpContext.Session.GetString("UserRole") != null)
            {
                UserRole = _contextAccessor.HttpContext.Session.GetString("UserRole").ToString();
            }
            return UserRole;
        }

        public static string? PhotoPath()
        {
            string? PhotoPath = null;
            if (_contextAccessor.HttpContext.Session.GetString("PhotoPath") != null)
            {
                PhotoPath = _contextAccessor.HttpContext.Session.GetString("PhotoPath").ToString();
            }
            return PhotoPath;
        }

        public static int? StudentID()
        {
            int? StudentID = null;
            if (_contextAccessor.HttpContext.Session.GetString("StudentID") != null)
            {
                StudentID = Convert.ToInt32(_contextAccessor.HttpContext.Session.GetString("StudentID"));
            }
            return StudentID;
        }

        public static DateTime? Created()
        {
            DateTime? Created = null;
            if (_contextAccessor.HttpContext.Session.GetString("Created") != null)
            {
                Created = Convert.ToDateTime(_contextAccessor.HttpContext.Session.GetString("Created"));
            }
            return Created;
        }
    }
}
