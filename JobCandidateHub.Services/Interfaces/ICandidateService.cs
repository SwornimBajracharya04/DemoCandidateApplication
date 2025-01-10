using JobCandidateHub.Services.ViewModels;
using System.Threading.Tasks;

namespace JobCandidateHub.Services.Interfaces
{
    public interface ICandidateService
    {
        Task<CandidateViewModel> CreateOrUpdateCandidateAsync(CandidateViewModel candidate);
        Task<CandidateViewModel> GetCandidateByEmailAsync(string email);
    }
}
