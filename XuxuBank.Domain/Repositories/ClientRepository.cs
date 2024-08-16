using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using XuxuBank.Domain.Config;
using XuxuBank.Domain.Interface;
using XuxuBank.Domain.Models;

namespace XuxuBank.Domain.Repositories;
public record ClientRepository(IOptions<SystemConfiguration> Config) : BaseRepository(Config), IClientRepository
{
    public async Task<Guid> Insert(CreateTransactionModel Model)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        try
        {
            await connection.OpenAsync();
            var sql = @"INSERT INTO People (Name, Nickname, Birthdate, Stack) VALUES (@Name, @Nickname, @Birthdate, @Stack) RETURNING ExternalId";

            var parametersWithStack = new
            {
            };
            var externalId = await connection.ExecuteScalarAsync<Guid>(sql, parametersWithStack);
            return externalId;
        }
        finally
        {
            await connection.CloseAsync();
        }
    }
}
