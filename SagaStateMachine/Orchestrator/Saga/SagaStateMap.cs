using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Orchestrator.Saga;

public class SagaStateMap : SagaClassMap<SagaState>
{
    protected override void Configure(EntityTypeBuilder<SagaState> entity, ModelBuilder model)
    {
        entity.Property(x => x.CurrentState).HasMaxLength(64);
        entity.Property(x => x.CustomProperty);

        // If using Optimistic concurrency, otherwise remove this property
        // entity.Property(x => x.RowVersion).IsRowVersion();
    }
}