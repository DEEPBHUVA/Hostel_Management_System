using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Hostel_Management_System.Areas.SEC_User.Models
{
	public class SEC_UserModel
	{
		public int? UserID { get; set; }

		[DisplayName("UserName")]
		public string UserName { get; set; }
		[Required]
		[DisplayName("Password")]
		public string? Password { get; set; }
        [Required]
        [DisplayName("OldPassword")]
        public string? OldPassword { get; set; }
        [Required]
        [DisplayName("NewPassword")]
        public string? NewPassword { get; set; }
        public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int? StudentID { get; set; }
		public string UserRole { get; set; }
		public string? PhotoPath { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
	}
}
