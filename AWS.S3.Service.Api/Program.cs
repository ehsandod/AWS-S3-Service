using AWS.S3.Service.Api.Configs;
using AWS.S3.Service.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IAppConfiguration, AppConfiguration>()
        .AddSwaggerGen(c =>
        {
           c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
        })
        .AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

//app.MapControllers();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DotNet_AWS");
});

app.Run();
