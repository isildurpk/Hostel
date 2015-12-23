using System;
using MugenMvvmToolkit.Models;

namespace HostelPortable.Projections
{
    public class LivingProjection : NotifyPropertyChangedBase
    {
        public int Id { get; set; }

        public int ContractNumber { get; set; }

        public int RoomId { get; set; }

        public int RoomNumber { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}