namespace Bookstore.Application.Dtos;

public record AuthorUpdate(
    long AuthorId,
    string FirstName,
    string LastName);