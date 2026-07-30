using BookManagementSystem.Context;
using BookManagementSystem.Models;
using BookManagementSystem.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookDataService _bookDataService;
        private readonly BookContext _bookContext;
        public BooksController(IBookDataService bookDataService,BookContext bookContext)
        {
            _bookDataService = bookDataService;
            _bookContext = bookContext;
        }

        [HttpPost]
        public IActionResult Create(CreateBookRequest request)
        {
            var book = new Book
            {
                Title = request.Title,
                Author = request.Author,
                PublishYear = request.PublishYear,
                Genre = request.Genre,
                IsAvailable = request.IsAvailable
            };

            _bookDataService.Save(book);
            return CreatedAtAction(nameof(Create), new { id = book.Id }, book);
        }

        [HttpGet("search")]
        public  IActionResult GetByName ([FromQuery] string title)
        {
            
            if (string.IsNullOrWhiteSpace(title)|| title.Length <= 2)
            {
                return BadRequest("The search name must be at least 2 characters long.");
            }

            
            var book = _bookContext.Books.FirstOrDefault(b => EF.Functions.Like(b.Title, $"%{title.ToLower().Trim()}%"));



            if (book == null)
            {
                return NotFound($"No Book found matching the name '{title}'.");
            }

            return Ok(book);
        }

        [HttpDelete("{id}")]
        public bool DeleteById(int id)
        {
            var book = _bookContext.Books.Find(id);
            if(book==null)
                return false;
            _bookContext.Books.Remove(book);
            _bookContext.SaveChanges();
            return true;
        }

        [HttpGet]
        public IActionResult Read()
        {
            var books = _bookContext.Books.ToList();
            return Ok(books);
        }

        [HttpGet("Sort_By_Year")]
        public IActionResult SortByYear()
        {
            var books = _bookContext.Books.
                OrderByDescending(b=>b.PublishYear).
                ToList();

            return Ok(books);
        }
        [HttpGet("by_Genre")]
        public IActionResult SortByGenre([FromQuery]string genre)
        { 
        if (string.IsNullOrWhiteSpace(genre)|| genre.Length < 2)
            {
                return BadRequest("The search name must be at least 2 characters long.");
            }

            
            var books = _bookContext.Books.
                Where(b => EF.Functions
                .Like(b.Genre, $"{genre.ToLower().Trim()}%"))
                .ToList();



            if (books == null)
            {
                return NotFound($"No Book found matching the name '{genre}'.");
            }

                return Ok(books);
    }
}

}