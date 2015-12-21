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
        private readonly IRepository _repository;

        #region Fields

        private Task _initializedTask;

        #endregion

        #region Constructors

        public StudentWorkspaceVm(IRepository repository)
        {
            _repository = repository;

            DisplayName = UiResources.StudentWorkspaceName;

            RefreshCommand = RelayCommandBase.FromAsyncHandler(RefreshAsync);
        }

        #endregion

        #region Commands

        public ICommand RefreshCommand { get; private set; }

        #endregion

        #region Properties

        public GridViewModel<StudentProjection> StudentsVm { get; private set; }

        #endregion

        #region Command`s methods

        private async Task RefreshAsync()
        {
            await _initializedTask;
        }

        #endregion

        #region Overrides of ViewModelBase

        protected override void OnInitialized()
        {
            base.OnInitialized();

            StudentsVm = GetViewModel<GridViewModel<StudentProjection>>();

            _initializedTask = _repository.LoadStudentProjectionsAsync().TryExecuteSynchronously(task => StudentsVm.UpdateItemsSource(task.Result))
                .WithBusyIndicator(this);
        }

        #endregion
    }
}