using AutoMapper;
using Edulingual.Caching.Interfaces;
using Edulingual.DAL.Interfaces;
using Edulingual.Domain.Entities;
using Edulingual.UnitTests.Mock;
using Moq;

namespace Edulingual.UnitTests.ServiceTests;

[TestFixture]
public class CourseAreaServiceTests
{
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private Mock<ICourseAreaRepository> _mockCourseAreaRepo;
    private Mock<IMapper> _mockMapper;
    private Mock<IDataCached> _mockDataCached;

    [SetUp]
    public void SetUp()
    {
        _mockCourseAreaRepo = MockRepository.SetUpMockCourseAreaRepository();
        _mockDataCached = MockCommonObject.SetUpDataCached();
        _mockMapper = MockCommonObject.SetUpMockMapper();
        _mockUnitOfWork = MockRepository.SetUpMockUnitOfWork();
    }
}
