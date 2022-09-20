using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiMusica.Entidades;

namespace WebApiMusica.Controllers
{
    [ApiController]
    [Route("api/canciones")]
    public class CancionesController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public CancionesController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cancion>>> GetAll()
        {
            return await dbContext.Canciones.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Cancion>> GetById(int id)
        {
            return await dbContext.Canciones.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Cancion canciones)
        {
            var existeArtista = await dbContext.Artistas.AnyAsync(x => x.Id == canciones.ArtistaId);

            if(!existeArtista)
            {
                return BadRequest($"No existe el artista con el id: {canciones.ArtistaId}");
            }

            dbContext.Add(canciones);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Cancion canciones, int id)
        {
            var exist = await dbContext.Canciones.AnyAsync(x =>x.Id == id);

            if(!exist)
            {
                return NotFound("La clase especifica no existe. ");
            }

            if(canciones.Id != id)
            {
                return BadRequest("El id de la cancion no coincide con el url establecido en la url. ");
            }

            dbContext.Update(canciones);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Canciones.AnyAsync(x => x.Id == id);
            if(!exist)
            {
                return NotFound("El recurso no fue encontrado. ");
            }

            dbContext.Remove(new Cancion { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
