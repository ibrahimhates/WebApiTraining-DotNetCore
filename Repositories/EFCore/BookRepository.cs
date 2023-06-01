﻿using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public sealed class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext context) : base(context)
        {

        } 

        public void CreateOneBook(Book book) => Create(book);

        public void DeleteOneBook(Book book) => Delete(book);

        public async Task<PagedList<Book>> GetAllBooksAsync(BookParameters bookParameters,
            bool trackChanges)
        {
            var books =  await FindAll(trackChanges)
                .FilterBooks(bookParameters.minPrice, bookParameters.maxPrice)
                .Search(bookParameters.SearchTerm)
                .Sort(bookParameters.OrderBy)
                .ToListAsync(); 

            return PagedList<Book>.ToPagedList(books, 
                bookParameters.PageNumber,
                bookParameters.PageSize);
        }

        public async Task<List<Book>> GetAllBooksAsync(bool trackChanges)
        {
            var allbooks = await FindAll(trackChanges).OrderBy(b => b.Id).ToListAsync();
            return allbooks;
        }

        public async Task<IEnumerable<Book>> GetAllBooksWithDetailsAsync(bool trackChanges)
        {
            return await _context
                .Books
                .Include(b => b.Category)
                .OrderBy(b => b.Id)
                .ToListAsync();
        }

        public async Task<Book?> GetOneBookByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(b => b.Id==id, trackChanges)
            .SingleOrDefaultAsync();

        public void UpdateOneBook(Book book) => Update(book);
    }
}
