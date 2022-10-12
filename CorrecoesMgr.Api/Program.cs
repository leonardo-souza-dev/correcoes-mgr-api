using System.Diagnostics;
using System.Reflection;
using CorrecoesMgr.Repository;
using CorrecoesMgr.Domain;

try
{
    // builder
    var myAllowSpecificOrigins = "_loremIpsum";
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: myAllowSpecificOrigins,
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
    app.UseCors(myAllowSpecificOrigins);

    // variaveis
    var caminhoArquivoDb = ObterCaminhoArquivoDb(nameof(Correcao));

    // controller
    _ = new CorrecaoController(
        app,
        new CorrecaoRepository(caminhoArquivoDb),
        new ModuloValorRepository(caminhoArquivoDb));


    app.Run();


    static string ObterCaminhoArquivoDb(string entidade)
    {
        string? nomeAssembly = Assembly.GetEntryAssembly()?.GetName().Name;
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