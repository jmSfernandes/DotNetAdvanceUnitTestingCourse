using Library.API.Controllers;
using Library.API.Data.Models;
using Library.API.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Library.Api.Test;

public class BookControllerTests
{
    private readonly BooksController _controller;
    private readonly IBookService _service;

    public BookControllerTests()
    {
        _service = new BookService();
        _controller = new BooksController(_service);
    }

    [Fact]
    public void GetAllTest()
    {
        var result = _controller.Get();
        
        var response = Assert.IsType<OkObjectResult>(result.Result);
        
        var list = Assert.IsType<List<Book>>(response.Value);
       

        Assert.Equal(5, list.Count);
    }

    [Theory]
    [MemberData(nameof(BookTestData.Data), MemberType = typeof(BookTestData))]
    public void GetByIdTest(string id)
    {
        var guid = new Guid(id);
        var result = _controller.Get(guid);
        
        var response = Assert.IsType<OkObjectResult>(result.Result);
        var book =Assert.IsType<Book>(response.Value);

        Assert.NotNull(book);
    }

    [Theory]
    [MemberData(nameof(BookTestData.DataFalse), MemberType = typeof(BookTestData))]
    public void GetByIdNotFoundTest(string id)
    {
        var guid = new Guid(id);
        var result = _controller.Get(guid);
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void AddBookTest()
    {
        var completeBook = new Book()
        {
            Title = "title",
            Description = "Description",
            Author = "Author"
        };

        var createResponse = _controller.Post(completeBook);

        var response = Assert.IsType<CreatedAtActionResult>(createResponse);
        
        var book = Assert.IsType<Book>(response.Value);

        Assert.NotNull(book);
        Assert.NotNull(book.Id);
        Assert.Equal(completeBook.Title, book.Title);
    }

    [Fact]
    public void AddInvalidBookTest()
    {
        var invalidBook = new Book()
        {
            Description = "Description",
            Author = "Author"
        };
        _controller.ModelState.AddModelError("Title", "Title is a required field");
        var invalidResponse = _controller.Post(invalidBook);
        Assert.IsType<BadRequestObjectResult>(invalidResponse);
    }


    [Theory]
    [MemberData(nameof(BookTestData.Data), MemberType = typeof(BookTestData))]
    public void DeleteByIdTest(string id)
    {
        var guid = new Guid(id);
        var result = _controller.Remove(guid);
        Assert.IsType<OkResult>(result);
        Assert.Equal(4, _service.GetAll().Count());
    }

    [Theory]
    [MemberData(nameof(BookTestData.DataFalse), MemberType = typeof(BookTestData))]
    public void DeleteByIdInvalidIdTest(string id)
    {
        var guid = new Guid(id);
        var result = _controller.Remove(guid);
        Assert.IsType<NotFoundResult>(result);
        Assert.Equal(5, _service.GetAll().Count());
    }
}