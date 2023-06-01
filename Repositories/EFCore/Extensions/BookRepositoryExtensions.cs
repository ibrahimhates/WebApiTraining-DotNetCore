using Entities.Models;
using Repositories.EFCore.Extensions;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace Repositories.EFCore
{
    public static class BookRepositoryExtensions
    {
        public static IQueryable<Book> FilterBooks(this IQueryable<Book> books,
            uint minPrice, uint maxPrice) =>
        books.Where(b =>
            (b.Price >= minPrice) &&
            (b.Price <= maxPrice));
        public static IQueryable<Book> Search(this IQueryable<Book> books,
            string searchTerm)
        {
            if(string.IsNullOrWhiteSpace(searchTerm))
                return books;

            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return books
                .Where(b => b.Title.ToLower().Contains(lowerCaseTerm));
        }
        public static IQueryable<Book> Sort(this IQueryable<Book> books,
            string orderBy)
        {
            if(string.IsNullOrEmpty(orderBy)) 
                return books.OrderBy(b => b.Id);

            var orderByQuery = OrderQueryBuilder.CreateOrderQuery<Book>(orderBy);

            if(orderByQuery is null)
                return books.OrderBy(b => b.Id);

            return books.OrderBy(orderByQuery);
        }
    }
}
