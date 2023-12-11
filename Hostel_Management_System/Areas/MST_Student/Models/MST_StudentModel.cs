namespace Hostel_Management_System.Areas.MST_Student.Models
{
    public class MST_StudentModel
    {
        public int SId { get; set; }
        public int FullName { get; set;}
        public DateTime Dob { get; set;}
        public string Email { get; set;}
        public string ContactNo { get; set;}
        public string Address { get; set;}
        public string FatherName { get; set;}
        public string FatherContactNo { get; set;}
        public string MotherName { get; set;}
        public string MotherContactNo { get;set;}
        public bool Status { get; set;}
        public int RoomId { get; set;}
        public int RoomNo{ get; set; }
        public int Age { get; set;}
        public string Photopath { get; set;}
        public DateTime Created { get; set;}
        public DateTime Modified { get; set;}
    }
}
