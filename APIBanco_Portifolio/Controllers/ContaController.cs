using _4_Recursos;
using Aplicacao.Interfaces;
using Dominio.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Apresentacao.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaController : ControllerBase
    {
        private readonly IContaService _contaService;

        public ContaController(IContaService contaService)
        {
            _contaService = contaService;
        }

        [HttpPost("CriarConta")]
        public IActionResult Criado([FromBody] CriarContaDto criarContaDto)
        {
            try
            {
                var contaCriada = _contaService.Criar(criarContaDto);

                if (contaCriada is null)
                {
                    return Unauthorized(MensagensCliente.ClienteNaoEncontrado);
                }
                return Created($"/{criarContaDto.ClienteId}", contaCriada);
            }

            catch (Exception excecao)
            {
                return BadRequest(MensagensConta.ContaExistente);
            }
        }

        [HttpGet("BuscarTodasAsContas")]
        public IActionResult Leia()
        {
            var resultado = _contaService.Ler();

            return Ok(resultado);
        }

        [HttpGet("BuscarContaPeloNumero")]
        public IActionResult Leia(string numero)
        {
            var resultado = _contaService.Ler(numero);

            if (resultado is null)
            {
                return NotFound(MensagensConta.ContaNaoEncontrada);
            }
            return Ok(resultado);
        }

        [HttpPut("AtualizarConta")]
        public IActionResult Atualizado(string numeroConta, [FromBody] Conta novosDados)
        {
            var atualizandoConta = _contaService.Atualizar(numeroConta, novosDados);

            if (atualizandoConta is null)
            {
                return BadRequest(MensagensConta.ContaNaoEncontrada);
            }

            return Ok(atualizandoConta);
        }

        [HttpDelete("DeletarConta")]
        public IActionResult Deletando(string numero)
        {
            var resultado = _contaService.Deletar(numero);

            if (resultado is null)
            {
                return NotFound(MensagensConta.ContaNaoEncontrada);
            }
            return Ok(resultado);
        }
    }
}
