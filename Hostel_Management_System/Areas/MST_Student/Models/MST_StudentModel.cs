using System.ComponentModel.DataAnnotations;

namespace Hostel_Management_System.Areas.MST_Student.Models
{
    public class MST_StudentModel
    {
        public int? StudentID { get; set; }

        [Required(ErrorMessage = "Student Name Required!")]
        public string StudentName { get; set; }
        //=======================================================
        [Required(ErrorMessage = "Email Address Required!")]
        [EmailAddress]
        public string Email { get; set; }
        //=======================================================
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
       ErrorMessage = "Entered phone format is not valid.")]
        public string MobileNo { get; set; }
        //=======================================================
        [Required(ErrorMessage = "Select Blood Group!")]
        public string BloodGroup { get; set; }
        //=======================================================
        [Required]
        public DateTime BirthDate { get; set; }
		//=======================================================
		[Required(ErrorMessage = "Enter Age, it is required")]
		[Range(1, 100, ErrorMessage = "Please enter the correct value")]
		public int Age { get; set; }
        //=======================================================
        [Required(ErrorMessage = "Father Name Required!")]
        public string FatherName { get; set; }
        //=======================================================
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
       ErrorMessage = "Entered phone format is not valid.")]
        public string FatherMobileNo { get; set; }
        //=======================================================
        [Required(ErrorMessage = "Mother Name Required!")]
        public string MotherName { get; set; }
        //=======================================================
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
       ErrorMessage = "Entered phone format is not valid.")]
        public string MotherMobileNo { get; set; }
        //=======================================================
        [Required(ErrorMessage = "Gaurdian Name Required!")]
        public string LocalGurdianName { get; set; }
        //=======================================================
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
       ErrorMessage = "Entered phone format is not valid.")]
        public string LocalGurdianNo { get; set; }
        //=======================================================
        [Required]
        public string Nationlity { get; set; }
        //=======================================================
        [Required(ErrorMessage = "Aadhar Card No. Required!")]
        public string AadharCardNo { get; set; }
        //=======================================================
        [Required]
        public string PresentAddress { get; set; }
        //=======================================================
        [Required]
        public string PermentAddress { get; set; }
        //=======================================================
        [Required(ErrorMessage = "Select Status!")]
        public string isActive { get; set; }
        //=======================================================
        public string? PhotoPath { get; set; }
        //=======================================================
        public string CourseName { get; set; }
        //=======================================================
        [Required(ErrorMessage = "Select Course!")]
        public int CourseID { get; set; }
        //=======================================================
        public string Remarks { get; set; } 
        public IFormFile? File { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    public class MST_StudentDropdown
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
    }
}
