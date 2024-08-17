using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using XuxuBank.Domain.Config;
using XuxuBank.Domain.Interface;
using XuxuBank.Domain.Models;

namespace XuxuBank.Domain.Repositories;
public record ClientRepository(IOptions<SystemConfiguration> Config) : BaseRepository(Config), IClientRepository
{

    public async Task<ClientModel> Get(long Id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        try
        {
            await connection.OpenAsync();
            var sql = @"SELECT * FROM Clients WHERE Id = @Id";
            var parametersLimit = new { Id };
            var client = await connection.QuerySingleOrDefaultAsync<ClientModel>(sql, parametersLimit);
            return client;
        }
        finally
        {
            await connection.CloseAsync();
        }
    }
}
