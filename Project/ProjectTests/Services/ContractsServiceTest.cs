using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Project.DTOs.Get;
using Project.DTOs.Post;
using Project.Exceptions;
using Project.Models;
using Project.Repositories.Interfaces;
using Project.Services;
using Project.Services.Interfaces;
using Xunit;

namespace ProjectTests.Services
{
    public class ContractsServiceTests
    {
        private readonly ContractsService _contractsService;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IContractsRepository> _contractsRepositoryMock;
        private readonly Mock<ICompaniesRepository> _companiesRepositoryMock;
        private readonly Mock<IIndividualsRepository> _individualsRepositoryMock;
        private readonly Mock<ISoftwaresRepository> _softwaresRepositoryMock;
        private readonly Mock<IVersionsRepository> _versionsRepositoryMock;
        private readonly Mock<IPaymentsRepository> _paymentsRepositoryMock;
        private readonly Mock<IDiscountsRepository> _discountsRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public ContractsServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _contractsRepositoryMock = new Mock<IContractsRepository>();
            _companiesRepositoryMock = new Mock<ICompaniesRepository>();
            _individualsRepositoryMock = new Mock<IIndividualsRepository>();
            _softwaresRepositoryMock = new Mock<ISoftwaresRepository>();
            _versionsRepositoryMock = new Mock<IVersionsRepository>();
            _discountsRepositoryMock = new Mock<IDiscountsRepository>();
            _paymentsRepositoryMock = new Mock<IPaymentsRepository>();

            _mapperMock = new Mock<IMapper>();

            _unitOfWorkMock.SetupGet(uow => uow.Contracts).Returns(_contractsRepositoryMock.Object);
            _unitOfWorkMock.SetupGet(uow => uow.Companies).Returns(_companiesRepositoryMock.Object);
            _unitOfWorkMock.SetupGet(uow => uow.Individuals).Returns(_individualsRepositoryMock.Object);
            _unitOfWorkMock.SetupGet(uow => uow.Softwares).Returns(_softwaresRepositoryMock.Object);
            _unitOfWorkMock.SetupGet(uow => uow.Versions).Returns(_versionsRepositoryMock.Object);
            _unitOfWorkMock.SetupGet(uow => uow.Discounts).Returns(_discountsRepositoryMock.Object);

            _contractsService = new ContractsService(_mapperMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task AddContract_ShouldThrowException_WhenClientDoesNotExist()
        {
            // Arrange
            var dto = new ContractPostDto
            {
                TypeOfClient = "Company",
                IdClient = 1,
                IdSoftware = 1,
                IdVersion = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(4),
                ContinuedSupportYears = 1
            };

            _companiesRepositoryMock.Setup(repo => repo.ExistsById(dto.IdClient)).ReturnsAsync(false);

            // Act
            Func<Task> act = async () => await _contractsService.AddContract(dto);

            // Assert
            await act.Should().ThrowAsync<DoesntExistException>();
        }

        [Fact]
        public async Task AddContract_ShouldThrowException_WhenSoftwareDoesNotExist()
        {
            // Arrange
            var dto = new ContractPostDto
            {
                TypeOfClient = "Company",
                IdClient = 1,
                IdSoftware = 1,
                IdVersion = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(4),
                ContinuedSupportYears = 1
            };

            _companiesRepositoryMock.Setup(repo => repo.ExistsById(dto.IdClient)).ReturnsAsync(true);
            _softwaresRepositoryMock.Setup(repo => repo.ExistsById(dto.IdSoftware)).ReturnsAsync(false);

            // Act
            Func<Task> act = async () => await _contractsService.AddContract(dto);

            // Assert
            await act.Should().ThrowAsync<DoesntExistException>();
        }

       

        [Fact]
        public async Task CalculateTotalDiscount_ShouldReturnCorrectDiscount_WhenReturningClient()
        {
            _discountsRepositoryMock.Setup(repo => repo.GetHighestDiscount()).ReturnsAsync(10m);

            var result = await _contractsService.CalculateTotalDiscount(true);

            result.Should().Be(15m);
        }

        [Fact]
        public void ContractDateRangeIsCorrect_ShouldReturnFalse_WhenRangeIsLessThan3Days()
        {
            var startDate = DateTime.Now;
            var endDate = startDate.AddDays(2);

            var result = _contractsService.ContractDateRangeIsCorrect(startDate, endDate);

            result.Should().BeFalse();
        }

        [Fact]
        public void ContractDateRangeIsCorrect_ShouldReturnFalse_WhenRangeIsMoreThan30Days()
        {
            var startDate = DateTime.Now;
            var endDate = startDate.AddDays(31);

            var result = _contractsService.ContractDateRangeIsCorrect(startDate, endDate);

            result.Should().BeFalse();
        }

        [Fact]
        public void ContractDateRangeIsCorrect_ShouldReturnTrue_WhenRangeIsWithin3To30Days()
        {
            var startDate = DateTime.Now;
            var endDate = startDate.AddDays(15);

            var result = _contractsService.ContractDateRangeIsCorrect(startDate, endDate);

            result.Should().BeTrue();
        }
       

        

        [Fact]
        public async Task AddPayment_ShouldThrowException_WhenPaymentExceedsFullAmount()
        {
            var contractId = 1L;
            var amount = 1200M;
            var contract = new Contract
            {
                IdContract = contractId,
                FullPrice = 1000M,
                EndDate = DateTime.Now.AddDays(10),
                Status = "Created"
            };

            _contractsRepositoryMock.Setup(repo => repo.ExistsById(contractId)).ReturnsAsync(true);
            _contractsRepositoryMock.Setup(repo => repo.ContractCanBePaid(contractId)).ReturnsAsync(true);
            _contractsRepositoryMock.Setup(repo => repo.NewPaymentDoesntExceedFullPrice(contractId, amount)).ReturnsAsync(false);
            _contractsRepositoryMock.Setup(repo => repo.GetById(contractId)).ReturnsAsync(contract);

            Func<Task> act = async () => await _contractsService.AddPayment(contractId, amount);

            await act.Should().ThrowAsync<Exception>().WithMessage("Payment is too big. Transaction is cancelled");
        }

        

        
    }
}
