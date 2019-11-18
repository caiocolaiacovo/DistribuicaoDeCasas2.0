using Microsoft.AspNetCore.Mvc;
using Selecao.Aplicacao;
using Selecao.Aplicacao.Dtos;

namespace Selecao.Apresentacao.Controllers
{
  [ApiController]
    [Route("[controller]")]
    public class FamiliaController : ControllerBase
    {
        public readonly ArmazenadorDeFamilia _armazenadorDeFamilia;
        
        [HttpPost]
        public IActionResult Salvar(FamiliaDto familiaDto)
        {
            _armazenadorDeFamilia.Armazenar(familiaDto);
            return Ok();
        }
    }
}