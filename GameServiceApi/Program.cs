using GameService.Application.Boundaries;
using GameService.Application.Commands;
using GameService.Application.Handlers;
using GameService.Application.Mediator;
using GameService.Application.Queries;
using GameService.Application.Services;
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
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGamesService, GamesService>();
builder.Services.AddScoped<IMediator, Mediator>();

// Register Handlers
builder.Services.AddScoped<IRequestHandler<GetUserQuery, GetUserOutput>, UserQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetUsersQuery, GetUsersOutput>, UsersQueryHandler>();
builder.Services.AddScoped<IRequestHandler<CreateUserCommand, CreateUserOutput>, CreateUserCommandHandler>();

builder.Services.AddHttpClient<ISteamApiClient, SteamApiClient>(client =>
{
    client.BaseAddress = new Uri("https://api.steampowered.com/");
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
