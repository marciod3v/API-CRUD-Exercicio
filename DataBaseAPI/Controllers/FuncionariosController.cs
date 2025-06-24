using DataBaseAPI.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;


namespace DataBaseAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FuncionariosController : ApiController
    {
        readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["web-api"].ConnectionString;
        string logPath = ConfigurationManager.AppSettings["logPath"] + @"\logfuncionarios.txt";
        Repositories.FuncionarioRepository funcionarioRepository = new Repositories.FuncionarioRepository();
        // GET: api/Funcionarios

        [HttpGet]
        [Route("api/getAllFuncionarios")]
        public async Task<IHttpActionResult> Get()
        {
            List<Models.Funcionario> funcionarios = new List<Models.Funcionario>();
            try
            {
                funcionarios = await funcionarioRepository.GetAllFuncionarios();
                return Ok(funcionarios);
            }
            catch (Exception ex)
            {
                Logger.GetLog(ex,logPath);
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("api/getFuncionarioById/{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var func = await funcionarioRepository.GetFuncionarioById(id);
                if (func == null)
                {
                    return NotFound();
                }

                return Ok(func);
            }
            catch (Exception ex)
            {
                Logger.GetLog(ex,logPath);
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("api/getFuncionariosByName/{nome}")]

        public async Task<IHttpActionResult> Get(string nome)
        {
            try
            {
                var func = await funcionarioRepository.GetFuncionarioByName(nome);
                return Ok(func);
            }
            catch (Exception ex)
            {

                Logger.GetLog(ex,logPath);
                return InternalServerError();
            }
        }

        // POST: api/Funcionarios
        [HttpPost]
        [Route("api/funcionarios")]
        public async Task<IHttpActionResult> PostAsync([FromBody]Models.Funcionario funcionario)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("ERRO: Dados Incorretos");
                }

                if (await funcionarioRepository.CreateFuncionario(funcionario))
                {
                    return Ok("Registro inserido com sucesso");
                }

                return BadRequest();
            }
            catch (Exception ex)
            {

                Logger.GetLog(ex,logPath);
                return InternalServerError();
              
            }
        }

        // PUT: api/Funcionarios/5
        [HttpPut]
        [Route("api/putFuncionarios/{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody] Models.Funcionario funcionario)
        {
            if (id != funcionario.Codigo)
            {
                return NotFound();
            }

            try
            {
                if (await funcionarioRepository.UpdateFuncionario(funcionario))
                {
                    return Ok("Registro atualizado");
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                {
                    Logger.GetLog(ex,logPath);
                    return InternalServerError();
                }
            }
        }
        // DELETE: api/Funcionarios/5
        [HttpDelete]
        [Route("api/deleteFuncionarios/{id}")]
        public async Task<IHttpActionResult> DeleteAsync(int id)
        {
            try
            {
                if (await funcionarioRepository.DeleteFuncionario(id))
                {
                    return Ok("Registro deletado");
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                Logger.GetLog(ex, logPath);
                return InternalServerError();

            }
        }
    }
}
