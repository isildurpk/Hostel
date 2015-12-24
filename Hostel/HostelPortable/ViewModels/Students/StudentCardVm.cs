using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using HostelPortable.Interfaces;
using HostelPortable.Projections;
using MugenMvvmToolkit;
using MugenMvvmToolkit.Interfaces.Models;
using MugenMvvmToolkit.Interfaces.Presenters;
using MugenMvvmToolkit.Models;
using MugenMvvmToolkit.ViewModels;

namespace HostelPortable.ViewModels.Students
{
    public class StudentCardVm : EditableViewModel<StudentCardProjection>, IHasDisplayName
    {
        #region Fields

        private readonly IMessagePresenter _messagePresenter;
        private readonly IRepository _repository;

        private int _studentId;

        #endregion

        #region Constructors

        public StudentCardVm(IMessagePresenter messagePresenter, IRepository repository)
        {
            _messagePresenter = messagePresenter;
            _repository = repository;

            ApplyCommand = RelayCommandBase.FromAsyncHandler(ApplyAsync, CanApply, this);
            CancelCommand = new RelayCommand(Cancel, CanCancel, this);
            AddLivingCommand = RelayCommandBase.FromAsyncHandler(AddLivingAsync, CanAddLiving, this);
            EditLivingCommand = RelayCommandBase.FromAsyncHandler(EditLivingAsync, CanEditLiving, this);
            DeleteLivingCommand = RelayCommandBase.FromAsyncHandler(DeleteLivingAsync, CanDeleteLiving, this);
        }

        #endregion

        #region Commands

        public ICommand ApplyCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public ICommand AddLivingCommand { get; private set; }

        public ICommand EditLivingCommand { get; private set; }

        public ICommand DeleteLivingCommand { get; private set; }

        #endregion

        #region Entity properties

