﻿using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using rentACar.Application.Services.Repositories;

namespace rentACar.Application.Features.Auths.Rules
{
    public class AuthBusinessRules
    {
        private readonly IUserRepository _userRepository;

        public AuthBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task EmailCantNotBeDuplicatedWhenRegistered(string email)
        {
            User? user = await _userRepository.GetAsync(x => x.Email == email);
            if (user is not null) throw new BusinessException("Mail already exists");
        }
    }
}
