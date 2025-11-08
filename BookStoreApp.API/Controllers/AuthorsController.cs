using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Author;
using BookStoreApp.API.Static;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorsController> _logger;
        private const string EntityName = Messages.AuthorEntityName;

        public AuthorsController(BookStoreDbContext context, IMapper mapper, ILogger<AuthorsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Authors 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorReadDto>>> GetAuthors()
        {
            _logger.LogInformation(Messages.RequestInitiated, nameof(GetAuthors));
            try
            {
                var authors = await _context.Authors.ToListAsync();
                var result = _mapper.Map<IEnumerable<AuthorReadDto>>(authors);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.ErrorPerformingAction, nameof(GetAuthors));
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorReadDto>> GetAuthor(int id)
        {
            _logger.LogInformation(Messages.RequestInitiated, nameof(GetAuthor));
            try
            {
                var author = await _context.Authors.FindAsync(id);

                if (author == null)
                {
                    _logger.LogWarning(Messages.EntityNotFound, nameof(GetAuthor), EntityName, id);
                    return NotFound();
                }

                var authorReadDto = _mapper.Map<AuthorReadDto>(author);
                return authorReadDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.ErrorPerformingAction, nameof(GetAuthor));
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // PUT: api/Authors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDto authorDto)
        {
            _logger.LogInformation(Messages.RequestInitiated, nameof(PutAuthor));

            if (id != authorDto.Id)
            {
                _logger.LogWarning(Messages.IdMismatch, nameof(PutAuthor), EntityName, id, authorDto.Id);
                return BadRequest();
            }

            try
            {
                var author = await _context.Authors.FindAsync(id);

                if (author == null)
                {
                    _logger.LogWarning(Messages.EntityNotFound, nameof(PutAuthor), EntityName, id);
                    return NotFound();
                }

                _mapper.Map(authorDto, author);
                _context.Entry(author).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await AuthorExists(id))
                {
                    _logger.LogWarning(Messages.EntityNotFound, nameof(PutAuthor), EntityName, id);
                    return NotFound();
                }

                _logger.LogError(ex, Messages.ConcurrencyError, nameof(PutAuthor), EntityName, id);
                return StatusCode(500, Messages.Error500Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.ErrorPerformingAction, nameof(PutAuthor));
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // POST: api/Authors
        [HttpPost]
        public async Task<ActionResult<AuthorReadDto>> PostAuthor(AuthorCreateDto authorDto)
        {
            _logger.LogInformation(Messages.RequestInitiated, nameof(PostAuthor));
            try
            {
                var author = _mapper.Map<Author>(authorDto);

                await _context.Authors.AddAsync(author);
                await _context.SaveChangesAsync();

                var readDto = _mapper.Map<AuthorReadDto>(author);
                return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, readDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.ErrorPerformingAction, nameof(PostAuthor));
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            _logger.LogInformation(Messages.RequestInitiated, nameof(DeleteAuthor));
            try
            {
                var author = await _context.Authors.FindAsync(id);
                if (author == null)
                {
                    _logger.LogWarning(Messages.EntityNotFound, nameof(DeleteAuthor), EntityName, id);
                    return NotFound();
                }

                _logger.LogInformation(Messages.DeletingEntity, nameof(DeleteAuthor), EntityName, id);
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.ErrorPerformingAction, nameof(DeleteAuthor));
                return StatusCode(500, Messages.Error500Message);
            }
        }

        private async Task<bool> AuthorExists(int id)
        {
            return await _context.Authors.AnyAsync(e => e.Id == id);
        }
    }
}
