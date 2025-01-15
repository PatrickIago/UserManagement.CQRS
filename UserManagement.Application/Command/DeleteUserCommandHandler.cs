using MediatR;
using UserManagement.Application.Interfaces;
namespace UserManagement.Application.Command;
public record DeleteUserCommand(int Id) : IRequest<DeleteUserResult>;
public record DeleteUserResult(string Message);

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserResult>
{
    private readonly IUserRepository _repo;
    public DeleteUserCommandHandler(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<DeleteUserResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repo.GetById(request.Id, cancellationToken);

        if (user == null)
        {
            return new DeleteUserResult($"Usuário com ID {request.Id} não foi encontrado.");
        }

        await _repo.Delete(request.Id, cancellationToken);

        return new DeleteUserResult($"Usuário com ID {request.Id} foi deletado com sucesso.");
    }
}