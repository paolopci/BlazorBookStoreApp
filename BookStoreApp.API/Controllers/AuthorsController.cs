using BookStoreApp.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        // Contesto EF Core usato per interrogare e aggiornare gli autori nel database.
        private readonly BookStoreDbContext _context;

        // Il contesto viene fornito da DI; da qui derivano tutte le query degli endpoint.
        public AuthorsController(BookStoreDbContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet]
        // Restituisce l'intero catalogo di autori.
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            return await _context.Authors.ToListAsync();
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        // Recupera un singolo autore, se presente.
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        // Aggiorna l'autore corrispondente all'id verificando che il payload sia coerente.
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AuthorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        // Inserisce un nuovo autore e restituisce il 201 con la risorsa creata.
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        // Elimina l'autore richiesto se esiste.
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Metodo di supporto per controllare l'esistenza di un autore durante gli aggiornamenti.
        private async Task<bool> AuthorExists(int id)
        {
            return  await _context.Authors.AnyAsync(e => e.Id == id);
        }
    }
}
