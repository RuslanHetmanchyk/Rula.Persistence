using System;

namespace Rula.Persistence.Core
{
    /// <summary>
    /// Thrown when a save file version is newer than the library supports.
    /// </summary>
    public sealed class UnsupportedSaveVersionException
        : Exception
    {
        /// <summary>
        /// Initializes a new instance of the exception.
        /// </summary>
        /// <param name="saveVersion">
        /// Version found in save file.
        /// </param>
        /// <param name="supportedVersion">
        /// Maximum supported version.
        /// </param>
        public UnsupportedSaveVersionException(
            int saveVersion,
            int supportedVersion)
            : base(
                $"Save version '{saveVersion}' is not supported. " +
                $"Current supported version is '{supportedVersion}'.")
        {
            SaveVersion = saveVersion;
            SupportedVersion = supportedVersion;
        }

        /// <summary>
        /// Gets the version found in the save file.
        /// </summary>
        public int SaveVersion { get; }

        /// <summary>
        /// Gets the maximum supported version.
        /// </summary>
        public int SupportedVersion { get; }
    }
}