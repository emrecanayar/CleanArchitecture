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

namespace Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommand : IRequest<UpdatedUserDto>, ISecuredRequest
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public string[] Roles => new[] { Admin, UserUpdate };

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdatedUserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserBusinessRules _userBusinessRules;

        public UpdateUserCommandHandler(IUserRepository userRepository,
                                        UserBusinessRules userBusinessRules)
        {
            _userRepository = userRepository;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<UpdatedUserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User mappedUser = ObjectMapper.Mapper.Map<User>(request);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);
            mappedUser.PasswordHash = passwordHash;
            mappedUser.PasswordSalt = passwordSalt;

            User updatedUser = await _userRepository.UpdateAsync(mappedUser);
            UpdatedUserDto updatedUserDto = ObjectMapper.Mapper.Map<UpdatedUserDto>(updatedUser);
            return updatedUserDto;
        }
    }
}