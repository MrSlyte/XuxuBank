

using XuxuBank.Controller;
using XuxuBank.Domain.Interface;
using XuxuBank.Domain.Repositories;
using XuxuBank.Domain.Services;

namespace XuxuBank.Configuration;
internal static class DependencyInjection
{
    internal static void ConfigureDependencyInjection(this IServiceCollection services)
    {
        services.AddTransient<IClientRepository, ClientRepository>();
        services.AddTransient<IClientTransactionRepository, ClientTransactionRepository>();
        services.AddTransient<IClientService, ClientService>();
        services.AddTransient<ClientsController>();
    }
}