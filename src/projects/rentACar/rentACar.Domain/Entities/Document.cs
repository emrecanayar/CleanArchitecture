using Core.Domain.ComplexTypes;
using Core.Domain.Entities.Base;

namespace rentACar.Domain.Entities
{
    public class Document : Entity
    {
        public string Token { get; set; }

        public Document()
        {

        }

        public Document(int id, string token, RecordStatu status, string createdBy, DateTime createdDate, string modifiedBy, DateTime? modifiedDate, bool isDeleted) : this()
        {
            Id = id;
            Token = token;
            Status = status;
            CreatedBy = createdBy;
            CreatedDate = createdDate;
            ModifiedBy = modifiedBy;
            ModifiedDate = modifiedDate;
            IsDeleted = isDeleted;
        }
    }
}
