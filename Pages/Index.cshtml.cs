using Microsoft.AspNetCore.Mvc.RazorPages;
using MKFilmsStreamer.API;
using MKFilmsStreamer.Models;
namespace MKFilmsStreamer.Pages;

public class IndexModel : PageModel
{
    public int overview_cutoff = 100;
    public string imagePath = "https://image.tmdb.org/t/p/w500";
    public List<string>? movieTitles = new List<string>();
    public List<string>? overviews = new List<string>();
    public List<string>? posterURLs = new List<string>();
    public List<string>? releaseDate = new List<string>();
    public List<string>? movieIDs = new List<string>();
    public List<List<string>> genreSet = new List<List<string>>();

    public async Task OnGet()
    {
        await Fetch.GetMovieTrends();
        foreach(Poster poster in Fetch.posterSet.results)
        {
            movieTitles.Add(poster.title);
            if(poster.overview.Length >= overview_cutoff) {
                overviews.Add(poster.overview.Substring(0, overview_cutoff) + "...");
            }
            else {
                overview_cutoff = poster.overview.Length;
                overviews.Add(poster.overview.Substring(0, overview_cutoff) + "...");
            }
            string path = imagePath + poster.poster_path;
            posterURLs.Add(path);
            string[] rawdate = poster.release_date.Split('-');
            string formattedDate = Tools.formatDate(rawdate);
            releaseDate.Add(formattedDate);
            movieIDs.Add(poster.id.ToString());
            List<int> codedGenres = poster.genre_ids;
            List<string> decodedGenres = Tools.decodeGenres(codedGenres);
            genreSet.Add(decodedGenres);
        }
         foreach(Poster poster in Fetch.posterSetPage2.results)
        {
            movieTitles.Add(poster.title);
            if(poster.overview.Length >= overview_cutoff) {
                overviews.Add(poster.overview.Substring(0, overview_cutoff) + "...");
            }
            else {
                overview_cutoff = poster.overview.Length;
                overviews.Add(poster.overview.Substring(0, overview_cutoff) + "...");
            }
            string path = imagePath + poster.poster_path;
            posterURLs.Add(path);
            string[] rawdate = poster.release_date.Split('-');
            string formattedDate = Tools.formatDate(rawdate);
            releaseDate.Add(formattedDate);
            movieIDs.Add(poster.id.ToString());
            List<int> codedGenres = poster.genre_ids;
            List<string> decodedGenres = Tools.decodeGenres(codedGenres);
            genreSet.Add(decodedGenres);
        }
    }//OnGet()

    public async Task OnPostSearch(string search){
        await Fetch.Search(search);
        foreach(Result result in Fetch.resultSet.results)
        {
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
        }
    }

    public async Task OnPostDetails(int movieID){
        // redirect to the movie page
        // pass along the movieID
        Response.Redirect("./Movie?_id=" + movieID);
    } // OnPostDetails()

} // class IndexModel