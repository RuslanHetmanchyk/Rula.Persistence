using System;
using System.Collections.Generic;
using Rula.Persistence.Abstractions;
using Rula.Persistence.Internal;

namespace Rula.Persistence.Core
{
    /// <summary>
    /// Stores registered saveable objects.
    /// </summary>
    public sealed class SaveRegistry
    {
        private readonly Dictionary<string, ISaveableWrapper> _saveables =
            new Dictionary<string, ISaveableWrapper>();

        /// <summary>
        /// Gets all registered saveable objects.
        /// </summary>
        internal IReadOnlyDictionary<string, ISaveableWrapper> Saveables
            => _saveables;

        /// <summary>
        /// Registers a saveable object.
        /// </summary>
        /// <typeparam name="T">
        /// Type of state handled by the saveable.
        /// </typeparam>
        /// <param name="saveable">
        /// Saveable object to register.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when saveable is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the save key is already registered.
        /// </exception>
        public void Register<T>(
            ISaveable<T> saveable)
        {
            Guard.NotNull(
                saveable,
                nameof(saveable));

            if (_saveables.ContainsKey(saveable.SaveKey))
            {
                throw new InvalidOperationException(
                    $"Save key '{saveable.SaveKey}' is already registered.");
            }

            _saveables.Add(
                saveable.SaveKey,
                new SaveableWrapper<T>(saveable));
        }

        /// <summary>
        /// Unregisters a saveable object.
        /// </summary>
        /// <param name="saveKey">
        /// Save key to remove.
        /// </param>
        public void Unregister(
            string saveKey)
        {
            Guard.NotNullOrWhiteSpace(
                saveKey,
                nameof(saveKey));

            _saveables.Remove(saveKey);
        }

        /// <summary>
        /// Removes all registered saveables.
        /// </summary>
        public void Clear()
        {
            _saveables.Clear();
        }
    }
}