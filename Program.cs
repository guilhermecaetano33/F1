using F1Stats.Models;
using F1Stats.Services;

namespace F1Stats
{
    class Program
    {
        static readonly F1ApiService Api = new();

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            bool continuar = true;
            while (continuar)
            {
                MostrarMenu();
                string? opcao = Console.ReadLine();

                try
                {
                    switch (opcao)
                    {
                        case "1":
                            await MostrarClassificacaoPilotos();
                            break;
                        case "2":
                            await MostrarClassificacaoConstrutores();
                            break;
                        case "3":
                            await BuscarPiloto();
                            break;
                        case "4":
                            await CompararPilotos();
                            break;
                        case "5":
                            await MostrarUltimaCorrida();
                            break;
                        case "0":
                            continuar = false;
                            Console.WriteLine("\nValeu por usar o F1 Stats! Até a próxima corrida. 🏁");
                            break;
                        default:
                            Console.WriteLine("\nOpção inválida. Tente novamente.");
                            break;
                    }
                }
                catch (HttpRequestException)
                {
                    Console.WriteLine("\n[Erro] Não foi possível conectar à API. Verifique sua internet e tente novamente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[Erro inesperado] {ex.Message}");
                }

                if (continuar)
                {
                    Console.WriteLine("\nPressione ENTER para voltar ao menu...");
                    Console.ReadLine();
                }
            }
        }

        static void MostrarMenu()
        {
            Console.Clear();
            Console.WriteLine("=========================================");
            Console.WriteLine("           F1 STATS - Dados Reais         ");
            Console.WriteLine("=========================================");
            Console.WriteLine("1 - Classificação de pilotos (por temporada)");
            Console.WriteLine("2 - Classificação de construtores (por temporada)");
            Console.WriteLine("3 - Buscar informações de um piloto");
            Console.WriteLine("4 - Comparar dois pilotos");
            Console.WriteLine("5 - Ver resultados da última corrida");
            Console.WriteLine("0 - Sair");
            Console.Write("\nEscolha uma opção: ");
        }

        static string PerguntarTemporada()
        {
            Console.Write("\nDigite o ano da temporada (ex: 2024) ou ENTER para a atual: ");
            string? entrada = Console.ReadLine();
            return string.IsNullOrWhiteSpace(entrada) ? "current" : entrada.Trim();
        }

        static async Task MostrarClassificacaoPilotos()
        {
            string temporada = PerguntarTemporada();
            Console.WriteLine("\nBuscando classificação de pilotos...");

            var pilotos = await Api.GetClassificacaoPilotosAsync(temporada);

            if (pilotos.Count == 0)
            {
                Console.WriteLine("Nenhum dado encontrado para essa temporada.");
                return;
            }

            Console.WriteLine($"\n{"Pos",-5}{"Piloto",-25}{"Equipe",-20}{"Pontos",-10}{"Vitórias",-10}");
            Console.WriteLine(new string('-', 70));

            foreach (var p in pilotos)
            {
                string nome = p.Driver?.NomeCompleto ?? "?";
                string equipe = p.Constructors?.FirstOrDefault()?.Name ?? "?";
                Console.WriteLine($"{p.Position,-5}{nome,-25}{equipe,-20}{p.Points,-10}{p.Wins,-10}");
            }
        }

        static async Task MostrarClassificacaoConstrutores()
        {
            string temporada = PerguntarTemporada();
            Console.WriteLine("\nBuscando classificação de construtores...");

            var equipes = await Api.GetClassificacaoConstrutoresAsync(temporada);

            if (equipes.Count == 0)
            {
                Console.WriteLine("Nenhum dado encontrado para essa temporada.");
                return;
            }

            Console.WriteLine($"\n{"Pos",-5}{"Equipe",-25}{"Pontos",-10}{"Vitórias",-10}");
            Console.WriteLine(new string('-', 55));

            foreach (var e in equipes)
            {
                Console.WriteLine($"{e.Position,-5}{e.Constructor?.Name,-25}{e.Points,-10}{e.Wins,-10}");
            }
        }

