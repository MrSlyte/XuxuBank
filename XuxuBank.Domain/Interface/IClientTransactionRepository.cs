using XuxuBank.Domain.Models;

namespace XuxuBank.Domain.Interface;
public interface IClientTransactionRepository
{
    Task<bool> Insert(CreateTransactionModel Model, long Id);
    Task<SimpleExtractModel> SimpleExtractAsync(long Id);
}