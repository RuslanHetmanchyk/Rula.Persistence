![Build](https://github.com/RuslanHetmanchyk/Rula.Persistence/actions/workflows/build.yml/badge.svg)

# Rula.Persistence

Lightweight, extensible and platform-agnostic save system for .NET applications and games.

`Rula.Persistence` provides the core abstractions and save management pipeline while allowing applications to choose their own storage, serialization and logging implementations.

---

## Features

- Platform independent
- Async save/load operations
- Multiple save slots
- Strongly typed saveables
- Save versioning support
- Save file migrations
- Pluggable serializers
- Pluggable storage providers
- Pluggable logging
- Testable architecture
- Unity friendly through separate integration package

---

## Installation

Add a reference to the package:

```bash
dotnet add package Rula.Persistence
```

---

## Quick Start

### Create a saveable object

```csharp
public sealed class PlayerSaveable
    : ISaveable<PlayerData>
{
    private readonly Player _player;

    public PlayerSaveable(
        Player player)
    {
        _player = player;
    }

    public string SaveKey => "player";

    public PlayerData CaptureState()
    {
        return new PlayerData
        {
            Gold = _player.Gold
        };
    }

    public void RestoreState(
        PlayerData state)
    {
        _player.Gold = state.Gold;
    }
}
```

### Create manager

```csharp
var saveManager = new SaveManager(
    storage,
    serializer);
```

### Register saveables

```csharp
saveManager.Register(
    new PlayerSaveable(player));
```

### Save

```csharp
await saveManager.SaveAsync(
    "slot1");
```

### Load

```csharp
await saveManager.LoadAsync(
    "slot1");
```

---

## Architecture

The library is built around a few core abstractions:

```text
ISaveable<T>
ISaveStorage
ISaveSerializer
ISaveLogger
IClock
```

Applications provide implementations for these interfaces.

---

## Custom Storage

Implement:

```csharp
ISaveStorage
```

Example use cases:

- File system
- Cloud storage
- Database
- PlayerPrefs
- Encrypted storage

---

## Custom Serialization

Implement:

```csharp
ISaveSerializer
```

Supported options include:

- Newtonsoft.Json
- System.Text.Json
- Binary serializers
- Custom formats

---

## Logging

Implement:

```csharp
ISaveLogger
```

or use the built-in:

```csharp
NullSaveLogger
```

---

## Save Versioning

Each save file contains a version number.

```csharp
public static class SaveVersion
{
    public const int Current = 1;
}
```

The version is validated during loading.

---

## Migrations

Older save files can be upgraded automatically.

```csharp
public sealed class V1ToV2Migration
    : ISaveMigration
{
    public int FromVersion => 1;

    public int ToVersion => 2;

    public SaveFile Migrate(
        SaveFile saveFile)
    {
        saveFile.Version = 2;

        return saveFile;
    }
}
```

Register migrations:

```csharp
var registry =
    new MigrationRegistry();

registry.Register(
    new V1ToV2Migration());
```

Pass registry to SaveManager:

```csharp
var manager = new SaveManager(
    storage,
    serializer,
    migrationRegistry: registry);
```

---

## Unity Support

Unity integration is provided through a separate package:

```text
Rula.Persistence.Unity
```

This package contains Unity-specific implementations while keeping the core library free of engine dependencies.

---

## Roadmap

- Unity integration package
- Encryption package
- System.Text.Json package
- Auto-save support
- Save compression

---

## License

MIT
