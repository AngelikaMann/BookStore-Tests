using AutoMapper;
using Bookstore.Application.Contracts;
using Bookstore.Application.Dtos;
using Bookstore.Application.Exceptions;
using Bookstore.Application.Services;
using Bookstore.Application.Validation;
using Bookstore.Domain.Entities;
using FluentValidation;
using Moq;
using Xunit;

namespace Bookstore.Application.UnitTests.Services;

public class AuthorUpdateServiceTests
{
    private AuthorUpdateValidator Validator { get; }
    private IMapper Mapper { get; }

    public AuthorUpdateServiceTests()
    {
        Mapper = new MapperConfiguration(cfg =>
       cfg.AddMaps(typeof(DtoEntityMapperProfile))).CreateMapper();
        Validator = new AuthorUpdateValidator();
    }

    [Fact]
    public async Task Author_Updated()
    {
        //Arrange
        var author = new Author();

        var authorUpdate = new AuthorUpdate(1, "Test", "Test");
        var authorRepositoryMock = new Mock<IAuthorRepository>();
        authorRepositoryMock.Setup(mock => mock.GetAuthorByIdAsync(1))
            .ReturnsAsync(author);

        var applicationLoggerMock = new Mock<IApplicationLogger<AuthorUpdateService>>();
        var authorUpdateService = new AuthorUpdateService(authorRepositoryMock.Object,
    Mapper, Validator, applicationLoggerMock.Object);

        //Act
        await authorUpdateService.UpdateAuthorAsync(authorUpdate);
        //Assert
        authorRepositoryMock.Verify(mock => mock.UpdateAsync(), Times.Once);
        applicationLoggerMock.Verify(mock =>
        mock.LogUpdateAuthorAsyncCalled(authorUpdate), Times.Once);
        applicationLoggerMock.Verify(mock =>
       mock.LogAuthorUpdated(author), Times.Once);

    }
    [Fact]

    public async Task AuthorNotFoundException_For_Non_Existend_IdAsync()
    {
        //Arrange
        var authorUpdate = new AuthorUpdate(1, "Test", "Test");
        var authorRepositoryMock = new Mock<IAuthorRepository>();


        var applicationLoggerMock = new Mock<IApplicationLogger<AuthorUpdateService>>();
        var authorUpdateService = new AuthorUpdateService(authorRepositoryMock.Object,
            Mapper, Validator, applicationLoggerMock.Object);
        //Act
        try
        {
            await authorUpdateService.UpdateAuthorAsync(authorUpdate);
            throw new Exception("Supposed to throw AuthorNotFoundException");
        }
        catch (AuthorNotFoundException)
        {
            //Assert

            applicationLoggerMock.Verify(mock =>
             mock.LogUpdateAuthorAsyncCalled(authorUpdate), Times.Once);
            applicationLoggerMock.Verify(mock =>
            mock.LogAuthorNotFound(authorUpdate.AuthorId), Times.Once);
        }
    }

    [Fact]

    public async Task ValidationException_For_Invalid_AuthorUpdate()
    {
        //Arrange
        var authorUpdate = new AuthorUpdate(1, string.Empty, "Test");
        var authorRepositoryMock = new Mock<IAuthorRepository>();


        var applicationLoggerMock = new Mock<IApplicationLogger<AuthorUpdateService>>();
        var authorUpdateService = new AuthorUpdateService(authorRepositoryMock.Object,
            Mapper, Validator, applicationLoggerMock.Object);
        //Act
        try
        {
            await authorUpdateService.UpdateAuthorAsync(authorUpdate);
            throw new Exception("Supposed to throw AuthorNotFoundException");
        }
        catch (ValidationException ex)
        {
            //Assert

            applicationLoggerMock.Verify(mock =>
             mock.LogUpdateAuthorAsyncCalled(authorUpdate), Times.Once);
            applicationLoggerMock.Verify(mock =>
            mock.LogValidationErrorInUpdateAuthor(ex, authorUpdate), Times.Once);
        }
    }

}
