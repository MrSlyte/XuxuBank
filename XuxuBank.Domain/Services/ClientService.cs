using FluentValidation;
using XuxuBank.Domain.Interface;
using XuxuBank.Domain.Models;
using XuxuBank.Domain.Validation;

namespace XuxuBank.Domain.Services;

public class ClientService(IClientRepository clientRepository, IClientTransactionRepository clientTransactionRepository) : IClientService
{
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly IClientTransactionRepository _clientTransactionRepository = clientTransactionRepository;

    public async Task<SimpleExtractModel> Post(CreateTransactionModel Model, long Id)
    {
        var validModel = new ClientTransactionValidationModel(Id, Model.Valor, Model.Tipo, Model.Descricao);

        var clientValidation = await (new ClientModelValidation(_clientRepository)).ValidateAsync(new ClientModel(Id, string.Empty, 0));
        if (!clientValidation.IsValid)
            throw new ArgumentOutOfRangeException(clientValidation.Errors[0].ErrorMessage);

        var transactionValidation = await (new CreateTransactionModelValidation(_clientTransactionRepository)).ValidateAsync(validModel);
        if (!transactionValidation.IsValid)
            throw new ValidationException(transactionValidation.Errors);

        if (Model.Tipo.Equals("D", StringComparison.InvariantCultureIgnoreCase))
            Model = new CreateTransactionModel(-Model.Valor, Model.Tipo, Model.Descricao);
        var inserted = await _clientTransactionRepository.Insert(Model, Id);
        if (inserted)
        {
            return await _clientTransactionRepository.SimpleExtractAsync(Id);
        }
        throw new ValidationException("Não consegui inserir a transação do cliente");
    }
}

 