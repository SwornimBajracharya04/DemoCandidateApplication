using JobCandidateHub.Core.Entities;
using JobCandidateHub.Repositories.Data;
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
            if (string.IsNullOrEmpty(candidate.Email))
            {
                return null;
            }

            var existingCandidate = await _context.Candidate.AsNoTracking().FirstOrDefaultAsync(c => c.Email == candidate.Email);

            if (existingCandidate == null)
            {
                var model = new Candidate();
                model.FirstName = candidate.FirstName;
                model.LastName = candidate.LastName;
                model.PhoneNumber= candidate.PhoneNumber;
                model.Email= candidate.Email;
                model.Comment = candidate.Comment;
                model.TimeIntervalToCall = candidate.TimeIntervalToCall;
                model.LinkedinURL = candidate.LinkedinURL;
                model.GitHubURL = candidate.GitHubURL;

                await _context.Candidate.AddAsync(model);
            }
            else
            {
                existingCandidate.FirstName = candidate.FirstName;
                existingCandidate.LastName = candidate.LastName;
                existingCandidate.PhoneNumber = candidate.PhoneNumber;
                existingCandidate.Email = candidate.Email;
                existingCandidate.Comment = candidate.Comment;
                existingCandidate.TimeIntervalToCall = candidate.TimeIntervalToCall;
                existingCandidate.LinkedinURL = candidate.LinkedinURL;
                existingCandidate.GitHubURL = candidate.GitHubURL;

                _context.Candidate.Update(existingCandidate);
            }

            await _context.SaveChangesAsync();
            return candidate;
        }

        public async Task<CandidateViewModel> GetCandidateByEmailAsync(string email)
        {
            var existingCandidate = await _context.Candidate.AsNoTracking().FirstOrDefaultAsync(c => c.Email == email);

            if (existingCandidate == null)
            {
                return null;
            }

            var returnModel = new CandidateViewModel();

            returnModel.FirstName = existingCandidate.FirstName;
            returnModel.LastName = existingCandidate.LastName;
            returnModel.PhoneNumber = existingCandidate.PhoneNumber;
            returnModel.Email = existingCandidate.Email;
            returnModel.Comment = existingCandidate.Comment;
            returnModel.TimeIntervalToCall = existingCandidate.TimeIntervalToCall;
            returnModel.LinkedinURL = existingCandidate.LinkedinURL;
            returnModel.GitHubURL = existingCandidate.GitHubURL;

            return returnModel;

        }
    }
}
