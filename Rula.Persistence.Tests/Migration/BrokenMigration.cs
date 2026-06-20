using Rula.Persistence.Core;
using Rula.Persistence.Migration;

namespace Rula.Persistence.Tests.Migration;

/// <summary>
/// Migration that does not advance version.
/// </summary>
public sealed class BrokenMigration
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
        return saveFile;
    }
}