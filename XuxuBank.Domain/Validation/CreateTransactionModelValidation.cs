using FluentValidation;
using XuxuBank.Domain.Interface;
using XuxuBank.Domain.Models;

namespace XuxuBank.Domain.Validation
{
    internal class CreateTransactionModelValidation(IClientRepository clientRepository) : AbstractValidator<CreateTransactionModel>
    {
    }
}
