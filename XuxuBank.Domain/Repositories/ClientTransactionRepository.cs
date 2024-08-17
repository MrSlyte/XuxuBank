using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using XuxuBank.Domain.Config;
using XuxuBank.Domain.Interface;
using XuxuBank.Domain.Models;

namespace XuxuBank.Domain.Repositories;

public record ClientTransactionRepository(IOptions<SystemConfiguration> Config) : BaseRepository(Config), IClientTransactionRepository
{
    public async Task<bool> Insert(CreateTransactionModel Model, long Id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        try
        {
            await connection.OpenAsync();
            var sql = @"INSERT INTO ClientTransactions (ClientId, Type, Value, Description, Date) VALUES (@ClientId, @Type, @Value, @Description, @Date)";

            var parametersWithStack = new
            {
                ClientId = Id,
                Type = Model.Tipo.ToUpper(),
                Value = Model.Valor,
                Description = Model.Descricao,
                Date = DateTime.Now
            };
            var rowsAffected = await connection.ExecuteAsync(sql, parametersWithStack);
            return rowsAffected > 0;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            await connection.CloseAsync();
        }
    }

    public async Task<SimpleExtractModel> SimpleExtractAsync(long Id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        try
        {
            await connection.OpenAsync();
            var sql = @"SELECT COALESCE(SUM(Value),0) FROM ClientTransactions WHERE ClientId = @ClientId AND Type = @Type";
            var parametersLimit = new { ClientId = Id, Type = "C" };
            var limit = await connection.QuerySingleOrDefaultAsync<long>(sql, parametersLimit);

            var parametersBalance = new { ClientId = Id, Type = "D" };
            var balance = await connection.QuerySingleOrDefaultAsync<long>(sql, parametersBalance);

            return new SimpleExtractModel(limit, balance);
        }
        finally
        {
            await connection.CloseAsync();
        }
    }
}
