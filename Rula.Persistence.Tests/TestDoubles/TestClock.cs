using System;
using Rula.Persistence.Abstractions;

namespace Rula.Persistence.Tests.TestDoubles;

/// <summary>
/// Fixed clock used for tests.
/// </summary>
public sealed class TestClock : IClock
{
    public DateTime UtcNow { get; set; }
}