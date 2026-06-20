using Rula.Persistence.Abstractions;

namespace Rula.Persistence.Internal
{
    /// <summary>
    /// Logger implementation that ignores all messages.
    /// </summary>
    internal sealed class NullSaveLogger : ISaveLogger
    {
        public void Log(string message)
        {
        }

        public void LogWarning(string message)
        {
        }

        public void LogError(string message)
        {
        }
    }
}