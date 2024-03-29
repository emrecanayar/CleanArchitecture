﻿using Core.Domain.ComplexTypes;
using Core.Domain.Dtos;
using Core.Domain.Entities;
using Core.Persistence.Constants;
using Core.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace Core.Persistence.Repositories
{
    public class CustomStringLocalizer
    {
        private static CultureInfo _culture;
        private readonly IConfiguration _configuration;
        private const string STR_DICTIONARY_DYNAMIC_FILTER = "dynamic";

        public CustomStringLocalizer(IConfiguration configuration)
        {
            _culture = CultureInfo.CurrentCulture;
            _configuration = configuration;
        }

        public CustomStringLocalizer(CultureInfo culture, IConfiguration configuration) : this(configuration)
        {
            _culture = culture;
        }

        public CultureInfo GetCulture() => _culture;

        public void SetCulture(string symbol)
        {
            _culture = new CultureInfo(symbol);
        }

        public static string GetValue(string name)
        {
            var culture = _culture ?? CultureInfo.CurrentCulture;
            string? translation = string.Empty;
            using var db = CreateDbContext();
            translation = db.Set<Dictionary>().Where(x => x.Language.Symbol == culture.Name
               && x.EntryKey == name && x.Status == RecordStatu.Active).AsNoTracking().FirstOrDefault()?.EntryValue;
            return translation ?? string.Empty;
        }
        public static List<DictionaryDto> GetValues(string name, bool getAllTranslations)
        {

            using var db = CreateDbContext();
            List<Dictionary> dictionaries = new List<Dictionary>();
            if (getAllTranslations)
            {
                dictionaries = db.Set<Dictionary>().Include(nameof(Dictionary.Language)).Where(x => x.EntryKey == name).ToList();
            }
            else
            {
                var culture = _culture ?? CultureInfo.CurrentCulture;
                dictionaries = db.Set<Dictionary>().Include(nameof(Dictionary.Language)).Where(x => x.Language.Symbol == culture.Name && x.EntryKey == name).ToList();
            }
            var dictionariesDtos = dictionaries.Select(x => new DictionaryDto()
            {
                Id = x.Id,
                Entity = x.Entity,
                EntryKey = x.EntryKey,
                EntryValue = x.EntryValue,
                LanguageId = x.LanguageId,
                Property = x.Property,
                ValueType = x.ValueType
            });
            return dictionariesDtos.ToList();

        }

        public string this[string name]
        {
            get
            {
                var culture = _culture ?? CultureInfo.CurrentCulture;
                string? translation = string.Empty;
                using var db = CreateDbContext();
                translation = db.Set<Dictionary>().Where(x => x.Language.Symbol == culture.Name
                   && x.EntryKey == name && x.Status == RecordStatu.Active).AsNoTracking().FirstOrDefault()?.EntryValue;
                return translation ?? string.Empty;
            }
        }

        public List<DictionaryDto> this[string name, bool getAllTranslations]
        {
            get
            {
                using var db = CreateDbContext();
                List<Dictionary> dictionaries = new List<Dictionary>();
                if (getAllTranslations)
                {
                    dictionaries = db.Set<Dictionary>().Include(nameof(Dictionary.Language)).Where(x => x.EntryKey == name).ToList();
                }
                else
                {
                    var culture = _culture ?? CultureInfo.CurrentCulture;
                    dictionaries = db.Set<Dictionary>().Include(nameof(Dictionary.Language)).Where(x => x.Language.Symbol == culture.Name && x.EntryKey == name).ToList();
                }
                var dictionariesDtos = dictionaries.Select(x => new DictionaryDto()
                {
                    Id = x.Id,
                    Entity = x.Entity,
                    EntryKey = x.EntryKey,
                    EntryValue = x.EntryValue,
                    LanguageId = x.LanguageId,
                    Property = x.Property,
                    ValueType = x.ValueType
                });
                return dictionariesDtos.ToList();
            }
        }

        public static LanguageDto GetCurrentLanguage()
        {
            CultureInfo culture = _culture ?? CultureInfo.CurrentCulture;

            using LocalizationDbContext db = CreateDbContext();
            Language lang = db.Set<Language>().Where(x => x.Status == RecordStatu.Active && x.Symbol == culture.Name).AsNoTracking().FirstOrDefault();

            return new LanguageDto
            {
                Id = lang.Id,
                Name = lang.Name,
                Flag = lang.Flag,
                Symbol = lang.Symbol,
            };
        }

        public static List<Language> GetActiveLanguages()
        {
            using LocalizationDbContext db = CreateDbContext();
            List<Language> langs = db.Set<Language>().Where(x => x.Status == RecordStatu.Active).AsNoTracking().ToList();
            return langs;
        }

        public string GetUserCulture(int userId)
        {
            using LocalizationDbContext db = CreateDbContext();
            User user = db.Set<User>().Where(x => x.Status == RecordStatu.Active && x.Id == 1).AsNoTracking().FirstOrDefault();
            return Enum.GetName(user.CultureType);
        }

        private static LocalizationDbContext CreateDbContext()
        {
            var builder = new DbContextOptionsBuilder<LocalizationDbContext>();
            builder.UseSqlServer(LocalizationConnectionString.Execute());
            return new LocalizationDbContext(builder.Options);
        }
    }
}