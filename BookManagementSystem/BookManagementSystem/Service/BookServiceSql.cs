using BookManagementSystem.Context;
using BookManagementSystem.Models;

namespace BookManagementSystem.Service
{
    public class BookServiceSql : IBookDataService
    {
        private readonly BookContext _context;
        public BookServiceSql(BookContext context)
        {
            _context = context;
        }

        public List<Book> Load()
        {
            return _context.Books.ToList();
        }

        public void Save(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }
        public bool Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
                return false;
            _context.Books.Remove(book);
            _context.SaveChanges();
            return true;
        }

        public bool Update(int id, Book updatedBook)
        {
            var book = _context.Books.Find(id);
            if (book is null)
                return false;

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.PublishYear = updatedBook.PublishYear;
            book.IsAvailable = updatedBook.IsAvailable;
            
            _context.SaveChanges();
            return true;
        }
    }
}
