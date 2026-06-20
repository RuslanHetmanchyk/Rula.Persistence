using System.Threading;
using System.Threading.Tasks;

namespace Rula.Persistence.Abstractions
{
    /// <summary>
    /// Provides save file storage operations.
    /// </summary>
    public interface ISaveStorage
    {
        /// <summary>
        /// Saves serialized data to the specified slot.
        /// </summary>
        /// <param name="slot">
        /// Save slot identifier.
        /// </param>
        /// <param name="data">
        /// Serialized save data.
        /// </param>
        /// <param name="token">
        /// Cancellation token.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation.
        /// </returns>
        Task SaveAsync(
            string slot,
            string data,
            CancellationToken token = default);

        /// <summary>
        /// Loads serialized data from the specified slot.
        /// </summary>
        /// <param name="slot">
        /// Save slot identifier.
        /// </param>
        /// <param name="token">
        /// Cancellation token.
        /// </param>
        /// <returns>
        /// A task containing serialized save data.
        /// </returns>
        Task<string> LoadAsync(
            string slot,
            CancellationToken token = default);

        /// <summary>
        /// Determines whether the specified slot exists.
        /// </summary>
        /// <param name="slot">
        /// Save slot identifier.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the slot exists;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        bool Exists(
            string slot);

        /// <summary>
        /// Deletes the specified save slot.
        /// </summary>
        /// <param name="slot">
        /// Save slot identifier.
        /// </param>
        void Delete(
            string slot);
    }
}