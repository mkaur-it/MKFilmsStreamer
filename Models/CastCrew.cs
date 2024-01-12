namespace MKFilmsStreamer.Models; 
public class CastCrew
{
    public int id { get; set; }
    public List<Cast>? cast { get; set; }
    public List<Crew>? crew { get; set; }
}