using Domains.Classes;
using Domains.Interfaces.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContatosAPI.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class ContatosController : ControllerBase
    {
        private readonly IContatoRepositorio _contatoRepositorio;

        public ContatosController(IContatoRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;
        }

        [HttpGet("/contatos")]
        [ProducesResponseType(typeof(IEnumerable<Contato>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var listaContatos = await _contatoRepositorio.GetAll();
                return Ok(listaContatos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorMessage = ex.Message,
                    StackTraceError = ex.StackTrace
                });
            }
        }

        [HttpGet("/contatos/{id:int}")]
        [ProducesResponseType(typeof(Contato), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var contato = await _contatoRepositorio.GetById(id);
                if (contato is null)
                    return NotFound();

                return Ok(contato);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorMessage = ex.Message,
                    StackTraceError = ex.StackTrace
                });
            }
        }

        [HttpPost("/contatos")]
        [ProducesResponseType(typeof(Contato), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] Contato contato)
        {
            try
            {
                var novoContato = await _contatoRepositorio.Post(contato);
                return Created(new Uri($"{Request.Path}/{novoContato.ContatoId}", UriKind.Relative), novoContato);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorMessage = ex.Message,
                    StackTraceError = ex.StackTrace
                });
            }           
        }

        [HttpPut("/contatos/{id:int}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Contato contato)
        {
            try
            {
                var c = await _contatoRepositorio.GetById(id);
                if (c is null)
                    return NotFound(new { mensagem = "Contato não localizado!" });

                await _contatoRepositorio.Put(contato);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorMessage = ex.Message,
                    StackTraceError = ex.StackTrace
                });
            }
           
        }

        [HttpPatch("/contatos/{id:int}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] string descricao)
        {
            try
            {
                var contato = await _contatoRepositorio.GetById(id);
                if (contato is null)
                    return NotFound(new { mensagem = "Contato não localizado!" });

                await _contatoRepositorio.Patch(contato, descricao);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorMessage = ex.Message,
                    StackTraceError = ex.StackTrace
                });
            }            
        }

        [HttpDelete("/contatos/{id:int}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Contato), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var c = await _contatoRepositorio.GetById(id);
                if (c is null)
                    return NotFound(new { mensagem = "Contato não localizado para exclusão!" });

                await _contatoRepositorio.Delete(c);
                return Ok(c);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorMessage = ex.Message,
                    StackTraceError = ex.StackTrace
                });
            }           
        }
    }
}