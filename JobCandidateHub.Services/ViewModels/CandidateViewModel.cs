using System.ComponentModel.DataAnnotations;

namespace JobCandidateHub.Services.ViewModels
{
    public class CandidateViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string TimeIntervalToCall { get; set; }
        public string LinkedinURL{ get; set; }
        public string GitHubURL { get; set; }
        [Required]
        public string Comment { get; set; }
    }
}
