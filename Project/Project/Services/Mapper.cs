using Project.DTOs;
using Project.DTOs.Get;
using Project.DTOs.Post;
using Project.DTOs.Put;
using Project.Models;
using Version = Project.Models.Version;

namespace Project.Services;

public class Mapper
{
    public Individual Map(IndividualPostDto dto)
    {
        var individual = new Individual()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Address = dto.Address,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            Pesel = dto.Pesel
        };
        return individual;
    }

    public IndividualGetDto Map(Individual individual)
    {
        var dto = new IndividualGetDto()
        {
            IdIndividual = individual.IdIndividual,
            FirstName = individual.FirstName,
            LastName = individual.LastName,
            Address = individual.Address,
            Email = individual.Email,
            PhoneNumber = individual.PhoneNumber,
            Pesel = individual.Pesel
        };
        return dto;
    }
    
    public Individual Map(IndividualPutDto dto, Individual oldIndividual)
    {
        var newIndividual = new Individual()
        {
            IdIndividual = oldIndividual.IdIndividual,
            FirstName = dto.FirstName ?? oldIndividual.FirstName,
            LastName = dto.LastName ?? oldIndividual.LastName,
            Address = dto.Address ?? oldIndividual.Address,
            Email = dto.Email ?? oldIndividual.Email,
            PhoneNumber = dto.PhoneNumber ?? oldIndividual.PhoneNumber,
            Pesel = oldIndividual.Pesel
        };
        return newIndividual;
    }
    
    public Company Map(CompanyPostDto dto)
    {
        var company = new Company()
        {
            CompanyName = dto.CompanyName,
            Address = dto.Address,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            Krs = dto.Krs
        };
        return company;
    }

    public CompanyGetDto Map(Company company)
    {
        var dto = new CompanyGetDto()
        {
            IdCompany = company.IdCompany,
            CompanyName = company.CompanyName,
            Address = company.Address,
            Email = company.Email,
            PhoneNumber = company.PhoneNumber,
            Krs = company.Krs
        };
        return dto;
    }
    public Company Map(CompanyPutDto dto, Company oldCompany)
    {
        var newCompany = new Company()
        {
            IdCompany = oldCompany.IdCompany,
            CompanyName = dto.CompanyName ?? oldCompany.CompanyName,
            Address = dto.Address ?? oldCompany.Address,
            Email = dto.Email ?? oldCompany.Email,
            PhoneNumber = dto.PhoneNumber ?? oldCompany.PhoneNumber,
            Krs = oldCompany.Krs
        };
        return newCompany;
    }

    public Software Map(SoftwarePostDto dto)
    {
        Software newSoftware = new Software()
        {
            Name = dto.Name,
            Description = dto.Description,
            IdCategory = dto.IdCategory,
            Price = dto.Price
        };
        return newSoftware;
    }

    public SoftwareGetDto Map(Software addSoftware)
    {
        SoftwareGetDto dto = new SoftwareGetDto()
        {
            IdSoftware = addSoftware.IdSoftware,
            Name = addSoftware.Name,
            Description = addSoftware.Description,
            IdCategory = addSoftware.IdCategory,
            Price = addSoftware.Price
        };
        return dto;
    }

    public Version Map(VersionPostDto addVersion)
    {
        Version newVersion = new Version()
        {
            VersionNumber = addVersion.VersionNumber,
            Date = addVersion.Date,
            Comments = addVersion.Comments,
            IdSoftware = addVersion.IdSoftware
            
        };
        return newVersion;
    }

    public VersionGetDto Map(Version addVersion)
    {
        VersionGetDto dto = new VersionGetDto()
        {
            IdVersion = addVersion.IdVersion,
            VersionNumber = addVersion.VersionNumber,
            Date = addVersion.Date,
            Comments = addVersion.Comments,
            IdSoftware = addVersion.IdSoftware
            
        };
        return dto;
    }
}