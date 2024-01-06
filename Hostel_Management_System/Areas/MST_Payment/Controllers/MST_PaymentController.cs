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
                        paymentModel.StudentID = Convert.ToInt32(reader["StudentID"]);
                        paymentModel.StudentName = reader["StudentName"].ToString();
                        paymentModel.BankName = reader["BankName"].ToString();
                        paymentModel.ChequeNo = reader["ChequeNo"].ToString();
                        paymentModel.PaymentDate = Convert.ToDateTime(reader["PaymentDate"]);
                        paymentModel.MobileNo = reader["MobileNo"].ToString();
                        paymentModel.Amount = Convert.ToDecimal(reader["Amount"]);
                        paymentModel.PaidBY = reader["PaidBY"].ToString();
                        paymentModel.ReceiptNo = reader["ReceiptNo"].ToString();
                        paymentModel.CourseName = reader["CourseName"].ToString();
                        paymentModel.Remark = reader["Remark"].ToString();
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

        #region CreatePDF
        public FileResult CreatePdf(int paymentId)
        {
            MemoryStream workStream = new MemoryStream();
            DateTime dTime = DateTime.Now;

            // File name to be created   
            string strPDFFileName = string.Format("Payment_Receipt_" + paymentId + "_" + dTime.ToString("yyyy-MM-dd") + ".pdf");

            float customWidth = PageSize.A4.Width;
            float customHeight = PageSize.A4.Height / 2;
            Rectangle customPageSize = new Rectangle(customWidth, customHeight);

            Document doc = new Document(customPageSize); 

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

                #region WaterMark Logo
                var logoPath1 = "D:\\CollageMaterial\\Sem-5\\.Net\\.Net_Project\\Hostel_Management_System\\Hostel_Management_System\\wwwroot\\assets\\img\\logo-light.png";
                var logoImage = iTextSharp.text.Image.GetInstance(logoPath1);

                // Set the dimensions of your logo
                float logoWidth1 = 150f; // Set your desired width
                float logoHeight1 = 75f; // Set your desired height

                // Scale the image while maintaining the aspect ratio
                float aspectRatio = logoImage.Width / logoImage.Height;
                logoImage.ScaleAbsolute(logoWidth1, logoWidth1 / aspectRatio);

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
                #endregion

                #region Heading
                var logoPath = "D:\\CollageMaterial\\Sem-5\\.Net\\.Net_Project\\Hostel_Management_System\\Hostel_Management_System\\wwwroot\\assets\\img\\logo.png";
                var pngImage = iTextSharp.text.Image.GetInstance(logoPath);

                var table = new PdfPTable(2);
                table.WidthPercentage = 100;

                float logoWidth = 200f; // Adjust the width as needed
                float logoHeight = 50f; // Adjust the height as needed
                pngImage.ScaleAbsolute(logoWidth, logoHeight);

                var logoCell = new PdfPCell(pngImage)
                {
                    Padding = 5f,
                    Border = Rectangle.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    BorderWidth = 0.5f,
                    
                };
                table.AddCell(logoCell);

                BaseFont customTitleFont = BaseFont.CreateFont("D:\\CollageMaterial\\Sem-5\\.Net\\.Net_Project\\Hostel_Management_System\\Hostel_Management_System\\wwwroot\\font\\Poppins-SemiBold.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                Font customPdfFont = new Font(customTitleFont, 14, Font.NORMAL, new BaseColor(80, 75, 142, 255));

                BaseFont customFont = BaseFont.CreateFont("D:\\CollageMaterial\\Sem-5\\.Net\\.Net_Project\\Hostel_Management_System\\Hostel_Management_System\\wwwroot\\font\\Poppins-Regular.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                Font customDePdfFont = new Font(customFont, 9, Font.NORMAL, BaseColor.GRAY);

                var paragraph = new Paragraph();

                paragraph.Add(new Phrase(" Skyline Boys Hostel", customPdfFont));
                paragraph.Add(new Phrase("\n\n+91-815606587/88 | www.skyline.com", customDePdfFont));
                paragraph.Add(new Phrase("\nskyline@gmail.com", customDePdfFont));

                
                // Create the cell with the paragraph
                var paragraphCell = new PdfPCell(paragraph)
                {
                    Padding = 5f,
                    Border = Rectangle.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    BorderWidth = 0.5f,
                };

                // Add the cell to the table
                table.AddCell(paragraphCell);

                doc.Add(table);
                #endregion

                #region Title
                BaseFont customHeadingFont = BaseFont.CreateFont("D:\\CollageMaterial\\Sem-5\\.Net\\.Net_Project\\Hostel_Management_System\\Hostel_Management_System\\wwwroot\\font\\Poppins-Regular.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                Font customReHeadingFont = new Font(customHeadingFont, 10, Font.NORMAL, BaseColor.DARK_GRAY);

                PdfPTable tableHeading = new PdfPTable(1)
                {
                    WidthPercentage = 100
                };

                // Add the heading text in a cell
                PdfPCell headingCell = new PdfPCell(new Phrase("Skyline Boys Hostel | Managed by D.B Patel | Rajkot, Zone, Gujarat in India.", customReHeadingFont))
                {
                    BackgroundColor = new BaseColor(216, 227, 255, 255),
                    PaddingBottom = 5f,
                    Border = Rectangle.BOX, // Remove cell border
                    HorizontalAlignment = Element.ALIGN_CENTER,
                };

                // Add the cell to the table
                tableHeading.AddCell(headingCell);
                doc.Add(tableHeading);
                #endregion

                #region ReceiptDetails

                PdfPTable tablePayment = new PdfPTable(4)
                {
                    WidthPercentage = 100,                
                };

                tablePayment.SetWidths(new float[] { 15, 40, 20, 25});
                // Add payment data to the table (example data)
                PdfPCell studentNameCell = new PdfPCell(new Phrase(" Student Name:", new Font(customReHeadingFont) { Color = BaseColor.GRAY }))
                {
                    Border = Rectangle.NO_BORDER, // No border
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                PdfPCell paymentDetailsCell = new PdfPCell(new Phrase(paymentDetails.StudentName, customReHeadingFont))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                PdfPCell RecieptCell = new PdfPCell(new Phrase("Reciept No:", new Font(customReHeadingFont) { Color = BaseColor.GRAY }))
                {
                    Border = Rectangle.NO_BORDER, // No border
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                PdfPCell RecieptDataCell = new PdfPCell(new Phrase(paymentDetails.ReceiptNo, new Font(customReHeadingFont) { Color = BaseColor.RED }))
                {
                    Border = Rectangle.NO_BORDER, // No border
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };


                tablePayment.AddCell(studentNameCell);
                tablePayment.AddCell(paymentDetailsCell);
                tablePayment.AddCell(RecieptCell);
                tablePayment.AddCell(RecieptDataCell);
                
                PdfPCell Mobilecell = new PdfPCell(new Phrase(" Mobile No:", new Font(customReHeadingFont) { Color = BaseColor.GRAY }))
                {
                    Border = Rectangle.NO_BORDER, // No border
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                PdfPCell MobileDatacell = new PdfPCell(new Phrase(paymentDetails.MobileNo, customReHeadingFont))
                {
                    Border = Rectangle.NO_BORDER, // No border
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                PdfPCell paymentDateCell = new PdfPCell(new Phrase("Payment Date:", new Font(customReHeadingFont) { Color = BaseColor.GRAY }))
                {
                    Border = Rectangle.NO_BORDER, // No border
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                string formattedPaymentDate = paymentDetails.PaymentDate.ToString("yyyy/MM/dd"); // Adjust the format as needed

                PdfPCell paymentDateDetailsCell = new PdfPCell(new Phrase(formattedPaymentDate, customReHeadingFont))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                tablePayment.AddCell(Mobilecell);
                tablePayment.AddCell(MobileDatacell);
                tablePayment.AddCell(paymentDateCell);
                tablePayment.AddCell(paymentDateDetailsCell);

                PdfPCell RegistrationCell = new PdfPCell(new Phrase(" Registration No:", new Font(customReHeadingFont) { Color = BaseColor.GRAY }))
                {
                    Border = Rectangle.NO_BORDER, // No border
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                PdfPCell RegistrationDataCell = new PdfPCell(new Phrase("", customReHeadingFont))
                {
                    Border = Rectangle.NO_BORDER, // No border
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                PdfPCell FinancialYearCell = new PdfPCell(new Phrase("Financial Year:", new Font(customReHeadingFont) { Color = BaseColor.GRAY }))
                {
                    Border = Rectangle.NO_BORDER, // No border
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                PdfPCell FinancialYearDataCell = new PdfPCell(new Phrase("2023-24", customReHeadingFont))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                tablePayment.AddCell(RegistrationCell);
                tablePayment.AddCell(RegistrationDataCell);
                tablePayment.AddCell(FinancialYearCell);
                tablePayment.AddCell(FinancialYearDataCell);


                PdfPCell StudentIDcell = new PdfPCell(new Phrase(" SID:", new Font(customReHeadingFont) { Color = BaseColor.GRAY }))
                {
                    Border = Rectangle.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                PdfPCell StudentIDDatacell = new PdfPCell(new Phrase(paymentDetails.StudentID.ToString(), customReHeadingFont))
                {
                    Border = Rectangle.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                PdfPCell CourseCell = new PdfPCell(new Phrase("Course:", new Font(customReHeadingFont) { Color = BaseColor.GRAY }))
                {
                    Border = Rectangle.BOTTOM_BORDER, 
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                PdfPCell CourseDataCell = new PdfPCell(new Phrase(paymentDetails.CourseName, customReHeadingFont))
                {
                    Border = Rectangle.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                tablePayment.AddCell(StudentIDcell);
                tablePayment.AddCell(StudentIDDatacell);
                tablePayment.AddCell(CourseCell);
                tablePayment.AddCell(CourseDataCell);

                doc.Add(new Paragraph("")); 
                doc.Add(tablePayment);


                PdfPTable tablePaymentDetails = new PdfPTable(4)
                {
                    WidthPercentage = 100,
                };

                tablePaymentDetails.SetWidths(new float[] { 15, 40, 20, 25 });

                PdfPCell particularscell = new PdfPCell(new Phrase(" Particulars:", new Font(customReHeadingFont) { Color = BaseColor.GRAY }))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                PdfPCell paticularsDetailscell = new PdfPCell(new Phrase(paymentDetails.Remark, customReHeadingFont))
                {
                    Border = Rectangle.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                PdfPCell Amountcell = new PdfPCell(new Phrase("Amount(₹):", new Font(customReHeadingFont) { Color = BaseColor.GRAY }))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                PdfPCell AmountDataCell = new PdfPCell(new Phrase(paymentDetails.Amount.ToString(), customReHeadingFont))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                tablePaymentDetails.AddCell(particularscell);
                tablePaymentDetails.AddCell(paticularsDetailscell);
                tablePaymentDetails.AddCell(Amountcell);
                tablePaymentDetails.AddCell(AmountDataCell);

                PdfPCell paymentmode = new PdfPCell(new Phrase(" Payment Mode:", new Font(customReHeadingFont) { Color = BaseColor.GRAY }))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                PdfPCell paymentmodedata = new PdfPCell(new Phrase(paymentDetails.PaidBY, customReHeadingFont))
                {
                    Border = Rectangle.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };
                PdfPCell abc = new PdfPCell(new Phrase("", new Font(customReHeadingFont) { Color = BaseColor.GRAY }))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                PdfPCell abcdata = new PdfPCell(new Phrase("", customReHeadingFont))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                tablePaymentDetails.AddCell(paymentmode);
                tablePaymentDetails.AddCell(paymentmodedata);
                tablePaymentDetails.AddCell(abc);
                tablePaymentDetails.AddCell(abcdata);


                PdfPCell Chequeno = new PdfPCell(new Phrase(" Cheque No:", new Font(customReHeadingFont) { Color = BaseColor.GRAY }))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                PdfPCell Chequenodata = new PdfPCell(new Phrase(paymentDetails.ChequeNo, customReHeadingFont))
                {
                    Border = Rectangle.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };
                PdfPCell abdv = new PdfPCell(new Phrase("", new Font(customReHeadingFont) { Color = BaseColor.GRAY }))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                PdfPCell abdvdata = new PdfPCell(new Phrase("", customReHeadingFont))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                tablePaymentDetails.AddCell(Chequeno);
                tablePaymentDetails.AddCell(Chequenodata);
                tablePaymentDetails.AddCell(abdv);
                tablePaymentDetails.AddCell(abdvdata);

                PdfPCell Bank = new PdfPCell(new Phrase(" Bank Name:", new Font(customReHeadingFont) { Color = BaseColor.GRAY }))
                {
                    Border = Rectangle.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                PdfPCell Bankdata = new PdfPCell(new Phrase(paymentDetails.BankName, customReHeadingFont))
                {
                    Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                PdfPCell abd = new PdfPCell(new Phrase("", new Font(customReHeadingFont) { Color = BaseColor.GRAY }))
                {
                    Border = Rectangle.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                PdfPCell abddata = new PdfPCell(new Phrase("", customReHeadingFont))
                {
                    Border = Rectangle.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                tablePaymentDetails.AddCell(Bank);
                tablePaymentDetails.AddCell(Bankdata);
                tablePaymentDetails.AddCell(abd);
                tablePaymentDetails.AddCell(abddata);

                PdfPCell blank = new PdfPCell(new Phrase("", new Font(customReHeadingFont) { Color = BaseColor.GRAY }))
                {
                    Border = Rectangle.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                PdfPCell blankdata = new PdfPCell(new Phrase("", customReHeadingFont))
                {
                    Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                Font boldFont = new Font(customReHeadingFont.BaseFont, customReHeadingFont.Size, Font.BOLD, BaseColor.BLACK);

                PdfPCell Total = new PdfPCell(new Phrase("Total(₹):", boldFont))
                {
                    Border = Rectangle.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                PdfPCell TotalData = new PdfPCell(new Phrase(paymentDetails.Amount.ToString(), customReHeadingFont))
                {
                    Border = Rectangle.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 5f,
                };

                tablePaymentDetails.AddCell(blank);
                tablePaymentDetails.AddCell(blankdata);
                tablePaymentDetails.AddCell(Total);
                tablePaymentDetails.AddCell(TotalData);

                doc.Add(tablePaymentDetails);

                #endregion

                #region Footer

                doc.Add(new Paragraph("")); // Add an empty paragraph for spacing

                Paragraph noticeparagraph = new Paragraph("  This Receipt is issued to Realisation of the cheque", new Font(customReHeadingFont) { Color = BaseColor.BLACK });
                doc.Add(noticeparagraph);

                Font boldFont1 = new Font(customHeadingFont, customReHeadingFont.Size, Font.BOLD, new BaseColor(80, 75, 142, 255));
                Paragraph additionalContent = new Paragraph();
                additionalContent.Add(new Chunk(" \u2022", boldFont1)); // Unicode character for bullet point
                additionalContent.Add(new Chunk(" Terms and Conditions:", boldFont1));
                doc.Add(additionalContent);

                BaseFont customnoticeFont = BaseFont.CreateFont("D:\\CollageMaterial\\Sem-5\\.Net\\.Net_Project\\Hostel_Management_System\\Hostel_Management_System\\wwwroot\\font\\Poppins-Regular.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                Font customnoticedgFont = new Font(customnoticeFont, 7, Font.NORMAL, BaseColor.DARK_GRAY);

                Paragraph Termandcon1 = new Paragraph();
                Termandcon1.Add(new Chunk(" \u2022", new Font(customReHeadingFont) { Color = BaseColor.GRAY })); // Unicode character for bullet point
                Termandcon1.Add(new Phrase(" Payments can be made through the accepted modes, including [Cash/Cheque]. For online transfers, residents must use the provided bank details.", new Font(customnoticedgFont) { Color = BaseColor.GRAY }));
                doc.Add(Termandcon1);

                Paragraph Termandcon2 = new Paragraph();
                Termandcon2.Add(new Chunk(" \u2022", new Font(customReHeadingFont) { Color = BaseColor.GRAY })); // Unicode character for bullet point
                Termandcon2.Add(new Phrase(" Refunds, if applicable, will be processed according to the hostel's refund policy as detailed in the handbook.", new Font(customnoticedgFont) { Color = BaseColor.GRAY }));
                doc.Add(Termandcon2);

                Paragraph Termandcon3 = new Paragraph();
                Termandcon3.Add(new Chunk(" \u2022", new Font(customReHeadingFont) { Color = BaseColor.GRAY })); // Unicode character for bullet point
                Termandcon3.Add(new Phrase(" The hostel fees are expected to be paid by the specified due dates. Late payments will incur penalties as outlined in the hostel's guidelines.", new Font(customnoticedgFont) { Color = BaseColor.GRAY }));
                doc.Add(Termandcon3);

                Font RegardsFont = new Font(customHeadingFont, 10, Font.ITALIC, new BaseColor(80, 75, 142, 255));
                Paragraph Regards = new Paragraph();
                Regards.Add(new Phrase("For, SKYLINE BOYS HOSTEL     ", new Font(RegardsFont)));
                Regards.Alignment = Element.ALIGN_RIGHT;

                Font printFont = new Font(customHeadingFont, 9, Font.NORMAL,new BaseColor(80, 75, 142, 255));
                Paragraph printTimeParagraph = new Paragraph();
                printTimeParagraph.Add(new Phrase("  Printed at: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), new Font(printFont)));
                printTimeParagraph.Alignment = Element.ALIGN_LEFT; // Align to the right
                doc.Add(printTimeParagraph);

                doc.Add(Regards);

                #endregion
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
        #endregion

    }
}
