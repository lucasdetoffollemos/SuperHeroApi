using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {

        private readonly DataContext _context;
        public SuperHeroController(DataContext dataContext)
        {
            _context = dataContext;
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }


        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeroes()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetHero(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);

            if (hero == null)
                return BadRequest("Hero not found!");

            return Ok(hero);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(int id, SuperHero newSuperHero)
        {

            var hero = await _context.SuperHeroes.FindAsync(id);

            if (hero == null)
                return BadRequest("Hero not found!");

           
            hero.Name = newSuperHero.Name;
            hero.FirstName = newSuperHero.FirstName;
            hero.LastName = newSuperHero.LastName;
            hero.Place = newSuperHero.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {

            var hero = await _context.SuperHeroes.FindAsync(id);

            if (hero == null)
                return BadRequest("Hero not found!");


            _context.SuperHeroes.Remove(hero);

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }








    }
     
}
