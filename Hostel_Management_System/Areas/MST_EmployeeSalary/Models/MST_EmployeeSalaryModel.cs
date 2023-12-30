namespace Hostel_Management_System.Areas.MST_EmployeeSalary.Models
{
    public class MST_EmployeeSalaryModel
    {
        public int? EmployeeSalaryID { get; set; }
        public int EmployeeID { get; set; }
        public string PaymentMode { get; set; }
        public decimal Salary { get; set; }
        public DateTime SalaryDate { get; set;}
        public string ChequeNo { get; set; }
        public string BankName { get; set; }
        public string Remark { get; set; }
        public bool Status { get; set; }
        public DateTime Created { get; set; }  
        public DateTime Modified { get; set;}

    }
}
