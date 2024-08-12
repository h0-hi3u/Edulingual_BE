using Microsoft.EntityFrameworkCore;

namespace Edulingual.Domain.Interfaces;

public interface IModelMapper
{
    void Mapping(ModelBuilder modelBuilder);
}
