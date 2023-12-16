namespace Hostel_Management_System.Areas.Room_Allocate.Models
{
    public class Room_AllocateModel
    {
        public int? RoomAllocateID { get; set; }
        public int RoomId { get; set; }
        public int StudentID { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Modified { get; set; } = DateTime.Now;

    }
}
