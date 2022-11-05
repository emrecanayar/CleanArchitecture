using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using rentACar.Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;
using rentACar.Application.Features;

namespace Application.Features.Users.Queries.GetByIdUser;

public class GetByIdUserQuery : IRequest<UserDto>
{
    public int Id { get; set; }

    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserBusinessRules _userBusinessRules;

        public GetByIdUserQueryHandler(IUserRepository userRepository,
                                       UserBusinessRules userBusinessRules)
        {
            _userRepository = userRepository;
            _userBusinessRules = userBusinessRules;
        }


        public async Task<UserDto> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            await _userBusinessRules.UserIdShouldExistWhenSelected(request.Id);

            User? user = await _userRepository.GetAsync(b => b.Id == request.Id);
            UserDto userDto = ObjectMapper.Mapper.Map<UserDto>(user);
            return userDto;
        }
    }
}