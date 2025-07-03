using MassTransit;
using ServiceB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddMassTransit(x =>
{
    x.SetEndpointNameFormatter(KebabCaseEndpointNameFormatter.Instance);

    x.AddConsumer<ActivityTwoConsumer>();
    x.AddConsumer<CompensateActivityTwoConsumer>();

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


app.Run();