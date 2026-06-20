using Rula.Persistence.Core;

namespace Rula.Persistence.Migration
{
    /// <summary>
    /// Represents a migration between two save file versions.
    /// </summary>
    public interface ISaveMigration
    {
        /// <summary>
        /// Gets source version.
        /// </summary>
        int FromVersion { get; }

        /// <summary>
        /// Gets target version.
        /// </summary>
        int ToVersion { get; }

        /// <summary>
        /// Migrates save file to the target version.
        /// </summary>
        /// <param name="saveFile">
        /// Save file to migrate.
        /// </param>
        /// <returns>
        /// Migrated save file.
        /// </returns>
        SaveFile Migrate(
            SaveFile saveFile);
    }
}