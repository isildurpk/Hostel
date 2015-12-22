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
                    return conn.Query<StudentCardProjection>("GetStudentCardProjection", new { studentId }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                }
            });
        }

        public Task UpdateStudentCardAsync(int studentId, StudentCardProjection projection)
        {
            Should.NotBeNull(projection, nameof(projection));

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
                            projection.LastName,
                            projection.FirstName,
                            projection.MiddleName,
                            projection.BirthDate,
                            projection.SexId,
                            projection.Series,
                            projection.Number,
                            projection.IssueDate,
                            projection.IssuedBy
                        },
                        commandType: CommandType.StoredProcedure);
                }
            });
        }
    }

    #endregion
}