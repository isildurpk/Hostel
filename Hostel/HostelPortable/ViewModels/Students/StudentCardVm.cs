﻿using HostelPortable.Interfaces;
using HostelPortable.Projections;
using MugenMvvmToolkit.ViewModels;

namespace HostelPortable.ViewModels.Students
{
    public sealed class StudentCardVm : WorkspaceViewModel
    {
        #region Fields

        private readonly IRepository _repository;

        private int _studentId;

        #endregion

        #region Constructors

        public StudentCardVm(IRepository repository)
        {
            _repository = repository;
            DisplayName = UiResources.StudentCardName;
        }

        #endregion

        #region Properties

        public bool IsMale
        {
            get { return Student?.Passport?.SexId == (int) Sex.Male; }
            set { Student.Passport.SexId = value ? (int) Sex.Male : (int) Sex.Female; }
        }

        public bool IsNewRecord { get; private set; }

        public StudentCardProjection Student { get; private set; }

        #endregion

        #region Methods

        public async void Initialize(int? studentId = null)
        {
            if (studentId == null)
            {
                IsNewRecord = true;
                return;
            }

            _studentId = studentId.Value;
            Student = await _repository.GetStudentCardProjectionAsync(_studentId);
            OnPropertyChanged(nameof(Student));
            OnPropertyChanged(nameof(IsMale));
        }

        #endregion
    }
}