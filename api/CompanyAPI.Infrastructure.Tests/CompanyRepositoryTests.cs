using CompanyAPI.Domain.Entities;
using CompanyAPI.Infrastructure.Context;
using CompanyAPI.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CompanyAPI.Infrastructure.Tests
{
    public class CompanyRepositoryTests
    {
        private readonly Mock<CompanyDbContext> _mockedContext;

        public CompanyRepositoryTests() 
        {
            var mockSet = new Mock<DbSet<Company>>();
            mockSet.Setup(c => c.AsQueryable()).Verifiable();

            _mockedContext = new Mock<CompanyDbContext>(new DbContextOptions<CompanyDbContext>());
            _mockedContext.Setup(c => c.Companies).Returns(mockSet.Object);
        }

        [Fact]
        public void CompanyRepository_GetAll_ShouldReturnListOfCompanies()
        {
            // arrange
            var repository = new CompanyRepository(_mockedContext.Object);

            // act
            repository.GetAll();

            // assert
            _mockedContext.Verify(c => c.Companies.AsQueryable(), Times.Once());
        }
    }
}