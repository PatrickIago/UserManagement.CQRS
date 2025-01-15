using MediatR;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Command;

public record CreateUserCommand(string firstName, string lastName, string email, int age) : IRequest<CreateUserResult>;
public record CreateUserResult(int id);

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResult>
{
    private readonly IUserRepository _repo;
    public CreateUserCommandHandler(IUserRepository repo)
    {
        _repo = repo;
    }
    public async Task<CreateUserResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new UserEntity
        {
            FirstName = request.firstName,
            LastName = request.lastName,
            Email = request.email,
            Age = request.age
        };

        var userId = await _repo.Create(user, cancellationToken);

        return new CreateUserResult(userId);
    }
}
