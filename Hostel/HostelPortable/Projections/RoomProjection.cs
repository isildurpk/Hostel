using MugenMvvmToolkit.Models;

namespace HostelPortable.Projections
{
    public class RoomProjection : NotifyPropertyChangedBase
    {
        public int Id { get; set; }

        public int RoomNumber { get; set; }

        public int FreeSeatsNumber { get; set; }

        public string Display => $"№{RoomNumber}. Свободных мест: {FreeSeatsNumber} ";
    }
}