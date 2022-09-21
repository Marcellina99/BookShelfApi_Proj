using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookSelfApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookSelfApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookShelfController : ControllerBase
    {
        private readonly BookSelfContext _bookShelfContext;

        public BookShelfController(BookSelfContext bookShelfContext)
        {
            _bookShelfContext = bookShelfContext;
        }


        // Get: api/Books
        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            return await _bookShelfContext.Books.ToListAsync();
        }


        // Get: api/Book/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookByIdAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = await _bookShelfContext.Books.FindAsync(id);
            if (book == null){
                return NotFound();
            }
            return Ok(book);
        }

        
        // Post: api/Book
        [HttpPost]
        public async Task<ActionResult<Book>> CreateBookAsync(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _bookShelfContext.AddAsync(book);
            await _bookShelfContext.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateBookAsync), new {id = book.Id}, book);
        }


        // Put: api/Book/update
        [HttpPut("{id}")]
        public async Task<ActionResult> PutBookAsync(string id, [FromBody] Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _bookShelfContext.Entry(book).State = EntityState.Modified;

            try
            {
                await _bookShelfContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else {
                    throw;
                }
            }
            
            return NoContent();
        }

        private bool BookExists(string id)
        {
            return (_bookShelfContext.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // Delete a book
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(string id)
        {
            if (_bookShelfContext.Books == null)
            {
                return NotFound();
            }

            var book = await _bookShelfContext.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _bookShelfContext.Books?.Remove(book);
            await _bookShelfContext.SaveChangesAsync();

            return NoContent();
        }
    }
}