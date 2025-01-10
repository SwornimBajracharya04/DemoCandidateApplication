using JobCandidateHub.Controllers;
using JobCandidateHub.Services.Interfaces;
using JobCandidateHub.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace JobCandidateHub.UnitTest
{
    public class CandidateControllerTest
    {
        private readonly Mock<ICandidateService> _mockService;
        private readonly CandidateController _candidateController;

        public CandidateControllerTest()
        {
            // Arrange: Create the mock for ICandidateService and Controller
            _mockService = new Mock<ICandidateService>();
            _candidateController = new CandidateController(_mockService.Object);
        }

        private List<CandidateViewModel> CandidatesValue = new List<CandidateViewModel>()
        {
            new CandidateViewModel{FirstName="Swornim", LastName="Bajracharya", PhoneNumber="123123", Email="", Comment="Hello World"}, //missing email 0
            new CandidateViewModel{FirstName="Swornim", LastName="Bajracharya", PhoneNumber="123123", Email="swornim@asdf.com", Comment="Hello World"}, //good data 1
            new CandidateViewModel{FirstName="asdf", LastName="Bajracharya", PhoneNumber="123123", Email="swornim@asdf.com", Comment="Hello World"}, //good update data 2
            new CandidateViewModel{FirstName="Swornim", LastName="Bajracharya", PhoneNumber="123123", Email="swornim@asdf.com", Comment=""}, // missing required field 3
            new CandidateViewModel{FirstName="Swornim", LastName="", PhoneNumber="", Email="swornim@asdf.com", Comment=""}, // missing required fields 4
            new CandidateViewModel() // null value 5
        };
        [Fact]
        public void CandidateController_hello_ConnectionTest()
        {
            var expected = "Hello World";

            var result = _candidateController.hello();

            Assert.Equal(expected, result.ToString());

        }

        [Fact]
        public async Task CreateOrUpdate_ReturnsBadRequest_WhenModelIsNull()
        {
            // Act
            var result = await _candidateController.CreateOrUpdate(null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task CreateOrUpdate_ReturnsBadRequest_WhenEmailIsMissing()
        {
            // Arrange
            var model = CandidatesValue[0];

            // Act
            var result = await _candidateController.CreateOrUpdate(model);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Fail to Create/Update Candidate. Email required. ", badRequestResult.Value);
        }

        [Fact]
        public async Task CreateOrUpdate_ReturnsOk_WhenCandidateCreatedOrUpdated()
        {
            // Arrange
            var model = CandidatesValue[1];
            _mockService.Setup(service => service.CreateOrUpdateCandidateAsync(model))
                                 .ReturnsAsync(model);

            // Act
            var result = await _candidateController.CreateOrUpdate(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(model, okResult.Value);
        }

        [Fact]
        public async Task CreateOrUpdate_ReturnsBadRequest_WhenCreateOrUpdateFails()
        {
            // Arrange
            var model = CandidatesValue[5];
            _mockService.Setup(service => service.CreateOrUpdateCandidateAsync(model))
                                 .ReturnsAsync((CandidateViewModel)null);

            // Act
            var result = await _candidateController.CreateOrUpdate(model);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Fail to Create/Update Candidate. Email required. ", badRequestResult.Value);
        }
        [Fact]
        public async Task GetByEmail_ReturnsBadRequest_WhenEmailIsNull()
        {
            // Act
            var result = await _candidateController.GetByEmail(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Please enter Email Address", badRequestResult.Value);
        }

        [Fact]
        public async Task GetByEmail_ReturnsBadRequest_WhenCandidateNotFound()
        {
            // Arrange
            var email = "test@example.com";
            _mockService.Setup(service => service.GetCandidateByEmailAsync(email))
                                 .ReturnsAsync((CandidateViewModel)null);

            // Act
            var result = await _candidateController.GetByEmail(email);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Candidate with Email Address not found", badRequestResult.Value);
        }

        [Fact]
        public async Task GetByEmail_ReturnsOk_WhenCandidateFound()
        {
            // Arrange
            var email = "test@example.com";
            var candidate = new CandidateViewModel { Email = email };
            _mockService.Setup(service => service.GetCandidateByEmailAsync(email))
                                 .ReturnsAsync(candidate);

            // Act
            var result = await _candidateController.GetByEmail(email);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(candidate, okResult.Value);
        }

        [Fact]
        public async Task CreateOrUpdate_ReturnsBadRequest_WhenEmailIsInvalid()
        {
            // Arrange: Create a CandidateViewModel with an invalid Email format
            var model = new CandidateViewModel { FirstName = "John", LastName = "Doe", Email = "invalid-email", Comment = "Test comment" };

            // Act: Call the CreateOrUpdate action with the invalid email
            var result = await _candidateController.CreateOrUpdate(model);

            // Assert: Check that the result is a BadRequest
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains("Email", badRequestResult.Value.ToString());
        }

        [Fact]
        public async Task CreateOrUpdate_ReturnsOk_WhenAllRequiredFieldsAreValid()
        {
            // Arrange: Create a valid CandidateViewModel with all required fields
            var model = new CandidateViewModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "test@example.com",
                Comment = "Test comment"
            };
            var updatedCandidate = new CandidateViewModel { FirstName = "John", LastName = "Doe", Email = "test@example.com", Comment = "Test comment" };
            _mockService.Setup(service => service.CreateOrUpdateCandidateAsync(model))
                                 .ReturnsAsync(updatedCandidate);

            // Act: Call the CreateOrUpdate action with a valid model
            var result = await _candidateController.CreateOrUpdate(model);

            // Assert: Check that the result is an OkObjectResult with the updated candidate
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(updatedCandidate, okResult.Value);
        }
    }
}
