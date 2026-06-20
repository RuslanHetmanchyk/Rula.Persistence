using System;
using System.Collections.Generic;
using Rula.Persistence.Core;
using Rula.Persistence.Internal;

namespace Rula.Persistence.Migration
{
    /// <summary>
    /// Stores registered save migrations.
    /// </summary>
    public sealed class MigrationRegistry
    {
        private readonly Dictionary<int, ISaveMigration> _migrations =
            new Dictionary<int, ISaveMigration>();

        /// <summary>
        /// Registers a migration.
        /// </summary>
        /// <param name="migration">
        /// Migration to register.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when migration is null.
        /// </exception>
        /// <exception cref="MigrationException">
        /// Thrown when migration for the source version
        /// is already registered.
        /// </exception>
        public void Register(
            ISaveMigration migration)
        {
            Guard.NotNull(
                migration,
                nameof(migration));

            if (_migrations.ContainsKey(
                    migration.FromVersion))
            {
                throw new MigrationException(
                    $"Migration from version '{migration.FromVersion}' is already registered.");
            }

            _migrations.Add(
                migration.FromVersion,
                migration);
        }

        /// <summary>
        /// Attempts to get migration by source version.
        /// </summary>
        /// <param name="version">
        /// Source version.
        /// </param>
        /// <param name="migration">
        /// Found migration instance.
        /// </param>
        /// <returns>
        /// True if migration exists; otherwise false.
        /// </returns>
        public bool TryGetMigration(
            int version,
            out ISaveMigration migration)
        {
            return _migrations.TryGetValue(
                version,
                out migration!);
        }

        /// <summary>
        /// Applies all required migrations to reach
        /// the specified target version.
        /// </summary>
        /// <param name="saveFile">
        /// Save file to migrate.
        /// </param>
        /// <param name="targetVersion">
        /// Desired target version.
        /// </param>
        /// <returns>
        /// Migrated save file.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when save file is null.
        /// </exception>
        /// <exception cref="MigrationException">
        /// Thrown when a required migration
        /// cannot be found.
        /// </exception>
        public SaveFile ApplyMigrations(
            SaveFile saveFile,
            int targetVersion)
        {
            Guard.NotNull(
                saveFile,
                nameof(saveFile));

            while (saveFile.Version < targetVersion)
            {
                if (!TryGetMigration(
                        saveFile.Version,
                        out var migration))
                {
                    throw new MigrationException(
                        $"No migration found from version '{saveFile.Version}' to version '{targetVersion}'.");
                }

                saveFile = migration.Migrate(
                    saveFile);

                if (saveFile.Version < migration.ToVersion)
                {
                    throw new MigrationException(
                        $"Migration from version '{migration.FromVersion}' to version '{migration.ToVersion}' did not advance save version.");
                }
            }

            return saveFile;
        }
    }
}