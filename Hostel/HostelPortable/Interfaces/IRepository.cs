﻿using System.Collections.Generic;
using System.Threading.Tasks;
using HostelPortable.Projections;

namespace HostelPortable.Interfaces
{
    public interface IRepository
    {
        Task<IList<StudentProjection>> LoadStudentProjectionsAsync();
    }
}