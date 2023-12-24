using Note.DataAccess;
using Note.DataAccess.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Internal;
using Note.UnitTests.Repository;
using NUnit.Framework;

namespace Note.UnitTests.Repository;

[TestFixture]
[Category("Intedration")]
public class UserRepositoryTest : RepositoryTestsBaseClass
{
    [Test]
    public void GetAllUsersTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();
        var Users = new UserEntity[]
        {
            new UserEntity()
            {
                Login = "testLogin1",
                Password = "******1",
                Name = "Name1",
                Surname = "SecondName1",
                ExternalId = Guid.NewGuid()
            },
            new UserEntity()
            {
                Login = "testLogin2",
                Password = "******2",
                Name = "Name2",
                Surname = "SecondName2",
                ExternalId = Guid.NewGuid()
            }
        };
        context.Users.AddRange(Users);
        context.SaveChanges();

        //execute
        var repository = new Repository<UserEntity>(DbContextFactory);
        var actualUsers = repository.GetAll();

        //assert        
        actualUsers.Should().BeEquivalentTo(Users);
    }

    [Test]
    public void GetAllUsersWithFilterTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();
        var Users = new UserEntity[]
        {
            new UserEntity()
            {
                Login = "testLogin1",
                Password = "******1",
                Name = "Name1",
                Surname = "SecondName1",
                ExternalId = Guid.NewGuid()
            },
            new UserEntity()
            {
                Login = "testLogin2",
                Password = "******2",
                Name = "Name2",
                Surname = "SecondName2",
                ExternalId = Guid.NewGuid()
            }
        };
        context.Users.AddRange(Users);
        context.SaveChanges();

        //execute
        var repository = new Repository<UserEntity>(DbContextFactory);
        var actualUsers = repository.GetAll(x => x.Name == "Name1").ToArray();

        //assert
        actualUsers.Should().BeEquivalentTo(Users.Where(x => x.Name == "Name1"));
    }

    [Test]
    public void SaveNewUserTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();

        //execute
        var User = new UserEntity()
        {
            Login = "testLogin1",
            Password = "******1",
            Name = "Name1",
            Surname = "SecondName1",
            ExternalId = Guid.NewGuid()
        };
        var repository = new Repository<UserEntity>(DbContextFactory);
        repository.Save(User);

        //assert
        var actualUser = context.Users.SingleOrDefault();
        actualUser.Should().BeEquivalentTo(User, options => options.Excluding(x => x.Id)
            .Excluding(x => x.ModificationTime)
            .Excluding(x => x.CreationTime)
            .Excluding(x => x.ExternalId));
        actualUser.Id.Should().NotBe(default);
        actualUser.ModificationTime.Should().NotBe(default);
        actualUser.CreationTime.Should().NotBe(default);
        actualUser.ExternalId.Should().NotBe(Guid.Empty);
    }

    [Test]
    public void UpdateUserTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();
        var User = new UserEntity()
        {
            Login = "testLogin1",
            Password = "******1",
            Name = "Name1",
            Surname = "SecondName1",
            ExternalId = Guid.NewGuid()
        };
        context.Users.Add(User);
        context.SaveChanges();

        //execute
        User.Name = "new Name1";
        User.Surname = "new SecondName1";
        var repository = new Repository<UserEntity>(DbContextFactory);
        repository.Save(User);

        //assert
        var actualUser = context.Users.SingleOrDefault();
        actualUser.Should().BeEquivalentTo(User);
    }

    [Test]
    public void DeleteUserTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();
        var User = new UserEntity()
        {
            Login = "testLogin1",
            Password = "******1",
            Name = "Name1",
            Surname = "SecondName1",
            ExternalId = Guid.NewGuid()
        };
        context.Users.Add(User);
        context.SaveChanges();

        //execute
        var repository = new Repository<UserEntity>(DbContextFactory);
        repository.Delete(User);

        //assert
        context.Users.Count().Should().Be(0);
    }

    [Test]
    public void GetByUserTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();
        var Users = new UserEntity[]
        {
            new UserEntity()
            {
                Login = "testLogin1",
                Password = "******1",
                Name = "Name1",
                Surname = "SecondName1",
                ExternalId = Guid.NewGuid()
            },
            new UserEntity()
            {
                Login = "testLogin2",
                Password = "******2",
                Name = "Name2",
                Surname = "SecondName2",
                ExternalId = Guid.NewGuid()
            }
        };
        context.Users.AddRange(Users);
        context.SaveChanges();

        // positive case
        //execute
        var repository = new Repository<UserEntity>(DbContextFactory);
        var actualUser = repository.GetById(Users[0].Id);
        //assert
        actualUser.Should().BeEquivalentTo(Users[0]);

        // negative case
        //execute
        actualUser = repository.GetById(Users[Users.Length - 1].Id + 10);
        //assert
        actualUser.Should().BeNull();
    }

    [SetUp]
    public void SetUp()
    {
        CleanUp();
    }

    [TearDown]
    public void TearDown()
    {
        CleanUp();
    }

    public void CleanUp()
    {
        using (var context = DbContextFactory.CreateDbContext())
        {
            context.Users.RemoveRange(context.Users);
            context.SaveChanges();
        }
    }
}
