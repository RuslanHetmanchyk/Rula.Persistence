using System;

namespace Rula.Persistence.Internal
{
    /// <summary>
    /// Provides non-generic access to saveable objects.
    /// </summary>
    internal interface ISaveableWrapper
    {
        /// <summary>
        /// Gets the state type.
        /// </summary>
        Type StateType { get; }

        /// <summary>
        /// Captures current state.
        /// </summary>
        object CaptureState();

        /// <summary>
        /// Restores state.
        /// </summary>
        /// <param name="state">
        /// State to restore.
        /// </param>
        void RestoreState(object state);
    }
}