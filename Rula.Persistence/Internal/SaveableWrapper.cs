using System;
using Rula.Persistence.Abstractions;

namespace Rula.Persistence.Internal
{
    /// <summary>
    /// Adapts generic saveables to a non-generic interface.
    /// </summary>
    /// <typeparam name="T">
    /// Save state type.
    /// </typeparam>
    internal sealed class SaveableWrapper<T>
        : ISaveableWrapper
    {
        private readonly ISaveable<T> _saveable;

        /// <summary>
        /// Initializes a new wrapper.
        /// </summary>
        /// <param name="saveable">
        /// Wrapped saveable object.
        /// </param>
        public SaveableWrapper(
            ISaveable<T> saveable)
        {
            _saveable = saveable;
        }

        /// <inheritdoc />
        public Type StateType => typeof(T);

        /// <inheritdoc />
        public object CaptureState()
        {
            return _saveable.CaptureState()!;
        }

        /// <inheritdoc />
        public void RestoreState(
            object state)
        {
            _saveable.RestoreState((T)state);
        }
    }
}