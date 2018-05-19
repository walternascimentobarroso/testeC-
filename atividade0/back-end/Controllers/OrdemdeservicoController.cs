using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using back_end.Models;

namespace back_end.Controllers
{
    [Route("api/[controller]")]
    public class OrdemdeservicoController : Controller
    {
        // GET api/Ordemdeservico
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new OrdemdeservicoQuery(db);
                var result = await query.LatestPostsAsync();
                return new OkObjectResult(result);
            }
        }

        // GET api/Ordemdeservico/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new OrdemdeservicoQuery(db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                return new NotFoundResult();
                return new OkObjectResult(result);
            }
        }

        // POST api/Ordemdeservico
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Ordemdeservico body)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                body.Db = db;
                await body.InsertAsync();
                return new OkObjectResult(body);
            }
        }

        // PUT api/Ordemdeservico/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody]Ordemdeservico body)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new OrdemdeservicoQuery(db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                    return new NotFoundResult();
                result.descricao = body.descricao;
                await result.UpdateAsync();
                return new OkObjectResult(result);
            }
        }

        // DELETE api/Ordemdeservico/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new OrdemdeservicoQuery(db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                    return new NotFoundResult();
                await result.DeleteAsync();
                return new OkResult();
            }
        }

        // DELETE api/Ordemdeservico
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new OrdemdeservicoQuery(db);
                await query.DeleteAllAsync();
                return new OkResult();
            }
        }
    }
}
