using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Book;
using BookStoreApp.API.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<BooksController> _logger;
        private const string EntityName = Messages.BookEntityName;

        public BooksController(BookStoreDbContext context, IMapper mapper, ILogger<BooksController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Books
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<BookReadDto>>> GetBooks()
        {
            _logger.LogInformation(Messages.RequestInitiated, nameof(GetBooks));
            try
            {
                var books = await _context.Books
                    .Include(b => b.Author)
                    .ToListAsync();
                var result = _mapper.Map<IEnumerable<BookReadDto>>(books);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.ErrorPerformingAction, nameof(GetBooks));
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<BookReadDto>> GetBook(int id)
        {
            _logger.LogInformation(Messages.RequestInitiated, nameof(GetBook));
            try
            {
                var book = await _context.Books
                    .Include(b => b.Author)
                    .FirstOrDefaultAsync(b => b.Id == id);

                if (book == null)
                {
                    _logger.LogWarning(Messages.EntityNotFound, nameof(GetBook), EntityName, id);
                    return NotFound();
                }

                var bookReadDto = _mapper.Map<BookReadDto>(book);
                return bookReadDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.ErrorPerformingAction, nameof(GetBook));
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookUpdateDto bookDto)
        {
            _logger.LogInformation(Messages.RequestInitiated, nameof(PutBook));
            if (id != bookDto.Id)
            {
                _logger.LogWarning(Messages.IdMismatch, nameof(PutBook), EntityName, id, bookDto.Id);
                return BadRequest();
            }

            try
            {
                var book = await _context.Books.FindAsync(id);

                if (book == null)
                {
                    _logger.LogWarning(Messages.EntityNotFound, nameof(PutBook), EntityName, id);
                    return NotFound();
                }

                _mapper.Map(bookDto, book);

                _context.Entry(book).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await BookExists(id))
                {
                    _logger.LogWarning(Messages.EntityNotFound, nameof(PutBook), EntityName, id);
                    return NotFound();
                }
                _logger.LogError(ex, Messages.ConcurrencyError, nameof(PutBook), EntityName, id);
                return StatusCode(500, Messages.Error500Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.ErrorPerformingAction, nameof(PutBook));
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookReadDto>> PostBook(BookCreateDto bookDto)
        {
            _logger.LogInformation(Messages.RequestInitiated, nameof(PostBook));
            try
            {
                var book = _mapper.Map<Book>(bookDto);
                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();
                var readDto = _mapper.Map<BookReadDto>(book);

                return CreatedAtAction(nameof(GetBook), new { id = book.Id }, readDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.ErrorPerformingAction, nameof(PostBook));
                return StatusCode(500, Messages.Error500Message);
            }


        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            _logger.LogInformation(Messages.RequestInitiated, nameof(DeleteBook));
            try
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                {
                    _logger.LogWarning(Messages.EntityNotFound, nameof(DeleteBook), EntityName, id);
                    return NotFound();
                }

                _logger.LogInformation(Messages.DeletingEntity, nameof(DeleteBook), EntityName, id);
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.ErrorPerformingAction, nameof(DeleteBook));
                return StatusCode(500, Messages.Error500Message);
            }
        }

        private async Task<bool> BookExists(int id)
        {
            return await _context.Books.AnyAsync(e => e.Id == id);
        }
    }
}
