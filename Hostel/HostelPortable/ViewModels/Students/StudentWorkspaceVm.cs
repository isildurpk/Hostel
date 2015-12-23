using System.Threading.Tasks;
using System.Windows.Input;
using HostelPortable.Interfaces;
using HostelPortable.Projections;
using MugenMvvmToolkit;
using MugenMvvmToolkit.Models;
using MugenMvvmToolkit.ViewModels;

namespace HostelPortable.ViewModels.Students
{
    public sealed class StudentWorkspaceVm : WorkspaceViewModel
    {
        #region Fields

        private readonly IRepository _repository;

        #endregion

        #region Constructors

        public StudentWorkspaceVm(IRepository repository)
        {
            _repository = repository;

            DisplayName = UiResources.StudentWorkspaceName;

            AddCommand = RelayCommandBase.FromAsyncHandler(AddAsync, CanAdd, this);
            EditCommand = RelayCommandBase.FromAsyncHandler(EditAsync, CanEdit, this);
            RefreshCommand = RelayCommandBase.FromAsyncHandler(RefreshAsync);
        }

        #endregion

        #region Commands

        public ICommand AddCommand { get; private set; }

        public ICommand EditCommand { get; private set; }

        public ICommand RefreshCommand { get; private set; }

        #endregion

        #region Properties

        public GridViewModel<StudentProjection> StudentsVm { get; private set; }

        #endregion

        #region Command`s methods

        private async Task AddAsync()
        {
            using (var vm = GetViewModel<StudentCardVm>())
            {
                vm.Initialize();
                await vm.ShowAsync();
                await RefreshAsync();
            }
        }

        private bool CanAdd()
        {
            return StudentsVm != null;
        }

        private async Task EditAsync()
        {
            using (var vm = GetViewModel<StudentCardVm>())
            {
                vm.Initialize(StudentsVm.SelectedItem.Id);
                await vm.ShowAsync();
                await RefreshAsync();
            }
        }

        private bool CanEdit()
        {
            return StudentsVm?.SelectedItem != null;
        }

        private async Task RefreshAsync()
        {
            StudentsVm.UpdateItemsSource(await _repository.LoadStudentProjectionsAsync().WithBusyIndicator(this));
        }

        #endregion

        #region Overrides of ViewModelBase

        protected override void OnInitialized()
        {
            base.OnInitialized();

            StudentsVm = GetViewModel<GridViewModel<StudentProjection>>();

            _repository.LoadStudentProjectionsAsync().TryExecuteSynchronously(task => StudentsVm.UpdateItemsSource(task.Result))
                .WithBusyIndicator(this);
        }

        #endregion
    }
}