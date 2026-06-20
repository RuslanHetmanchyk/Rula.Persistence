using Rula.Persistence.Core;
using Rula.Persistence.Migration;

namespace Rula.Persistence.Tests.Migration;

/// <summary>
/// Test migration from version 1 to version 2.
/// </summary>
public sealed class TestMigrationV1ToV2
    : ISaveMigration
{
    /// <summary>
    /// Gets a value indicating whether migration was executed.
    /// </summary>
    public bool WasCalled { get; private set; }

    /// <inheritdoc />
    public int FromVersion => 1;

    /// <inheritdoc />
    public int ToVersion => 2;

    /// <inheritdoc />
    public SaveFile Migrate(
        SaveFile saveFile)
    {
        WasCalled = true;

        saveFile.Version = 2;

        return saveFile;
    }
}