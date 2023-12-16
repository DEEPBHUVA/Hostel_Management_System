using System.ComponentModel.DataAnnotations;

namespace Hostel_Management_System.Areas.MST_Room.Models
{
    public class MST_RoomModel
    {
        public int? RoomId { get; set; }
        [Required(ErrorMessage ="Please Enter Room No!")]
        public int RoomNo { get; set; }
        [Required(ErrorMessage = "Please Enter Status!")]
        public bool Status {  get; set; }
        public int Capacity {  get; set; }
        public int SeatCount { get; set; }
		public DateTime Created { get; set; } = DateTime.Now;   
        public DateTime Modified { get; set; } = DateTime.Now;
    }

    public class MST_RoomDropdown
    {
        public int RoomId { get; set; }
        public int RoomNo { get; set; }
    }
}
