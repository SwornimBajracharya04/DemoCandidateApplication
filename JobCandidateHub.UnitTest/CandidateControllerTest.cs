using JobCandidateHub.Controllers;
using JobCandidateHub.Core.Interfaces;
using JobCandidateHub.Repositories.Data;
using JobCandidateHub.Services.Interfaces;
using JobCandidateHub.Services.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JobCandidateHub.UnitTest
{
    public class CandidateControllerTest
    {
        private readonly Mock<ICandidateService> _mockService;
        private readonly Mock<CandidateDbContext> _mockDbContext;
        private readonly CandidateController _candidateController;

        public CandidateControllerTest()
        {
            // Arrange: Create the mock for ICandidateService and Controller
            _mockService = new Mock<ICandidateService>();
            _candidateController = new CandidateController(_mockService.Object);
        }

        private List<CandidateViewModel> CandidatesValue = new List<CandidateViewModel>()
        {
            new CandidateViewModel{FirstName="Swornim", LastName="Bajracharya", PhoneNumber="123123", Email="", Comment="Hello World"}, //missing email
            new CandidateViewModel{FirstName="Swornim", LastName="Bajracharya", PhoneNumber="123123", Email="swornim@asdf.com", Comment="Hello World"}, //good data
            new CandidateViewModel{FirstName="asdf", LastName="Bajracharya", PhoneNumber="123123", Email="swornim@asdf.com", Comment="Hello World"}, //good update data
            new CandidateViewModel{FirstName="Swornim", LastName="Bajracharya", PhoneNumber="123123", Email="swornim@asdf.com", Comment=""}, // missing required field
            new CandidateViewModel{FirstName="Swornim", LastName="", PhoneNumber="", Email="swornim@asdf.com", Comment=""}, // missing required fields
            new CandidateViewModel() // null value email
        };
        [Fact]
        public void CandidateController_hello_ConnectionTest()
        {
            var expected = "Hello World";

            var result = _candidateController.hello();

            Assert.Equal(expected, result.ToString());

        }

        [Fact]
        public async Task Create_ShouldReturnCandidate_WhenValidCandidateIsPassed()
        {
            // Arrange
            var candidate = CandidatesValue[1];
            _mockService.Setup(repo => repo.CreateOrUpdateCandidateAsync(candidate)).ReturnsAsync(candidate);

            // Act
            var result = await _candidateController.GetByEmail(candidate.Email);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(candidate.FirstName, result.ToString());
            _mockService.Verify(repo => repo.CreateOrUpdateCandidateAsync(It.IsAny<CandidateViewModel>()), Times.Once);
        }

        //[Fact]
        //public async Task Create_ShouldThrowArgumentNullException_WhenCandidateIsNull()
        //{
        //    // Act & Assert
        //    await Assert.ThrowsAsync<ArgumentNullException>(() => _candidateService.CreateOrUpdateCandidateAsync(null));
        //}

        //[Fact]
        //public async Task Update_ShouldReturnUpdatedCandidate_WhenValidCandidateIsPassed()
        //{
        //    // Arrange
        //    var candidate = new CandidateViewModel { Id = 1, Name = "John Doe", Email = "john@example.com" };
        //    var updatedCandidate = new CandidateViewModel { Id = 1, Name = "John Doe Updated", Email = "john.updated@example.com" };

        //    _mockRepo.Setup(repo => repo.GetById(candidate.Id)).Returns(candidate);
        //    _mockRepo.Setup(repo => repo.CreateOrUpdateCandidateAsync(updatedCandidate)).ReturnsAsync(updatedCandidate);

        //    // Act
        //    var result = await _candidateService.CreateOrUpdateCandidateAsync(updatedCandidate);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equal(updatedCandidate.FirstName, result.FirstName);
        //    _mockRepo.Verify(repo => repo.CreateOrUpdateCandidateAsync(It.IsAny<CandidateViewModel>()), Times.Once);
        //}

        //[Fact]
        //public void Update_ShouldThrowInvalidOperationException_WhenCandidateNotFound()
        //{
        //    // Arrange
        //    var candidate = new CandidateViewModel {  FirstName = "Non Existing Candidate", Email = "nonexistent@example.com" };

        //    //_mockRepo.Setup(repo => repo.GetById(candidate.Id)).Returns((CandidateViewModel)null);

        //    // Act & Assert
        //    Assert.ThrowsAsync<InvalidOperationException>(() => _candidateService.CreateOrUpdateCandidateAsync(candidate));
        //}

        //[Fact]
        //public void Update_ShouldThrowArgumentNullException_WhenCandidateIsNull()
        //{
        //    // Act & Assert
        //    Assert.ThrowsAsync<ArgumentNullException>(() => _candidateService.CreateOrUpdateCandidateAsync(null));
        //}
    }
}
