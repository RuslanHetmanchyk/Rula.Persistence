using Rula.Persistence.Core;
using Rula.Persistence.Migration;

namespace Rula.Persistence.Tests.Migration;

/// <summary>
/// Test migration from version 1 to version 2.
/// </summary>
public sealed class TestMigration
    : ISaveMigration
{
    /// <inheritdoc />
    public int FromVersion => 1;

    /// <inheritdoc />
    public int ToVersion => 2;

    /// <inheritdoc />
    public SaveFile Migrate(
        SaveFile saveFile)
    {
        saveFile.Version = ToVersion;

        return saveFile;
    }
}