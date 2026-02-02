using BooksApi.Application.DTOs;
using BooksApi.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly ILoanService _loanService;

    public UsersController(ILoanService loanService)
    {
        _loanService = loanService;
    }

    [HttpGet("{userId}/loans")]
    public async Task<ActionResult<List<LoanDto>>> GetUserLoans(int userId)
    {
        var loans = await _loanService.GetUserLoansAsync(userId);
        return Ok(loans);
    }
}
