namespace Hostel_Management_System.Areas.MST_BillCalculation.Models
{
    public class MST_BillCalculationModel
    {
        public int? BillID { get; set; }
        public int BillTypeID { get; set;}
        public decimal Amount { get; set;}
        public DateTime BillDate { get; set;}
        public string Description{ get; set;}
        public DateTime Created { get; set;}
        public DateTime Modified { get; set;}
    }
}
