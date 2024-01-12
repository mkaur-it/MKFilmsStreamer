namespace MKFilmsStreamer.Models; 
public class SimilarMovies
    {
        public int page { get; set; }
        public List<Result>? results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }