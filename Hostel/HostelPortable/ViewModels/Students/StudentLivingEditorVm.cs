using System;
using System.Collections.Generic;
using System.Linq;
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

        public int? ContractNumber
        {
            get { return Entity.ContractNumber.ToNulllable(); }
            set
            {
                if (value == Entity.ContractNumber) return;
                Entity.ContractNumber = value.GetValueOrDefault();
                Validate();
                OnPropertyChanged();
            }
        }

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
                Entity.RoomId = (value?.Id).GetValueOrDefault();
                Validate();
                OnPropertyChanged();
            }
        }

        #endregion

        #region Overrides of EditableViewModel

        protected override void OnEntityInitialized()
        {
            base.OnEntityInitialized();

            _repository.LoadRoomsWithFreeSeatsAsync(0).TryExecuteSynchronously(task =>
            {
                RoomList = task.Result;
                SelectedRoom = RoomList.SingleOrDefault(r => r.Id == Entity.RoomId);
            }).WithBusyIndicator(this);

            Validate();
        }

        #endregion

        #region Methods

        private void Validate()
        {
            Validator.ClearErrors();

            if (ContractNumber == null)
            {
                Validator.SetErrors(nameof(ContractNumber), UiResources.ErrorRequired);
            }
            else if (ContractNumber < 0)
            {
                Validator.SetErrors(nameof(ContractNumber), UiResources.ErrorNegative);
            }

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