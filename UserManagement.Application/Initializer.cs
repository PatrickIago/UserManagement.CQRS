using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Application.Command;
using UserManagement.Application.Query;

namespace UserManagement.Application;
public class Initializer
{
    public static void Initialize(IServiceCollection services)
    {
        // COMMAND
        services.AddTransient<IRequestHandler<CreateUserCommand, CreateUserResult>, CreateUserCommandHandler>();
        services.AddTransient<IRequestHandler<UserUpdateCommand, UserUpdateResult>, UserUpdateCommandHandler>();
        services.AddTransient<IRequestHandler<DeleteUserCommand, DeleteUserResult>, DeleteUserCommandHandler>();

        // QUERY
        services.AddTransient<IRequestHandler<GetUserQuery, IEnumerable<UserViewModel>>, GetUserQueryHandler>();
        services.AddTransient<IRequestHandler<GetUserByNameQuery, IEnumerable<GetUserByNameResult>>, GetUserByNameQueryHandler>();
        services.AddTransient<IRequestHandler<GetUserByIdQuery, GetUserByIResult>, GetUserByIdQueryHandler>();
    }
}