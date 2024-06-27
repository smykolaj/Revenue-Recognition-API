using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Project.DTOs.Get;
using Project.DTOs.Post;
using Project.DTOs.Put;
using Project.Exceptions;
using Project.Models;
using Project.Repositories.Interfaces;
using Project.Services;
using Project.Services.Interfaces;
using Xunit;

namespace ProjectTests.Services
{
    public class ClientsServiceTest
    {
        private readonly ClientsService _clientsService;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IIndividualsRepository> _individualsRepositoryMock;
        private readonly Mock<ICompaniesRepository> _companiesRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        
        public ClientsServiceTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _individualsRepositoryMock = new Mock<IIndividualsRepository>();
            _companiesRepositoryMock = new Mock<ICompaniesRepository>();
            _mapperMock = new Mock<IMapper>();

            _unitOfWorkMock.SetupGet(uow => uow.Individuals).Returns(_individualsRepositoryMock.Object);
            _unitOfWorkMock.SetupGet(uow => uow.Companies).Returns(_companiesRepositoryMock.Object);

            _clientsService = new ClientsService(_mapperMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task AddIndividualClient_ShouldThrowNotUniqueException_WhenPeselExists()
        {
            var client = new IndividualPostDto { Pesel = "12345678901" };
            _individualsRepositoryMock.Setup(repo => repo.ExistsByPesel(client.Pesel)).ReturnsAsync(true);

            Func<Task> act = async () => await _clientsService.AddIndividualClient(client);

            await act.Should().ThrowAsync<NotUniqueException>();
        }

        [Fact]
        public async Task AddIndividualClient_ShouldThrowNotUniqueException_WhenEmailExists()
        {
            var client = new IndividualPostDto { Email = "test@example.com" };
            _individualsRepositoryMock.Setup(repo => repo.ExistsByEmail(client.Email)).ReturnsAsync(true);

            Func<Task> act = async () => await _clientsService.AddIndividualClient(client);

            await act.Should().ThrowAsync<NotUniqueException>();
        }

        [Fact]
        public async Task AddIndividualClient_ShouldThrowNotUniqueException_WhenPhoneNumberExists()
        {
            var client = new IndividualPostDto { PhoneNumber = "123456789" };
            _individualsRepositoryMock.Setup(repo => repo.ExistsByPhoneNumber(client.PhoneNumber)).ReturnsAsync(true);

            Func<Task> act = async () => await _clientsService.AddIndividualClient(client);

            await act.Should().ThrowAsync<NotUniqueException>();
        }

        [Fact]
        public async Task AddIndividualClient_ShouldAddClient_WhenDataIsUnique()
        {
            var client = new IndividualPostDto
            {
                Pesel = "12345678901",
                Email = "test@example.com",
                PhoneNumber = "123456789"
            };
            var individual = new Individual
                { Pesel = client.Pesel, Email = client.Email, PhoneNumber = client.PhoneNumber };
            var individualGetDto = new IndividualGetDto
                { Pesel = client.Pesel, Email = client.Email, PhoneNumber = client.PhoneNumber };

            _individualsRepositoryMock.Setup(repo => repo.ExistsByPesel(client.Pesel)).ReturnsAsync(false);
            _individualsRepositoryMock.Setup(repo => repo.ExistsByEmail(client.Email)).ReturnsAsync(false);
            _individualsRepositoryMock.Setup(repo => repo.ExistsByPhoneNumber(client.PhoneNumber)).ReturnsAsync(false);
            _individualsRepositoryMock.Setup(repo => repo.AddIndividual(It.IsAny<Individual>()))
                .ReturnsAsync(individual);

            _mapperMock.Setup(m => m.Map(client)).Returns(individual);
            _mapperMock.Setup(m => m.Map(individual)).Returns(individualGetDto);

            var result = await _clientsService.AddIndividualClient(client);

            result.Should().BeEquivalentTo(individualGetDto);
        }

        [Fact]
        public async Task AddCompanyClient_ShouldThrowNotUniqueException_WhenKrsExists()
        {
            var client = new CompanyPostDto { Krs = "1234567890" };
            _companiesRepositoryMock.Setup(repo => repo.ExistsByKrs(client.Krs)).ReturnsAsync(true);

            Func<Task> act = async () => await _clientsService.AddCompanyClient(client);

            await act.Should().ThrowAsync<NotUniqueException>();
        }

        [Fact]
        public async Task AddCompanyClient_ShouldThrowNotUniqueException_WhenEmailExists()
        {
            var client = new CompanyPostDto { Email = "test@example.com" };
            _companiesRepositoryMock.Setup(repo => repo.ExistsByEmail(client.Email)).ReturnsAsync(true);

            Func<Task> act = async () => await _clientsService.AddCompanyClient(client);

            await act.Should().ThrowAsync<NotUniqueException>();
        }

        [Fact]
        public async Task AddCompanyClient_ShouldThrowNotUniqueException_WhenPhoneNumberExists()
        {
            var client = new CompanyPostDto { PhoneNumber = "123456789" };
            _companiesRepositoryMock.Setup(repo => repo.ExistsByPhoneNumber(client.PhoneNumber)).ReturnsAsync(true);

            Func<Task> act = async () => await _clientsService.AddCompanyClient(client);

            await act.Should().ThrowAsync<NotUniqueException>();
        }

        [Fact]
        public async Task AddCompanyClient_ShouldAddClient_WhenDataIsUnique()
        {
            var client = new CompanyPostDto
            {
                Krs = "1234567890",
                Email = "test@example.com",
                PhoneNumber = "123456789"
            };
            var company = new Company { Krs = client.Krs, Email = client.Email, PhoneNumber = client.PhoneNumber };
            var companyGetDto = new CompanyGetDto
                { Krs = client.Krs, Email = client.Email, PhoneNumber = client.PhoneNumber };

            _companiesRepositoryMock.Setup(repo => repo.ExistsByKrs(client.Krs)).ReturnsAsync(false);
            _companiesRepositoryMock.Setup(repo => repo.ExistsByEmail(client.Email)).ReturnsAsync(false);
            _companiesRepositoryMock.Setup(repo => repo.ExistsByPhoneNumber(client.PhoneNumber)).ReturnsAsync(false);
            _companiesRepositoryMock.Setup(repo => repo.AddCompany(It.IsAny<Company>())).ReturnsAsync(company);

            _mapperMock.Setup(m => m.Map(client)).Returns(company);
            _mapperMock.Setup(m => m.Map(company)).Returns(companyGetDto);

            var result = await _clientsService.AddCompanyClient(client);

            result.Should().BeEquivalentTo(companyGetDto);
        }

        [Fact]
        public async Task SoftDeleteIndividualClient_ShouldThrowDoesntExistException_WhenClientDoesNotExist()
        {
            var idIndividual = 1L;
            _individualsRepositoryMock.Setup(repo => repo.ExistsById(idIndividual)).ReturnsAsync(false);

            Func<Task> act = async () => await _clientsService.SoftDeleteIndividualClient(idIndividual);

            await act.Should().ThrowAsync<DoesntExistException>();
        }

        [Fact]
        public async Task SoftDeleteIndividualClient_ShouldDeleteClient_WhenClientExists()
        {
            var idIndividual = 1L;
            var individual = new Individual { IdIndividual = idIndividual };

            _individualsRepositoryMock.Setup(repo => repo.ExistsById(idIndividual)).ReturnsAsync(true);
            _individualsRepositoryMock.Setup(repo => repo.GetById(idIndividual)).ReturnsAsync(individual);

            await _clientsService.SoftDeleteIndividualClient(idIndividual);

            _individualsRepositoryMock.Verify(repo => repo.DeleteIndividual(individual), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateDataAboutIndividual_ShouldThrowDoesntExistException_WhenClientDoesNotExist()
        {
            var idIndividual = 1L;
            var client = new IndividualPutDto();

            _individualsRepositoryMock.Setup(repo => repo.ExistsById(idIndividual)).ReturnsAsync(false);

            Func<Task> act = async () => await _clientsService.UpdateDataAboutIndividual(idIndividual, client);

            await act.Should().ThrowAsync<DoesntExistException>();
        }
        [Fact]
        public async Task UpdateDataAboutIndividual_ShouldThrowNotUniqueException_WhenPhoneNumberExists()
        {
            var idIndividual = 1L;
            var client = new IndividualPutDto { PhoneNumber = "123456789" };
            var oldIndividual = new Individual { PhoneNumber = "987654321" };

            _individualsRepositoryMock.Setup(repo => repo.ExistsById(idIndividual)).ReturnsAsync(true);
            _individualsRepositoryMock.Setup(repo => repo.GetById(idIndividual)).ReturnsAsync(oldIndividual);
            _individualsRepositoryMock.Setup(repo => repo.ExistsByPhoneNumber(client.PhoneNumber)).ReturnsAsync(true);

            Func<Task> act = async () => await _clientsService.UpdateDataAboutIndividual(idIndividual, client);

            await act.Should().ThrowAsync<NotUniqueException>();
        }

        [Fact]
        public async Task UpdateDataAboutIndividual_ShouldUpdateClient_WhenDataIsValid()
        {
            var idIndividual = 1L;
            var client = new IndividualPutDto { Email = "new@example.com", PhoneNumber = "123456789" };
            var oldIndividual = new Individual { Email = "old@example.com", PhoneNumber = "987654321" };
            var updatedIndividual = new Individual { Email = "new@example.com", PhoneNumber = "123456789" };
            var individualGetDto = new IndividualGetDto { Email = "new@example.com", PhoneNumber = "123456789" };

            _individualsRepositoryMock.Setup(repo => repo.ExistsById(idIndividual)).ReturnsAsync(true);
            _individualsRepositoryMock.Setup(repo => repo.GetById(idIndividual)).ReturnsAsync(oldIndividual);
            _individualsRepositoryMock.Setup(repo => repo.UpdateIndividual(It.IsAny<Individual>(), oldIndividual)).ReturnsAsync(updatedIndividual);

            _mapperMock.Setup(m => m.Map(client, oldIndividual)).Returns(updatedIndividual);
            _mapperMock.Setup(m => m.Map(updatedIndividual)).Returns(individualGetDto);

            var result = await _clientsService.UpdateDataAboutIndividual(idIndividual, client);

            result.Should().BeEquivalentTo(individualGetDto);
        }

        [Fact]
        public async Task UpdateDataAboutCompany_ShouldThrowDoesntExistException_WhenClientDoesNotExist()
        {
            var idCompany = 1L;
            var client = new CompanyPutDto();

            _companiesRepositoryMock.Setup(repo => repo.ExistsById(idCompany)).ReturnsAsync(false);

            Func<Task> act = async () => await _clientsService.UpdateDataAboutCompany(idCompany, client);

            await act.Should().ThrowAsync<DoesntExistException>();
        }

        [Fact]
        public async Task UpdateDataAboutCompany_ShouldThrowNotUniqueException_WhenEmailExists()
        {
            var idCompany = 1L;
            var client = new CompanyPutDto { Email = "new@example.com" };
            var oldCompany = new Company { Email = "old@example.com" };

            _companiesRepositoryMock.Setup(repo => repo.ExistsById(idCompany)).ReturnsAsync(true);
            _companiesRepositoryMock.Setup(repo => repo.GetById(idCompany)).ReturnsAsync(oldCompany);
            _companiesRepositoryMock.Setup(repo => repo.ExistsByEmail(client.Email)).ReturnsAsync(true);

            Func<Task> act = async () => await _clientsService.UpdateDataAboutCompany(idCompany, client);

            await act.Should().ThrowAsync<NotUniqueException>();
        }

        [Fact]
        public async Task UpdateDataAboutCompany_ShouldThrowNotUniqueException_WhenPhoneNumberExists()
        {
            var idCompany = 1L;
            var client = new CompanyPutDto { PhoneNumber = "123456789" };
            var oldCompany = new Company { PhoneNumber = "987654321" };

            _companiesRepositoryMock.Setup(repo => repo.ExistsById(idCompany)).ReturnsAsync(true);
            _companiesRepositoryMock.Setup(repo => repo.GetById(idCompany)).ReturnsAsync(oldCompany);
            _companiesRepositoryMock.Setup(repo => repo.ExistsByPhoneNumber(client.PhoneNumber)).ReturnsAsync(true);

            Func<Task> act = async () => await _clientsService.UpdateDataAboutCompany(idCompany, client);

            await act.Should().ThrowAsync<NotUniqueException>();
        }

        [Fact]
        public async Task UpdateDataAboutCompany_ShouldUpdateClient_WhenDataIsValid()
        {
            var idCompany = 1L;
            var client = new CompanyPutDto { Email = "new@example.com", PhoneNumber = "123456789" };
            var oldCompany = new Company { Email = "old@example.com", PhoneNumber = "987654321" };
            var updatedCompany = new Company { Email = "new@example.com", PhoneNumber = "123456789" };
            var companyGetDto = new CompanyGetDto { Email = "new@example.com", PhoneNumber = "123456789" };

            _companiesRepositoryMock.Setup(repo => repo.ExistsById(idCompany)).ReturnsAsync(true);
            _companiesRepositoryMock.Setup(repo => repo.GetById(idCompany)).ReturnsAsync(oldCompany);
            _companiesRepositoryMock.Setup(repo => repo.UpdateCompany(It.IsAny<Company>(), oldCompany)).ReturnsAsync(updatedCompany);

            _mapperMock.Setup(m => m.Map(client, oldCompany)).Returns(updatedCompany);
            _mapperMock.Setup(m => m.Map(updatedCompany)).Returns(companyGetDto);

            var result = await _clientsService.UpdateDataAboutCompany(idCompany, client);

            result.Should().BeEquivalentTo(companyGetDto);
        }
    }
}