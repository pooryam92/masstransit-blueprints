using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var rabbitUser = builder.AddParameter("rabbitUser", "guest");
var rabbitPass = builder.AddParameter("rabbitPassword", "guest", true);

var rabbitmq = builder.AddRabbitMQ("rabbitmq", rabbitUser, rabbitPass)
    .WithManagementPlugin(5123)
    .WithLifetime(ContainerLifetime.Persistent);

var postgres = builder.AddPostgres("postgres")
    .WithLifetime(ContainerLifetime.Persistent);


builder.AddProject<Orchestrator>("orchestrator")
    .WithReference(rabbitmq)
    .WithReference(postgres)
    .WaitFor(rabbitmq)
    .WaitFor(postgres);

builder.Build().Run();