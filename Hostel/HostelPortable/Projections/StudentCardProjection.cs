using MugenMvvmToolkit.Models;

namespace HostelPortable.Projections
{
    public class StudentCardProjection : NotifyPropertyChangedBase
    {
        public int? Id { get; set; }

        public string Comment { get; set; }

        public bool? MedicalExamination { get; set; }

        public string NumberOfAuto { get; set; }

        public string Phone { get; set; }

        public PassportProjection Passport { get; set; }
    }
}