using Finances.Models.Transactions;
using Finances.Services.Contracts;

using Microsoft.AspNetCore.Mvc;

namespace Finances.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TransactionsController : ControllerBase
{

    private readonly ITransactionService _transactionService;

    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserTransactions([FromQuery] int userId)
    {
        var result = await _transactionService.GetAllUserTransactionsAsync(userId);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTransaction(int id)
    {
        var result = await _transactionService.GetTransactionAsync(id);

        return result is null ? NotFound("Transaction Not Found !") : Ok(result);
    }


    [HttpPost]
    public async Task<IActionResult> Create(CreateTransactionDto transaction)
    {
        var result = await _transactionService.CreateTransactionAsync(transaction);

        return result.Match<IActionResult>(
            id => CreatedAtAction(nameof(GetTransaction), new { id }, id),
            error => BadRequest(error)
        );
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateTransactionDto transaction)
    {
        var result = await _transactionService.UpdateTransactionAsync(transaction);

        return result.Match<IActionResult>(
    updatedTransaction => Ok(updatedTransaction),
    error => BadRequest(error)
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _transactionService.DeleteTransactionAsync(id);

        return result.MatchFirst<IActionResult>(
    success => NoContent(),
error => BadRequest(error)
        );
    }


}
