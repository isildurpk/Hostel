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
                    p.Add(nameof(projection.MedicalExamination), projection.MedicalExamination);
                    p.Add(projection.Phone);
                    p.Add(projection.NumberOfAuto);
                    p.Add(projection.Comment);
                    p.Add(nameof(projection.PassportId), projection.PassportId);
                    p.Add(projection.LastName);
                    p.Add(projection.FirstName);
                    p.Add(projection.MiddleName);
                    p.Add(nameof(projection.BirthDate), projection.BirthDate);
                    p.Add(nameof(projection.SexId), projection.SexId);
                    p.Add(nameof(projection.Series), projection.Series);
                    p.Add(nameof(projection.Number), projection.Number);
                    p.Add(nameof(projection.IssueDate), projection.IssueDate);
                    p.Add(projection.IssuedBy);

                    p.Add("@identity", direction:ParameterDirection.ReturnValue);

                    conn.Open();
                    conn.Execute("AddNewStudent", p, commandType: CommandType.StoredProcedure);

                    projection.Id = p.Get<int>("@identity");
                }
            });
        }
    }

    #endregion
}