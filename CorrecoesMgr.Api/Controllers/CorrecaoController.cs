using CorrecoesMgr.Domain;
using CorrecoesMgr.Services;
using Microsoft.AspNetCore.Mvc;

namespace CorrecoesMgr.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CorrecaoController : ControllerBase
{
    private readonly ICorrecaoService _correcaoService;

    public CorrecaoController(ICorrecaoService correcaoService)
        => _correcaoService = correcaoService;

    [HttpGet]
    public IResult ObterTodas()
        => Results.Ok(_correcaoService.ObterTodas());

    [HttpGet("{id}")]
    public IResult Obter(int id)
        => Results.Ok(_correcaoService.Obter(id));

    [HttpPost]
    public IResult Salvar(Correcao correcao)
        => Results.Ok(_correcaoService.Salvar(correcao));

    [HttpPut]
    public IResult Atualizar(int id, Correcao correcao)
    {
        if (id != correcao.Id)
        {
            return Results.BadRequest("Ids diferentes");
        }

        _correcaoService.Atualizar(correcao);
        return Results.Ok();
    }

    [HttpDelete]
    public IResult Deletar(int id)
        => _correcaoService.Deletar(id) ? Results.Ok() : Results.StatusCode(500);
}