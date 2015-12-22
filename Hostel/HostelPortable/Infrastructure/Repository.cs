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
                    return conn.Query<StudentCardProjection, PassportProjection, StudentCardProjection>("GetStudentCardProjection",
                        (student, passport) =>
                        {
                            student.Passport = passport;
                            return student;
                        },
                        new { studentId },
                        commandType: CommandType.StoredProcedure).SingleOrDefault();
                }
            });
        }

        public Task UpdateStudentCardAsync(int studentId, StudentCardProjection projection)
        {
            Should.NotBeNull(projection, nameof(projection));
            Should.NotBeNull(projection.Passport, nameof(projection.Passport));

            return Task.Factory.StartNew(() =>
            {
                using (var conn = _getConnection())
                {
                    conn.Open();
                    conn.Execute("UpdateStudentCard",
                        new
                        {
                            studentId,
                            projection.MedicalExamination,
                            projection.Phone,
                            projection.NumberOfAuto,
                            projection.Comment,
                            projection.Passport.LastName,
                            projection.Passport.FirstName,
                            projection.Passport.MiddleName,
                            projection.Passport.BirthDate,
                            projection.Passport.SexId,
                            projection.Passport.Series,
                            projection.Passport.Number,
                            projection.Passport.IssueDate,
                            projection.Passport.IssuedBy
                        },
                        commandType: CommandType.StoredProcedure);
                }
            });
        }
    }

    #endregion
}