using CompanyAPI.Application.Command;
using CompanyAPI.Application.Dto;
using CompanyAPI.Application.Query;
using CompanyAPI.Domain.Entities;
using CompanyAPI.Infrastructure.Repositories;
using FluentAssertions;
using Moq;

namespace CompanyAPI.Application.Tests
{
    public class GetCompanyByIsinQueryHandlerTests
    {
        private Mock<ICompanyRepository> _companyRepositoryMock;

        public GetCompanyByIsinQueryHandlerTests()
        {
            _companyRepositoryMock = new Mock<ICompanyRepository>();
        }

        [Fact]
        public async Task GetCompanyByIsinQueryHandler_Handle_ShouldCorrectlyReturnCompanyDto()
        {
            // arrange
            _companyRepositoryMock.Setup(x => x.GetByIsinAsync(It.IsAny<string>())).ReturnsAsync(new Company("DB0123456789", "Apple", "EXC", "Ticker") { Id = 1 });
            var queryHandler = new GetCompanyByIsinQueryHandler(_companyRepositoryMock.Object);
            var query = new GetCompanyByIsinQuery() { Isin = "DB0123456789" };

            // act
            var result = await queryHandler.Handle(query, default);

            // assert
            result.Should().BeOfType<CompanyDto>();
            _companyRepositoryMock.Verify(x => x.GetByIsinAsync(query.Isin), Times.Once());
        }

        [Fact]
        public async Task CreateCompanyCommandHandler_Handle_ShouldFailIfQueryIsNull()
        {
            // arrange
            var queryHandler = new GetCompanyByIsinQueryHandler(_companyRepositoryMock.Object);

            // act
            var action = async () => await queryHandler.Handle(null, default);

            // assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateCompanyCommandHandler_Handle_ShouldFailIfIsinIsNull()
        {
            // arrange
            var queryHandler = new GetCompanyByIsinQueryHandler(_companyRepositoryMock.Object);

            // act
            var action = async () => await queryHandler.Handle(new GetCompanyByIsinQuery(), default);

            // assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateCompanyCommandHandler_Handle_WhenNoCompanyFound_ShouldReturnEmptyCompanyDto()
        {
            // arrange
            _companyRepositoryMock.Setup(x => x.GetByIsinAsync(It.IsAny<string>())).Returns(Task.FromResult<Company>(null));
            var queryHandler = new GetCompanyByIsinQueryHandler(_companyRepositoryMock.Object);

            // act
            var result = await queryHandler.Handle(new GetCompanyByIsinQuery() { Isin = "DB0123456789" }, default);

            // assert
            result.Should().BeOfType<CompanyDto>();
            result.Id.Should().Be(0);
        }
    }
}
