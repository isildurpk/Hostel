using System;
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
        private RoomProjection _selectedRoom;

        #endregion

        #region Constructors

        public StudentLivingEditorVm(IRepository repository)
        {
            _repository = repository;
        }

        #endregion

        #region Properties

        public DateTime? DateFrom
        {
            get { return Entity.DateFrom.ToNulllable(); }
            set
            {
                if (value == Entity.DateFrom) return;
                Entity.DateFrom = value.GetValueOrDefault();
                Validate();
                OnPropertyChanged();
            }
        }

        public DateTime? DateTo
        {
            get { return Entity.DateTo; }
            set
            {
                if (value == Entity.DateTo) return;
                Entity.DateTo = value;
                Validate();
                OnPropertyChanged();
            }
        }

        public IList<RoomProjection> RoomList { get; private set; }

        public RoomProjection SelectedRoom
        {
            get { return _selectedRoom; }
            set
            {
                _selectedRoom = value;
                Entity.RoomId = value.Id;
                Validate();
                OnPropertyChanged();
            }
        }

        #endregion

        #region Overrides of EditableViewModel

        protected override void OnInitialized()
        {
            base.OnInitialized();

            _repository.LoadRoomsWithFreeSeatsAsync(0).TryExecuteSynchronously(task => RoomList = task.Result).WithBusyIndicator(this);
        }

        protected override void OnEntityInitialized()
        {
            base.OnEntityInitialized();

            Validate();
        }

        #endregion

        #region Methods

        private void Validate()
        {
            Validator.ClearErrors();

            if (DateFrom == null)
            {
                Validator.SetErrors(nameof(DateFrom), UiResources.ErrorRequired);
            }
            else if (DateTo != null && DateFrom >= DateTo)
            {
                Validator.SetErrors(nameof(DateFrom), UiResources.ErrorDateRange);
                Validator.SetErrors(nameof(DateTo), UiResources.ErrorDateRange);
            }

            if (SelectedRoom == null)
            {
                Validator.SetErrors(nameof(SelectedRoom), UiResources.ErrorRequired);
            }
        }

        #endregion
    }
}