using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MeuCepConsulta.Models;

namespace MeuCepConsulta.Controllers
{
    [Route("api/cep")]
    public class CepController : Controller
    {
        private readonly HttpClient _httpClient;

        public CepController()
        {
            _httpClient = new HttpClient();
        }

        [HttpGet("{cep}")]
        public async Task<IActionResult> GetCep(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep) || cep.Length < 8)
                return BadRequest(new { message = "CEP inválido!" });

            var responseString = await _httpClient.GetStringAsync($"https://viacep.com.br/ws/{cep}/json/");
            var data = JsonSerializer.Deserialize<CepModel>(responseString,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (data is null)
                return BadRequest(new { message = "Erro ao tratar dados do ViaCEP" });

            // Se ViaCEP retornou erro
            if (data.Erro)
                return NotFound(new { message = "CEP não encontrado" });

            // Se navegador → renderiza HTML
            if (Request.Headers["Accept"].ToString().Contains("text/html"))
                return View("Index", data);

            // Se API (curl, fetch, etc) → retorna JSON puro
            return Content(responseString, "application/json");
        }
    }
}
