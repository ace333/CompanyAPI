using CompanyAPI.Application.Dto;
using CompanyAPI.Application.Query;
using CompanyAPI.Domain.Entities;
using CompanyAPI.Infrastructure.Repositories;
using FluentAssertions;
using Moq;

namespace CompanyAPI.Application.Tests
{
    public class GetCompanyByIdQueryHandlerTests
    {
        private Mock<ICompanyRepository> _companyRepositoryMock;

        public GetCompanyByIdQueryHandlerTests()
        {
            _companyRepositoryMock = new Mock<ICompanyRepository>();
        }

        [Fact]
        public async Task GetCompanyByIdQueryHandler_Handle_ShouldCorrectlyReturnCompanyDto()
        {
            // arrange
            _companyRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Company("DB0123456789", "Apple", "EXC", "Ticker") { Id = 1 });
            var queryHandler = new GetCompanyByIdQueryHandler(_companyRepositoryMock.Object);
            var query = new GetCompanyByIdQuery() { Id = 1 };

            // act
            var result = await queryHandler.Handle(query, default);

            // assert
            result.Should().BeOfType<CompanyDto>();
            _companyRepositoryMock.Verify(x => x.GetByIdAsync(query.Id.Value), Times.Once());
        }

        [Fact]
        public async Task CreateCompanyCommandHandler_Handle_ShouldFailIfQueryIsNull()
        {
            // arrange
            var queryHandler = new GetCompanyByIdQueryHandler(_companyRepositoryMock.Object);

            // act
            var action = async () => await queryHandler.Handle(null, default);

            // assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateCompanyCommandHandler_Handle_ShouldFailIfIdIsNull()
        {
            // arrange
            var queryHandler = new GetCompanyByIdQueryHandler(_companyRepositoryMock.Object);

            // act
            var action = async () => await queryHandler.Handle(new GetCompanyByIdQuery(), default);

            // assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateCompanyCommandHandler_Handle_WhenNoCompanyFound_ShouldReturnEmptyCompanyDto()
        {
            // arrange
            _companyRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult<Company>(null));
            var queryHandler = new GetCompanyByIdQueryHandler(_companyRepositoryMock.Object);

            // act
            var result = await queryHandler.Handle(new GetCompanyByIdQuery() { Id = 1 }, default);

            // assert
            result.Should().BeOfType<CompanyDto>();
            result.Id.Should().Be(0);
        }
    }
}
