using Bookstore.Application.Contracts;
using Bookstore.Application.Dtos;
using Bookstore.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Bookstore.Application
{
    public class ApplicationLogger<T> : IApplicationLogger<T>
    {
        public ILogger<T> Logger { get; }

        public ApplicationLogger(ILogger<T> logger)
        {
            Logger = logger;
        }

        public void LogCreateAuthorAsyncCalled(AuthorCreate authorCreate)
        {
            Logger.LogInformation("AuthorCreate called. {authorCreate}", authorCreate);
        }

        public void LogValidationErrorInCreateAuthor(ValidationException ex, AuthorCreate authorCreate)
        {
            Logger.LogError(ex, "Validation Error in CreateAuthor. {authorCreate}", authorCreate);
        }

        public void LogAuthorCreated(long id)
        {
            Logger.LogInformation("Author created{id}", id);
        }

        public void LogAuthorNotFound(long authorId)
        {
            Logger.LogError("Author not found inUpdateAuthor {authorId}", authorId);
        }

        public void LogAuthorUpdated(Author author)
        {
            Logger.LogInformation("Author updated. {author}", author);
        }

        public void LogUpdateAuthorAsyncCalled(AuthorUpdate authorUpdate)
        {
            Logger.LogInformation("AuthorUpdate called. {authorUpdate}", authorUpdate);
        }

        public void LogValidationErrorInUpdateAuthor(Exception ex, AuthorUpdate authorUpdate)
        {
            Logger.LogError(ex, "Validation Error in UpdateAuthor. {authorUpdate}", authorUpdate);
        }
    }
}
