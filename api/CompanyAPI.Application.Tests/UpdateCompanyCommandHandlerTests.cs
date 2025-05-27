using CompanyAPI.Application.Command;
using CompanyAPI.Domain.Entities;
using CompanyAPI.Infrastructure.Repositories;
using FluentAssertions;
using Moq;

namespace CompanyAPI.Application.Tests
{
    public class UpdateCompanyCommandHandlerTests
    {
        private Mock<ICompanyRepository> _companyRepositoryMock;

        public UpdateCompanyCommandHandlerTests()
        {
            _companyRepositoryMock = new Mock<ICompanyRepository>();
            _companyRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Company("DB0123456789", "Apple", "EXC", "Ticker") { Id = 1 });
        }

        [Fact]
        public async Task UpdateCompanyCommandHandler_Handle_ShouldCorrectlyUpdateCompany()
        {
            // arrange
            var commandHandler = new UpdateCompanyCommandHandler(_companyRepositoryMock.Object);
            var command = new UpdateCompanyCommand() { Isin = "DB0123456789", Exchange = "EXC", Name = "Apple", Ticker = "Ticker", Website = "http://website.com", Id = 1 };

            // act
            await commandHandler.Handle(command, default);

            // assert
            _companyRepositoryMock.Verify(x => x.CommitAsync(), Times.Once());
        }

        [Fact]
        public async Task CreateCompanyCommandHandler_Handle_ShouldFailIfCommandIsNull()
        {
            // arrange
            var commandHandler = new UpdateCompanyCommandHandler(_companyRepositoryMock.Object);

            // act
            var action = async () => await commandHandler.Handle(null, default);

            // assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateCompanyCommandHandler_Handle_ShouldFailIfIdIsNull()
        {
            // arrange
            var commandHandler = new UpdateCompanyCommandHandler(_companyRepositoryMock.Object);
            var command = new UpdateCompanyCommand() { Isin = "DB0123456789", Exchange = "EXC", Name = "Apple", Ticker = "Ticker", Website = "http://website.com" };

            // act
            var action = async () => await commandHandler.Handle(null, default);

            // assert
            await action.Should().ThrowAsync<ArgumentException>();
        }
    }
}
