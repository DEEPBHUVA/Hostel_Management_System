using Hostel_Management_System.Areas.MST_Payment.Models;
using Hostel_Management_System.Areas.MST_Room.Models;
using Hostel_Management_System.Areas.MST_Student.Models;
using Hostel_Management_System.DAL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using static Hostel_Management_System.Areas.MST_Student.Controllers.MST_StudentController;

namespace Hostel_Management_System.Areas.MST_Payment.Controllers
{
    [Area("MST_Payment")]
    [Route("MST_Payment/{Controller}/{action}")]
    public class MST_PaymentController : Controller
    {
        MST_Payment_DAL dalMST_Payment = new MST_Payment_DAL();

        #region Configuration
        public IConfiguration Configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public MST_PaymentController(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region SelectAllPayment
        public IActionResult Index()
        {
            DataTable dt = dalMST_Payment.PR_MST_Payment_SelectAll();
            return View("MST_PaymentList",dt);
        }
        #endregion

        #region Add
        public IActionResult Add(int? PaymentID)
        {

			#region Student Dropdown
			string MyConnectionStr1 = this.Configuration.GetConnectionString("ConStr");
			SqlConnection connection2 = new SqlConnection(MyConnectionStr1);
			DataTable dt2 = new DataTable();
			connection2.Open();
			SqlCommand cmd2 = connection2.CreateCommand();
			cmd2.CommandType = CommandType.StoredProcedure;
			cmd2.CommandText = "PR_MST_StudentDropdown";
			SqlDataReader reader2 = cmd2.ExecuteReader();
			dt2.Load(reader2);

			List<MST_StudentDropdown> list1 = new List<MST_StudentDropdown>();
			foreach (DataRow dr in dt2.Rows)
			{
				MST_StudentDropdown dlist = new MST_StudentDropdown();
				dlist.StudentID = Convert.ToInt32(dr["StudentID"]);
				dlist.StudentName = dr["StudentName"].ToString();
				list1.Add(dlist);
			}
			ViewBag.StudentList = list1;
			#endregion

			if (PaymentID != null)
            {
                DataTable dt = dalMST_Payment.PR_MST_Payment_SelectByPK(PaymentID);
                MST_PaymentModel modelMST_Payment = new MST_PaymentModel();

                foreach (DataRow row in dt.Rows)
                {
                    modelMST_Payment.StudentID = Convert.ToInt32(row["StudentID"]);
                    modelMST_Payment.Amount = Convert.ToDecimal(row["Amount"]);
                    modelMST_Payment.PaidBY = row["PaidBY"].ToString();
                    modelMST_Payment.MobileNo = row["MobileNo"].ToString();
                    modelMST_Payment.Remark = row["Remark"].ToString();
                    modelMST_Payment.BankName = row["BankName"].ToString();
                    modelMST_Payment.ChequeNo = row["ChequeNo"].ToString();
                    modelMST_Payment.PaymentDate = Convert.ToDateTime(row["PaymentDate"]);

                }
                return View("MST_PaymentAddEdit", modelMST_Payment);
            }
            return View("MST_PaymentAddEdit");
        }
        #endregion

        #region Save(Insert/Update)
        public IActionResult Save(MST_PaymentModel modelMST_Payment)
        {
            if (modelMST_Payment.PaymentID == null)
            {
                DataTable dt = dalMST_Payment.PR_MST_Payment_Insert(modelMST_Payment.StudentID, modelMST_Payment.PaymentDate, modelMST_Payment.MobileNo, modelMST_Payment.Amount,modelMST_Payment.Remark, modelMST_Payment.PaidBY,modelMST_Payment.BankName,modelMST_Payment.ChequeNo);
                TempData["MST_Payment_AlertMessage"] = "Record Inserted Successfully!!";
            }
            else
            {
                DataTable dt = dalMST_Payment.PR_MST_Payment_Update((int)modelMST_Payment.PaymentID, modelMST_Payment.StudentID, modelMST_Payment.PaymentDate, modelMST_Payment.MobileNo, modelMST_Payment.Amount, modelMST_Payment.Remark, modelMST_Payment.PaidBY, modelMST_Payment.BankName, modelMST_Payment.ChequeNo);
                TempData["MST_Payment_AlertMessage"] = "Record Updated Successfully!!";
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int PaymentID)
        {
            if (Convert.ToBoolean(dalMST_Payment.PR_MST_Payment_Delete(PaymentID)))
            {
                TempData["MST_Payment_Delete_AlertMessage"] = "Record Deleted Successfully";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Cancle
        public IActionResult Cancle()
        {
            return RedirectToAction("Index");
        }
        #endregion

        public MST_PaymentModel GetPaymentDetails(int PaymentID)
        {
            MST_PaymentModel paymentModel = new MST_PaymentModel();

            string myconnStr = this.Configuration.GetConnectionString("ConStr");
            SqlConnection connection = new SqlConnection(myconnStr);

            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PaymentID", PaymentID);
                cmd.CommandText = "PR_MST_Payment_SelectByPK";

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        paymentModel.StudentName = reader["StudentName"].ToString();
                        paymentModel.BankName = reader["BankName"].ToString();
                        paymentModel.ChequeNo = reader["ChequeNo"].ToString();
                        paymentModel.PaymentDate = Convert.ToDateTime(reader["PaymentDate"]);
                        paymentModel.MobileNo = reader["MobileNo"].ToString();
                        paymentModel.Amount = Convert.ToDecimal(reader["Amount"]);
                        paymentModel.PaidBY = reader["PaidBY"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return paymentModel;
        }


        public FileResult CreatePdf(int paymentId)
        {
            MemoryStream workStream = new MemoryStream();
            DateTime dTime = DateTime.Now;

            // File name to be created   
            string strPDFFileName = string.Format("Payment_Data_" + paymentId + "_" + dTime.ToString("yyyy-MM-dd") + ".pdf");

            float customWidth = PageSize.A4.Width;
            float customHeight = PageSize.A4.Height / 2;
            Rectangle customPageSize = new Rectangle(customWidth, customHeight);

            Document doc = new Document(customPageSize); // Set page size to landscape

            doc.SetMargins(10f, 10f, 10f, 10f); // Set margins

            // File will be created in this path  
            string strAttachment = Path.Combine(_hostingEnvironment.WebRootPath, "Downloadss", strPDFFileName);

            PdfWriter writer = PdfWriter.GetInstance(doc, workStream);
            writer.PageEvent = new PageEventHelper();
            writer.CloseStream = false;

            doc.Open();

            MST_PaymentModel paymentDetails = GetPaymentDetails(paymentId);

            if (paymentDetails != null)
            {
                PdfContentByte contentByte = writer.DirectContent;
                contentByte.SetLineWidth(0.5f); // Set border width
                contentByte.Rectangle(doc.Left, doc.Bottom, doc.Right - doc.Left, doc.Top - doc.Bottom);
                contentByte.Stroke(); // Draw the rectangle as a border

                var logoPath = "D:\\CollageMaterial\\Sem-5\\.Net\\.Net_Project\\Hostel_Management_System\\Hostel_Management_System\\wwwroot\\assets\\img\\logo-light.png";
                var logoImage = iTextSharp.text.Image.GetInstance(logoPath);

                // Set the dimensions of your logo
                float logoWidth = 150f; // Set your desired width
                float logoHeight = 75f; // Set your desired height

                // Scale the image while maintaining the aspect ratio
                float aspectRatio = logoImage.Width / logoImage.Height;
                logoImage.ScaleAbsolute(logoWidth, logoWidth / aspectRatio);

                // Calculate logo position to center vertically and horizontally
                float pageWidth = doc.PageSize.Width;
                float pageHeight = doc.PageSize.Height;

                float logoX = (pageWidth - logoImage.ScaledWidth) / 2;
                float logoY = (pageHeight - logoImage.ScaledHeight) / 2;

                // Set the logo position
                logoImage.SetAbsolutePosition(logoX, logoY);

                // Set opacity using a graphics state
                PdfGState gState = new PdfGState();
                gState.FillOpacity = 0.01f; // Set opacity value (0.0f to 1.0f)

                // Get the PdfContentByte for the document
                PdfContentByte canvas = writer.DirectContentUnder; // Or DirectContent for above the content

                // Set the graphics state
                canvas.SetGState(gState);

                // Add the logo to the document
                doc.Add(logoImage);

            }
            else
            {
                // Handle the case where payment details are not found for the given PaymentID
                // You can return an error message or handle it as per your application's requirements.
            }

            // Closing the document  
            doc.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return File(workStream, "application/pdf", strPDFFileName);
        }

    }
}
