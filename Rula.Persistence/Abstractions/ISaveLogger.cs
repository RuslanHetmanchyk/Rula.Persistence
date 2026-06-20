namespace Rula.Persistence.Abstractions
{
    /// <summary>
    /// Provides logging for persistence operations.
    /// </summary>
    public interface ISaveLogger
    {
        /// <summary>
        /// Writes an informational message.
        /// </summary>
        void Log(string message);

        /// <summary>
        /// Writes a warning message.
        /// </summary>
        void LogWarning(string message);

        /// <summary>
        /// Writes an error message.
        /// </summary>
        void LogError(string message);
    }
}