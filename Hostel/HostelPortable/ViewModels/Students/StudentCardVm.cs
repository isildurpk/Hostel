using System;
using System.Threading.Tasks;
using System.Windows.Input;
using HostelPortable.Interfaces;
using HostelPortable.Projections;
using MugenMvvmToolkit.Interfaces.Models;
using MugenMvvmToolkit.Models;
using MugenMvvmToolkit.ViewModels;

namespace HostelPortable.ViewModels.Students
{
    public class StudentCardVm : EditableViewModel<StudentCardProjection>, IHasDisplayName
    {
        #region Fields

        private readonly IRepository _repository;

        private int _studentId;

        #endregion

        #region Constructors

        public StudentCardVm(IRepository repository)
        {
            _repository = repository;

            ApplyCommand = RelayCommandBase.FromAsyncHandler(ApplyAsync, CanApply, this);
            CancelCommand = new RelayCommand(Cancel, CanCancel, this);
        }

        #endregion

        #region Commands

        public ICommand ApplyCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        #endregion

        #region Properties

        public string Comment
        {
            get { return Entity.Comment; }
            set
            {
                if (value.Equals(Entity.Comment)) return;
                Entity.Comment = value;
                OnPropertyChanged();
            }
        }

        public bool MedicalExamination
        {
            get { return Entity.MedicalExamination; }
            set
            {
                if (value == Entity.MedicalExamination) return;
                Entity.MedicalExamination = value;
                OnPropertyChanged();
            }
        }

        public string NumberOfAuto
        {
            get { return Entity.NumberOfAuto; }
            set
            {
                if (value.Equals(Entity.NumberOfAuto)) return;
                Entity.NumberOfAuto = value;
                OnPropertyChanged();
            }
        }

        public string Phone
        {
            get { return Entity.Phone; }
            set
            {
                if (value.Equals(Entity.Phone)) return;
                Entity.Phone = value;
                OnPropertyChanged();
            }
        }

        public bool IsFemale
        {
            get { return Entity.SexId == (byte) Sex.Female; }
            set
            {
                Entity.SexId = value ? (byte) Sex.Female : (byte) Sex.Male;
                OnPropertyChanged();
            }
        }

        public DateTime? BirthDate
        {
            get { return Entity.BirthDate; }
            set
            {
                if (value.Equals(Entity.BirthDate)) return;
                Entity.BirthDate = value;
                OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get { return Entity.FirstName; }
            set
            {
                if (value.Equals(Entity.FirstName)) return;
                Entity.FirstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return Entity.LastName; }
            set
            {
                if (value.Equals(Entity.LastName)) return;
                Entity.LastName = value;
                OnPropertyChanged();
            }
        }

        public string MiddleName
        {
            get { return Entity.MiddleName; }
            set
            {
                if (value.Equals(Entity.MiddleName)) return;
                Entity.MiddleName = value;
                OnPropertyChanged();
            }
        }

        public DateTime? IssueDate
        {
            get { return Entity.IssueDate; }
            set
            {
                if (value.Equals(Entity.IssueDate)) return;
                Entity.IssueDate = value;
                OnPropertyChanged();
            }
        }

        public string IssuedBy
        {
            get { return Entity.IssuedBy; }
            set
            {
                if (value.Equals(Entity.IssuedBy)) return;
                Entity.IssuedBy = value;
                OnPropertyChanged();
            }
        }

        public int? Number
        {
            get { return Entity.Number; }
            set
            {
                if (value == Entity.Number) return;
                Entity.Number = value;
                OnPropertyChanged();
            }
        }

        public int? Series
        {
            get { return Entity.Series; }
            set
            {
                if (value == Entity.Series) return;
                Entity.Series = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Command`s methods

        private async Task ApplyAsync()
        {
            if (IsNewRecord)
            {
                Entity.PassportId = Guid.NewGuid();
                await _repository.AddStudentAsync(Entity).WithBusyIndicator(this);
                IsNewRecord = false;
                OnPropertyChanged(nameof(IsNewRecord));
            }
            else
            {
                await _repository.UpdateStudentCardAsync(Entity).WithBusyIndicator(this);
            }

            SaveEntityState(Entity);
            OnPropertyChanged(nameof(HasChanges));
        }

        private bool CanApply()
        {
            return HasChanges && IsValid;
        }

        private void Cancel()
        {
            CancelChanges();
        }

        private bool CanCancel()
        {
            return HasChanges;
        }

        #endregion

        #region Methods

        public async void Initialize(int? studentId = null)
        {
            if (studentId == null)
            {
                InitializeEntity(new StudentCardProjection(), true);
                return;
            }

            _studentId = studentId.Value;
            InitializeEntity(await _repository.GetStudentCardProjectionAsync(_studentId).WithBusyIndicator(this), false);
        }

        #endregion

        #region Implements of IHasDisplayName

        public string DisplayName { get; set; } = UiResources.StudentCardName;

        #endregion
    }
}