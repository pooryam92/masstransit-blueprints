using System.Reflection;
using MassTransit;
using Messages;
using Microsoft.EntityFrameworkCore;
using Orchestrator.Saga;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<OrchestratorDbContext>(dbBuilder =>
    dbBuilder.UseNpgsql(builder.Configuration.GetConnectionString("postgres")));

builder.Services.AddMassTransit(x =>
{
    x.AddSagaStateMachine<SagaStateMachine, SagaState>()
        .EntityFrameworkRepository(r =>
        {
            r.ConcurrencyMode = ConcurrencyMode.Pessimistic;
            r.AddDbContext<DbContext, OrchestratorDbContext>();
            r.UsePostgres();
        });

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration.GetConnectionString("rabbitmq"));
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/start-saga", async (IPublishEndpoint publishEndpoint) =>
{
    var message = new StartSaga() { CorrelationId = Guid.NewGuid() };
    await publishEndpoint.Publish(message);
    return Results.Ok("Message published");
});

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OrchestratorDbContext>();
    await db.Database.MigrateAsync();
}


app.Run();