namespace XuxuBank.Domain.Models;

public readonly record struct CreateTransactionModel(long Valor, string Tipo, string Descricao);
