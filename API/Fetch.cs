using System.Net.Http;
using System.Text.Json;
using MKFilmsStreamer.Models;

namespace MKFilmsStreamer.API;

public static class Fetch{
    public static HttpClient client = new HttpClient();
    public const string API_KEY = "d194eb72915bc79fac2eb1a70a71ddd3";
    public static string? MKFilmsData { get; set; }
    public static string? ActorIMGsData { get; set; }
    public static string? ActorSMData { get; set; }
    public static string? ActorKnownPostersData { get; set; }
    public static string? MKFilmsDataPage2 { get; set; }
    public static PosterSet posterSet = new PosterSet();
    public static PosterSet posterSetPage2 = new PosterSet();
    public static ResultSet resultSet = new ResultSet();
    public static Movie movie = new Movie();
    public static CastCrew castCrew = new CastCrew();
    public static VideoSet videoSet = new VideoSet();
    public static Actor actor = new Actor();
    public static ProfileSet actorImageGallery = new ProfileSet();
    public static ActorSocialMedia actorSocialMediaSet = new ActorSocialMedia();
    public static ActorKnownPosters actorKnownPosters = new ActorKnownPosters();
    public static TvTrends tvTrends = new TvTrends();
    public static SimilarMovies similarMovies = new SimilarMovies();

    public static async Task GetMovieTrends(){
        string apiUrlMovieTrendsPage1 = "https://api.themoviedb.org/3/trending/movie/day?api_key=" + API_KEY;
        string apiUrlMovieTrendsPage2 = "https://api.themoviedb.org/3/trending/movie/day?api_key=" + API_KEY +"&page=2";
        ClearHeaders();
       
        // get the latest Movie Trends, for today
        // https://api.themoviedb.org/3/trending/movie/day?api_key=<<api_key>>&page=1
        HttpResponseMessage movieResponse = await client.GetAsync(apiUrlMovieTrendsPage1);
        if((movieResponse.IsSuccessStatusCode)){ // 200 OK
            MKFilmsData = await movieResponse.Content.ReadAsStringAsync();
            // parse JSON into C# classes
            posterSet = JsonSerializer.Deserialize<PosterSet>(MKFilmsData);
        }
        else{
            MKFilmsData = null;
        }  

        // get the latest Movie Trends, for today
        // https://api.themoviedb.org/3/trending/movie/day?api_key=<<api_key>>&page=2
        HttpResponseMessage movieResponsePage2 = await client.GetAsync(apiUrlMovieTrendsPage2);
        if((movieResponsePage2.IsSuccessStatusCode)){ // 200 OK
            MKFilmsDataPage2 = await movieResponsePage2.Content.ReadAsStringAsync();
            // parse JSON into C# classes
            posterSetPage2 = JsonSerializer.Deserialize<PosterSet>(MKFilmsDataPage2);
        }
        else{
            MKFilmsDataPage2 = null;
        }
    } // GetMovieTrends()

    public static async Task Search(string search) {
        string searchUrl = "https://api.themoviedb.org/3/search/movie?api_key=" + API_KEY + "&page=1&query=" + search;
        ClearHeaders();
        
        // Search for a specific movie title
        // https://api.themoviedb.org/3/search/movie?api_key=d194eb72915bc79fac2eb1a70a71ddd3&page=1&query=avengers
        HttpResponseMessage response = await client.GetAsync(searchUrl);

        if(response.IsSuccessStatusCode) { // 200 OK
            MKFilmsData = await response.Content.ReadAsStringAsync();
            // object here to hold the data!
            resultSet = JsonSerializer.Deserialize<ResultSet>(MKFilmsData);
        }
        else {
            MKFilmsData = null;
        }
    } // Search()
    
