using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rula.Persistence.Abstractions;

namespace Rula.Persistence.Tests.TestDoubles;

/// <summary>
/// In-memory storage used for tests.
/// </summary>
public sealed class MemoryStorage : ISaveStorage
{
    private readonly Dictionary<string, string> _storage =
        new Dictionary<string, string>();

    public Task SaveAsync(
        string slot,
        string data,
        CancellationToken token = default)
    {
        _storage[slot] = data;

        return Task.CompletedTask;
    }

    public Task<string> LoadAsync(
        string slot,
        CancellationToken token = default)
    {
        return Task.FromResult(_storage[slot]);
    }

    public bool Exists(
        string slot)
    {
        return _storage.ContainsKey(slot);
    }

    public void Delete(
        string slot)
    {
        _storage.Remove(slot);
    }
}