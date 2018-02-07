using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookEditorDemo.Models
{
    // I've chosen to use Repository pattern for sake of this demo, 
    // though it has some long-term flaws.
    public interface IBooksRepository
    {
        Book GetBook(int id);

        IEnumerable<BookInfo> GetBooks(PaginationData page);

        void AddBook(Book book);

        void RemoveBook(int id);

        void UpdateBook(Book book);
    }
}
