using System.Threading.Tasks;
using NUnit.Framework;
using Rula.Persistence.Core;
using Rula.Persistence.Migration;
using Rula.Persistence.Tests.TestDoubles;

namespace Rula.Persistence.Tests.Migration;

/// <summary>
/// Tests migration integration with SaveManager.
/// </summary>
[TestFixture]
public sealed class MigrationPipelineTests
{
    // [Test]
    // public async Task Migration_Is_Applied_During_Load()
    // {
    //     // Arrange
    //
    //     var storage = new MemoryStorage();
    //
    //     var serializer = new JsonNetSerializer();
    //
    //     var migration = new TestMigrationV1ToV2();
    //
    //     var migrationRegistry =
    //         new MigrationRegistry();
    //
    //     migrationRegistry.Register(
    //         migration);
    //
    //     var saveFile = new SaveFile
    //     {
    //         Version = 1
    //     };
    //
    //     await storage.SaveAsync(
    //         "slot1",
    //         serializer.Serialize(saveFile));
    //
    //     var manager = new SaveManager(
    //         storage,
    //         serializer,
    //         migrationRegistry: migrationRegistry);
    //
    //     // Act
    //
    //     await manager.LoadAsync(
    //         "slot1");
    //
    //     // Assert
    //
    //     Assert.That(
    //         migration.WasCalled,
    //         Is.True);
    // }
}