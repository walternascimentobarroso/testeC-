using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using back_end.Models;

namespace back_end.Controllers
{
    [Route("api/[controller]")]
    public class FuncionarioController : Controller
    {
        // GET api/Funcionario
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new FuncionarioQuery(db);
                var result = await query.LatestPostsAsync();
                return new OkObjectResult(result);
            }
        }

        // GET api/Funcionario/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new FuncionarioQuery(db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                return new NotFoundResult();
                return new OkObjectResult(result);
            }
        }

        // POST api/Funcionario
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Funcionario body)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                body.Db = db;
                await body.InsertAsync();
                return new OkObjectResult(body);
            }
        }

        // PUT api/Funcionario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody]Funcionario body)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new FuncionarioQuery(db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                    return new NotFoundResult();
                result.nome = body.nome;
                await result.UpdateAsync();
                return new OkObjectResult(result);
            }
        }

        // DELETE api/Funcionario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new FuncionarioQuery(db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                    return new NotFoundResult();
                await result.DeleteAsync();
                return new OkResult();
            }
        }

        // DELETE api/Funcionario
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new FuncionarioQuery(db);
                await query.DeleteAllAsync();
                return new OkResult();
            }
        }
    }
}
