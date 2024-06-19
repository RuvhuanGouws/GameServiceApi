using GameService.Application.Mediators;
using GameService.Application.UseCases;
using GameService.Infrastructure.Persistence;
using GameService.Infrastructure.SteamApi;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserMediator, UserMediator>();
builder.Services.AddScoped<IGameMediator, GameMediator>();
builder.Services.AddScoped<ISteamApiClient, SteamApiClient>();
builder.Services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
builder.Services.AddScoped<IGetUserUseCase, GetUserUseCase>();
builder.Services.AddScoped<IGetUsersUseCase, GetUsersUseCase>();
builder.Services.AddHttpClient<ISteamApiClient, SteamApiClient>(client =>
{
    client.BaseAddress = new Uri("https://api.steampowered.com/");
});
builder.Services.AddSingleton<CosmosClient>(serviceProvider =>
{
    var connectionString = builder.Configuration["Cosmos_ConnectionString"];
    return new CosmosClient(connectionString);
});

builder.Services.AddSingleton<Container>(serviceProvider =>
{
    var client = serviceProvider.GetRequiredService<CosmosClient>();
    var databaseName = "GameService";
    var containerName = "Users";

    return client.GetContainer(databaseName, containerName);
});
//builder.Services.AddSingleton<Container>(serviceProvider =>
//{
//    var client = serviceProvider.GetRequiredService<CosmosClient>();
//    var databaseName = "GameService";
//    var containerName = "Groups";

//    return client.GetContainer(databaseName, containerName);
//});
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

app.Run();
