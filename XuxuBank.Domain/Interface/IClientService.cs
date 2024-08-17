using XuxuBank.Domain.Models;

namespace XuxuBank.Domain.Interface;
public interface IClientService
{
    Task<SimpleExtractModel> Post(CreateTransactionModel Model, long Id);
}