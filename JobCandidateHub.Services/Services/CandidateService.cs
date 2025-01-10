using JobCandidateHub.Core.Entities;
using JobCandidateHub.Repositories.Data;
using JobCandidateHub.Repositories.Repositories;
using JobCandidateHub.Services.Interfaces;
using JobCandidateHub.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace JobCandidateHub.Services.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly CandidateDbContext _context;

        public CandidateService(CandidateDbContext context)
        {
            _context = context;
        }
        public async Task<CandidateViewModel> CreateOrUpdateCandidateAsync(CandidateViewModel candidate)
        {
            var existingCandidate = await _context.CandidateRepository.FirstOrDefaultAsync(c => c.Email == candidate.Email);

            if (existingCandidate == null)
            {
                var model = new Candidate();
                model.FirstName = candidate.FirstName;
                model.LastName = candidate.LastName;
                model.PhoneNumber= candidate.PhoneNumber;

                model.Email= candidate.Email;
                model.Comment = candidate.Comment;

                await _context.CandidateRepository.AddAsync(model);
            }
            else
            {
                existingCandidate.FirstName = candidate.FirstName;
                existingCandidate.LastName = candidate.LastName;
                existingCandidate.PhoneNumber = candidate.PhoneNumber;

                _context.CandidateRepository.Update(existingCandidate);
            }

            await _context.SaveChangesAsync();
            return candidate;
        }
    }
}
