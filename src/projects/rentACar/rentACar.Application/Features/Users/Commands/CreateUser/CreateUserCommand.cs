using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using Core.Application.Pipelines.Authorization;
using Core.Domain.Entities;
using Core.Security.Hashing;
using MediatR;
using rentACar.Application.Features;
using rentACar.Application.Services.Repositories;
using static Application.Features.Users.Constants.OperationClaims;
using static Core.Domain.Constants.OperationClaims;

namespace Application.Features.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<CreatedUserDto>, ISecuredRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public string[] Roles => new[] { Admin, UserAdd };

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreatedUserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserBusinessRules _userBusinessRules;

        public CreateUserCommandHandler(IUserRepository userRepository,
                                        UserBusinessRules userBusinessRules)
        {
            _userRepository = userRepository;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<CreatedUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User mappedUser = ObjectMapper.Mapper.Map<User>(request);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);
            mappedUser.PasswordHash = passwordHash;
            mappedUser.PasswordSalt = passwordSalt;

            User createdUser = await _userRepository.AddAsync(mappedUser);
            CreatedUserDto createdUserDto = ObjectMapper.Mapper.Map<CreatedUserDto>(createdUser);
            return createdUserDto;
        }
    }
}