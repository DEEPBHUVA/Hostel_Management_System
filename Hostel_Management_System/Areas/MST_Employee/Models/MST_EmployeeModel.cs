namespace Hostel_Management_System.Areas.MST_Employee.Models
{
    public class MST_EmployeeModel
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public DateTime BirthDate {  get; set; }
        public int Age { get; set; }
        public string BloodGroup { get; set; }
        public string Gender { get; set; }
        public DateTime JoiningDate { get; set; }
        public decimal Salary {  get; set; }
        public string Address { get; set; }
        public int EmployeeTypeID { get; set; }
        public bool isActive { get; set; }
        public string? PhotoPath { get; set; }
        public IFormFile? File { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
