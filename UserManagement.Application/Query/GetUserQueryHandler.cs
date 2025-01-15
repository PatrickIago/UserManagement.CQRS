using MediatR;
using UserManagement.Application.Interfaces;
namespace UserManagement.Application.Query;

public record UserViewModel(int id, string firstName, string lastName, string email, int age);
public record GetUserQuery : IRequest<IEnumerable<UserViewModel>>;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, IEnumerable<UserViewModel>>
{
    private readonly IUserRepository _repo;

    public GetUserQueryHandler(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<UserViewModel>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var users = await _repo.Get(cancellationToken);

        var userViewModels = users.Select(user => new UserViewModel(
           user.Id,
           user.FirstName,
           user.LastName,
           user.Email,
           user.Age
       ));

        return userViewModels;
    }
}
