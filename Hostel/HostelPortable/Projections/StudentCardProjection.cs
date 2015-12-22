using System;
using MugenMvvmToolkit.Models;

namespace HostelPortable.Projections
{
    public class StudentCardProjection : NotifyPropertyChangedBase
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        public bool MedicalExamination { get; set; }

        public string NumberOfAuto { get; set; }

        public string Phone { get; set; }

        public Guid PassportId { get; set; }

        public DateTime? BirthDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime? IssueDate { get; set; }

        public string IssuedBy { get; set; }

        public int? Number { get; set; }

        public int? Series { get; set; }

        public int SexId { get; set; }
    }
}