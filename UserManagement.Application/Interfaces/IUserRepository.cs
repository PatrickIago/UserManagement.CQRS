using UserManagement.Domain.Entities;
namespace UserManagement.Application.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<UserEntity>> Get(CancellationToken cancellationToken);
    Task<UserEntity> GetById(int id, CancellationToken cancellationToken);
    Task<IEnumerable<UserEntity>> GetByName(string name, CancellationToken cancellationToken);
    Task<int> Create(UserEntity user, CancellationToken cancellationToken);
    Task Update(UserEntity user, CancellationToken cancellationToken);
    Task Delete(int id, CancellationToken cancellationToken);
}
