using Core.Persistence.ComplexTypes;
using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rentACar.Domain.Entities
{
    public class Brand : Entity
    {
        public string Name { get; set; }

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
