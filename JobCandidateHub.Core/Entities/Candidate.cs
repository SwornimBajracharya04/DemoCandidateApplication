using System.ComponentModel.DataAnnotations;

namespace JobCandidateHub.Core.Entities
{
    public class Candidate : BaseEntity
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
