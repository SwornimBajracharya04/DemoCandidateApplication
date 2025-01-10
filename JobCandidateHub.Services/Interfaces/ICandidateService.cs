using JobCandidateHub.Core.Entities;
using JobCandidateHub.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.Services.Interfaces
{
    public interface ICandidateService
    {
        Task<CandidateViewModel> CreateOrUpdateCandidateAsync(CandidateViewModel candidate);
    }
}
