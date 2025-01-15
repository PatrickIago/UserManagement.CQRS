using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;
using UserManagement.Infra.Data;
namespace UserManagement.Infrastructure.Repositories;
public class UserRepository : IUserRepository
{
    private readonly UserManagementDbContext _context;

    public UserRepository(UserManagementDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserEntity>> Get(CancellationToken cancellationToken)
    {
        var users = await _context.Users.ToListAsync(cancellationToken);
        return users;
    }

    public async Task<UserEntity> GetById(int id, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (user == null)
        {
            throw new KeyNotFoundException($"Usuário com o ID {id} não foi encontrado.");
        }

        return user;
    }

    public async Task<IEnumerable<UserEntity>> GetByName(string name, CancellationToken cancellationToken)
    {
        var users = await _context.Users
            .Where(x => x.FirstName.Contains(name) || x.LastName.Contains(name))
            .ToListAsync(cancellationToken);

        return users;
    }

    public async Task<int> Create(UserEntity userEntity, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(userEntity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return userEntity.Id;
    }

    public async Task Update(UserEntity userEntity, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userEntity.Id, cancellationToken);

        if (user == null)
        {
            throw new KeyNotFoundException($"Usuário com o ID {userEntity.Id} não foi encontrado.");
        }

        user.FirstName = userEntity.FirstName;
        user.LastName = userEntity.LastName;
        user.Email = userEntity.Email;
        user.Age = userEntity.Age;

        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(int id, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
        {
            throw new KeyNotFoundException($"Usuário com o ID {id} não foi encontrado.");
        }

        _context.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
    }
}