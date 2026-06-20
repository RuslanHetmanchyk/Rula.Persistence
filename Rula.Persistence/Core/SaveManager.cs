using System;
using System.Threading;
using System.Threading.Tasks;
using Rula.Persistence.Abstractions;
using Rula.Persistence.Internal;
using Rula.Persistence.Migration;

namespace Rula.Persistence.Core
{
    /// <summary>
    /// Coordinates save and load operations.
    /// </summary>
    public sealed class SaveManager
    {
        private readonly SaveRegistry _registry;
        private readonly ISaveStorage _storage;
        private readonly ISaveSerializer _serializer;
        private readonly IClock _clock;
        private readonly ISaveLogger _logger;
        private readonly MigrationRegistry _migrationRegistry;

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveManager"/> class.
        /// </summary>
        /// <param name="storage">
        /// Save storage implementation.
        /// </param>
        /// <param name="serializer">
        /// Serializer implementation.
        /// </param>
        /// <param name="clock">
        /// Clock implementation.
        /// </param>
        /// <param name="logger">
        /// Logger implementation.
        /// </param>
        /// <param name="migrationRegistry">
        /// Migration registry.
        /// </param>
        public SaveManager(
            ISaveStorage storage,
            ISaveSerializer serializer,
            IClock? clock = null,
            ISaveLogger? logger = null,
            MigrationRegistry? migrationRegistry = null)
        {
            Guard.NotNull(storage, nameof(storage));
            Guard.NotNull(serializer, nameof(serializer));

            _registry = new SaveRegistry();
            _storage = storage;
            _serializer = serializer;
            _clock = clock ?? new SystemClock();
            _logger = logger ?? new NullSaveLogger();
            _migrationRegistry = migrationRegistry ?? new MigrationRegistry();
        }

        /// <summary>
        /// Registers a saveable object.
        /// </summary>
        /// <typeparam name="T">
        /// State type.
        /// </typeparam>
        /// <param name="saveable">
        /// Saveable object.
        /// </param>
        public void Register<T>(
            ISaveable<T> saveable)
        {
            _registry.Register(saveable);
        }

        /// <summary>
        /// Unregisters a saveable object.
        /// </summary>
        /// <param name="saveKey">
        /// Save key.
        /// </param>
        public void Unregister(
            string saveKey)
        {
            _registry.Unregister(saveKey);
        }

        /// <summary>
        /// Saves all registered objects.
        /// </summary>
        /// <param name="slot">
        /// Save slot identifier.
        /// </param>
        /// <param name="token">
        /// Cancellation token.
        /// </param>
        public async Task SaveAsync(
            string slot,
            CancellationToken token = default)
        {
            Guard.NotNullOrWhiteSpace(
                slot,
                nameof(slot));

            var saveFile = new SaveFile
            {
                Version = SaveVersion.Current,
                BuildVersion = string.Empty,
                Timestamp = new DateTimeOffset(
                    _clock.UtcNow).ToUnixTimeSeconds()
            };

            foreach (var pair in _registry.Saveables)
            {
                var state = pair.Value.CaptureState();

                var entry = new SaveEntry
                {
                    Type = pair.Value.StateType.FullName ?? string.Empty,
                    Data = _serializer.Serialize(state)
                };

                saveFile.Objects.Add(
                    pair.Key,
                    entry);
            }

            var json = _serializer.Serialize(saveFile);

            await _storage.SaveAsync(
                slot,
                json,
                token);

            _logger.Log(
                $"Saved slot '{slot}'.");
        }

        /// <summary>
        /// Loads all registered objects.
        /// </summary>
        /// <param name="slot">
        /// Save slot identifier.
        /// </param>
        /// <param name="token">
        /// Cancellation token.
        /// </param>
        public async Task LoadAsync(
            string slot,
            CancellationToken token = default)
        {
            Guard.NotNullOrWhiteSpace(
                slot,
                nameof(slot));

            if (!_storage.Exists(slot))
            {
                _logger.LogWarning(
                    $"Slot '{slot}' does not exist.");

                return;
            }

            var json = await _storage.LoadAsync(
                slot,
                token);

            var saveFile =
                _serializer.Deserialize<SaveFile>(json);

            if (saveFile == null)
            {
                throw new InvalidOperationException(
                    "Failed to deserialize save file.");
            }

            if (saveFile.Version > SaveVersion.Current)
            {
                throw new UnsupportedSaveVersionException(
                    saveFile.Version,
                    SaveVersion.Current);
            }

            saveFile = _migrationRegistry.ApplyMigrations(
                saveFile,
                SaveVersion.Current);

            foreach (var pair in _registry.Saveables)
            {
                if (!saveFile.Objects.TryGetValue(
                        pair.Key,
                        out var entry))
                {
                    continue;
                }

                var state =
                    _serializer.Deserialize(
                        entry.Data,
                        pair.Value.StateType);

                pair.Value.RestoreState(state);
            }

            _logger.Log(
                $"Loaded slot '{slot}'.");
        }

        /// <summary>
        /// Determines whether a save slot exists.
        /// </summary>
        /// <param name="slot">
        /// Save slot identifier.
        /// </param>
        /// <returns>
        /// True if the slot exists; otherwise false.
        /// </returns>
        public bool SaveExists(
            string slot)
        {
            Guard.NotNullOrWhiteSpace(
                slot,
                nameof(slot));

            return _storage.Exists(slot);
        }

        /// <summary>
        /// Deletes a save slot.
        /// </summary>
        /// <param name="slot">
        /// Save slot identifier.
        /// </param>
        public void DeleteSave(
            string slot)
        {
            Guard.NotNullOrWhiteSpace(
                slot,
                nameof(slot));

            _storage.Delete(slot);

            _logger.Log(
                $"Deleted slot '{slot}'.");
        }
    }
}