using System;

namespace Rula.Persistence.Migration
{
    /// <summary>
    /// Represents migration related errors.
    /// </summary>
    public sealed class MigrationException
        : Exception
    {
        /// <summary>
        /// Initializes a new instance of the exception.
        /// </summary>
        /// <param name="message">
        /// Error message.
        /// </param>
        public MigrationException(
            string message)
            : base(message)
        {
        }
    }
}