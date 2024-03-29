﻿
using Core.Domain.Entities.Base;
using Core.Domain.Enums;

namespace Core.Domain.Entities
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public AuthenticatorType AuthenticatorType { get; set; }
        public CultureType CultureType { get; set; }
        public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }

        public User()
        {
            UserOperationClaims = new HashSet<UserOperationClaim>();
            RefreshTokens = new HashSet<RefreshToken>();
        }

        public User(int id, string firstName, string lastName, string email, byte[] passwordSalt, byte[] passwordHash, AuthenticatorType authenticatorType, CultureType cultureType) : this()
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordSalt = passwordSalt;
            PasswordHash = passwordHash;
            AuthenticatorType = authenticatorType;
            CultureType = CultureType;

        }
    }
}
