using System.Diagnostics;
using System.Reflection;
using CorrecoesMgr.Dto;
using CorrecoesMgr.Repository;
using CorrecoesMgr.Domain;

try
{
    // builder
    var MyAllowSpecificOrigins = "_loremIpsum";
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy
                                .WithOrigins("http://localhost:3000")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();
                          });
    });
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();



    // app
    var app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseHttpsRedirection();
    app.UseCors(MyAllowSpecificOrigins);



    // variaveis
    var caminhoArquivoDb = ObterCaminhoArquivoDb(nameof(Correcao));


    // endpoints
    app.MapGet("/correcoes", () =>
    {
        return Results.Ok(new CorrecaoRepository(caminhoArquivoDb).GetAll());
    });

    app.MapPut("/inserir-correcao", (CorrecaoDto correcaoDto) =>
    {
        var correcao = correcaoDto.ToModel();

        var correcaoInserida = new CorrecaoRepository(caminhoArquivoDb).Insert(correcao);

        return Results.Ok(correcaoInserida);
    });

    app.Run();


    static string ObterCaminhoArquivoDb(string entidade)
    {
        string nomeAssembly = Assembly.GetEntryAssembly().GetName().Name;
        var nomeDb = $"{nomeAssembly}-{entidade.ToLower()}.db";

        return $"{Directory.GetCurrentDirectory()}/{nomeDb}";
    }

    static string ObterCaminhoDbDev()
    {
        var pastaRaiz = "";
        var pastaAtual = Directory.GetCurrentDirectory();
        var ehPastaRaiz = false;

        List<string> arquivos;
        while (!ehPastaRaiz)
        {
            arquivos = Directory.GetFiles(pastaAtual).ToList();
            foreach (var arquivo in arquivos)
            {
                if (!arquivo.ToLowerInvariant().EndsWith(".sln"))
                    continue;

                ehPastaRaiz = true;
                pastaRaiz = Directory.GetParent(arquivo)?.FullName;
            }

            pastaAtual = Directory.GetParent(pastaAtual)?.FullName;
        }

        return pastaRaiz;
    }

}
catch (Exception ex)
{
    Console.WriteLine(ex);
    Debug.WriteLine(ex);
    throw;
}