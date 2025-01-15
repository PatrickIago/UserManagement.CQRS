using MediatR;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Query;
public record GetUserByIdQuery(int id) : IRequest<GetUserByIResult>;
public record GetUserByIResult(UserEntity user);

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIResult>
{
    private readonly IUserRepository _repo;

    public GetUserByIdQueryHandler(IUserRepository repo)
    {
        _repo = repo;
    }
    public async Task<GetUserByIResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _repo.GetById(request.id, cancellationToken);

        if (user == null)
        {
            throw new KeyNotFoundException($"Usuário com o ID {request.id} não foi encontrado.");
        }

        return new GetUserByIResult(user);
    }
}
