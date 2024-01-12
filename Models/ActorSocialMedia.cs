namespace MKFilmsStreamer.Models; 
public class ActorSocialMedia
{
    public long id { get; set; }
    public string? freebase_mid { get; set; }
    public string? freebase_id { get; set; }
    public string? imdb_id { get; set; }
    public long? tvrage_id { get; set; }
    public string? wikidata_id { get; set; } 
    public object? facebook_id { get; set; }
    public string? instagram_id { get; set; }
    public object? tiktok_id { get; set; }
    public string? twitter_id { get; set; }
    public object? youtube_id { get; set; }
}