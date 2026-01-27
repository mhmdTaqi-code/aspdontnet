using BooksApi.Application.DTOs;
using BooksApi.Application.Interfaces;
using BooksApi.Domain.Entities;
using BooksApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Infrastructure.Services;

public class LoanService : ILoanService
{
    private readonly ApplicationDbContext _context;

    public LoanService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<LoanDto> CreateLoanAsync(CreateLoanRequest request)
    {
        var loan = new Loan
        {
            UserId = request.UserId,
            BookId = request.BookId,
            LoanDate = DateTime.UtcNow,
            ReturnDate = null
        };

        _context.Loans.Add(loan);
        await _context.SaveChangesAsync();

        // Load the book to get the title
        await _context.Entry(loan)
            .Reference(l => l.Book)
            .LoadAsync();

        return new LoanDto
        {
            Id = loan.Id,
            BookTitle = loan.Book.Title,
            LoanDate = loan.LoanDate,
            ReturnDate = loan.ReturnDate
        };
    }

    public async Task<List<LoanDto>> GetUserLoansAsync(int userId)
    {
        return await _context.Loans
            .Where(l => l.UserId == userId)
            .Include(l => l.Book)
            .Select(l => new LoanDto
            {
                Id = l.Id,
                BookTitle = l.Book.Title,
                LoanDate = l.LoanDate,
                ReturnDate = l.ReturnDate
            })
            .ToListAsync();
    }
}
