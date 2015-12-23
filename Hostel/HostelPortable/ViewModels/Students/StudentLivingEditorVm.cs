using System.Collections.Generic;
using HostelPortable.Interfaces;
using HostelPortable.Projections;
using MugenMvvmToolkit;
using MugenMvvmToolkit.ViewModels;

namespace HostelPortable.ViewModels.Students
{
    public class StudentLivingEditorVm : EditableViewModel<LivingProjection>
    {
        #region Fields

        private readonly IRepository _repository;

        #endregion

        #region Constructors

        public StudentLivingEditorVm(IRepository repository)
        {
            _repository = repository;
        }

        #endregion

        #region Properties

        public IList<RoomProjection> RoomList { get; private set; }

        public RoomProjection SelectedRoom { get; set; }

        #endregion

        #region Overrides of ViewModelBase

        protected override void OnInitialized()
        {
            base.OnInitialized();

            _repository.LoadRoomsWithFreeSeatsAsync(0).TryExecuteSynchronously(task => RoomList = task.Result).WithBusyIndicator(this);
        }

        #endregion
    }
}