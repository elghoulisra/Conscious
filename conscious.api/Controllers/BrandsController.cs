using conscious.api.Data;
using conscious.api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace conscious.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly BrandsDbContext _context;

        public BrandsController(BrandsDbContext context)
        {
            _context = context;
        }

        // GET: api/brands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            return await _context.Brands.ToListAsync();
        }

        // GET: api/brands/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            return brand;
        }

        // POST: api/brands
        [HttpPost]
        public async Task<ActionResult<Brand>> PostBrand(Brand brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBrand), new { id = brand.Id }, brand);
        }

        // PUT: api/brands/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrand(int id, Brand brand)
        {
            if (id != brand.Id)
            {
                return BadRequest();
            }

            _context.Entry(brand).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandExists(id))
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

        // DELETE: api/brands/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();

            return NoContent();
        }

  // POST api/brands/{brandId}/vote
        [HttpPost("{brandId}/vote")]
        public async Task<IActionResult> VoteForBoycott(int brandId, [FromBody] VoteRequest voteRequest)
        {
            if (voteRequest == null || string.IsNullOrEmpty(voteRequest.Reason))
            {
                return BadRequest("La raison du boycott est obligatoire.");
            }

            // Rechercher la marque
            var brand = await _context.Brands.FindAsync(brandId);

            if (brand == null)
            {
                return NotFound($"Marque avec ID {brandId} non trouvée.");
            }

            // Logique de vote, ici nous marquons la marque comme boycottée
            brand.IsBoycotted = true;  // Marque la marque comme boycottée
            brand.Reason = voteRequest.Reason;  // Assigne la raison du boycott

            // Sauvegarder les changements dans la base de données
            _context.Brands.Update(brand);
            await _context.SaveChangesAsync();

            // Retourner une réponse de succès
            return Ok(new { message = "Votre vote a été pris en compte." });
        }
        private bool BrandExists(int id)
        {
            return _context.Brands.Any(e => e.Id == id);
        }
    }
}
