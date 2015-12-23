using System.Threading.Tasks;
using System.Windows.Input;
using HostelPortable.Interfaces;
using MugenMvvmToolkit.Interfaces.ViewModels;
using MugenMvvmToolkit.Models;
using MugenMvvmToolkit.ViewModels;

namespace HostelPortable.ViewModels
{
    public sealed class EditorWrapperVm<T> : WrapperViewModelBase<T>, IEditorWrapperVm
        where T : class, IEditableViewModel
    {
        #region Fields

        private ICommand _applyCommand;

        #endregion

        #region Constructors

        public EditorWrapperVm()
        {
            ApplyCommand = RelayCommandBase.FromAsyncHandler<object>(Apply, CanApply, false, this);

            DisplayName = UiResources.EditorName;
        }

        #endregion

        #region Implementation of IEditableViewModel

        public ICommand ApplyCommand
        {
            get { return _applyCommand; }
            set
            {
                if (Equals(value, _applyCommand))
                {
                    return;
                }

                _applyCommand = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Command`s methods

        private Task Apply(object obj)
        {
            OperationResult = true;
            return CloseAsync(obj).WithBusyIndicator(this);
        }

        private bool CanApply(object obj)
        {
            return ViewModel != null && ViewModel.HasChanges && ViewModel.IsValid;
        }

        #endregion

        #region Overrides of WrapperViewModelBaase<T>

        protected override void OnClosed(object parameter)
        {
            if (!OperationResult.GetValueOrDefault() && ViewModel != null && ViewModel.IsEntityInitialized)
            {
                ViewModel.CancelChanges();
            }
        }

        #endregion
    }
}