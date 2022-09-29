using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiMusica.Entidades;

namespace WebApiMusica.Controllers
{
    [ApiController]
    [Route("api/musica")]
    public class MusicaController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public MusicaController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // [HttpGet("/Listado")] /Listado
        // [HttpGet("Listado")] api/musica/Listado
        [HttpGet] // api/musica
        public async Task<ActionResult<List<Artista>>> Get()
        {
            return await dbContext.Artistas.Include(x => x.canciones).ToListAsync();
        }

        [HttpGet("primero")] //api/alumnos/primero
        public async Task<ActionResult<Artista>> PrimerArtista([FromHeader] int valor, [FromQuery] string artista, [FromQuery] int artistaId)
        {
            return await dbContext.Artistas.FirstOrDefaultAsync();
        }

        [HttpGet("primero2")]
        public ActionResult<Artista> PrimerArtistaD()
        {
            return new Artista() { Nombre = "DOS" };
        }

        [HttpGet("{id:int}/{param=Dan Reynolds}")] 
        public async Task<ActionResult<Artista>> Get(int id, string param)
        {
            var artista = await dbContext.Artistas.FirstOrDefaultAsync(x => x.Id == id);

            if (artista == null)
            {
                return NotFound();
            }

            return artista;
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<Artista>> Get([FromRoute] string nombre)
        {
            var artista = await dbContext.Artistas.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));

            if (artista == null)
            {
                return NotFound();
            }

            return artista;
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Artista artista)
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
