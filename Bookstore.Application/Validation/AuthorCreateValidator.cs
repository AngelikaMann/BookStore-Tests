using Bookstore.Application.Dtos;
using FluentValidation;

namespace Bookstore.Application.Validation;

public class AuthorCreateValidator : AbstractValidator<AuthorCreate>
{
    public AuthorCreateValidator()
    {
        RuleFor(author => author.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(author => author.LastName).NotEmpty().MaximumLength(50);
    }
}
