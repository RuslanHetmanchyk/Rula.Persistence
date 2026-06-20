namespace Rula.Persistence.Abstractions
{
    /// <summary>
    /// Represents an object whose state can be saved and restored.
    /// </summary>
    /// <typeparam name="T">
    /// Type of state captured by this saveable object.
    /// </typeparam>
    public interface ISaveable<T>
    {
        /// <summary>
        /// Unique identifier used to store and restore the object's state.
        /// </summary>
        string SaveKey { get; }

        /// <summary>
        /// Captures the current state of the object.
        /// </summary>
        /// <returns>
        /// State object that will be serialized and written to storage.
        /// </returns>
        T CaptureState();

        /// <summary>
        /// Restores the object from previously captured state.
        /// </summary>
        /// <param name="state">
        /// State to restore.
        /// </param>
        void RestoreState(T state);
    }
}