using System;

namespace Rula.Persistence.Abstractions
{
    /// <summary>
    /// Provides access to the current time.
    /// </summary>
    public interface IClock
    {
        /// <summary>
        /// Gets the current date and time expressed as
        /// Coordinated Universal Time (UTC).
        /// </summary>
        DateTime UtcNow { get; }
    }
}