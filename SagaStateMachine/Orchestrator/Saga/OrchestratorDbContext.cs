using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;

namespace Orchestrator.Saga;

public class OrchestratorDbContext : SagaDbContext
{
    public OrchestratorDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override IEnumerable<ISagaClassMap> Configurations
    {
        get { yield return new SagaStateMap(); }
    }
}