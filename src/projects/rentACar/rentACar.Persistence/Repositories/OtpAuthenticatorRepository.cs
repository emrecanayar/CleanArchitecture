﻿using Core.Domain.Entities;
using Core.Persistence.Repositories;
using rentACar.Application.Services.Repositories;
using rentACar.Persistence.Contexts;

namespace rentACar.Persistence.Repositories
{
    public class OtpAuthenticatorRepository : EfRepositoryBase<OtpAuthenticator, BaseDbContext>, IOtpAuthenticatorRepository
    {
        public OtpAuthenticatorRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
