using GameService.Application.Boundaries;
using GameService.Application.Commands;
using GameService.Application.Handlers;
using GameService.Application.Mediator;
using GameService.Application.Queries;
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
builder.Services.AddScoped<ISteamApiClient, SteamApiClient>();
builder.Services.AddScoped<IMediator, Mediator>();

// Register Handlers
builder.Services.AddTransient<IRequestHandler<GetUserBySteamIdQuery, GetUserOutput>, GetUserBySteamIdHandler>();
builder.Services.AddTransient<IRequestHandler<GetUserByIdQuery, GetUserOutput>, GetUserByIdHandler>();
builder.Services.AddTransient<IRequestHandler<GetUsersQuery, GetUsersOutput>, GetUsersListHandler>();
builder.Services.AddTransient<IRequestHandler<CreateUserCommand, CreateUserOutput>, CreateUserHandler>();
builder.Services.AddTransient<IRequestHandler<GetOwnedGamesQuery, GetOwnedGamesOutput>, GetOwnedGamesHandler>();
builder.Services.AddTransient<IRequestHandler<GetGameDetailsQuery, GetGameDetailsOutput>, GetGameDetailsHandler>();

// Steam Client
builder.Services.AddHttpClient<ISteamApiClient, SteamApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["SteamBaseUri"]!);
});

// Cosmos Setup
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
