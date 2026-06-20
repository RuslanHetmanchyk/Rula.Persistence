using System;
using Rula.Persistence.Abstractions;

namespace Rula.Persistence.Internal
{
    /// <summary>
    /// Uses the system clock as a source of current UTC time.
    /// </summary>
    internal sealed class SystemClock : IClock
    {
        /// <inheritdoc />
        public DateTime UtcNow => DateTime.UtcNow;
    }
}