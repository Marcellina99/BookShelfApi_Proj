using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookSelfApi.Data
{
    public class BookSelfContext : DbContext 
    {
        public BookSelfContext(DbContextOptions options) :  base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}