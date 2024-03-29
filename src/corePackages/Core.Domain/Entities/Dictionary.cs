﻿using Core.Domain.Entities.Base;

namespace Core.Domain.Entities
{
    public class Dictionary : Entity
    {
        public int LanguageId { get; set; }
        public string EntryKey { get; set; }
        public string EntryValue { get; set; }
        public string ValueType { get; set; }
        public string Entity { get; set; }
        public string Property { get; set; }
        public Language Language { get; set; }

        public Dictionary(int id, int languageId, string entryKey, string entryValue, string valueType, string entity, string property)
        {
            Id = id;
            LanguageId = languageId;
            EntryKey = entryKey;
            EntryValue = entryValue;
            ValueType = valueType;
            Entity = entity;
            Property = property;

        }
    }
}
