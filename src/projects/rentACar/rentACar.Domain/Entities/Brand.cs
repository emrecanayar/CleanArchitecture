using Core.Domain.ComplexTypes;
using Core.Domain.Entities.Base;

namespace rentACar.Domain.Entities
{
    public class Brand : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<Model> Models { get; set; }
        public virtual ICollection<BrandDocument> BrandDocuments { get; set; }

        public Brand()
        {

        }

        public Brand(int id, string name, RecordStatu status, string createdBy, DateTime createdDate, string modifiedBy, DateTime? modifiedDate, bool isDeleted) : this()
        {
            Id = id;
            Name = name;
            Status = status;
            CreatedBy = createdBy;
            CreatedDate = createdDate;
            ModifiedBy = modifiedBy;
            ModifiedDate = modifiedDate;
            IsDeleted = isDeleted;
        }
    }
}
