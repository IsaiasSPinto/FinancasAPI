using System.Security.Claims;

using Finances.Models.Account;
using Finances.Services.Contracts;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finances.Controllers;
[Route("api/[controller]")]
[Authorize]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
            _accountService = accountService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAccount(int id)
    {
        var result = await _accountService.GetAccountByIdAsync(id);

        return result.Match<IActionResult>(
            account => Ok(account),
            error => NotFound("Account Not Found !")
        );
    }

    [HttpGet]
    public async Task<IActionResult> GetUserAccounts()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var result = await _accountService.GetAccountsByUserIdAsync(userId);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDto createAccountDto)
    {
        var result = await _accountService.CreateAccountAsync(createAccountDto);

        return result.Match<IActionResult>(
            account => CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account),
            errors => BadRequest(errors)
        );
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAccount([FromBody] UpdateAccountDto updateAccountDto)
    {
        var result = await _accountService.UpdateAccountAsync(updateAccountDto);

        return result.Match<IActionResult>(
            account => Ok(account),
            errors => BadRequest(errors)
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _accountService.DeleteAccountAsync(id);

        return NoContent();
    }
}
