using Bookstore.Application.Dtos;
using Bookstore.Application.Validation;
using Xunit;

namespace Bookstore.Application.UnitTests.Validation;

public class AuthorUpdateValidatorTests
{
    private AuthorUpdateValidator AuthorUpdateValidator { get; }
        = new AuthorUpdateValidator();


    [Fact]
    public void Valid_AuthorCreate_PassesValidation()
    {
        //Arrange
        var authorUpdate = new AuthorUpdate(1, "Vasya", "Test");
        //Act
        var result = AuthorUpdateValidator.Validate(authorUpdate);
        //Assert
        Assert.True(result.IsValid);
    }
    [Fact]
    public void Validation_Error_For_Empty_FirstName()
    {
        //Arrange
        var authorUpdate = new AuthorUpdate(1, string.Empty, "Test");
        //Act
        var result = AuthorUpdateValidator.Validate(authorUpdate);
        //Assert
        Assert.Single(result.Errors);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error =>
        error.ErrorCode.Equals("NotEmptyValidator") &&
        error.PropertyName.Equals("FirstName"));

    }


    [Fact]
    public void Validation_Error_For_Empty_LastName()
    {
        //Arrange
        var authorUpdate = new AuthorUpdate(1, "Test", string.Empty);
        //Act
        var result = AuthorUpdateValidator.Validate(authorUpdate);
        //Assert
        Assert.Single(result.Errors);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error =>
        error.ErrorCode.Equals("NotEmptyValidator") &&
        error.PropertyName.Equals("LastName"));

    }



    [Fact]
    public void Validation_Error_For_To_Long_FirstName()
    {
        //Arrange
        var authorUpdate = new AuthorUpdate(1,
            @"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
            AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
            AAAAAAAAAAAAAAAAAAAA", "Test");
        //Act
        var result = AuthorUpdateValidator.Validate(authorUpdate);
        //Assert
        Assert.Single(result.Errors);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error =>
        error.ErrorCode.Equals("MaximumLengthValidator") &&
        error.PropertyName.Equals("FirstName"));

    }

    [Fact]
    public void Validation_Error_For_To_Long_LastName()
    {
        //Arrange
        var authorUpdate = new AuthorUpdate(1, "Test",
            @"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
            AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
            AAAAAAAAAAAAAAAAAAAA");
        //Act
        var result = AuthorUpdateValidator.Validate(authorUpdate);
        //Assert
        Assert.Single(result.Errors);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error =>
        error.ErrorCode.Equals("MaximumLengthValidator") &&
        error.PropertyName.Equals("LastName"));

    }

}
