using System.Net.Http.Json;
using System.Text.Json;
using F1Stats.Models;

namespace F1Stats.Services
{
    /// <summary>
    /// Consome a API pública Jolpica-F1 (sucessora da antiga Ergast API),
    /// que fornece dados reais e gratuitos de Fórmula 1.
    /// Documentação: https://github.com/jolpica/jolpica-f1
    /// </summary>
    public class F1ApiService
    {
        private readonly HttpClient _http;
        private const string BaseUrl = "https://api.jolpi.ca/ergast/f1";

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public F1ApiService()
        {
            _http = new HttpClient();
            _http.Timeout = TimeSpan.FromSeconds(15);
        }

        /// <summary>
        /// Busca a classificação de pilotos de uma temporada específica.
        /// Use "current" para a temporada em andamento.
        /// </summary>
        public async Task<List<DriverStanding>> GetClassificacaoPilotosAsync(string temporada)
        {
            string url = $"{BaseUrl}/{temporada}/driverstandings.json";
            var root = await _http.GetFromJsonAsync<ApiRoot<object>>(url, JsonOptions);

            var lista = root?.MRData?.StandingsTable?.StandingsLists?.FirstOrDefault();
            return lista?.DriverStandings ?? new List<DriverStanding>();
        }

        /// <summary>
        /// Busca a classificação de construtores (equipes) de uma temporada específica.
        /// Use "current" para a temporada em andamento.
        /// </summary>
        public async Task<List<ConstructorStanding>> GetClassificacaoConstrutoresAsync(string temporada)
        {
            string url = $"{BaseUrl}/{temporada}/constructorstandings.json";
            var root = await _http.GetFromJsonAsync<ApiRoot<object>>(url, JsonOptions);

            var lista = root?.MRData?.StandingsTable?.StandingsLists?.FirstOrDefault();
            return lista?.ConstructorStandings ?? new List<ConstructorStanding>();
        }

        /// <summary>
        /// Busca informações de um piloto pelo seu "driverId" da API
        /// (ex: "max_verstappen", "lewis_hamilton", "leclerc").
        /// </summary>
        public async Task<Driver?> GetPilotoAsync(string driverId)
        {
            string url = $"{BaseUrl}/drivers/{driverId}.json";
            var root = await _http.GetFromJsonAsync<ApiRoot<object>>(url, JsonOptions);

            return root?.MRData?.DriverTable?.Drivers?.FirstOrDefault();
        }

        /// <summary>
        /// Busca os resultados da última corrida realizada (temporada atual).
        /// </summary>
        public async Task<Race?> GetUltimaCorridaAsync()
        {
            string url = $"{BaseUrl}/current/last/results.json";
            var root = await _http.GetFromJsonAsync<ApiRoot<object>>(url, JsonOptions);

            return root?.MRData?.RaceTable?.Races?.FirstOrDefault();
        }

        /// <summary>
        /// Converte um nome digitado pelo usuário (ex: "Max Verstappen")
        /// para o formato de driverId usado pela API (ex: "max_verstappen").
        /// Não é perfeito para todos os pilotos, mas cobre a maioria dos casos.
        /// </summary>
        public static string ConverterParaDriverId(string nomeDigitado)
        {
            return nomeDigitado
                .Trim()
                .ToLowerInvariant()
                .Replace(" ", "_");
        }
    }
}
