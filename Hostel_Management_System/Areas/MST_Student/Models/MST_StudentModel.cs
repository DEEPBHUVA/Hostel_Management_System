namespace Hostel_Management_System.Areas.MST_Student.Models
{
    public class MST_StudentModel
    {
        public int? StudentID { get; set; }
        public string StudentName { get; set; }
        public string Email {  get; set; }
        public string MobileNo { get; set; }    
        public string BloodGroup { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public string FatherName {  get; set; }
        public string FatherMobileNo { get; set; }
        public string MotherName {  get; set; }
        public string MotherMobileNo { get; set; }
        public string LocalGurdianName {  get; set; }
        public string LocalGurdianNo {  get; set; }
        public string Nationlity { get; set; }
        public string AadharCardNo {  get; set; }
        public string PresentAddress {  get; set; }
        public string PermentAddress { get; set; }
        public string isActive { get; set; }
        public string? PhotoPath { get; set; }
        public string CourseName { get; set; }
        public int CourseID {  get; set; }
        public IFormFile? File { get; set; }
        public DateTime Created { get; set;}
        public DateTime Modified { get; set;}
    }

    public class MST_StudentDropdown
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
    }
}
