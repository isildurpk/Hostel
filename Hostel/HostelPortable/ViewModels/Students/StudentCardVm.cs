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

        #region Properties

        public bool IsNewRecord { get; private set; }

        #endregion

        #region Methods

        public void Initialize(int? studentId = null)
        {
            if (studentId == null)
            {
                IsNewRecord = true;
                return;
            }

            _studentId = studentId.Value;
        }

        #endregion
    }
}