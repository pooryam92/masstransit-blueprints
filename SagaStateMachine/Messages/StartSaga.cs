﻿using MassTransit;

namespace Messages;

public class StartSaga : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }
    public string? CustomProperty { get; set; }
}