using BooksApi.Application.DTOs;
using BooksApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoansController : ControllerBase
{
    private readonly ILoanService _loanService;

    public LoansController(ILoanService loanService)
    {
        _loanService = loanService;
    }

    [HttpPost]
    public async Task<ActionResult<LoanDto>> CreateLoan([FromBody] CreateLoanRequest request)
    {
        var loanDto = await _loanService.CreateLoanAsync(request);
        return CreatedAtAction(nameof(CreateLoan), new { id = loanDto.Id }, loanDto);
    }
}
