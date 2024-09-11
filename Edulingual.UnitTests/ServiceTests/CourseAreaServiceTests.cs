using AutoMapper;
using Edulingual.Caching.Interfaces;
using Edulingual.DAL.Interfaces;
using Edulingual.Domain.Entities;
using Edulingual.Service.Implementations;
using Edulingual.Service.Interfaces;
using Edulingual.UnitTests.Mock;
using Moq;
using System.Net.NetworkInformation;

namespace Edulingual.UnitTests.ServiceTests;

[TestFixture]
public class CourseAreaServiceTests
{
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private Mock<ICourseAreaRepository> _mockCourseAreaRepo;
    private Mock<IMapper> _mockMapper;
    private Mock<IDataCached> _mockDataCached;
    private Mock<ICourseAreaService> _mockCourseAreaService;

    [SetUp]
    public void SetUp()
    {
        _mockCourseAreaRepo = MockRepository.SetUpMockCourseAreaRepository();
        _mockDataCached = MockCommonObject.SetUpDataCached();
        _mockMapper = MockCommonObject.SetUpMockMapper();
        _mockUnitOfWork = MockRepository.SetUpMockUnitOfWork();
        //_mockCourseAreaService = new CourseAreaService(unitOfWork: _mockUnitOfWork.Object, courseAreaRepo: _mockCourseAreaRepo.Object, mapper: _mockMapper.Object, dataCached: _mockDataCached.Object);
    }

    [Test]
    public async Task GetAll_WithoutInvalid_ShouldReturnSuccess()
    {
        //var result = _mockCourseAreaRepo.
    }
}
