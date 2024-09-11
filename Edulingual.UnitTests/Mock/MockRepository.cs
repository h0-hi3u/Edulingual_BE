using Edulingual.Common.Models;
using Edulingual.DAL.Interfaces;
using Edulingual.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;

namespace Edulingual.UnitTests;

public static class MockRepository
{
    public static Mock<IUnitOfWork> SetUpMockUnitOfWork()
    {
        var _mockUnitOfWork = new Mock<IUnitOfWork>();

        return _mockUnitOfWork;
    }
    public static Mock<IUserRepository> SetUpMockUserRepository()
    {
        IEnumerable<User> listUsers = new List<User>
        {
            new User
            {
                Username = "test1",
                Email = "test1@gmail.com",
                Password = "A123456@",
                Phone = "0123456789",
                FullName = "test1",
                Description = "desccription",
                ImageUrl = "imageUrl",
                RoleId = Guid.Parse("57e597c5-1bc6-431d-8e8b-e9aef5fccfd2"),
            },
            new User
            {
                Username = "test2",
                Email = "test2@gmail.com",
                Password = "A123456@",
                Phone = "0123456789",
                FullName = "test2",
                Description = "desccription",
                ImageUrl = "imageUrl",
                RoleId = Guid.Parse("57e597c5-1bc6-431d-8e8b-e9aef5fccfd2"),
            },
            new User
            {
                Username = "test3",
                Email = "test3@gmail.com",
                Password = "A123456@",
                Phone = "0123456789",
                FullName = "test3",
                Description = "desccription",
                ImageUrl = "imageUrl",
                RoleId = Guid.Parse("57e597c5-1bc6-431d-8e8b-e9aef5fccfd2"),
            },
            new User
            {
                Username = "test4",
                Email = "test4@gmail.com",
                Password = "A123456@",
                Phone = "0123456789",
                FullName = "test4",
                Description = "desccription",
                ImageUrl = "imageUrl",
                RoleId = Guid.Parse("57e597c5-1bc6-431d-8e8b-e9aef5fccfd2"),
            },
            new User
            {
                Username = "test5",
                Email = "test5@gmail.com",
                Password = "A123456@",
                Phone = "0123456789",
                FullName = "test5",
                Description = "desccription",
                ImageUrl = "imageUrl",
                RoleId = Guid.Parse("57e597c5-1bc6-431d-8e8b-e9aef5fccfd2"),
            },
        };

        IPaginate<User> paginate = new Paginate<User>
        {
            PageSize = 5,
            PageIndex = 1,
            TotalRecord = 5,
            TotalPage = 1,
            Data = listUsers
        };

        Mock<IUserRepository> _mockUserRepo = new Mock<IUserRepository>();
        //_mockUserRepo.Setup(ur => ur.GetAll())
        //    .Returns((Microsoft.EntityFrameworkCore.DbSet<User>)listUsers);
        _mockUserRepo.Setup(ur => ur.AddAsync(new User()));
        _mockUserRepo.Setup(ur => ur.AddRangeAsync(listUsers));
        _mockUserRepo.Setup(ur => ur.Update(It.IsAny<User>()));
        _mockUserRepo.Setup(ur => ur.Delete(new User()));

        //_mockUserRepo.Setup(ur => ur.GetOneAsync(
        //It.IsAny<Expression<Func<User, bool>>>(),
        //It.IsAny<Func<IQueryable<User>, IIncludableQueryable<User, object>>>(),
        //It.IsAny<bool>()))
        //    .ReturnsAsync(new User
        //    {
        //        Email = "hieu@gmail.com",
        //        Password= "$2a$11$QCUYg1vgdoTov1ZszbJbqeVAECzZik1pjvCwolrwHsbOwiapResMW",
        //        FullName = "Ho Trong Hieu",
        //        Role = new Role
        //        {
        //            Name = "Student"
        //        }
        //    });

        _mockUserRepo.Setup(ur => ur.GetOneAsync(
        It.Is<Expression<Func<User, bool>>>(p => p.Compile().Invoke(new User
        {
            Email = "hieu@gmail.com",
            Password = "$2a$11$QCUYg1vgdoTov1ZszbJbqeVAECzZik1pjvCwolrwHsbOwiapResMW",
            FullName = "Ho Trong Hieu",
            Role = new Role
            {
                Name = "Student"
            }
        })),
        It.IsAny<Func<IQueryable<User>, IIncludableQueryable<User, object>>>(),
        It.IsAny<bool>()))
            .ReturnsAsync(new User
            {
                Email = "hieu@gmail.com",
                Password = "$2a$11$QCUYg1vgdoTov1ZszbJbqeVAECzZik1pjvCwolrwHsbOwiapResMW",
                FullName = "Ho Trong Hieu",
                Role = new Role
                {
                    Name = "Student"
                }
            });

        _mockUserRepo.Setup(ur => ur.GetListAsync(
        It.IsAny<Expression<Func<User, bool>>>(),
        It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
        It.IsAny<Func<IQueryable<User>, IIncludableQueryable<User, object>>>(),
        It.IsAny<bool>()))
            .ReturnsAsync(listUsers);

        _mockUserRepo.Setup(ur => ur.GetPagingAsync(
        It.IsAny<Expression<Func<User, bool>>>(),
        It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
        It.IsAny<Func<IQueryable<User>, IIncludableQueryable<User, object>>>(),
        It.IsAny<int>(),
        It.IsAny<int>()))
            .ReturnsAsync(paginate);

        return _mockUserRepo;
    }

