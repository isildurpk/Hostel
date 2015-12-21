using MugenMvvmToolkit;
using MugenMvvmToolkit.Infrastructure.Presenters;
using MugenMvvmToolkit.Interfaces.Presenters;
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
        }

        #endregion
    }
}