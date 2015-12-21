using System;
using MugenMvvmToolkit.Models;

namespace HostelPortable.Projections
{
    public class PassportProjection : NotifyPropertyChangedBase
    {
        public Guid Id { get; set; }

        public DateTime? BirthDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public int? IssueDate { get; set; }

        public string IssuedBy { get; set; }

        public int? Number { get; set; }

        public int? Series { get; set; }

        public int SexId { get; set; }
    }
}
