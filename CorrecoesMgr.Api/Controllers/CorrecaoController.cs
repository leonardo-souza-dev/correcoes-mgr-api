using CorrecoesMgr.Domain.Entities;
using CorrecoesMgr.Infra;
using Microsoft.AspNetCore.Mvc;

namespace CorrecoesMgr.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CorrecaoController : ControllerBase
{
    private readonly ICorrecaoRepository _correcaoRepository;

    public CorrecaoController(ICorrecaoRepository correcaoRepository)
        => _correcaoRepository = correcaoRepository;

    [HttpGet]
    public IResult ObterTodas()
        => Results.Ok(_correcaoRepository.ObterTodas());

    [HttpGet("{id}")]
    public IResult Obter(int id)
        => Results.Ok(_correcaoRepository.Obter(id));

    [HttpPost]
    public IResult Salvar(Correcao correcao)
        => Results.Ok(_correcaoRepository.Salvar(correcao));

    [HttpPut]
    public IResult Atualizar(int id, Correcao correcao)
    {
        if (id != correcao.Id)
        {
            return Results.BadRequest("Ids diferentes");
        }

        _correcaoRepository.Atualizar(correcao);
        return Results.Ok();
    }

    [HttpDelete]
    public IResult Deletar(int id)
        => _correcaoRepository.Deletar(id) ? Results.Ok() : Results.StatusCode(500);
}