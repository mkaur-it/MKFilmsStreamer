namespace MKFilmsStreamer.Models; 
public class TvTrends
    {
    public bool adult { get; set; }
    public string? backdrop_path { get; set; }
    public List<string>? created_by { get; set; }
    public List<string>? episode_run_time { get; set; }
    public string? first_air_date { get; set; }
    public List<string>? genres { get; set; }
    public string? homepage { get; set; }
    public int id { get; set; }
    public bool in_production { get; set; }
    public List<string>? languages { get; set; }
    public string? last_air_date { get; set; }
    public string? last_episode_to_air { get; set; }
    public string? name { get; set; }
    public object? next_episode_to_air { get; set; }
    public List<Network>? networks { get; set; }
    public int number_of_episodes { get; set; }
    public int number_of_seasons { get; set; }
    public List<string>? origin_country { get; set; }
    public string? original_language { get; set; }
    public string? original_name { get; set; }
    public string? overview { get; set; }
    public double popularity { get; set; }
    public object? poster_path { get; set; }
    public List<ProductionCompany>? production_companies { get; set; }
    public List<ProductionCountry>? production_countries { get; set; }
    public List<Season>? seasons { get; set; }
    public List<SpokenLanguage>? spoken_languages { get; set; }
    public string? status { get; set; }
    public string? tagline { get; set; }
    public string? type { get; set; }
    public double vote_average { get; set; }
    public int vote_count { get; set; }
}