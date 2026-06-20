using Rula.Persistence.Abstractions;
using Rula.Persistence.Tests.TestData;

namespace Rula.Persistence.Tests.TestDoubles;

/// <summary>
/// Test saveable implementation.
/// </summary>
public sealed class TestSaveable
    : ISaveable<TestPlayerData>
{
    public string SaveKey => "player";

    public TestPlayerData State { get; set; }
        = new TestPlayerData();

    public TestPlayerData CaptureState()
    {
        return State;
    }

    public void RestoreState(
        TestPlayerData state)
    {
        State = state;
    }
}