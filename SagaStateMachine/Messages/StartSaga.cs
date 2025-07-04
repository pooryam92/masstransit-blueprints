﻿using MassTransit;

namespace Messages;

[EntityName("start-saga")]
public class StartSaga : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }
    public string? CustomProperty { get; set; }
}