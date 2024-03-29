﻿using Core.Domain.ComplexTypes;

namespace Core.Domain.Entities.Base
{
    public class Entity
    {
        public int Id { get; set; }
        public RecordStatu Status { get; set; } = RecordStatu.Active;
        public string CreatedBy { get; set; } = "Admin";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }

        public Entity()
        {

        }

        public Entity(int id, RecordStatu status, string createdBy, DateTime createdDate, string modifiedBy, DateTime? modifiedDate, bool isDeleted) : this()
        {
            Id = id;
            Status = status;
            CreatedBy = createdBy;
            CreatedDate = createdDate;
            ModifiedBy = modifiedBy;
            ModifiedDate = modifiedDate;
            IsDeleted = isDeleted;
        }
    }
}
