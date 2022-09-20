using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiMusica.Entidades;

namespace WebApiMusica.Controllers
{
    [ApiController]
    [Route("api/musica")]
    public class MusicaController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public MusicaController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Artista>>> Get()
        {
            return await dbContext.Artistas.Include(x => x.canciones).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Artista artista)
        {
            dbContext.Add(artista);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Artista artista, int id)
        {
            if(artista.Id != id)
            {
                return BadRequest("El id del artista no coincide con el establecido en la url");
            }

            dbContext.Update(artista);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Artistas.AnyAsync(x => x.Id == id);
            if (!exist) 
            {
                return NotFound();
                //return BadRequest("El recurso no fue encontrado");
            }

            dbContext.Remove(new Artista()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
