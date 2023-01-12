using Bookstore.Application.Dtos;
using FluentValidation;

namespace Bookstore.Application.Validation;

public class AuthorUpdateValidator : AbstractValidator<AuthorUpdate>
{
    public AuthorUpdateValidator()
    {
        RuleFor(author => author.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(author => author.LastName).NotEmpty().MaximumLength(50);

    }
}
