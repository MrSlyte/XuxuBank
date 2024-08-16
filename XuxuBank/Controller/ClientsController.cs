using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using XuxuBank.Domain.Interface;
using XuxuBank.Domain.Models;

namespace XuxuBank.Controller;
internal class ClientsController(IClientService clientService) : ControllerBase
{
    private const string PostRoute = "/clientes/{id:long}/transacoes";
    private const string GetRoute = "/clientes/{id:long}/extrato";

    private readonly IClientService _clientService = clientService;
    internal void Map(WebApplication app)
    {
        app.MapPost(PostRoute, Post);
    }

    private async Task<IResult> Post(CreateTransactionModel Model)
    {
        try
        {
            var resultModel = await _clientService.Post(Model);
            return Results.Ok(resultModel);
        }
        catch (ValidationException valEx)
        {
            return Results.UnprocessableEntity(valEx.Errors);
        }
        catch(ArgumentOutOfRangeException)
        {
            return Results.NotFound();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.ToString());
        }
    }
}
