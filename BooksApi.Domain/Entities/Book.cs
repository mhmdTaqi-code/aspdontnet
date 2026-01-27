namespace BooksApi.Domain.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    
    // Navigation property
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
