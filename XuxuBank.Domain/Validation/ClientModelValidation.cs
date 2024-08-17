using FluentValidation;
using XuxuBank.Domain.Interface;
using XuxuBank.Domain.Models;

namespace XuxuBank.Domain.Validation;
internal class ClientModelValidation : AbstractValidator<ClientModel>
{
    public ClientModelValidation(IClientRepository _clientRepository)
    {
        RuleFor(x => x.Id).MustAsync(async (clientId, canToken) =>
        {
            var client = await _clientRepository.Get(clientId);
            return client.Id != 0;
        }).WithMessage("O cliente não existe");
    }
}