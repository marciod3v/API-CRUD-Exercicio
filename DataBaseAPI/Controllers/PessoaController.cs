using DataBaseAPI.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace DataBaseAPI.Controllers
{
    public class PessoaController : ApiController
    {
        readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["web-api"].ConnectionString;
        string logPath = ConfigurationManager.AppSettings["logPath"] + @"\logPessoa.txt";
        Repositories.PessoaRepository pessoaRepository = new Repositories.PessoaRepository();
        
        // GET: api/Pessoa

        [HttpGet]
        [Route("api/pessoa")]
        public async Task<IHttpActionResult> GetAsync()
        {
            List<Models.Pessoa> pessoas = new List<Models.Pessoa>();

            try
            {
                pessoas = await pessoaRepository.GetAllPersons();
                return Ok(pessoas);
            }
            catch (Exception ex)
            {

                Logger.GetLog(ex, logPath);
                return InternalServerError();
            }
        }

        // GET: api/Pessoa/5
        [HttpGet]
        [Route("api/pessoa/{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                Models.Pessoa pessoa = await pessoaRepository.GetPessoaById(id);
                if (pessoa == null)
                {
                    return NotFound();                    
                }

                return Ok(pessoa);
            }
            catch (Exception ex)
            {

                Logger.GetLog(ex, logPath);
                return InternalServerError();
            }
        }

        // POST: api/Pessoa

        [HttpPost]
        [Route("api/pessoa")]
        public async Task<IHttpActionResult> Post([FromBody]Models.Pessoa pessoa)
        {
            try
            {
                if (await pessoaRepository.CreatePessoa(pessoa))
                {
                    return Ok("Registro inserido");
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                Logger.GetLog(ex, logPath);
                return InternalServerError();
            }
        }

        // PUT: api/Pessoa/5

        [HttpPut]
        [Route("api/pessoa/{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody] Models.Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                return NotFound();
            }

            try
            {
                if (await pessoaRepository.UpdatePessoa(id,pessoa))
                {
                    return Ok("Registro atualizado");
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                Logger.GetLog(ex, logPath);
                return InternalServerError();
            }
        }

        // DELETE: api/Pessoa/5
        [HttpDelete]
        [Route("api/pessoa/{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                if (await pessoaRepository.DeletePessoa(id))
                {
                    return Ok("Registro deletado");
                }

                return NotFound();
            }
            catch (Exception ex)
            {

                Logger.GetLog(ex, logPath);
                return InternalServerError();
            }
        }
    }
}
