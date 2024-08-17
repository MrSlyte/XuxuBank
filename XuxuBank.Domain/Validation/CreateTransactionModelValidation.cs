using FluentValidation;
using XuxuBank.Domain.Interface;
using XuxuBank.Domain.Models;

namespace XuxuBank.Domain.Validation;
internal class CreateTransactionModelValidation : AbstractValidator<ClientTransactionValidationModel>
{
    public CreateTransactionModelValidation(IClientTransactionRepository _transactionRepository)
    {
        RuleFor(x => x.Value).NotEmpty().WithMessage("Obrigatório informar um valor").GreaterThan(0).WithMessage("Informe um valor maior que 0");
        RuleFor(x => x.Type).NotEmpty().WithMessage("Obrigatório informar um tipo de transação - (C = Crédito | D = Débito)").Must(x => x.Equals("c", StringComparison.InvariantCultureIgnoreCase) || x.Equals("d", StringComparison.InvariantCultureIgnoreCase)).WithMessage("O tipo deve ser apenas C ou D");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Obrigatório informar uma descrição").MinimumLength(1).WithMessage("A descrição deve ter pelo menos 1 letra").MaximumLength(10).WithMessage("A descrição deve ter no máximo 10 letras");
        RuleFor(x => x).MustAsync(async (x, canToken) =>
        {
            if (x.Type.Equals("d", StringComparison.InvariantCultureIgnoreCase))
            {
                var extract = await _transactionRepository.SimpleExtractAsync(x.ClientId);
                if (extract.Saldo - x.Value < -extract.Limite)
                    return false;
                return true;
            }
            return true;
        }).WithMessage("Você não tem limite disponível para essa operação");
    }
}