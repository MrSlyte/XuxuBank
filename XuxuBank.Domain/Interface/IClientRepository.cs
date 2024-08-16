using XuxuBank.Domain.Models;

namespace XuxuBank.Domain.Interface;
public interface IClientRepository
{
    Task<Guid> Insert(CreateTransactionModel Model);
}