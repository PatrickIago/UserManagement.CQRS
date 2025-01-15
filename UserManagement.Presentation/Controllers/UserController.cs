using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Command;
using UserManagement.Application.Query;

namespace UserManagement.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Adiciona novo usuario")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
    {
        if (command == null)
        {
            return BadRequest("Dados inválidos.");
        }

        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetUserById), new { id = result.id }, result);
    }

    [HttpPut("Atualiza dados de um usuario")]
    public async Task<IActionResult> UpdateUser([FromBody] UserUpdateCommand command, CancellationToken cancellationToken)
    {
        if (command == null)
        {
            return BadRequest("Dados inválidos.");
        }

        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("Remove um usuario")]
    public async Task<IActionResult> DeleteUser(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteUserCommand(id);
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpGet("Retorna um usuario pelo id especifico")]
    public async Task<IActionResult> GetUserById(int id, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(id);
        try
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Usuário com ID {id} não encontrado.");
        }
    }

    [HttpGet("Retorna todos os usuarios")]
    public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
    {
        var query = new GetUserQuery();
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("Busca usuario pelo nome")]
    public async Task<IActionResult> GetUsersByName([FromQuery] string name, CancellationToken cancellationToken)
    {
        var query = new GetUserByNameQuery(name);
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }
}