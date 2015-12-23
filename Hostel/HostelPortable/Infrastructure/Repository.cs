using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using HostelPortable.Interfaces;
using HostelPortable.Projections;
using MugenMvvmToolkit;

namespace HostelPortable.Infrastructure
{
    public class Repository : IRepository
    {
        #region Fields

        private readonly Func<SqlConnection> _getConnection;

        #endregion

        #region Constructors

        public Repository(Func<SqlConnection> getConnection)
        {
            _getConnection = getConnection;
        }

        #endregion

        #region Implemetation of IRepository

        public Task<IList<StudentProjection>> LoadStudentProjectionsAsync()
        {
            return Task.Factory.StartNew<IList<StudentProjection>>(() =>
            {
                using (var conn = _getConnection())
                {
                    conn.Open();
                    return conn.Query<StudentProjection>("LoadStudentProjections", commandType: CommandType.StoredProcedure).ToList();
                }
            });
        }

        public Task<StudentCardProjection> GetStudentCardProjectionAsync(int studentId)
        {
            return Task.Factory.StartNew(() =>
            {
                using (var conn = _getConnection())
                {
                    conn.Open();
                    return
                        conn.Query<StudentCardProjection>("GetStudentCardProjection", new { studentId }, commandType: CommandType.StoredProcedure)
                            .SingleOrDefault();
                }
            });
        }

        public Task UpdateStudentCardAsync(StudentCardProjection projection)
        {
            Should.NotBeNull(projection, nameof(projection));

            return Task.Factory.StartNew(() =>
            {
                using (var conn = _getConnection())
                {
                    conn.Open();
                    conn.Execute("UpdateStudentCard", new
                    {
                        projection.Id,
                        projection.MedicalExamination,
                        projection.Phone,
                        projection.NumberOfAuto,
                        projection.Comment,
                        projection.PassportId,
                        projection.LastName,
                        projection.FirstName,
                        projection.MiddleName,
                        projection.BirthDate,
                        projection.SexId,
                        projection.Series,
                        projection.Number,
                        projection.IssueDate,
                        projection.IssuedBy
                    }, commandType: CommandType.StoredProcedure);
                }
            });
        }

        public Task AddStudentAsync(StudentCardProjection projection)
        {
            Should.NotBeNull(projection, nameof(projection));

            return Task.Factory.StartNew(() =>
            {
                using (var conn = _getConnection())
                {
                    var p = new DynamicParameters();
                    p.Add("@MedicalExamination", projection.MedicalExamination);
                    p.Add("@Phone", projection.Phone);
                    p.Add("@NumberOfAuto", projection.NumberOfAuto);
                    p.Add("@Comment", projection.Comment);
                    p.Add("@PassportId", projection.PassportId);
                    p.Add("@LastName", projection.LastName);
                    p.Add("@FirstName", projection.FirstName);
                    p.Add("@MiddleName", projection.MiddleName);
                    p.Add("@BirthDate", projection.BirthDate);
                    p.Add("@SexId", projection.SexId);
                    p.Add("@Series", projection.Series);
                    p.Add("@Number", projection.Number);
                    p.Add("@IssueDate", projection.IssueDate);
                    p.Add("@IssuedBy", projection.IssuedBy);

                    p.Add("@identity", direction: ParameterDirection.ReturnValue);

                    conn.Open();
                    conn.Execute("AddNewStudent", p, commandType: CommandType.StoredProcedure);

                    projection.Id = p.Get<int>("@identity");
                }
            });
        }

        public Task<IList<LivingProjection>> LoadLivingProjectionsAsync(int studentId)
        {
            return Task.Factory.StartNew<IList<LivingProjection>>(() =>
            {
                using (var conn = _getConnection())
                {
                    conn.Open();
                    return conn.Query<LivingProjection>("LoadLivingProjections", new { studentId }, commandType: CommandType.StoredProcedure).ToList();
                }
            });
        }

        public Task<IList<RoomProjection>> LoadRoomsWithFreeSeatsAsync(int hostelId)
        {
            return Task.Factory.StartNew<IList<RoomProjection>>(() =>
            {
                using (var conn = _getConnection())
                {
                    conn.Open();
                    return conn.Query<RoomProjection>("LoadRoomsWithFreeSeats", new { hostelId }, commandType: CommandType.StoredProcedure).ToList();
                }
            });
        }

        public Task AddLivingAsync(LivingProjection projection)
        {
            Should.NotBeNull(projection, nameof(projection));

            return Task.Delay(1);
        }
    }

    #endregion
}