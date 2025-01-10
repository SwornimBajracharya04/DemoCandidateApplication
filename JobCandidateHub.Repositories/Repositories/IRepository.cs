using JobCandidateHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.Repositories.Repositories
{
    public interface IRepository<T> where T: BaseEntity
    {
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        IEnumerable<T> GetAll();
    }
}
