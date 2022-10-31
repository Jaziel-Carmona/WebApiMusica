using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiMusica.Entidades;
using WebApiMusica.Services;
using WebApiMusica.Filtros;


namespace WebApiMusica.Controllers
{
    [ApiController]
    [Route("api/musica")]
    //[Authorize]
    public class MusicaController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IService service;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ILogger<MusicaController> logger;

        public MusicaController(ApplicationDbContext dbContext, IService service, 
            ServiceTransient serviceTransient, ServiceScoped serviceScoped, 
            ServiceSingleton serviceSingleton, ILogger<MusicaController> logger)
        {
            this.dbContext = dbContext;
            this.service = service;
            this.serviceTransient = serviceTransient;
            this.serviceScoped = serviceScoped;
            this.serviceSingleton = serviceSingleton;
            this.logger = logger;
        }

        [HttpGet("GUID")]
        [ResponseCache(Duration = 5)]
        [ServiceFilter(typeof(FiltroDeAccion))]
        public ActionResult ObtenerGuid()
        {
            throw new NotImplementedException();
            logger.LogInformation("Durante la ejecucion. ");
            return Ok(new
            {
                ArtistasControllerTransient = serviceTransient.guid,
                ServiceA_Transient = service.GetTransient(),
                ArtistasControllerScoped = serviceScoped.guid,
                ServiceA_Scoped = service.GetScoped(),
                ArtistasControllerSingleton = serviceSingleton.guid,
                ServiceA_Singleton = service.GetSingleton()
            });
        }

        [HttpGet("/Listado")] //  /Listado
        [HttpGet("Listado")] //  api/musica/Listado
        [HttpGet] // api/musica
        //[ResponseCache(Duration = 5)]
        //[Authorize]
        public async Task<ActionResult<List<Artista>>> Get()  
        {
            throw new NotImplementedException();
            logger.LogInformation("Se obtiene el listado de alumnos");
            logger.LogWarning("Mensaje de prueba warning");
            service.EjecutarJob();
            return await dbContext.Artistas.Include(x => x.canciones).ToListAsync();
        }

        [HttpGet("primero")] //api/alumnos/primero
        public async Task<ActionResult<Artista>> PrimerArtista([FromHeader] int valor, [FromQuery] string artista, [FromQuery] int artistaId) 
        {
            throw new NotImplementedException();
            return await dbContext.Artistas.FirstOrDefaultAsync();
        }

        [HttpGet("primero2")]
        public ActionResult<Artista> PrimerArtistaD()
        {
            return new Artista() { Nombre = "DOS" };
        }

        [HttpGet("{id:int}/{param?}")] 
        public async Task<ActionResult<Artista>> Get(int id)
        {
            var artista = await dbContext.Artistas.FirstOrDefaultAsync(x => x.Id == id);

            if (artista == null)
            {
                return NotFound();
            }

            return artista;
        }

        [HttpGet("obtenerArtista/{nombre}")]
        public async Task<ActionResult<Artista>> Get([FromRoute] string nombre)
        {
            var artista = await dbContext.Artistas.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));

            if (artista == null)
            {
                logger.LogError("No se encuentra el artista. ");
                return NotFound();
            }

            return artista;
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Artista artista)
        {
            //Ejemplo para validar desde el controlador con la BD con ayuda del dbContext

            var existeArtistaMismoNombre = await dbContext.Artistas.AnyAsync(x => x.Nombre == artista.Nombre);

            if (existeArtistaMismoNombre)
            {
                return BadRequest("Ya existe un artista con el nombre");
            }

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
