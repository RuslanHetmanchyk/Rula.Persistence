using System.Collections.Generic;

namespace Rula.Persistence.Core
{
    /// <summary>
    /// Represents a save file stored in persistent storage.
    /// </summary>
    public sealed class SaveFile
    {
        /// <summary>
        /// Current save file format version.
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Application version that created the save.
        /// </summary>
        public string BuildVersion { get; set; } = string.Empty;

        /// <summary>
        /// UTC timestamp when the save was created.
        /// </summary>
        public long Timestamp { get; set; }

        /// <summary>
        /// Collection of saved objects indexed by save key.
        /// </summary>
        public Dictionary<string, SaveEntry> Objects { get; set; }
            = new Dictionary<string, SaveEntry>();
    }
}