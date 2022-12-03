using Library.API.Data.Models;
using Library.API.Data.Services;
using LibraryApp.Controllers;
using LibraryApp.Data.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace LibraryApp.Test;

public class BooksControllerTests
{
    private BooksController _controller;
    private IBookService _bookService;
        
    public BooksControllerTests()
    {
        InitSetup();
    }

    private void InitSetup()
    {
        var mockRepo = new Mock<IBookService>();
        var data = new MockData();
        mockRepo.Setup(s => s.GetAll()).Returns(data.GetTestBookItems());
        mockRepo.Setup(s => s.GetById(It.IsAny<Guid>()))
            .Returns((Guid guid) => data.GetTestBookItems().FirstOrDefault(x => x.Id == guid));
        mockRepo.Setup(s => s.Remove(It.IsAny<Guid>())).Callback((Guid id) => data.RemoveItem(id));

        _bookService = mockRepo.Object;
        _controller = new BooksController(_bookService);
    }

    [Fact]
    public void IndexUnitTest()
    {
        var result = _controller.Index();
        var viewResult = Assert.IsType<ViewResult>(result);
        var books = Assert.IsType<List<Book>>(viewResult.Model);

        Assert.Equal(5, books.Count);
    }

    [Theory]
    [MemberData(nameof(BookTestData.Data), MemberType = typeof(BookTestData))]
    public void DetailsUnitTest(string id)
    {
        var guid = new Guid(id);
        var result = _controller.Details(guid);
        var viewResult = Assert.IsType<ViewResult>(result);
        var book = Assert.IsType<Book>(viewResult.Model);

        Assert.NotNull(book);
        Assert.Equal(guid, book.Id);
    }

    [Theory]
    [MemberData(nameof(BookTestData.DataFalse), MemberType = typeof(BookTestData))]
    public void DetailsNotFoundUnitTest(string id)
    {
        var guid = new Guid(id);
        var result = _controller.Details(guid);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void CreateUnitTest()
    {
        var book = new Book()
        {
            Title = "This is a title",
            Description = "Description",
            Author = "J. Fernandes"
        };
        var result = _controller.Create(book);
        Assert.IsType<RedirectToActionResult>(result);
    }

    [Fact]
    public void CreateInvalidFieldUnitTest()
    {
        var book = new Book()
        {
            Description = "Description",
            Author = "J. Fernandes"
        };
        _controller.ModelState.AddModelError("Title", "We need a title!");
        var result = _controller.Create(book);
        var response = Assert.IsType<BadRequestObjectResult>(result);
        var model = Assert.IsType<SerializableError>(response.Value);
        Assert.True(model.ContainsKey("Title"));
    }


    [Theory]
    [MemberData(nameof(BookTestData.Data), MemberType = typeof(BookTestData))]
    public void DeleteUnitTest(string id)
    {
        var guid = new Guid(id);
        var result = _controller.Delete(guid, null);
        var response = Assert.IsType<RedirectToActionResult>(result);
        var model = Assert.IsType<string>(response.ActionName);
        Assert.Equal("Index", model);
        Assert.Null(_bookService.GetAll().FirstOrDefault(x => x.Id == guid));
        //To restart the temp database for the rest of the tests;
        InitSetup(); 
    }
}