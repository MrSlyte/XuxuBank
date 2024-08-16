using FluentValidation;
using XuxuBank.Domain.Interface;
using XuxuBank.Domain.Models;
using XuxuBank.Domain.Validation;

namespace XuxuBank.Domain.Services;

public class ClientService(IClientRepository personRepository) : IClientService
{
    private readonly IClientRepository _personRepository = personRepository;

    public async Task<CreateTransactionResponseModel> Post(CreateTransactionModel Model)
    {
        var validator = await (new CreateTransactionModelValidation(_personRepository)).ValidateAsync(Model);
        if (!validator.IsValid)
            throw new ValidationException(validator.Errors);

        var resultGuid = await _personRepository.Insert(Model);
        return new CreateTransactionResponseModel(0, 0);
    }
}

