using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhaApiComSQLite.DTOs;
using MinhaApiComSQLite.Services.Interface;

namespace MinhaApiComSQLite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RelatoriosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public RelatoriosController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<ActionResult<RelatorioEstatisticasDto>> ObterRelatorio()
        {
            var relatorio = await _produtoService.ObterRelatorioAsync();
            return Ok(relatorio);
        }
    }
}
