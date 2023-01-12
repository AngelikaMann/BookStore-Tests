using Bookstore.Application.Dtos;
using Bookstore.Application.Validation;
using Xunit;

namespace Bookstore.Application.UnitTests.Validation;

public class BookUpdateValidatorTests
{
    private BookUpdateValidator BookUpdateValidator { get; }
        = new BookUpdateValidator();


    [Fact]
    public void Valid_BookUpdate_PassesValidation()
    {
        //Arrange
        var bookUpdate = new BookUpdate(1, "1234567891234", "Test", 1);
        //Act
        var result = BookUpdateValidator.Validate(bookUpdate);
        //Assert
        Assert.True(result.IsValid);
    }
    [Fact]
    public void Validation_Error_For_Empty_Title()
    {
        //Arrange
        var bookUpdate = new BookUpdate(1, "1234567891234", string.Empty, 1);
        //Act
        var result = BookUpdateValidator.Validate(bookUpdate);
        //Assert
        Assert.Single(result.Errors);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error =>
        error.ErrorCode.Equals("NotEmptyValidator") &&
        error.PropertyName.Equals("Title"));

    }

    [Fact]
    public void Validation_Error_For_To_Long_Title()
    {
        //Arrange
        var bookUpdate = new BookUpdate(1, "1234567891234",
            @"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa
            AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
            AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
            AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
            AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", 1);
        //Act
        var result = BookUpdateValidator.Validate(bookUpdate);
        //Assert
        Assert.Single(result.Errors);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error =>
        error.ErrorCode.Equals("MaximumLengthValidator") &&
        error.PropertyName.Equals("Title"));

    }
}
