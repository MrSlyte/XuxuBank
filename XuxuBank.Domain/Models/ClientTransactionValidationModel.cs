namespace XuxuBank.Domain.Models;

public readonly record struct ClientTransactionValidationModel(long ClientId, long Value, string Type, string Description);

