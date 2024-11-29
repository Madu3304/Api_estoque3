using System.Text.Json.Serialization;
using APITEST3.Context;
using APITEST3.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.
    ReferenceHandler = ReferenceHandler.IgnoreCycles);



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//definição da obtenção da string de conexão e o registro do contexto do EntityFramework
// no container de inativo, usando a instância de eb Application e o método.
string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>  //lambda aqui, a seguir.
options.UseMySql(mySqlConnection,
    ServerVersion.AutoDetect(mySqlConnection)));



//Registrar o serviço. Para utilizar o FROMSERVICES
builder.Services.AddTransient<IMeuServico, MeuServico>();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.DisableImplicitFromServicesParameters = true;
});
//AddTransient fala que um novo objeto do meu serviço vai ser criado toda vez que for solicitaado uma instância deste serviço. 
//Traduzindo cada vez que um componente ou uma classe solicitar essa dependende o sistema de injeçao de dependência vai criar uma nova instância do serviço.



//config 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
//usado apenas para mapear. . 
app.MapControllers();

app.Run();
