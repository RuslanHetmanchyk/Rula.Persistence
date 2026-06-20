using System;
using Newtonsoft.Json;
using Rula.Persistence.Abstractions;

namespace Rula.Persistence.Tests.TestDoubles;

/// <summary>
/// Newtonsoft.Json serializer used in tests.
/// </summary>
public sealed class JsonNetSerializer
    : ISaveSerializer
{
    public string Serialize<T>(
        T value)
    {
        return JsonConvert.SerializeObject(value);
    }

    public T Deserialize<T>(
        string data)
    {
        return JsonConvert.DeserializeObject<T>(data)!;
    }

    public object Deserialize(
        string data,
        Type type)
    {
        return JsonConvert.DeserializeObject(
            data,
            type)!;
    }
}