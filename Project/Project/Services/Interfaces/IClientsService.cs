using Project.DTOs;
using Project.DTOs.Get;
using Project.DTOs.Post;
using Project.DTOs.Put;
using Project.Models;

namespace Project.Services.Interfaces;

public interface IClientsService
{
    Task<IndividualGetDto> AddIndividualClient(IndividualPostDto client);
    Task<CompanyGetDto> AddCompanyClient(CompanyPostDto client);
    Task SoftDeleteIndividualClient(long idIndividual);
    Task<IndividualGetDto> UpdateDataAboutIndividual(long idIndividual, IndividualPutDto client);
    Task<CompanyGetDto> UpdateDataAboutCompany(long idCompany, CompanyPutDto client);
}