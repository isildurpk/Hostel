using System.Windows.Input;
using MugenMvvmToolkit.Interfaces.Models;
using MugenMvvmToolkit.Interfaces.ViewModels;

namespace HostelPortable.Interfaces
{
    public interface IEditorWrapperVm : IWrapperViewModel, ICloseableViewModel, IHasDisplayName
    {
        ICommand ApplyCommand { get; set; }
    }
}