        static async Task BuscarPiloto()
        {
            string temporada = PerguntarTemporada();
            Console.Write("\nDigite parte do nome do piloto (ex: hamilton, verstappen): ");
            string busca = (Console.ReadLine() ?? "").Trim();

            Console.WriteLine("\nBuscando...");
            var pilotos = await Api.GetClassificacaoPilotosAsync(temporada);

            var encontrados = pilotos.Where(p =>
                (p.Driver?.FamilyName?.Contains(busca, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (p.Driver?.GivenName?.Contains(busca, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (p.Driver?.Code?.Contains(busca, StringComparison.OrdinalIgnoreCase) ?? false)
            ).ToList();

            if (encontrados.Count == 0)
            {
                Console.WriteLine("Nenhum piloto encontrado com esse nome nessa temporada.");
                return;
            }

            foreach (var p in encontrados)
            {
                Console.WriteLine("\n-----------------------------------------");
                Console.WriteLine($"Nome: {p.Driver?.NomeCompleto}");
                Console.WriteLine($"Código: {p.Driver?.Code}");
                Console.WriteLine($"Nacionalidade: {p.Driver?.Nationality}");
                Console.WriteLine($"Data de nascimento: {p.Driver?.DateOfBirth}");
                Console.WriteLine($"Equipe: {p.Constructors?.FirstOrDefault()?.Name}");
                Console.WriteLine($"Posição no campeonato: {p.Position}");
                Console.WriteLine($"Pontos: {p.Points}");
                Console.WriteLine($"Vitórias: {p.Wins}");
            }
        }

        static async Task CompararPilotos()
        {
            string temporada = PerguntarTemporada();
            var pilotos = await Api.GetClassificacaoPilotosAsync(temporada);

            if (pilotos.Count == 0)
            {
                Console.WriteLine("Não foi possível carregar dados dessa temporada.");
                return;
            }

            Console.Write("\nNome (ou parte) do 1º piloto: ");
            string busca1 = (Console.ReadLine() ?? "").Trim();
            Console.Write("Nome (ou parte) do 2º piloto: ");
            string busca2 = (Console.ReadLine() ?? "").Trim();

            var p1 = EncontrarPiloto(pilotos, busca1);
            var p2 = EncontrarPiloto(pilotos, busca2);

            if (p1 == null || p2 == null)
            {
                Console.WriteLine("\nNão consegui encontrar um ou ambos os pilotos nessa temporada. Verifique o nome digitado.");
                return;
            }

            Console.WriteLine("\n=============== COMPARAÇÃO ===============");
            Console.WriteLine($"{"",-15}{p1.Driver?.NomeCompleto,-22}{p2.Driver?.NomeCompleto,-22}");
            Console.WriteLine(new string('-', 59));
            Console.WriteLine($"{"Equipe",-15}{p1.Constructors?.FirstOrDefault()?.Name,-22}{p2.Constructors?.FirstOrDefault()?.Name,-22}");
            Console.WriteLine($"{"Posição",-15}{p1.Position,-22}{p2.Position,-22}");
            Console.WriteLine($"{"Pontos",-15}{p1.Points,-22}{p2.Points,-22}");
            Console.WriteLine($"{"Vitórias",-15}{p1.Wins,-22}{p2.Wins,-22}");

            // Comentário simples com base nos pontos
            double pontos1 = double.TryParse(p1.Points, out var v1) ? v1 : 0;
            double pontos2 = double.TryParse(p2.Points, out var v2) ? v2 : 0;

            Console.WriteLine();
            if (pontos1 > pontos2)
                Console.WriteLine($"➡ {p1.Driver?.NomeCompleto} está à frente na classificação.");
            else if (pontos2 > pontos1)
                Console.WriteLine($"➡ {p2.Driver?.NomeCompleto} está à frente na classificação.");
            else
                Console.WriteLine("➡ Os dois pilotos estão empatados em pontos.");
        }

        static DriverStanding? EncontrarPiloto(List<DriverStanding> pilotos, string busca)
        {
            return pilotos.FirstOrDefault(p =>
                (p.Driver?.FamilyName?.Contains(busca, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (p.Driver?.GivenName?.Contains(busca, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (p.Driver?.Code?.Contains(busca, StringComparison.OrdinalIgnoreCase) ?? false)
            );
        }

        static async Task MostrarUltimaCorrida()
        {
            Console.WriteLine("\nBuscando resultados da última corrida...");
            var corrida = await Api.GetUltimaCorridaAsync();

            if (corrida == null)
            {
                Console.WriteLine("Não foi possível carregar os resultados.");
                return;
            }

            Console.WriteLine($"\n{corrida.RaceName} - {corrida.Circuit?.CircuitName} ({corrida.Circuit?.Location?.Country})");
            Console.WriteLine($"Data: {corrida.Date}");
            Console.WriteLine(new string('-', 70));
            Console.WriteLine($"{"Pos",-5}{"Piloto",-25}{"Equipe",-20}{"Pontos",-8}{"Status",-15}");

            if (corrida.Results != null)
            {
                foreach (var r in corrida.Results)
                {
                    string nome = r.Driver?.NomeCompleto ?? "?";
                    Console.WriteLine($"{r.Position,-5}{nome,-25}{r.Constructor?.Name,-20}{r.Points,-8}{r.Status,-15}");
                }
            }
        }
    }
}
