using MediatR;
using UserManagement.Application.Interfaces;
namespace UserManagement.Application.Command;

public record UserUpdateCommand(int Id, string firstName, string lastName, string email, int age) : IRequest<UserUpdateResult>;
public record UserUpdateResult(string Message);

public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand, UserUpdateResult>
{
    private readonly IUserRepository _repo;

    public UserUpdateCommandHandler(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<UserUpdateResult> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
    {
        var user = await _repo.GetById(request.Id, cancellationToken);

        if (user == null)
        {
            return new UserUpdateResult($"Usuário com ID {request.Id} não foi encontrado.");
        }

        user.FirstName = request.firstName;
        user.LastName = request.lastName;
        user.Email = request.email;
        user.Age = request.age;

        await _repo.Update(user, cancellationToken);

        return new UserUpdateResult($"Usuário com ID {request.Id} foi atualizado com sucesso.");
    }
}