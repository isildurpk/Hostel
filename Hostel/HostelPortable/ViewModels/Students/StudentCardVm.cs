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

        public bool? MedicalExamination
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

        public bool IsMale
        {
            get { return Entity.Passport?.SexId == (int) Sex.Male; }
            set
            {
                Entity.Passport.SexId = value ? (int) Sex.Male : (int) Sex.Female;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Command`s methods

        private async Task ApplyAsync()
        {
            if (!IsNewRecord)
            {
                await _repository.UpdateStudentCardAsync(_studentId, Entity).WithBusyIndicator(this);
            }
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
                InitializeEntity(new StudentCardProjection { Passport = new PassportProjection(Guid.NewGuid()) }, true);
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