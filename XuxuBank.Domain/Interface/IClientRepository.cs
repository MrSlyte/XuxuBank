using XuxuBank.Domain.Models;

namespace XuxuBank.Domain.Interface;
public interface IClientRepository
{
    Task<ClientModel> Get(long Id);
}