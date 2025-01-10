using JobCandidateHub.Core.Entities;
using JobCandidateHub.Core.Interfaces;
using JobCandidateHub.Repositories.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.Repositories.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly CandidateDbContext _context;

        public CandidateRepository(CandidateDbContext context)
        {
            _context = context;
        }

        public async Task<Candidate> CreateOrUpdateAsync(Candidate candidate)
        {
            var existingCandidate = await _context.Candidate.FirstOrDefaultAsync(c => c.ID == candidate.ID);
            
            if (existingCandidate == null)
            {
                _context.Candidate.Add(candidate);
            }
            else
            {
                existingCandidate.FirstName= candidate.FirstName;
                existingCandidate.LastName = candidate.LastName;
                existingCandidate.PhoneNumber = candidate.PhoneNumber;

                _context.Candidate.Update(existingCandidate);
            }

            await _context.SaveChangesAsync();
            return candidate;
        }
    }
}
