using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Rula.Persistence.Core;
using Rula.Persistence.Tests.TestData;
using Rula.Persistence.Tests.TestDoubles;

namespace Rula.Persistence.Tests.Core;

/// <summary>
/// Tests for SaveManager.
/// </summary>
[TestFixture]
public sealed class SaveManagerTests
{
    [Test]
    public async Task Save_Load_Restores_State()
    {
        // Arrange

        var storage = new MemoryStorage();

        var serializer = new JsonNetSerializer();

        var clock = new TestClock
        {
            UtcNow = new DateTime(
                2025,
                1,
                1,
                0,
                0,
                0,
                DateTimeKind.Utc)
        };

        var manager = new SaveManager(
            storage,
            serializer,
            clock);

        var saveable = new TestSaveable();

        saveable.State.Gold = 100;

        manager.Register(saveable);

        // Act

        await manager.SaveAsync("slot1");

        saveable.State.Gold = 0;

        await manager.LoadAsync("slot1");

        // Assert

        Assert.That(
            saveable.State.Gold,
            Is.EqualTo(100));
    }

    [Test]
    public async Task SaveExists_Returns_True_After_Save()
    {
        // Arrange

        var storage = new MemoryStorage();

        var manager = new SaveManager(
            storage,
            new JsonNetSerializer());

        var saveable = new TestSaveable();

        manager.Register(saveable);

        // Act

        await manager.SaveAsync("slot1");

        // Assert

        Assert.That(
            manager.SaveExists("slot1"),
            Is.True);
    }

    [Test]
    public async Task DeleteSave_Removes_Save()
    {
        // Arrange

        var storage = new MemoryStorage();

        var manager = new SaveManager(
            storage,
            new JsonNetSerializer());

        var saveable = new TestSaveable();

        manager.Register(saveable);

        await manager.SaveAsync("slot1");

        // Act

        manager.DeleteSave("slot1");

        // Assert

        Assert.That(
            manager.SaveExists("slot1"),
            Is.False);
    }

    [Test]
    public void Register_DuplicateKey_Throws()
    {
        // Arrange

        var manager = new SaveManager(
            new MemoryStorage(),
            new JsonNetSerializer());

        // Act & Assert

        manager.Register(new TestSaveable());

        Assert.Throws<InvalidOperationException>(
            () => manager.Register(
                new TestSaveable()));
    }

    [Test]
    public void Load_NonExistingSlot_DoesNotThrow()
    {
        // Arrange

        var manager = new SaveManager(
            new MemoryStorage(),
            new JsonNetSerializer());

        // Act & Assert

        Assert.DoesNotThrowAsync(
            async () => await manager.LoadAsync(
                "unknown_slot"));
    }

    [Test]
    public void SaveAsync_Throws_WhenSlotIsEmpty()
    {
        var manager = new SaveManager(
            new MemoryStorage(),
            new JsonNetSerializer());

        Assert.ThrowsAsync<ArgumentException>(
            async () => await manager.SaveAsync(
                string.Empty));
    }

    [Test]
    public void LoadAsync_Throws_WhenSlotIsEmpty()
    {
        var manager = new SaveManager(
            new MemoryStorage(),
            new JsonNetSerializer());

        Assert.ThrowsAsync<ArgumentException>(
            async () => await manager.LoadAsync(
                string.Empty));
    }

    [Test]
    public void Register_Null_Throws()
    {
        var manager = new SaveManager(
            new MemoryStorage(),
            new JsonNetSerializer());

        Assert.Throws<ArgumentNullException>(
            () => manager.Register<TestPlayerData>(
                null!));
    }

    [Test]
    public void SaveExists_Throws_WhenSlotIsEmpty()
    {
        var manager = new SaveManager(
            new MemoryStorage(),
            new JsonNetSerializer());

        Assert.Throws<ArgumentException>(
            () => manager.SaveExists(
                string.Empty));
    }

    [Test]
    public void DeleteSave_Throws_WhenSlotIsEmpty()
    {
        var manager = new SaveManager(
            new MemoryStorage(),
            new JsonNetSerializer());

        Assert.Throws<ArgumentException>(
            () => manager.DeleteSave(
                string.Empty));
    }

    [Test]
    public async Task Unregister_Removes_Saveable()
    {
        var storage = new MemoryStorage();

        var manager = new SaveManager(
            storage,
            new JsonNetSerializer());

        var saveable = new TestSaveable();

        saveable.State.Gold = 100;

        manager.Register(saveable);

        manager.Unregister(saveable.SaveKey);

        await manager.SaveAsync("slot1");

        saveable.State.Gold = 0;

        await manager.LoadAsync("slot1");

        Assert.That(
            saveable.State.Gold,
            Is.EqualTo(0));
    }

    [Test]
    public void Load_NewerSaveVersion_Throws()
    {
        var saveFile = new SaveFile
        {
            Version = SaveVersion.Current + 1
        };

        var serializer = new JsonNetSerializer();

        var storage = new MemoryStorage();

        storage.SaveAsync(
                "slot1",
                serializer.Serialize(saveFile))
            .GetAwaiter()
            .GetResult();

        var manager = new SaveManager(
            storage,
            serializer);

        Assert.ThrowsAsync<UnsupportedSaveVersionException>(
            async () => await manager.LoadAsync(
                "slot1"));
    }
}