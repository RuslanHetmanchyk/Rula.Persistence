using Rula.Persistence.Core;
using Rula.Persistence.Migration;

namespace Rula.Persistence.Tests.Migration;

/// <summary>
/// Test migration from version 1 to current version.
/// </summary>
public sealed class TestMigrationV1ToCurrent
    : ISaveMigration
{
    /// <inheritdoc />
    public int FromVersion => 1;

    /// <inheritdoc />
    public int ToVersion => SaveVersion.Current;

    /// <inheritdoc />
    public SaveFile Migrate(
        SaveFile saveFile)
    {
        saveFile.Version = ToVersion;

        return saveFile;
    }
}