using HostelPortable.Interfaces;
using HostelPortable.ViewModels;
using MugenMvvmToolkit.Interfaces;
using MugenMvvmToolkit.Modules;

namespace HostelPortable
{
    public class ViewModelWrapperRegistrationModule : WrapperRegistrationModuleBase
    {
        #region Overrides of WrapperRegistrationModuleBase

        protected override void RegisterWrappers(IConfigurableWrapperManager wrapperManager)
        {
            wrapperManager.AddWrapper<IEditorWrapperVm>(typeof (EditorWrapperVm<>));
        }

        #endregion
    }
}