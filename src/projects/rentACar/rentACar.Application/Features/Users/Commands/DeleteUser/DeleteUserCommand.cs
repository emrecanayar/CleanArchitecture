using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;
using rentACar.Application.Features;
using rentACar.Application.Services.Repositories;
using static Application.Features.Users.Constants.OperationClaims;
using static Core.Domain.Constants.OperationClaims;

namespace Application.Features.Users.Commands.DeleteUser;

public class DeleteUserCommand : IRequest<DeletedUserDto>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, UserDelete };

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeletedUserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserBusinessRules _userBusinessRules;

        public DeleteUserCommandHandler(IUserRepository userRepository,
                                        UserBusinessRules userBusinessRules)
        {
            _userRepository = userRepository;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<DeletedUserDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _userBusinessRules.UserIdShouldExistWhenSelected(request.Id);

            User mappedUser = ObjectMapper.Mapper.Map<User>(request);
            User deletedUser = await _userRepository.DeleteAsync(mappedUser);
            DeletedUserDto deletedUserDto = ObjectMapper.Mapper.Map<DeletedUserDto>(deletedUser);
            return deletedUserDto;
        }
    }
}