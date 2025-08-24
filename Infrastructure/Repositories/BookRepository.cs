using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BookRepository: IBookRepository
    {
        private readonly AppDbContext _context;
        private readonly IMemoryCache _cache;

        public BookRepository(AppDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task AddAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
            TimeSpan _threshold = TimeSpan.FromMinutes(10);

            if (!_cache.TryGetValue("DataBook", out IEnumerable<Book>? Data))
            {

                Data = await _context.Books.ToListAsync();
                _cache.Set("DataBook", Data, _threshold);

            }
            return Data;
    }
}
}
