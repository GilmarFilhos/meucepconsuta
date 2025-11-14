namespace ApiCepCompleto.Models
{

        public class CepHistorico
{
    public int Id { get; set; }
    public string Cep { get; set; }
    public string Logradouro { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Uf { get; set; }
    public string Ddd { get; set; }
    public string Ibge { get; set; }
    public DateTime DataConsulta { get; set; }
}

    }
