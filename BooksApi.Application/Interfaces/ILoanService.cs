using BooksApi.Application.DTOs;

namespace BooksApi.Application.Interfaces;

public interface ILoanService
{
    Task<LoanDto> CreateLoanAsync(CreateLoanRequest request);
    Task<List<LoanDto>> GetUserLoansAsync(int userId);
}
