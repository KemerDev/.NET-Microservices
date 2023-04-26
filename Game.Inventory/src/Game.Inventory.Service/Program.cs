using Game.Common.MongoDB;
using Game.Inventory.Service.Clients;
using Game.Inventory.Service.Entities;
using Polly;

Random jitter = new Random();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMongo().AddMongoRepository<InventoryCs>("inventory");
// register http client timeout policy via Polly
builder.Services.AddHttpClient<ItemClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5142");
})
.AddTransientHttpErrorPolicy(Error => Error.Or<TimeoutException>().WaitAndRetryAsync(
    4,
    retry => TimeSpan.FromSeconds(Math.Pow(2, retry)) + TimeSpan.FromMilliseconds(jitter.Next(0, 1000)),
    onRetry: (outcome, timespan, retry) =>
    {
        var serviceProvider = builder.Services.BuildServiceProvider();
        serviceProvider.GetService<ILogger<ItemClient>>()?.LogWarning($"{timespan.TotalSeconds} seconds delay, retry {retry}");
    }
))
.AddTransientHttpErrorPolicy(Error => Error.Or<TimeoutException>().CircuitBreakerAsync(
    3,
    TimeSpan.FromSeconds(15),

    onBreak: (outcome, timespan) =>
    {
        var serviceProvider = builder.Services.BuildServiceProvider();
        serviceProvider.GetService<ILogger<ItemClient>>()?.LogWarning($"Circuit open for {timespan.TotalSeconds} seconds");
    },
    onReset: () =>
    {
        var serviceProvider = builder.Services.BuildServiceProvider();
        serviceProvider.GetService<ILogger<ItemClient>>()?.LogWarning($"Circuit Closing");
    }
))
.AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(1));

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
