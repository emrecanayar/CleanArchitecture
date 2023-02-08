using Core.Domain.ComplexTypes;
using Core.Domain.Entities.Base;

namespace rentACar.Domain.Entities
{
    public class Model : Entity
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
        public decimal DailyPrice { get; set; }
        public string ImageUrl { get; set; }
        public virtual Brand? Brand { get; set; }

        public Model()
        {
        }

        public Model(int id, int brandId, string name, decimal dailyPrice, string imageUrl, RecordStatu status, string createdBy, DateTime createdDate, string modifiedBy, DateTime? modifiedDate, bool isDeleted) : this()
        {
            Id = id;
            BrandId = brandId;
            Name = name;
            DailyPrice = dailyPrice;
            ImageUrl = imageUrl;
            Status = status;
            CreatedBy = createdBy;
            CreatedDate = createdDate;
            ModifiedBy = modifiedBy;
            ModifiedDate = modifiedDate;
            IsDeleted = isDeleted;
        }
    }
}
