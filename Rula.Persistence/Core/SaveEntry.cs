namespace Rula.Persistence.Core
{
    /// <summary>
    /// Represents a single serialized saveable object inside a save file.
    /// </summary>
    public sealed class SaveEntry
    {
        /// <summary>
        /// Gets or sets the type name of the saved state.
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the serialized state payload.
        /// </summary>
        public string Data { get; set; } = string.Empty;
    }
}