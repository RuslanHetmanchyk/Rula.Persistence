using NUnit.Framework;
using Rula.Persistence.Core;
using Rula.Persistence.Migration;

namespace Rula.Persistence.Tests.Migration;

public class MigrationRegistryTests
{
    [Test]
    public void Register_Migration_Succeeds()
    {
        var registry = new MigrationRegistry();

        registry.Register(
            new TestMigration());

        var result = registry.TryGetMigration(
            1,
            out var migration);

        Assert.That(result, Is.True);

        Assert.That(migration, Is.Not.Null);
    }

    [Test]
    public void Register_DuplicateMigration_Throws()
    {
        var registry = new MigrationRegistry();

        registry.Register(
            new TestMigration());

        Assert.Throws<MigrationException>(
            () => registry.Register(
                new TestMigration()));
    }

    [Test]
    public void TryGetMigration_Returns_True_WhenExists()
    {
        var registry = new MigrationRegistry();

        registry.Register(
            new TestMigration());

        var result = registry.TryGetMigration(
            1,
            out var migration);

        Assert.That(result, Is.True);

        Assert.That(migration, Is.Not.Null);

        Assert.That(
            migration.FromVersion,
            Is.EqualTo(1));

        Assert.That(
            migration.ToVersion,
            Is.EqualTo(2));
    }

    [Test]
    public void TryGetMigration_Returns_False_WhenMissing()
    {
        var registry = new MigrationRegistry();

        var result = registry.TryGetMigration(
            999,
            out _);

        Assert.That(result, Is.False);
    }

    [Test]
    public void ApplyMigrations_SingleStep()
    {
        var registry = new MigrationRegistry();

        registry.Register(
            new TestMigration());

        var saveFile = new SaveFile
        {
            Version = 1
        };

        var result = registry.ApplyMigrations(
            saveFile,
            2);

        Assert.That(
            result.Version,
            Is.EqualTo(2));
    }

    [Test]
    public void ApplyMigrations_MultipleSteps()
    {
        var registry = new MigrationRegistry();

        registry.Register(
            new TestMigration());

        registry.Register(
            new TestMigrationV2ToV3());

        var saveFile = new SaveFile
        {
            Version = 1
        };

        var result = registry.ApplyMigrations(
            saveFile,
            3);

        Assert.That(
            result.Version,
            Is.EqualTo(3));
    }

    [Test]
    public void ApplyMigrations_MissingMigration_Throws()
    {
        var registry = new MigrationRegistry();

        var saveFile = new SaveFile
        {
            Version = 1
        };

        Assert.Throws<MigrationException>(
            () => registry.ApplyMigrations(
                saveFile,
                2));
    }

    [Test]
    public void ApplyMigrations_WhenMigrationDoesNotAdvanceVersion_Throws()
    {
        var registry = new MigrationRegistry();

        registry.Register(
            new BrokenMigration());

        var saveFile = new SaveFile
        {
            Version = 1
        };

        Assert.Throws<MigrationException>(
            () => registry.ApplyMigrations(
                saveFile,
                2));
    }
}