namespace Hostel_Management_System.Areas.MST_Payment.Models
{
    public class MST_PaymentModel
    {
        public int? PaymentID { get; set; }
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public DateTime PaymentDate { get; set; }
        public string MobileNo { get; set; }
        public decimal Amount { get; set; }
        public string? ChequeNo { get; set; }
        public string? BankName { get; set; }
        public string? Remark { get; set; }
        public string PaidBY { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
