using System.Data;

namespace Hostel_Management_System.DAL
{
    public class MST_Payment_DAL : MST_Payment_DALBase
    {
        internal DataTable PR_MST_Payment_Insert(int studentID, string? remark, DateTime paymentDate, string mobileNo, string paidBY, decimal amount)
        {
            throw new NotImplementedException();
        }

		internal DataTable PR_MST_Payment_Update(int paymentID, int studentID, string? remark, DateTime paymentDate, string mobileNo, string paidBY, decimal amount)
		{
			throw new NotImplementedException();
		}
	}
}
