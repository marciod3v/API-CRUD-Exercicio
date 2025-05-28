using DataBaseAPI.Models;
using DataBaseAPI.Repositories;
using DataBaseAPI.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Routing;

namespace DataBaseAPI.Controllers
{
    public class CarrosController : ApiController
    {
        readonly string logPath = ConfigurationManager.AppSettings["logPath"] + @"\logCarros.txt";
        CarroRepository CarroRepository = new CarroRepository();

        public CarrosController()
        {
            
        }

        [HttpGet]
        [Route("api/carros")]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
               List<Models.Carro> carros =  await CarroRepository.GetAllCarros();
               return Ok(carros);
            }
            catch (Exception ex)
            {
                Logger.GetLog(ex,logPath);
                return InternalServerError();
            }
        }


        // GET: api/Carros/5
        [HttpGet]
        [Route("api/carros/{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {

            try
            {
                Models.Carro carro = await CarroRepository.GetCarroById(id);
                return Ok(carro);
            }
            catch (Exception ex)
            {
                Logger.GetLog(ex, logPath);
                return InternalServerError();
            }
        }


        [HttpGet]
        [Route("api/carros/getbyname/{nome}")]
        public async Task<IHttpActionResult> GetByName(string nome)
        {
            List<Carro> carros = new List<Carro>();

            if (nome.Length < 3)
            {
                return BadRequest("Digite no mínimo 3 caracteres");
            }

            try
            {
                carros = await CarroRepository.GetCarroByName(nome);
                return Ok(carros);
            }
            catch (Exception ex)
            {

                Logger.GetLog(ex, logPath);
                return InternalServerError();
            }
        }

        // POST: api/Carros
        [HttpPost]
        [Route("api/carros")]
        public async Task<IHttpActionResult> Post([FromBody]Models.Carro carro)
        {
            try
            {
                if (await CarroRepository.PostCarro(carro))
                {
                    return Ok("Registro criado com sucesso");
                }
                
                return BadRequest("ERRO: Nenhum registro inserido");

            }
            catch (Exception ex)
            {
                Logger.GetLog(ex,logPath);
                return InternalServerError();
            }

        }
        [HttpPut]
        [Route("api/carros/{id}")]
        public async Task<IHttpActionResult> Put(Models.Carro carro)
        {
            try
            {
                if (await CarroRepository.PutCarro(carro))
                {
                    return Ok("Registro atualizado com sucesso");
                }

                return BadRequest("ERRO: Nenhum registro inserido");
            }
            catch (Exception ex)
            {
                Logger.GetLog(ex, logPath);
                return InternalServerError();
            }
        }

        // DELETE: api/Carros/5
        [HttpDelete]
        [Route("api/carros/{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                if (await CarroRepository.DeleteCarro(id))
                {
                    return Ok("Registro deletado");
                }

                return BadRequest("ERRO: Nenhum registro deletado");
            }
            catch (Exception ex)
            {
                Logger.GetLog(ex,logPath);
                return InternalServerError();
            }
        }
    }
}
