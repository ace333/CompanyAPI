using CompanyAPI.Application.Command;
using CompanyAPI.Domain.Entities;
using CompanyAPI.Infrastructure.Repositories;
using FluentAssertions;
using Moq;

namespace CompanyAPI.Application.Tests
{
    public class CreateCompanyCommandHandlerTests
    {
        private Mock<ICompanyRepository> _companyRepositoryMock;

        public CreateCompanyCommandHandlerTests()
        {
            _companyRepositoryMock = new Mock<ICompanyRepository>();
        }

        [Fact]
        public async Task CreateCompanyCommandHandler_Handle_ShouldCorrectlyCreateCompany()
        {
            // arrange
            var commandHandler = new CreateCompanyCommandHandler(_companyRepositoryMock.Object);
            var command = new CreateCompanyCommand() { Isin = "DB0123456789", Exchange = "EXC", Name = "Apple", Ticker = "Ticker", Website = "http://website.com" };

            // act
            await commandHandler.Handle(command, default);

            // assert
            _companyRepositoryMock.Verify(x => x.Add(It.Is<Company>(
                c => c.Isin == command.Isin && c.Exchange == command.Exchange && c.Name == command.Name && c.Ticker == command.Ticker && c.Website == command.Website)), 
                Times.Once());
            _companyRepositoryMock.Verify(x => x.CommitAsync(), Times.Once());
        }

        [Fact]
        public async Task CreateCompanyCommandHandler_Handle_ShouldFailIfCommandIsNull()
        {
            // arrange
            var commandHandler = new CreateCompanyCommandHandler(_companyRepositoryMock.Object);

            // act
            var action = async () => await commandHandler.Handle(null, default);

            // assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }
    }
}
