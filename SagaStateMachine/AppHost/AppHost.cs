using Grpc.AspNetCore.Server;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var user = builder.AddParameter("user", "masstransit");
var password = builder.AddParameter("password", "masstransit", true);

var rabbitmq = builder.AddRabbitMQ("rabbitmq", user, password)
    .WithManagementPlugin(5123);

var postgres = builder.AddPostgres("postgres", user, password, 5124);

builder.AddProject<Orchestrator>("orchestrator")
    .WithReference(rabbitmq)
    .WithReference(postgres)
    .WaitFor(rabbitmq)
    .WaitFor(postgres);

builder.AddProject<ServiceA>("serviceA")
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq);

builder.AddProject<ServiceB>("serviceB")
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq);

builder.Build().Run();