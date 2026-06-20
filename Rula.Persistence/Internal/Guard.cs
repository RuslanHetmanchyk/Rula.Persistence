using System;

namespace Rula.Persistence.Internal
{
    /// <summary>
    /// Provides helper methods for validating method arguments.
    /// </summary>
    internal static class Guard
    {
        /// <summary>
        /// Throws an exception if the specified value is null.
        /// </summary>
        /// <param name="value">
        /// Value to validate.
        /// </param>
        /// <param name="paramName">
        /// Name of the parameter being validated.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="value"/> is null.
        /// </exception>
        public static void NotNull(
            object value,
            string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// Throws an exception if the specified string is null,
        /// empty, or consists only of whitespace characters.
        /// </summary>
        /// <param name="value">
        /// String to validate.
        /// </param>
        /// <param name="paramName">
        /// Name of the parameter being validated.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when the value is null, empty,
        /// or contains only whitespace characters.
        /// </exception>
        public static void NotNullOrWhiteSpace(
            string value,
            string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    "Value cannot be null or whitespace.",
                    paramName);
            }
        }
    }
}