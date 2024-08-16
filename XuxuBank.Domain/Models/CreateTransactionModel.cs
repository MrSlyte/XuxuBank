namespace XuxuBank.Domain.Models;

public readonly record struct CreateTransactionModel(long Value, string Type, string Description);
