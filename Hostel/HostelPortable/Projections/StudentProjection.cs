using MugenMvvmToolkit.Models;

namespace HostelPortable.Projections
{
    public class StudentProjection : NotifyPropertyChangedBase
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public int? RoomNumber { get; set; }
    }
}
