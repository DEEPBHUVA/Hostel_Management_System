namespace Hostel_Management_System.Areas.MST_Course.Models
{
    public class MST_CourseModel
    {
        public int? CourseID {  get; set; }
        public string CourseName { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    public class MST_Course_DropdownModel
    {
        public int? CourseID { get; set; }
        public string CourseName { get; set; }
    }
}
