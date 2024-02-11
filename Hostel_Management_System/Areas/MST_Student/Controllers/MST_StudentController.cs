using ClosedXML.Excel;
using Hostel_Management_System.Areas.MST_Course.Models;
using Hostel_Management_System.Areas.MST_Student.Models;
using Hostel_Management_System.Areas.MST_Student.ViewModel;
using Hostel_Management_System.BAL;
using Hostel_Management_System.DAL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Hostel_Management_System.Areas.MST_Student.Controllers
{
	[CheckAccess]
	[Area("MST_Student")]
    [Route("MST_Student/{Controller}/{action}")]
    public class MST_StudentController : Controller
    {
        MST_Student_DAL dalMST_Student = new MST_Student_DAL();

        #region Configuration
        public IConfiguration Configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public MST_StudentController(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region SelectOnlyActiveStudent
        public IActionResult Index(string? StudentName, string? Course, int RoomNo,bool filter = false)
        {
			string MyCONNstr = this.Configuration.GetConnectionString("ConStr"); 
            DataTable dtable = new DataTable();
			SqlConnection SQLConn = new SqlConnection(MyCONNstr); 
            SQLConn.Open();
			SqlCommand cmd = SQLConn.CreateCommand(); 
            cmd.CommandType = CommandType.StoredProcedure;
			if (Convert.ToBoolean(filter))
			{
				cmd.CommandText = "PR_MST_Student_Filter";
				cmd.Parameters.AddWithValue("@StudentName", StudentName);
				cmd.Parameters.AddWithValue("@Course", Course);
				cmd.Parameters.AddWithValue("@RoomNo", RoomNo);
			}
			else
			{
				cmd.CommandText = "PR_MST_Student_SelectAll";
			}
			SqlDataReader objStr = cmd.ExecuteReader();
			dtable.Load(objStr);
			return View("MST_StudentList", dtable);
		}
        #endregion

        #region SelectAllStudent
        public IActionResult GetAllStudent()
        {
            DataTable dt = dalMST_Student.PR_MST_Student_GetAllStudent();
            return View("MST_AllStudentList", dt);
        }
        #endregion

        #region PR_MST_ALLStudent_Filter
        public IActionResult PR_MST_ALLStudent_Filter(string StudentName,string Email,int Age)
        {
            DataTable dt = dalMST_Student.PR_MST_ALLStudent_Filter(StudentName,Email, (int)Age);
            return View("MST_AllStudentList", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int StudentID)
        {
            try
            {
                if (Convert.ToBoolean(dalMST_Student.PR_MST_Student_DeleteByPk(StudentID)))
                {
                    TempData["MST_Student_Delete_AlertMessage"] = "Record Deleted Successfully";
                }
            }
            catch (Exception ex)
            {
                TempData["MST_Student_Delete_AlertMessage"] = ex.Message;
            }

            return RedirectToAction("GetAllStudent");
        }

        #endregion

        #region UpdateStatus
        public IActionResult UpdateStatus(int StudentID)
        {
            if (Convert.ToBoolean(dalMST_Student.PR_Mst_StudentUpdateStatus(StudentID)))
            {
                TempData["MST_Student_Remove_AlertMessage"] = "Record removed form this list successfully!!";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Add
        public IActionResult Add(int? StudentID) 
        {
            
            #region Course Dropdown
            string MyConnectionStr1 = this.Configuration.GetConnectionString("ConStr");
			SqlConnection connection2 = new SqlConnection(MyConnectionStr1);
            DataTable dt2 = new DataTable();
            connection2.Open();
            SqlCommand cmd2 = connection2.CreateCommand();
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.CommandText = "MST_Course_Dropdown";
            SqlDataReader reader2 = cmd2.ExecuteReader();
            dt2.Load(reader2);

            List<MST_Course_DropdownModel> list1 = new List<MST_Course_DropdownModel>();
            foreach (DataRow dr in dt2.Rows)
            {   
                MST_Course_DropdownModel dlist = new MST_Course_DropdownModel();
                dlist.CourseID = Convert.ToInt32(dr["CourseID"]);
                dlist.CourseName = dr["CourseName"].ToString();
                list1.Add(dlist);
            }
            ViewBag.CourseList = list1;
            #endregion

            if (StudentID != null)
            {
                DataTable dt = dalMST_Student.PR_MST_Student_SelectByPk(StudentID);
                MST_StudentModel modelMST_Student= new MST_StudentModel();

                foreach (DataRow row in dt.Rows)
                {
                    //modelMST_Student.StudentID = Convert.ToInt32(row["StudentID"]);
                    modelMST_Student.StudentName = row["StudentName"].ToString();
                    modelMST_Student.MobileNo = row["MobileNo"].ToString();
                    modelMST_Student.Email = row["Email"].ToString();
                    modelMST_Student.Age = Convert.ToInt32(row["Age"]);
                    modelMST_Student.BirthDate = Convert.ToDateTime(row["BirthDate"]);
                    modelMST_Student.BloodGroup = row["BloodGroup"].ToString();
                    modelMST_Student.FatherMobileNo = row["FatherMobileNo"].ToString();
                    modelMST_Student.FatherName = row["FatherName"].ToString();
                    modelMST_Student.MotherMobileNo = row["MotherMobileNo"].ToString();
                    modelMST_Student.MotherName = row["MotherName"].ToString();
                    modelMST_Student.LocalGurdianName = row["LocalGurdianName"].ToString();
                    modelMST_Student.LocalGurdianNo = row["LocalGurdianNo"].ToString();
                    modelMST_Student.Nationlity = row["Nationlity"].ToString();
                    modelMST_Student.AadharCardNo = row["AadharCardNo"].ToString();
                    modelMST_Student.PermentAddress = row["PermentAddress"].ToString();
                    modelMST_Student.PresentAddress = row["PresentAddress"].ToString();
                    modelMST_Student.PhotoPath = row["PhotoPath"].ToString();
                    modelMST_Student.isActive = row["isActive"].ToString();
                    modelMST_Student.CourseID = Convert.ToInt32(row["CourseID"]);
					modelMST_Student.Remarks = row["Remarks"].ToString();
				}
                
                return View("MST_StudentAddEdit", modelMST_Student);
            }
            return View("MST_StudentAddEdit");
        }
        #endregion

        #region Save(Insert/Update)
        public IActionResult Save(MST_StudentModel modelMST_Student)
        {
            #region PhotoPath
            if (modelMST_Student.File != null)
            {
                string FilePath = "wwwroot\\Images";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string fileNameWithPath = Path.Combine(path, modelMST_Student.File.FileName);
                modelMST_Student.PhotoPath = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + modelMST_Student.File.FileName;

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    modelMST_Student.File.CopyTo(stream);
                }
            }
            #endregion

            if (modelMST_Student.StudentID == null)
            {
                DataTable dt = dalMST_Student.PR_MST_Student_Insert(
                    modelMST_Student.StudentName,
                    modelMST_Student.Email,
                    modelMST_Student.MobileNo,
                    modelMST_Student.BloodGroup,
                    modelMST_Student.BirthDate,
                    modelMST_Student.Age,
                    modelMST_Student.FatherName,
                    modelMST_Student.FatherMobileNo,
                    modelMST_Student.MotherName,
                    modelMST_Student.MotherMobileNo,
                    modelMST_Student.LocalGurdianName,
                    modelMST_Student.LocalGurdianNo,
                    modelMST_Student.Nationlity,
                    modelMST_Student.AadharCardNo,
                    modelMST_Student.PresentAddress,
                    modelMST_Student.PermentAddress,
                    modelMST_Student.isActive,
                    modelMST_Student.CourseID,
                    modelMST_Student.Remarks,
                    modelMST_Student.PhotoPath
                );
                TempData["MST_Student_AlertMessage"] = "Record Inserted Successfully!!";
            }
            else
            {
                DataTable dt = dalMST_Student.PR_MST_Student_Update(
                    (int)modelMST_Student.StudentID,
                    modelMST_Student.StudentName,
                    modelMST_Student.Email,
                    modelMST_Student.MobileNo,
                    modelMST_Student.BloodGroup,
                    modelMST_Student.BirthDate,
                    modelMST_Student.Age,
                    modelMST_Student.FatherName,
                    modelMST_Student.FatherMobileNo,
                    modelMST_Student.MotherName,
                    modelMST_Student.MotherMobileNo,
                    modelMST_Student.LocalGurdianName,
                    modelMST_Student.LocalGurdianNo,
                    modelMST_Student.Nationlity,
                    modelMST_Student.AadharCardNo,
                    modelMST_Student.PresentAddress,
                    modelMST_Student.PermentAddress,
                    modelMST_Student.isActive,
                    modelMST_Student.CourseID,
                    modelMST_Student.Remarks,
                    modelMST_Student.PhotoPath
                );
                TempData["MST_Student_AlertMessage"] = "Record Updated Successfully!!";
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

        #region ViewProfile
        public IActionResult ViewProfile(int StudentID)
        {
            DataTable paymentData = dalMST_Student.PR_MST_Payment_SelectStudentID(StudentID);
            DataTable studentData = dalMST_Student.PR_MST_Student_SeleckbyPkWithAllData(StudentID);

            // Create an instance of the view model and populate its properties
            var viewModel = new View_Model
            {
                PaymentData = paymentData,
                StudentData = studentData
            };

            return View("MST_Student_ViewProfile", viewModel);
        }
        #endregion

        #region PaymentHistory
        public IActionResult PaymentHistory()
        {
            int studentID = (int)@CV.StudentID();
            string connectionString = this.Configuration.GetConnectionString("ConStr");
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("PR_Calculate_StudentFeesAmount", connection))
                {
                    command.Parameters.AddWithValue("@StudentID", studentID);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();
                    dataTable.Load(reader);
                }
            }

            // Convert DataTable to Dictionary for simplicity
            Dictionary<string, int> dataDictionary = new Dictionary<string, int>
            {
                { "TotalFeesAmount", Convert.ToInt32(dataTable.Rows[0]["TotalFeesAmount"]) },
            };

            // Pass data to view using ViewBag or ViewData
            ViewBag.TotalAMount = dataDictionary;

            DataTable dt = dalMST_Student.PR_MST_Payment_SelectStudentID(studentID);
            return View("MST_Student_PaymentHistory", dt);
        }
        #endregion

        #region ConvertInToList
        public List<MST_StudentModel> GetStudentModels()
        {
            List<MST_StudentModel> studentModels = new List<MST_StudentModel>();

            string myconnStr = this.Configuration.GetConnectionString("ConStr");
            SqlConnection connection = new SqlConnection(myconnStr);
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_MST_Student_GetAllStudent";

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    MST_StudentModel studentModel = new MST_StudentModel
                    {
                        StudentName = reader["StudentName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Age = (int)reader["Age"],
                        BirthDate = Convert.ToDateTime(reader["BirthDate"]),
                        MobileNo = reader["MobileNo"].ToString(),
                        AadharCardNo = reader["AadharCardNo"].ToString(),
                        CourseName = reader["CourseName"].ToString(),
                        isActive = reader["isActive"].ToString()
                        // Add other properties as needed
                    };

                    studentModels.Add(studentModel);
                }

                return studentModels;
            }
        }
        #endregion

        #region ExportStudentsToExcel
        public IActionResult ExportStudentsToExcel()
        {
           
            List<MST_StudentModel> studentModels = GetStudentModels();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Students");

                // Add headers with bold font and background color
                var headerRow = worksheet.Row(1);
                headerRow.Style.Font.Bold = true;
              
                headerRow.Style.Fill.BackgroundColor = XLColor.LightBlue; // You can choose any color you prefer
                // Add headers
                worksheet.Cell(1, 1).Value = "Student Name";
                worksheet.Cell(1, 2).Value = "Email";
                worksheet.Cell(1, 3).Value = "Age";
                worksheet.Cell(1, 4).Value = "BirthDate";
                worksheet.Cell(1, 5).Value = "Mobile No";
                worksheet.Cell(1, 6).Value = "Aadhar Card No";
                worksheet.Cell(1, 7).Value = "Course Name";
                worksheet.Cell(1, 8).Value = "isActive";

                // Add data
                int row = 2;
                foreach (var studentModel in studentModels)
                {
                    worksheet.Cell(row, 1).Value = studentModel.StudentName;
                    worksheet.Cell(row, 2).Value = studentModel.Email;
                    var ageCell = worksheet.Cell(row, 3);
                    ageCell.Value = studentModel.Age;
                    ageCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(row, 4).Value = studentModel.BirthDate.ToString("yyyy-MM-dd");
                    worksheet.Cell(row, 5).Value = studentModel.MobileNo;
                    worksheet.Cell(row, 6).Value = studentModel.AadharCardNo;
                    worksheet.Cell(row, 7).Value = studentModel.CourseName;
                    worksheet.Cell(row, 8).Value = studentModel.isActive;
                    // Add other properties...
                    row++;
                }

                worksheet.Columns().AdjustToContents();
                // Set content type and filename
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = "StudentData.xlsx";

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, contentType, fileName);
                }
            }
        }
        #endregion

        #region CreatePdf
        public FileResult CreatePdf()
        {
            MemoryStream workStream = new MemoryStream();
            DateTime dTime = DateTime.Now;

            // File name to be created   
            string strPDFFileName = string.Format("Student_Data" + "_" + dTime.ToString("yyyy-MM-dd") + ".pdf");
            Document doc = new Document(PageSize.A4.Rotate()); // Set page size to landscape
            doc.SetMargins(10f, 10f, 10f, 10f); // Set margins

            // Create PDF Table with 8 columns  
            PdfPTable tableLayout = new PdfPTable(8);
            tableLayout.TotalWidth = PageSize.A4.Rotate().Width - 20;
            tableLayout.LockedWidth = true;

            // File will be created in this path  
            string strAttachment = Path.Combine(_hostingEnvironment.WebRootPath, "Downloadss", strPDFFileName);

            PdfWriter writer = PdfWriter.GetInstance(doc, workStream);
            writer.PageEvent = new PageEventHelper();
            writer.CloseStream = false;

            doc.Open();

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

            BaseFont customFont = BaseFont.CreateFont("D:\\CollageMaterial\\Sem-5\\.Net\\.Net_Project\\Hostel_Management_System\\Hostel_Management_System\\wwwroot\\font\\Poppins-Regular.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            // Create a Font object with your custom font
            Font customPdfFont = new Font(customFont, 10, Font.NORMAL, BaseColor.BLACK);
            Paragraph paragraph = new Paragraph("Skyline Boyz Hostel", customPdfFont);
            paragraph.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph);

            List<MST_StudentModel> studentModels = GetStudentModels();
            doc.Add(Add_Content_To_PDF(tableLayout, studentModels));

            
            doc.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return File(workStream, "application/pdf", strPDFFileName);
        }

        public class PageEventHelper : PdfPageEventHelper
        {
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                base.OnEndPage(writer, document);

                PdfPTable pageTable = new PdfPTable(1);
                pageTable.TotalWidth = document.PageSize.Width - 20; // Adjust total width based on margins
                pageTable.HorizontalAlignment = Element.ALIGN_RIGHT;
                pageTable.DefaultCell.Border = 0;

                // Add page number
                pageTable.AddCell(new Phrase($"Page {writer.PageNumber}", new Font(Font.FontFamily.HELVETICA, 10)));

                // Position the table
                pageTable.WriteSelectedRows(0, -1, 780, document.PageSize.Height - 570, writer.DirectContent);
            }
        }

        protected PdfPTable Add_Content_To_PDF(PdfPTable tableLayout, List<MST_StudentModel> studentModels)
        {
            float[] headers = { 22, 20, 8, 10, 10, 12, 10, 8 }; // Header Widths  
            tableLayout.SetWidths(headers); // Set the pdf headers  
            tableLayout.WidthPercentage = 100; // Set the PDF File width percentage  
            tableLayout.HeaderRows = 1;

            // Add empty row for spacing
            AddEmptyRow(tableLayout, 12);

            // Add header  
            AddCellToHeader(tableLayout, "Name", Element.ALIGN_LEFT);
            AddCellToHeader(tableLayout, "Email", Element.ALIGN_LEFT);
            AddCellToHeader(tableLayout, "Age", Element.ALIGN_CENTER);
            AddCellToHeader(tableLayout, "Birth Date", Element.ALIGN_CENTER);
            AddCellToHeader(tableLayout, "Contact", Element.ALIGN_CENTER);
            AddCellToHeader(tableLayout, "AadharCard No", Element.ALIGN_CENTER);
            AddCellToHeader(tableLayout, "Course", Element.ALIGN_LEFT);
            AddCellToHeader(tableLayout, "Status", Element.ALIGN_CENTER);

            // Add body  
            foreach (var stu in studentModels)
            {
                AddCellToBody(tableLayout, stu.StudentName, Element.ALIGN_LEFT);
                AddCellToBody(tableLayout, stu.Email, Element.ALIGN_LEFT);
                AddCellToBody(tableLayout, stu.Age.ToString(), Element.ALIGN_CENTER);
                AddCellToBody(tableLayout, stu.BirthDate.ToString(), Element.ALIGN_CENTER);
                AddCellToBody(tableLayout, stu.MobileNo, Element.ALIGN_CENTER);
                AddCellToBody(tableLayout, stu.AadharCardNo, Element.ALIGN_CENTER);
                AddCellToBody(tableLayout, stu.CourseName, Element.ALIGN_LEFT);
                AddCellToBody(tableLayout, stu.isActive.ToString(), Element.ALIGN_CENTER);
            }

            return tableLayout;
        }


        // Method to add single cell to the Header  
        private static void AddCellToHeader(PdfPTable tableLayout, string cellText, int alignment)
        {
            BaseFont customFont = BaseFont.CreateFont("D:\\CollageMaterial\\Sem-5\\.Net\\.Net_Project\\Hostel_Management_System\\Hostel_Management_System\\wwwroot\\font\\Poppins-ExtraBold.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            // Create a Font object with your custom font
            Font customPdfFont = new Font(customFont, 10, Font.NORMAL, BaseColor.WHITE);
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, customPdfFont))
            {
                HorizontalAlignment = alignment,
                Padding = 6,
                BackgroundColor = new iTextSharp.text.BaseColor(0, 102, 204), // SkyBlue color
                BorderColor = BaseColor.WHITE,
                BorderWidth = 1f
            }); ;
        }

        private void AddCellToBody(PdfPTable tableLayout, string cellText, int alignment)
        {
            BaseFont customFont = BaseFont.CreateFont("D:\\CollageMaterial\\Sem-5\\.Net\\.Net_Project\\Hostel_Management_System\\Hostel_Management_System\\wwwroot\\font\\Poppins-Regular.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            // Create a Font object with your custom font
            Font customPdfFont = new Font(customFont, 10, Font.NORMAL, BaseColor.BLACK);

            // Use the custom font in your cell
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, customPdfFont))
            {
                HorizontalAlignment = alignment,
                Padding = 8,
                Border = Rectangle.BOTTOM_BORDER,
                BorderColor = BaseColor.LIGHT_GRAY
            });
        }

        private static void AddEmptyRow(PdfPTable tableLayout, int colSpan)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.FontFamily.TIMES_ROMAN, 12)))
            {
                Colspan = colSpan,
                Border = 0
            });
        }

    #endregion
    }
}



