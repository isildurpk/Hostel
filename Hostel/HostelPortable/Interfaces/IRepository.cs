using System.Collections.Generic;
using System.Threading.Tasks;
using HostelPortable.Projections;

namespace HostelPortable.Interfaces
{
    public interface IRepository
    {
        Task<IList<StudentProjection>> LoadStudentProjectionsAsync();

        Task<StudentCardProjection> GetStudentCardProjectionAsync(int studentId);

        Task UpdateStudentCardAsync(StudentCardProjection projection);

        Task AddStudentAsync(StudentCardProjection projection);

        Task<IList<LivingProjection>> LoadLivingProjectionsAsync(int studentId);
    }
}