    public static Mock<ICourseAreaRepository> SetUpMockCourseAreaRepository()
    {
        IEnumerable<CourseArea> listCourseAreas = new List<CourseArea>
        {
            new CourseArea
            {
                Name = "Ho Chi Minh"
            },
            new CourseArea
            {
                Name = "Da Nang"
            },
            new CourseArea
            {
                Name = "Ha Noi"
            },
            new CourseArea
            {
                Name = "Can Tho"
            },
            new CourseArea
            {
                Name = "Da Lat"
            },
        };

        IPaginate<CourseArea> paginate = new Paginate<CourseArea>
        {
            PageSize = 5,
            PageIndex = 1,
            TotalRecord = 5,
            TotalPage = 1,
            Data = listCourseAreas
        };

        Mock<ICourseAreaRepository> _mockCourseAreaRepo = new Mock<ICourseAreaRepository>();

        _mockCourseAreaRepo.Setup(ur => ur.AddAsync(It.IsAny<CourseArea>()));
        _mockCourseAreaRepo.Setup(ur => ur.AddRangeAsync(It.IsAny<IEnumerable<CourseArea>>()));
        _mockCourseAreaRepo.Setup(ur => ur.Update(It.IsAny<CourseArea>()));
        _mockCourseAreaRepo.Setup(ur => ur.Delete(It.IsAny<CourseArea>()));

        _mockCourseAreaRepo.Setup(ur => ur.GetOneAsync(
        It.Is<Expression<Func<CourseArea, bool>>>(p => p.Compile().Invoke(new CourseArea
        {
            Name = "Long An"
        })),
        It.IsAny<Func<IQueryable<CourseArea>, IIncludableQueryable<CourseArea, object>>>(),
        It.IsAny<bool>()))
            .ReturnsAsync(new CourseArea
            {
                Name = "Long An"
            });

        _mockCourseAreaRepo.Setup(ur => ur.GetListAsync(
        It.IsAny<Expression<Func<CourseArea, bool>>>(),
        It.IsAny<Func<IQueryable<CourseArea>, IOrderedQueryable<CourseArea>>>(),
        It.IsAny<Func<IQueryable<CourseArea>, IIncludableQueryable<CourseArea, object>>>(),
        It.IsAny<bool>()))
            .ReturnsAsync(It.IsAny<IEnumerable<CourseArea>>);

        _mockCourseAreaRepo.Setup(ur => ur.GetPagingAsync(
        It.IsAny<Expression<Func<CourseArea, bool>>>(),
        It.IsAny<Func<IQueryable<CourseArea>, IOrderedQueryable<CourseArea>>>(),
        It.IsAny<Func<IQueryable<CourseArea>, IIncludableQueryable<CourseArea, object>>>(),
        It.IsAny<int>(),
        It.IsAny<int>()))
            .ReturnsAsync(It.IsAny<IPaginate<CourseArea>>());

        return _mockCourseAreaRepo;
    }
}