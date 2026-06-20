using System;

namespace Rula.Persistence.Abstractions
{
    /// <summary>
    /// Provides serialization and deserialization services
    /// for save data.
    /// </summary>
    public interface ISaveSerializer
    {
        /// <summary>
        /// Serializes the specified value to a string.
        /// </summary>
        /// <typeparam name="T">
        /// Type of the value being serialized.
        /// </typeparam>
        /// <param name="value">
        /// Value to serialize.
        /// </param>
        /// <returns>
        /// Serialized string representation of the value.
        /// </returns>
        string Serialize<T>(
            T value);

        /// <summary>
        /// Deserializes the specified string to an instance
        /// of the requested type.
        /// </summary>
        /// <typeparam name="T">
        /// Target type.
        /// </typeparam>
        /// <param name="data">
        /// Serialized data.
        /// </param>
        /// <returns>
        /// Deserialized object.
        /// </returns>
        T Deserialize<T>(
            string data);

        /// <summary>
        /// Deserializes the specified string to an instance
        /// of the provided runtime type.
        /// </summary>
        /// <param name="data">
        /// Serialized data.
        /// </param>
        /// <param name="type">
        /// Target type.
        /// </param>
        /// <returns>
        /// Deserialized object.
        /// </returns>
        object Deserialize(
            string data,
            Type type);
    }
}