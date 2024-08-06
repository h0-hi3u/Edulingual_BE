using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edulingual.Common.Interface;

public interface IModelMapper
{
    void Mapping(ModelBuilder modelBuilder);
}
