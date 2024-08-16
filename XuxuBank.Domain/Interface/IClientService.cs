using XuxuBank.Domain.Models;

namespace XuxuBank.Domain.Interface;
public interface IClientService
{
    Task<CreateTransactionResponseModel> Post(CreateTransactionModel Model);
}