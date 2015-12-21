using MugenMvvmToolkit.ViewModels;

namespace HostelPortable.ViewModels.Students
{
    public sealed class StudentCardVm : WorkspaceViewModel
    {
        #region Fields

        private int _studentId;

        #endregion

        #region Constructors

        public StudentCardVm()
        {
            DisplayName = UiResources.StudentCardName;
        }

        #endregion

        #region Methods

        public void Initialize(int studentId)
        {
            _studentId = studentId;
        }

        #endregion
    }
}