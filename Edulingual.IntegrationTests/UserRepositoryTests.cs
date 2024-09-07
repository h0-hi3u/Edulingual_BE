//using Edulingual.DAL.Interfaces;
//using Edulingual.Domain.Entities;

//namespace Edulingual.IntegrationTests;
//public class UserRepositoryTests
//{
//    private User userExample;
//    private IUserRepository _userRepo;
//    private IUnitOfWork _unitOfWork;

//    [OneTimeSetUp]
//    public void SetUp()
//    {
//        DISetup.InitializeServices();
//        _userRepo = DISetup.GetRequiredService<IUserRepository>();
//        _unitOfWork = DISetup.GetRequiredService<IUnitOfWork>();
//        userExample = new User
//        {
//            Username = "hieu1",
//            Email = "hieu1@gmail.com",
//            Password = "A123456@",
//            Phone = "0123456789",
//            FullName = "Ho Trong Hieu1",
//            Description = "desccription",
//            ImageUrl = "imageUrl",
//            RoleId = Guid.Parse("57e597c5-1bc6-431d-8e8b-e9aef5fccfd2"),
//        };
//    }

//    [Test]
//    public void GetAll_Valid_GetAllSucess()
//    {
//        //act
//        var list = _userRepo.GetAll();

//        //assert
//        Assert.That(list is not null);
//    }

//    [Test, Order(1)]
//    public async Task CreateUser_WithValid_ShouldSuccess()
//    {
//        //act
//        await _userRepo.AddAsync(userExample);
//        bool isSucess = await _unitOfWork.SaveChangesAsync();

//        //assert
//        Assert.IsTrue(isSucess);
//    }

//    [Test]
//    public async Task GetOneAsync_WithExistEmail_ShouldSuccess()
//    {
//        //act
//        var user = await _userRepo.GetOneAsync(u => u.Email == userExample.Email);

//        //assert
//        Assert.IsNotNull(user);
//    }

//    [Test, Order(2)]
//    public async Task Update_WithValidFullname_ShouldSuccess()
//    {
//        //act
//        var user = await _userRepo.GetOneAsync(u => u.Email == userExample.Email);
//        user.FullName = "Test update fullname";
//        _userRepo.Update(user);
//        var isSucess = await _unitOfWork.SaveChangesAsync();

//        //assert
//        Assert.IsTrue(isSucess);

//    }

//    [Test]
//    public async Task Delete_WithValidUserEmail_ShouldSuccess()
//    {
//        //act
//        var user = await _userRepo.GetOneAsync(u => u.Email == userExample.Email);
//        if (user == null) Assert.Fail("Not found entity!");
//        _userRepo.Delete(user);
//        var isSucess = await _unitOfWork.SaveChangesAsync();

//        //assert
//        Assert.IsTrue(isSucess);
//    }
//}
