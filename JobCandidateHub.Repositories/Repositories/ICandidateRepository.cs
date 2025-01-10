using JobCandidateHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.Core.Interfaces
{
    public interface ICandidateRepository
    {
        Task<Candidate> CreateOrUpdateAsync(Candidate candidate);
        //Task<Candidate> GetByIdAsync(int id);
        //Task<bool> DeleteAsync(int id);
    }
}
