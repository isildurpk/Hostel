using System.Threading.Tasks;
using HostelPortable.Projections;
using MugenMvvmToolkit.ViewModels;

namespace HostelPortable.ViewModels.Students
{
    public sealed class StudentWorkspaceVm : WorkspaceViewModel
    {
        #region Fields

        private Task _initializedTask;

        #endregion


        #region Constructors

        public StudentWorkspaceVm()
        {
            DisplayName = UiResources.StudentWorkspaceName;
        }

        #endregion

        #region Properties

        public GridViewModel<StudentProjection> StudentsVm { get; private set; }

        #endregion

        #region Overrides of ViewModelBase

        protected override void OnInitialized()
        {
            base.OnInitialized();

            StudentsVm = GetViewModel<GridViewModel<StudentProjection>>();

            
        }

        #endregion
    }
}