using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using back_end.Models;

namespace back_end.Controllers
{
    [Route("api/[controller]")]
    public class DescontoController : Controller
    {
        // GET api/Desconto
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new DescontoQuery(db);
                var result = await query.LatestPostsAsync();
                return new OkObjectResult(result);
            }
        }

        // GET api/Desconto/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new DescontoQuery(db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                return new NotFoundResult();
                return new OkObjectResult(result);
            }
        }

        // POST api/Desconto
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Desconto body)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                body.Db = db;
                await body.InsertAsync();
                return new OkObjectResult(body);
            }
        }

        // PUT api/Desconto/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody]Desconto body)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new DescontoQuery(db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                    return new NotFoundResult();
                result.precototal = body.precototal;
                await result.UpdateAsync();
                return new OkObjectResult(result);
            }
        }

        // DELETE api/Desconto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new DescontoQuery(db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                    return new NotFoundResult();
                await result.DeleteAsync();
                return new OkResult();
            }
        }

        // DELETE api/Desconto
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new DescontoQuery(db);
                await query.DeleteAllAsync();
                return new OkResult();
            }
        }
    }
}
