using Microsoft.AspNetCore.Mvc.RazorPages;
using MKFilmsStreamer.API;
using MKFilmsStreamer.Models;

namespace MKFilmsStreamer.Pages;

public class MovieModel : PageModel {
    // Movie objects
    public string backdrop_path = "https://image.tmdb.org/t/p/w1920_and_h800_multi_faces";
    public string poster_path = "https://image.tmdb.org/t/p/w500";
    public string title = "";
    public string overview = "";
    public string yearReleased = "";
    public string revenue = "";
    public string tagline = "";
    public List<string> genres = new List<string>();

    // Cast objects
    public string profile_path = "https://image.tmdb.org/t/p/w500";
    public List<string> castIMGs = new List<string>();
    public List<string> castNames = new List<string>();
    public List<string> castIDs = new List<string>();

    // Video objects
    public List<string> videoNames = new List<string>();
    public List<string> videoKeys = new List<string>();
    
    //Similar Movies
    public List<string> similarMovies = new List<string>();
    public int overview_cutoff = 100;
    public string imagePath = "https://image.tmdb.org/t/p/w500";
    public List<string>? movieTitles = new List<string>();
    public List<string>? overviews = new List<string>();
    public List<string>? posterURLs = new List<string>();
    public List<string>? releaseDate = new List<string>();
    public List<string>? movieIDs = new List<string>();
    public List<List<string>> genreSet = new List<List<string>>();

    public async Task OnGet(string _id) {
        await Fetch.MovieDetails(_id);

        if(Fetch.movie.backdrop_path != null){
            backdrop_path += Fetch.movie.backdrop_path;
        }
        else{
            backdrop_path = "./assets/images/noPoster.jpg";
        }
        if(Fetch.movie.poster_path != null){
            poster_path += poster_path + Fetch.movie.poster_path;
        }
        else{
            poster_path = "./assets/images/noPoster.jpg";
        }
        
        title = Fetch.movie.title;
        overview = Fetch.movie.overview;
        yearReleased = Fetch.movie.release_date.Substring(0, 4);
        revenue = Fetch.movie.revenue.ToString("C0");
        tagline = Fetch.movie.tagline;
        // add the genres
        for(int i = 0; i < Fetch.movie.genres.Count; i++) {
            genres.Add(Fetch.movie.genres[i].name);
        }

        // loop through the cast and populate our cast objects
        
        for(int i = 0; i < Fetch.castCrew.cast.Count; i++) {
            if(Fetch.castCrew.cast[i].profile_path != null) {
                castIMGs.Add(profile_path + Fetch.castCrew.cast[i].profile_path);
            }
            else {
                castIMGs.Add("./assets/images/gender-neutral-headshot.jpg");
            }
            castNames.Add(Fetch.castCrew.cast[i].name);
            castIDs.Add(Fetch.castCrew.cast[i].id.ToString());
        } // cast for

        // loop through the videos and populate our video objects
        for(int i = 0; i < Fetch.videoSet.results.Count; i++) {
            if(Fetch.videoSet.results[i].site.ToLower() == "youtube") {
                videoNames.Add(Fetch.videoSet.results[i].name);
                videoKeys.Add(Fetch.videoSet.results[i].key);
            }
        } // video for

        //loop through similar movies results
        foreach(Result result in Fetch.similarMovies.results){
                movieTitles.Add(result.title);
                if((result.overview) != null && (result.overview.Length > overview_cutoff))
                    overviews.Add(result.overview.Substring(0, overview_cutoff) + " ...");
                else if((result.overview) != null && (result.overview.Length < overview_cutoff)){
                    overview_cutoff = result.overview.Length;
                    overviews.Add(result.overview.Substring(0, overview_cutoff) + "...");
                }
                else
                    overviews.Add("No Information Available");
                if(result.poster_path != null){
                    string path = imagePath + result.poster_path;
                    posterURLs.Add(path);
                }
                else{
                    //Add Placeholder image here : Pending
                    string path = "./assets/images/noPoster.jpg";
                    posterURLs.Add(path);
                }  
                
                if(result.release_date != ""){
                    string[] rawdate = result.release_date.Split('-');
                    string formattedDate = Tools.formatDate(rawdate);
                    releaseDate.Add(formattedDate);
                }else
                {
                    releaseDate.Add("Release date not available");
                }
                movieIDs.Add(result.id.ToString());
                List<int> codedGenres = result.genre_ids;
                List<string> decodedGenres = Tools.decodeGenres(codedGenres);
                genreSet.Add(decodedGenres);
        } // similar movies

    } // OnGet()

    public async Task OnPostDetails(int movieID){
        // redirect to the movie page
        // pass along the movieID
        Response.Redirect("./Movie?_id=" + movieID);
    } // OnPostDetails()
        
    public void OnPostGetActor(string actorID) {
        Response.Redirect("./Actor?actorID=" + actorID);
    } // OnPostGetActor()
}