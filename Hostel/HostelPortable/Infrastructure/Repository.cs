using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using HostelPortable.Interfaces;
using HostelPortable.Projections;

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

        #endregion
    }
}