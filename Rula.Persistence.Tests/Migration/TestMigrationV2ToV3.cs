using Rula.Persistence.Core;
using Rula.Persistence.Migration;

namespace Rula.Persistence.Tests.Migration;

/// <summary>
/// Test migration from version 2 to version 3.
/// </summary>
public sealed class TestMigrationV2ToV3
    : ISaveMigration
{
    /// <inheritdoc />
    public int FromVersion => 2;

    /// <inheritdoc />
    public int ToVersion => 3;

    /// <inheritdoc />
    public SaveFile Migrate(
        SaveFile saveFile)
    {
        saveFile.Version = ToVersion;

        return saveFile;
    }
}