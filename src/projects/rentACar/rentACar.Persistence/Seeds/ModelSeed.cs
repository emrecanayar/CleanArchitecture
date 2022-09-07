using Core.Persistence.ComplexTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using rentACar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rentACar.Persistence.Seeds
{
    public class ModelSeed : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.HasData(
                new Model { Id = 1, BrandId = 1, Name = "Series 4", DailyPrice = 1500, ImageUrl = "", Status = RecordStatu.Active, CreatedBy = "System", CreatedDate = DateTime.Now, ModifiedBy = "", ModifiedDate = null, IsDeleted = false },
                new Model { Id = 2, BrandId = 1, Name = "Series 3", DailyPrice = 1200, ImageUrl = "", Status = RecordStatu.Active, CreatedBy = "System", CreatedDate = DateTime.Now, ModifiedBy = "", ModifiedDate = null, IsDeleted = false },
                new Model { Id = 3, BrandId = 2, Name = "A180", DailyPrice = 1000, ImageUrl = "", Status = RecordStatu.Active, CreatedBy = "System", CreatedDate = DateTime.Now, ModifiedBy = "", ModifiedDate = null, IsDeleted = false }
                );
        }
    }
}