    public static async Task MovieDetails(string movieID) {
        ClearHeaders();
        string? url;

        url = "https://api.themoviedb.org/3/movie/" + movieID + "?api_key=" + API_KEY + "&language=en-US";
        // Get the details for a specific movie, based on the movieID
        // https://api.themoviedb.org/3/movie/505642?api_key=d194eb72915bc79fac2eb1a70a71ddd3&language=en-US
        HttpResponseMessage response = await client.GetAsync(url);

        if(response.IsSuccessStatusCode) { // 200 OK
            MKFilmsData = await response.Content.ReadAsStringAsync();
            // parse the JSON into C# classes
            movie = JsonSerializer.Deserialize<Movie>(MKFilmsData);
        }
        else {
            MKFilmsData = null;
        }

        url = "https://api.themoviedb.org/3/movie/" + movieID + "/credits?api_key=" + API_KEY + "&language=en-US";
        // Next get the cast members for the movie
        // https://api.themoviedb.org/3/movie/640146/credits?api_key=<<api_key>>&language=en-US
        response = await client.GetAsync(url);

        if(response.IsSuccessStatusCode) { // 200 OK
            MKFilmsData = await response.Content.ReadAsStringAsync();
            // parse the JSON into C# classes
            castCrew = JsonSerializer.Deserialize<CastCrew>(MKFilmsData);
        }
        else {
            MKFilmsData = null;
        }

        url = "https://api.themoviedb.org/3/movie/" + movieID + "/videos?api_key=" + API_KEY + "&language=en-US";
        // Last but not least let's grab the YouTube videos
        // https://api.themoviedb.org/3/movie/{movie_id}/videos?api_key=<<api_key>>&language=en-US
        response = await client.GetAsync(url);

        if(response.IsSuccessStatusCode) { // 200 OK
            MKFilmsData = await response.Content.ReadAsStringAsync();
            // parse the JSON into C# classes
            videoSet = JsonSerializer.Deserialize<VideoSet>(MKFilmsData);
        }
        else {
            MKFilmsData = null;
        }

        // Get Similar Movies
        // https://api.themoviedb.org/3/movie/804150/similar?api_key=d194eb72915bc79fac2eb1a70a71ddd3&language=en-US&page=1
        url = "https://api.themoviedb.org/3/movie/" + movieID + "/similar?api_key=" + API_KEY + "&language=en-US";
        response = await client.GetAsync(url);

        if(response.IsSuccessStatusCode) { // 200 OK
            MKFilmsData = await response.Content.ReadAsStringAsync();
            // parse the JSON into C# classes
            similarMovies = JsonSerializer.Deserialize<SimilarMovies>(MKFilmsData);
        }
        else {
            MKFilmsData = null;
        }

    } // MovieDetails()

    public static async Task GetActorDetails(string actorID){
        // URLs for all API calls for Actor/Actress Page

        //https://api.themoviedb.org/3/person/31?api_key=d194eb72915bc79fac2eb1a70a71ddd3&language=en-US
        string apiUrlActor = "https://api.themoviedb.org/3/person/" + actorID + "?api_key=" + API_KEY + "&language=en-US";  
        //https://api.themoviedb.org/3/person/34486/external_ids?api_key=d194eb72915bc79fac2eb1a70a71ddd3&language=en-US
        string apiUrllSocialMedia = "https://api.themoviedb.org/3/person/" + actorID + "/external_ids?api_key=" + API_KEY + "&language=en-US";       
        // https://api.themoviedb.org/3/person/31/images?api_key=d194eb72915bc79fac2eb1a70a71ddd3
        string apiUrlActorImageGallery = "https://api.themoviedb.org/3/person/" + actorID + "/images?api_key=" + API_KEY;
        //https://api.themoviedb.org/3/person/544096/movie_credits?api_key=d194eb72915bc79fac2eb1a70a71ddd3&language=en-US
        string apiUrlKnownPosters = "https://api.themoviedb.org/3/person/" + actorID + "/movie_credits?api_key=" + API_KEY + "&language=en-US"; 

        //reset HTTP headers
        ClearHeaders();
            
        // API call for Actor/Actress's basic details and Store JSON data in Actor class
        HttpResponseMessage actorResponse = await client.GetAsync(apiUrlActor);
        if(actorResponse.IsSuccessStatusCode){
            MKFilmsData = await actorResponse.Content.ReadAsStringAsync();
            actor = JsonSerializer.Deserialize<Actor>(MKFilmsData);            
        }
        else{
            
            MKFilmsData = null;
        }

        // API call for Actor/Actress's social media profiles and Store JSON data in ActorSocialMedia class
        HttpResponseMessage actorSocialMediaResponse = await client.GetAsync(apiUrllSocialMedia);        
        if(actorSocialMediaResponse.IsSuccessStatusCode){
            ActorSMData = await actorSocialMediaResponse.Content.ReadAsStringAsync();
            actorSocialMediaSet = JsonSerializer.Deserialize<ActorSocialMedia>(ActorSMData);     
        }
        else{
            
            ActorSMData = null;
        }

        // API call for Actor/Actress's image gallery profiles and Store JSON data in ProfileSet class
        HttpResponseMessage urlResponse = await client.GetAsync(apiUrlActorImageGallery);
        if(urlResponse.IsSuccessStatusCode){
            ActorIMGsData = await urlResponse.Content.ReadAsStringAsync();
            actorImageGallery = JsonSerializer.Deserialize<ProfileSet>(ActorIMGsData);            
        }
        else{
            
            ActorIMGsData = null;
        }

        // API call for Actor/Actress's known posters and Store JSON data in ActorKnownPosters class
        HttpResponseMessage actorPostersResponse = await client.GetAsync(apiUrlKnownPosters);
        if(actorPostersResponse.IsSuccessStatusCode){
            ActorKnownPostersData = await actorPostersResponse.Content.ReadAsStringAsync();
            actorKnownPosters = JsonSerializer.Deserialize<ActorKnownPosters>(ActorKnownPostersData);  
        }
        else{
            ActorKnownPostersData = null;
        }
    }//GetActorDetails()

