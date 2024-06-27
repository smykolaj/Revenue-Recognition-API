using Microsoft.AspNetCore.Mvc;
using Project.DTOs;
using Project.DTOs.Get;
using Project.DTOs.Post;
using Project.DTOs.Put;
using Project.Exceptions;
using Project.Models;
using Project.Repositories.Interfaces;
using Project.Services.Interfaces;

namespace Project.Services;

public class ClientsService : IClientsService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ClientsService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }


    public async Task<IndividualGetDto> AddIndividualClient(IndividualPostDto client)
    {
        if (await _unitOfWork.Individuals.ExistsByPesel(client.Pesel))
            throw new NotUniqueException("PESEL");
        

        if (await _unitOfWork.Individuals.ExistsByEmail(client.Email))
            throw new NotUniqueException("e-mail");
        

        if (await _unitOfWork.Individuals.ExistsByPhoneNumber(client.PhoneNumber))
            throw new NotUniqueException("phone number");
        

        var newIndividual = _mapper.Map(client);
        var dto = _mapper.Map(await _unitOfWork.Individuals.AddIndividual(newIndividual));
        
        return dto;
    }

    public async Task<CompanyGetDto> AddCompanyClient(CompanyPostDto client)
    {
        if ( await _unitOfWork.Companies.ExistsByKrs(client.Krs))
            throw new NotUniqueException("KRS");
        
        if ( await _unitOfWork.Companies.ExistsByEmail(client.Email))
            throw new NotUniqueException("e-mail");
        
        if (await _unitOfWork.Companies.ExistsByPhoneNumber(client.PhoneNumber))
            throw new NotUniqueException("phone number");
        

        var newCompany = _mapper.Map(client);
        var dto = _mapper.Map(await _unitOfWork.Companies.AddCompany(newCompany));

        return dto;
    }

    public async Task SoftDeleteIndividualClient(long idIndividual)
    {
        if (!await _unitOfWork.Individuals.ExistsById(idIndividual))
        {
            throw new DoesntExistException("individual", "id");
        }
        

        _unitOfWork.Individuals.DeleteIndividual(await _unitOfWork.Individuals.GetById(idIndividual));
        await _unitOfWork.CompleteAsync();
    }

    public async Task<IndividualGetDto> UpdateDataAboutIndividual(long idIndividual, IndividualPutDto client)
    {
        if (! await _unitOfWork.Individuals.ExistsById(idIndividual))
        {
            throw new DoesntExistException("individual", nameof(idIndividual));
        }
        

        
        var oldIndividual = await _unitOfWork.Individuals.GetById(idIndividual);
        
        if (client.Email is not null && !client.Email.Equals(oldIndividual.Email))
        {
            if (await _unitOfWork.Individuals.ExistsByEmail(client.Email))
                throw new NotUniqueException("e-mail");
        }
        if (client.PhoneNumber is not null && !client.PhoneNumber.Equals(oldIndividual.PhoneNumber))
        {
            if (await _unitOfWork.Individuals.ExistsByPhoneNumber(client.PhoneNumber))
                throw new NotUniqueException("phone number");
        }
        
        var newIndividual = _mapper.Map(client, oldIndividual);
        oldIndividual = await _unitOfWork.Individuals.UpdateIndividual(newIndividual, oldIndividual);
        return _mapper.Map(oldIndividual);
    }

    public async Task<CompanyGetDto> UpdateDataAboutCompany(long idCompany, CompanyPutDto client)
    {
        if (! await _unitOfWork.Companies.ExistsById(idCompany))
        {
            throw new DoesntExistException("company", nameof(idCompany));
        }
        var oldCompany = await _unitOfWork.Companies.GetById(idCompany);
        
        if (client.Email is not null && !client.Email.Equals(oldCompany.Email))
        {
            if (await _unitOfWork.Companies.ExistsByEmail(client.Email))
                throw new NotUniqueException("e-mail");
        }
        if (client.PhoneNumber is not null && !client.PhoneNumber.Equals(oldCompany.PhoneNumber))
        {
            if (await _unitOfWork.Companies.ExistsByPhoneNumber(client.PhoneNumber))
                throw new NotUniqueException("phone number");
        }
        
        var newCompany = _mapper.Map(client, oldCompany);
        oldCompany = await _unitOfWork.Companies.UpdateCompany(newCompany, oldCompany);
        return _mapper.Map(oldCompany);
    }
}