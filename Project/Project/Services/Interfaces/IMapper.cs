using Project.DTOs.Get;
using Project.DTOs.Post;
using Project.DTOs.Put;
using Project.Models;
using Version = Project.Models.Version;

namespace Project.Services.Interfaces;

public interface IMapper
{

    Individual Map(IndividualPostDto dto);
    IndividualGetDto Map(Individual individual);
    Individual Map(IndividualPutDto dto, Individual oldIndividual);
    Company Map(CompanyPostDto dto);
    CompanyGetDto Map(Company company);
    Company Map(CompanyPutDto dto, Company oldCompany);
    Software Map(SoftwarePostDto dto);
    SoftwareGetDto Map(Software addSoftware);
    Version Map(VersionPostDto addVersion, long idSoftware);
    VersionGetDto Map(Version addVersion);
    Discount Map(DiscountPostDto addDiscount);
    DiscountGetDto Map(Discount addDiscount);
    Contract Map(ContractPostDto dto, decimal price, string status, string typeOfClient);
    ContractGetDto Map(Contract addContract);
    PaymentGetDto Map(Payment addPayment);
}