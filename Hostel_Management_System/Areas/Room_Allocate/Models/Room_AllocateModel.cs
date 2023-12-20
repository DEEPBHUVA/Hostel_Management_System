using System.ComponentModel.DataAnnotations;

namespace Hostel_Management_System.Areas.Room_Allocate.Models
{
    public class Room_AllocateModel
    {
        public int? RoomAllocateID { get; set; }

        [Required(ErrorMessage ="Select room from dropdown!")]
        public int RoomId { get; set; }

		[Required(ErrorMessage = "Select student from dropdown!")]
		public int StudentID { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Modified { get; set; } = DateTime.Now;

    }
}
