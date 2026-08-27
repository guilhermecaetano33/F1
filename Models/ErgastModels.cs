using System.Text.Json.Serialization;

namespace F1Stats.Models
{
    // ===== Estruturas raiz compartilhadas pela API (formato Ergast/Jolpica) =====

    public class ApiRoot<T>
    {
        [JsonPropertyName("MRData")]
        public MRData<T>? MRData { get; set; }
    }

    public class MRData<T>
    {
        [JsonPropertyName("total")]
        public string? Total { get; set; }

        [JsonPropertyName("StandingsTable")]
        public StandingsTable? StandingsTable { get; set; }

        [JsonPropertyName("DriverTable")]
        public DriverTable? DriverTable { get; set; }

        [JsonPropertyName("RaceTable")]
        public RaceTable? RaceTable { get; set; }
    }

    // ===== Classificação (pilotos e construtores) =====

    public class StandingsTable
    {
        [JsonPropertyName("season")]
        public string? Season { get; set; }

        [JsonPropertyName("StandingsLists")]
        public List<StandingsList>? StandingsLists { get; set; }
    }

    public class StandingsList
    {
        [JsonPropertyName("season")]
        public string? Season { get; set; }

        [JsonPropertyName("round")]
        public string? Round { get; set; }

        [JsonPropertyName("DriverStandings")]
        public List<DriverStanding>? DriverStandings { get; set; }

        [JsonPropertyName("ConstructorStandings")]
        public List<ConstructorStanding>? ConstructorStandings { get; set; }
    }

    public class DriverStanding
    {
        [JsonPropertyName("position")]
        public string? Position { get; set; }

        [JsonPropertyName("points")]
        public string? Points { get; set; }

        [JsonPropertyName("wins")]
        public string? Wins { get; set; }

        [JsonPropertyName("Driver")]
        public Driver? Driver { get; set; }

        [JsonPropertyName("Constructors")]
        public List<Constructor>? Constructors { get; set; }
    }

    public class ConstructorStanding
    {
        [JsonPropertyName("position")]
        public string? Position { get; set; }

        [JsonPropertyName("points")]
        public string? Points { get; set; }

        [JsonPropertyName("wins")]
        public string? Wins { get; set; }

        [JsonPropertyName("Constructor")]
        public Constructor? Constructor { get; set; }
    }

    // ===== Pilotos e Construtores =====

    public class DriverTable
    {
        [JsonPropertyName("Drivers")]
        public List<Driver>? Drivers { get; set; }
    }

    public class Driver
    {
        [JsonPropertyName("driverId")]
        public string? DriverId { get; set; }

        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("permanentNumber")]
        public string? PermanentNumber { get; set; }

        [JsonPropertyName("givenName")]
        public string? GivenName { get; set; }

        [JsonPropertyName("familyName")]
        public string? FamilyName { get; set; }

        [JsonPropertyName("dateOfBirth")]
        public string? DateOfBirth { get; set; }

        [JsonPropertyName("nationality")]
        public string? Nationality { get; set; }

        public string NomeCompleto => $"{GivenName} {FamilyName}";
    }

    public class Constructor
    {
        [JsonPropertyName("constructorId")]
        public string? ConstructorId { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("nationality")]
        public string? Nationality { get; set; }
    }

    // ===== Corridas e resultados =====

    public class RaceTable
    {
        [JsonPropertyName("season")]
        public string? Season { get; set; }

        [JsonPropertyName("Races")]
        public List<Race>? Races { get; set; }
    }

    public class Race
    {
        [JsonPropertyName("season")]
        public string? Season { get; set; }

        [JsonPropertyName("round")]
        public string? Round { get; set; }

        [JsonPropertyName("raceName")]
        public string? RaceName { get; set; }

        [JsonPropertyName("date")]
        public string? Date { get; set; }

        [JsonPropertyName("Circuit")]
        public Circuit? Circuit { get; set; }

        [JsonPropertyName("Results")]
        public List<Result>? Results { get; set; }
    }

    public class Circuit
    {
        [JsonPropertyName("circuitName")]
        public string? CircuitName { get; set; }

        [JsonPropertyName("Location")]
        public Location? Location { get; set; }
    }

    public class Location
    {
        [JsonPropertyName("locality")]
        public string? Locality { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }
    }

    public class Result
    {
        [JsonPropertyName("position")]
        public string? Position { get; set; }

        [JsonPropertyName("points")]
        public string? Points { get; set; }

        [JsonPropertyName("grid")]
        public string? Grid { get; set; }

        [JsonPropertyName("laps")]
        public string? Laps { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("Driver")]
        public Driver? Driver { get; set; }

        [JsonPropertyName("Constructor")]
        public Constructor? Constructor { get; set; }
    }
}
