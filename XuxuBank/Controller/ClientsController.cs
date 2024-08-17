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
        app.MapGet(GetRoute, Get);
    }

    private async Task<IResult> Post([FromBody] CreateTransactionModel Model, [FromRoute] long Id)
    {
        try
        {
            var resultModel = await _clientService.Post(Model, Id);
            return Results.Ok(resultModel);
        }
        catch (ValidationException valEx)
        {
            return Results.UnprocessableEntity(valEx.Errors.Any() ? valEx.Errors.Select(x => x.ErrorMessage) : valEx.Message);
        }
        catch(ArgumentOutOfRangeException notFoundEx)
        {
            return Results.NotFound(notFoundEx.Message);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.ToString());
        }
    }

    private async Task<IResult> Get(int Id)
    {
        return Results.Ok($"Oi: {Id}");
    }
}
