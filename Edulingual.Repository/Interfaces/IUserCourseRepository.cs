﻿using Edulingual.Common.Interfaces;
using Edulingual.Domain.Entities;

namespace Edulingual.DAL.Interfaces;

public interface IUserCourseRepository : IRepository<UserCourse>, IAutoRegisterable
{
}
