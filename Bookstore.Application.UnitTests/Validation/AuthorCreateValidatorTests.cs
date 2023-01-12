using Bookstore.Application.Dtos;
using Bookstore.Application.Validation;
using Xunit;

namespace Bookstore.Application.UnitTests.Validation;

public class AuthorCreateValidatorTests
{
    private AuthorCreateValidator AuthorCreateValidator { get; }
        = new AuthorCreateValidator();


    [Fact]
    public void Valid_AuthorCreate_PassesValidation()
    {
        //Arrange
        var authorCreate = new AuthorCreate("Vasya", "Test");
        //Act
        var result = AuthorCreateValidator.Validate(authorCreate);
        //Assert
        Assert.True(result.IsValid);
    }
    [Fact]
    public void Validation_Error_For_Empty_FirstName()
    {
        //Arrange
        var authorCreate = new AuthorCreate(string.Empty, "Test");
        //Act
        var result = AuthorCreateValidator.Validate(authorCreate);
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
        var authorCreate = new AuthorCreate("Test", string.Empty);
        //Act
        var result = AuthorCreateValidator.Validate(authorCreate);
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
        var authorCreate = new AuthorCreate(
            @"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
            AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
            AAAAAAAAAAAAAAAAAAAA", "Test");
        //Act
        var result = AuthorCreateValidator.Validate(authorCreate);
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
        var authorCreate = new AuthorCreate("Test",
            @"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
            AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
            AAAAAAAAAAAAAAAAAAAA");
        //Act
        var result = AuthorCreateValidator.Validate(authorCreate);
        //Assert
        Assert.Single(result.Errors);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error =>
        error.ErrorCode.Equals("MaximumLengthValidator") &&
        error.PropertyName.Equals("LastName"));

    }

}
