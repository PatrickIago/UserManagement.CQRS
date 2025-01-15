using MediatR;
using UserManagement.Application.Interfaces;
namespace UserManagement.Application.Query;

public record GetUserByNameQuery(string Name) : IRequest<IEnumerable<GetUserByNameResult>>;
public record GetUserByNameResult(int Id, string FirstName, string LastName, string Email, int Age);

public class GetUserByNameQueryHandler : IRequestHandler<GetUserByNameQuery, IEnumerable<GetUserByNameResult>>
{
    private readonly IUserRepository _repo;

    public GetUserByNameQueryHandler(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<GetUserByNameResult>> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
    {
        var users = await _repo.GetByName(request.Name, cancellationToken);

        var results = users.Select(user => new GetUserByNameResult(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            user.Age
        ));

        return results;
    }
}