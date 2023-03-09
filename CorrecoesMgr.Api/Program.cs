using CorrecoesMgr.Infra;
using CorrecoesMgr.Services;

namespace CorrecoesMgr.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddTransient<ICorrecaoService, CorrecaoService>();
        builder.Services.AddTransient<ICorrecaoDao, CorrecaoDao>();

        //cors
        var myAllowSpecificOrigins = "_loremIpsum";
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

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();
        app.UseCors(myAllowSpecificOrigins);

        app.Run();
    }
}