        public string Comment
        {
            get { return Entity.Comment; }
            set
            {
                if (value.Equals(Entity.Comment)) return;
                Entity.Comment = value;
                Validate();
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
                Validate();
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
                Validate();
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
            get { return Entity.BirthDate.ToNulllable(); }
            set
            {
                if (value == Entity.BirthDate) return;
                Entity.BirthDate = value.GetValueOrDefault();
                Validate();
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
                Validate();
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
                Validate();
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
                Validate();
                OnPropertyChanged();
            }
        }

        public DateTime? IssueDate
        {
            get { return Entity.IssueDate; }
            set
            {
                if (value == Entity.IssueDate) return;
                Entity.IssueDate = value;
                Validate();
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
                Validate();
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
                Validate();
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
                Validate();
                OnPropertyChanged();
            }
        }

        #endregion

        #region Properties

        public GridViewModel<LivingProjection> LivingListVm { get; private set; }

        #endregion

        #region Command`s methods

        private async Task ApplyAsync()
        {
            if (IsNewRecord)
            {
                Entity.PassportId = Guid.NewGuid();
                await _repository.AddStudentAsync(Entity).WithBusyIndicator(this);
                _studentId = Entity.Id;
                IsNewRecord = false;
                OnPropertyChanged(nameof(IsNewRecord));
                LoadLivings();
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
            Validate();
        }

        private bool CanCancel()
        {
            return HasChanges;
        }

        private async Task AddLivingAsync()
        {
            using (var vm = GetViewModel<StudentLivingEditorVm>())
            using (var wrapper = vm.Wrap<IEditorWrapperVm>())
            {
                var projection = new LivingProjection();
                vm.Initialize(LivingListVm.ItemsSource);
                vm.InitializeEntity(projection, true);

                if (!await wrapper.ShowAsync())
                {
                    return;
                }

                await _repository.AddLivingAsync(projection, _studentId).WithBusyIndicator(this);
                LoadLivings();
            }
        }

        private bool CanAddLiving()
        {
            return LivingListVm != null;
        }

        private async Task EditLivingAsync()
        {
            using (var vm = GetViewModel<StudentLivingEditorVm>())
            using (var wrapper = vm.Wrap<IEditorWrapperVm>())
            {
                var projection = LivingListVm.SelectedItem;

                vm.Initialize(LivingListVm.ItemsSource.Where(x => x != LivingListVm.SelectedItem));
                vm.InitializeEntity(projection, false);

                if (!await wrapper.ShowAsync())
                {
                    return;
                }

                await _repository.UpdateLivingAsync(projection).WithBusyIndicator(this);
                LoadLivings();
            }
        }

        private bool CanEditLiving()
        {
            return LivingListVm?.SelectedItem != null;
        }

        private async Task DeleteLivingAsync()
        {
            if (!await _messagePresenter.ShowDeleteQuestion())
            {
                return;
            }

            await _repository.DeleteLivingAsync(LivingListVm.SelectedItem.Id).WithBusyIndicator(this);
            LivingListVm.ItemsSource.Remove(LivingListVm.SelectedItem);
        }

        private bool CanDeleteLiving()
        {
            return LivingListVm?.SelectedItem != null;
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

            await _repository.LoadLivingProjectionsAsync(_studentId)
                .TryExecuteSynchronously(task => LivingListVm.UpdateItemsSource(task.Result))
                .WithBusyIndicator(this);
        }

        private async void LoadLivings()
        {
            LivingListVm.UpdateItemsSource(await _repository.LoadLivingProjectionsAsync(_studentId).WithBusyIndicator(this));
        }

        private void Validate()
        {
            Validator.ClearErrors();

            if (string.IsNullOrEmpty(LastName))
            {
                Validator.SetErrors(nameof(LastName), UiResources.ErrorRequired);
            }
            else if (LastName.Length > 50)
            {
                Validator.SetErrors(nameof(LastName), string.Format(UiResources.ErrorMaxLengthFormat, 50));
            }

            if (string.IsNullOrEmpty(FirstName))
            {
                Validator.SetErrors(nameof(FirstName), UiResources.ErrorRequired);
            }
            else if (FirstName.Length > 50)
            {
                Validator.SetErrors(nameof(FirstName), string.Format(UiResources.ErrorMaxLengthFormat, 50));
            }

            if (string.IsNullOrEmpty(MiddleName))
            {
                Validator.SetErrors(nameof(MiddleName), UiResources.ErrorRequired);
            }
            else if (MiddleName.Length > 50)
            {
                Validator.SetErrors(nameof(MiddleName), string.Format(UiResources.ErrorMaxLengthFormat, 50));
            }

            if (BirthDate == null)
            {
                Validator.SetErrors(nameof(BirthDate), UiResources.ErrorRequired);
            }
            else if (BirthDate > DateTime.Now)
            {
                Validator.SetErrors(nameof(BirthDate), UiResources.ErrorDateMoreThanNow);
            }

            if (IssueDate > DateTime.Now)
            {
                Validator.SetErrors(nameof(IssueDate), UiResources.ErrorDateMoreThanNow);
            }

            if (IssuedBy?.Length > 500)
            {
                Validator.SetErrors(nameof(MiddleName), string.Format(UiResources.ErrorMaxLengthFormat, 50));
            }

            if (Phone != null)
            {
                if (!Regex.IsMatch(Phone, @"^\+?[\d,\(,\),-]+$"))
                {
                    Validator.SetErrors(nameof(Phone), UiResources.ErrorUnavailableSymbols);
                }
                else if (Phone.Length > 20)
                {
                    Validator.SetErrors(nameof(Phone), string.Format(UiResources.ErrorMaxLengthFormat, 20));
                }
            }

            if (NumberOfAuto?.Length > 20)
            {
                Validator.SetErrors(nameof(NumberOfAuto), string.Format(UiResources.ErrorMaxLengthFormat, 20));
            }

            if (Comment?.Length > 500)
            {
                Validator.SetErrors(nameof(NumberOfAuto), string.Format(UiResources.ErrorMaxLengthFormat, 500));
            }
        }

        #endregion

        #region Implementation of IHasDisplayName

        public string DisplayName { get; set; } = UiResources.StudentCardName;

        #endregion

        #region Overrides of EditableViewModel

        protected override void OnEntityInitialized()
        {
            base.OnEntityInitialized();

            Validate();
        }

        #endregion

        #region Overrides of ViewModelBase

        protected override void OnInitialized()
        {
            base.OnInitialized();

            LivingListVm = GetViewModel<GridViewModel<LivingProjection>>();
        }

        #endregion
    }
}