    public static async Task CreateOrUpdateMovie(int movie)
    {
        if (movie == 0)
        {
            // Handle invalid input
            return;
        }

        // Logic to create or update a movie using the provided 'movie' object
        // Example: await Fetch.CreateOrUpdateMovie(movie);
        // You may need to use posterSet or Poster properties here based on your logic
        // Replace the placeholder code below with your actual logic
        var posterSet = await GetPosterSet(); // Example: Assuming you have a method to get a PosterSet
        // Your logic here to use posterSet or Poster properties

        // Rest of the logic...
    }

    public static async Task DeleteMovie(int movieID)
    {
        if (movieID <= 0)
        {
            // Handle invalid input
            return;
        }

        // Logic to delete the movie with ID 'movieID'
        // Example: await Fetch.DeleteMovie(movieID);
        // You may need to use posterSet or Poster properties here based on your logic
        // Replace the placeholder code below with your actual logic
        var posterSet = await GetPosterSet(); // Example: Assuming you have a method to get a PosterSet
        var posterToRemove = posterSet?.results?.FirstOrDefault(p => p.id == movieID);

        // Your logic to delete the movie poster from the PosterSet
        if (posterToRemove != null)
        {
            posterSet.results?.Remove(posterToRemove);
            // Implement logic to update your data source (e.g., save changes to a database)
        }

        // Your logic to delete the movie from your data source (e.g., delete from a database)

        // Rest of the logic...
    }

    public static async Task UpdatePoster(int movieID, Poster updatedPoster)
    {
        if (movieID <= 0 || updatedPoster == null)
        {
            // Handle invalid input
            return;
        }

        // Logic to update the movie poster with ID 'movieID'
        // Example: await Fetch.UpdatePoster(movieID, updatedPoster);
        // You may need to use posterSet or Poster properties here based on your logic
        // Replace the placeholder code below with your actual logic
        var posterSet = await GetPosterSet(); // Example: Assuming you have a method to get a PosterSet
        var existingPoster = posterSet?.results?.FirstOrDefault(p => p.id == movieID);

        // Your logic to update the movie poster in the PosterSet
        if (existingPoster != null)
        {
            existingPoster.title = updatedPoster.title; // Update other properties accordingly
            // Implement logic to update your data source (e.g., save changes to a database)
        }

        // Rest of the logic...
    }

    public static async Task AddPoster(Poster newPoster)
    {
        if (newPoster == null)
        {
            // Handle invalid input
            return;
        }

        // Logic to add a new poster
        // Example: await Fetch.AddPoster(newPoster);
        // You may need to use posterSet or Poster properties here based on your logic
        // Replace the placeholder code below with your actual logic
        var posterSet = await GetPosterSet(); // Example: Assuming you have a method to get a PosterSet
        posterSet.results?.Add(newPoster);

        // Implement logic to update your data source (e.g., save changes to a database)

        // Rest of the logic...
    }

    // Your other methods remain unchanged...

    private static async Task<PosterSet> GetPosterSet()
    {
        // Implement logic to get a PosterSet from your data source
        // Example: Fetch data from an API or database
        // Replace the placeholder code below with your actual logic

        // For example purposes, you can return a dummy PosterSet
        return new PosterSet
        {
            page = 1,
            results = new List<Poster> { new Poster { id = 1, title = "Dummy Poster" } },
            total_pages = 1,
            total_results = 1
        };
    }

    private static void ClearHeaders(){
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("applicationException/json"));
    }
    
} //Fetch class