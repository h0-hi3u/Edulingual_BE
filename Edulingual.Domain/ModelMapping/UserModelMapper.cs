using Edulingual.Domain.Entities;
using Edulingual.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System;

namespace Edulingual.Domain.ModelMapping;
public class UserModelMapper : IModelMapper
{
    public void Mapping(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable(nameof(User));

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Username).HasMaxLength(200).HasColumnName("Username");
            entity.Property(e => e.Email).HasMaxLength(100).HasColumnName("Email");
            entity.Property(e => e.Phone).HasMaxLength(10);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100).HasColumnName("Fullname");
            entity.Property(e => e.Description);
            entity.Property(e => e.ImageUrl).HasColumnName("ImageUrl");
            entity.Property(e => e.Status);
            entity.Property(e => e.CreatedAt).HasColumnName("CreateAt");
            entity.Property(e => e.CreatedBy).HasColumnName("CreateBy");
            entity.Property(e => e.UpdatedAt).HasColumnName("UpdateAt");
            entity.Property(e => e.UpdatedBy).HasColumnName("UpdateBy");

            entity.HasOne(u => u.Role).WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
