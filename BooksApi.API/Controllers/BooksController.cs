using BooksApi.Domain.Entities;
using BooksApi.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BooksController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Book>> CreateBook([FromBody] CreateBookRequest request)
    {
        var book = new Book
        {
            Title = request.Title
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateBook), new { id = book.Id }, book);
    }

    public class CreateBookRequest
    {
        public string Title { get; set; } = string.Empty;
    }
}
