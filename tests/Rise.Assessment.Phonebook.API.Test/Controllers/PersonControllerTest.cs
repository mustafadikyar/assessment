using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Rise.Assessment.Phonebook.API.Controllers;
using Rise.Assessment.Phonebook.Application.Commands;
using Rise.Assessment.Phonebook.Application.DTOs;
using Rise.Assessment.Phonebook.Application.Queries;
using System.Threading.Tasks;
using Xunit;

namespace Rise.Assessment.Phonebook.API.Test.Controllers
{
    public class PersonControllerTest
    {
        [Theory]
        [InlineData(3)]
        public async Task GetPerson_ActionExecutes_ReturnOkObjectResultAsync(int personId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var returned = new PersonDTO();

            mediatorMock.Setup(e => e.Send(It.IsAny<GetPersonQuery>(), default))
                .Returns(() => Task.FromResult(returned as PersonDTO));

            var controller = new PersonController(mediatorMock.Object);

            // Act
            var result = await controller.GetPerson(personId);

            // Assert
            (result is OkObjectResult).Should().BeTrue();
        }

        [Theory]
        [InlineData(3)]
        public async Task GetPerson_ActionExecutes_ReturnPersonDTOAsync(int personId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var returned = new PersonDTO();

            mediatorMock.Setup(e => e.Send(It.IsAny<GetPersonQuery>(), default))
                .Returns(() => Task.FromResult(returned as PersonDTO));

            var controller = new PersonController(mediatorMock.Object);

            // Act
            var result = (await controller.GetPerson(personId)) as OkObjectResult;

            // Assert
            (result.Value is PersonDTO).Should().BeTrue();

        }

        [Theory]
        [InlineData(1)]
        public async Task GetPersonWithDetails_ActionExecutes_ReturnNotFoundResultAsync(int personId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var returned = new PersonDTO();

            mediatorMock.Setup(e => e.Send(It.IsAny<GetPersonWithDetailsQuery>(), default))
                .Returns(() => Task.FromResult(returned as PersonDTO));

            var controller = new PersonController(mediatorMock.Object);

            // Act
            var result = await controller.GetPersonWithDetails(personId);

            // Assert
            (result is OkObjectResult).Should().BeTrue();
        }

        [Theory]
        [InlineData(3)]
        public async Task GetPersonWithDetails_ActionExecutes_ReturnPersonDTOAsync(int personId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var returned = new PersonDTO();

            mediatorMock.Setup(e => e.Send(It.IsAny<GetPersonWithDetailsQuery>(), default))
                .Returns(() => Task.FromResult(returned as PersonDTO));

            var controller = new PersonController(mediatorMock.Object);

            // Act
            var result = (await controller.GetPersonWithDetails(personId)) as OkObjectResult;

            // Assert
            (result.Value is PersonDTO).Should().BeTrue();
        }

        [Fact]
        public async Task CreatePerson_ActionExecutes_ReturnCreatedResultAsync()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var returned = new PersonCreateDTO();

            mediatorMock.Setup(e => e.Send(It.IsAny<CreatePersonCommand>(), default))
                .Returns(() => Task.FromResult(returned as PersonCreateDTO));

            var controller = new PersonController(mediatorMock.Object);

            // Act
            var result = await controller.CreatePerson(new CreatePersonCommand { Name = "Mustafa", Surname = "Dikyar", Company = "ABC Company" });

            // Assert
            (result is CreatedResult).Should().BeTrue();
        }

        [Fact]
        public async Task CreatePersonDetail_ActionExecutes_ReturnCreatedResultAsync()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var returned = new PersonDetailCreateDTO();

            mediatorMock.Setup(e => e.Send(It.IsAny<CreatePersonDetailCommand>(), default))
                .Returns(() => Task.FromResult(returned as PersonDetailCreateDTO));

            var controller = new PersonController(mediatorMock.Object);

            // Act
            var result = await controller.CreatePersonDetail(new CreatePersonDetailCommand { Location = "Antalya", MailAddress = "mustafa@mail.com", PersonId = 1, PhoneNumber = "5433455432" });

            // Assert
            (result is CreatedResult).Should().BeTrue();
        }

        [Fact]
        public async Task DeletePerson_ActionExecutes_ReturnNoContentResultAsync()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var returned = new PersonDeleteDTO();

            mediatorMock.Setup(e => e.Send(It.IsAny<DeletePersonCommand>(), default))
                .Returns(() => Task.FromResult(returned as PersonDeleteDTO));

            var controller = new PersonController(mediatorMock.Object);

            // Act
            var result = await controller.DeletePerson(new DeletePersonCommand { Id = 2 });

            // Assert
            (result is NoContentResult).Should().BeTrue();
        }

        [Fact]
        public async Task DeletePersonDetail_ActionExecutes_ReturnNoContentResultAsync()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var returned = new PersonDetailDeleteDTO();

            mediatorMock.Setup(e => e.Send(It.IsAny<DeletePersonDetailCommand>(), default))
                .Returns(() => Task.FromResult(returned as PersonDetailDeleteDTO));

            var controller = new PersonController(mediatorMock.Object);

            // Act
            var result = await controller.DeletePersonDetail(new DeletePersonDetailCommand { Id = 2 });

            // Assert
            (result is NoContentResult).Should().BeTrue();
        }
    }
}
