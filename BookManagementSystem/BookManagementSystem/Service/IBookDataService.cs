using BookManagementSystem.Models;

namespace BookManagementSystem.Service
{
    public interface IBookDataService
    {
        void Save(Book book);
        List<Book> Load();
    }
}
