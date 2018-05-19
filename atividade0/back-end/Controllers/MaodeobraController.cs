using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using back_end.Models;

namespace back_end.Controllers
{
    [Route("api/[controller]")]
    public class MaodeobraController : Controller
    {
        // GET api/Maodeobra
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new MaodeobraQuery(db);
                var result = await query.LatestPostsAsync();
                return new OkObjectResult(result);
            }
        }

        // GET api/Maodeobra/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new MaodeobraQuery(db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                return new NotFoundResult();
                return new OkObjectResult(result);
            }
        }

        // POST api/Maodeobra
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Maodeobra body)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                body.Db = db;
                await body.InsertAsync();
                return new OkObjectResult(body);
            }
        }

        // PUT api/Maodeobra/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody]Maodeobra body)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new MaodeobraQuery(db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                    return new NotFoundResult();
                result.descricao = body.descricao;
                await result.UpdateAsync();
                return new OkObjectResult(result);
            }
        }

        // DELETE api/Maodeobra/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new MaodeobraQuery(db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                    return new NotFoundResult();
                await result.DeleteAsync();
                return new OkResult();
            }
        }

        // DELETE api/Maodeobra
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new MaodeobraQuery(db);
                await query.DeleteAllAsync();
                return new OkResult();
            }
        }
    }
}
