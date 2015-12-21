using System.Threading.Tasks;
using System.Windows.Input;
using HostelPortable.ViewModels.Students;
using MugenMvvmToolkit;
using MugenMvvmToolkit.Infrastructure.Presenters;
using MugenMvvmToolkit.Interfaces.Presenters;
using MugenMvvmToolkit.Models;
using MugenMvvmToolkit.ViewModels;

namespace HostelPortable.ViewModels
{
    public class MainVm : MultiViewModel
    {
        #region Constructors

        public MainVm(IViewModelPresenter viewModelPresenter)
        {
            Should.NotBeNull(viewModelPresenter, nameof(viewModelPresenter));
            viewModelPresenter.DynamicPresenters.Add(new DynamicMultiViewModelPresenter(this));

            OpenStudentsCommand = RelayCommandBase.FromAsyncHandler(OpenStudentsAsync);
        }

        #endregion

        #region Commands

        public ICommand OpenStudentsCommand { get; private set; }

        #endregion

        #region Command`s methods

        private async Task OpenStudentsAsync()
        {
            using (var vm = GetViewModel<StudentWorkspaceVm>())
            {
                await vm.ShowAsync();
            }
        }

        #endregion
    }
}