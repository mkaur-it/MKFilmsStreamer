using Microsoft.AspNetCore.Mvc.RazorPages;
using MKFilmsStreamer.API;
using MKFilmsStreamer.Models;

namespace MKFilmsStreamer.Pages;

public class ActorModel : PageModel {
    public string? actorName;
    public string? actorBiography;
    public string? actorBirthday;
    public string? actorDeathday;
    public string? actorGender;
    public string? actorBirthPlace;
    public string? actorKnownFor;
    public string? actorProfilePath;
    public string? actorHomepage;
    public string? actorInstaId;
    public string? actorFacebookId;
    public string? actorTwitterId;

    public string profile_path = "https://image.tmdb.org/t/p/w500";
    public string file_path = "https://image.tmdb.org/t/p/w500";
    public List<string> actorIMGs = new List<string>();
    public List<string>? actorKnownAs = new List<string>();
    // public List<ActorImage>? actorImages = new List<ActorImage>();

    public int age;
    public string[]? birthDate;
    public string[]? deathDate;

    public int overview_cutoff = 100;
    public string imagePath = "https://image.tmdb.org/t/p/w500";
    public List<string>? movieTitles = new List<string>();
    public List<string>? overviews = new List<string>();
    public List<string>? posterURLs = new List<string>();
    public List<string>? releaseDate = new List<string>();
    public List<string>? movieIDs = new List<string>();
    public List<List<string>> genreSet = new List<List<string>>();
     
    public async Task OnGet(string actorID){
        // Get actor details from API and store data in Actor class 
        await Fetch.GetActorDetails(actorID);

        // Get actor name
        actorName = Fetch.actor.name;

        // Determine actor gender
        if(Fetch.actor.gender == 1){
            actorGender = "Female";
        } 
        else{
            actorGender = "Male";
        }

        // Get actor biography
        if((Fetch.actor.biography != null) || (Fetch.actor.biography != "")){
             actorBiography = Fetch.actor.biography;
        } 
        else{
             actorBiography = "No Information Available";
        }

        // Get actor birthday
        if(Fetch.actor.birthday != null){
            birthDate = Fetch.actor.birthday.Split('-');
            actorBirthday =  Tools.formatDate(birthDate);
        }
        else{
            actorBirthday = "No Information Available";
        }

        // Get actor deathday (if applicable) and calculate age
        if(Fetch.actor.birthday != null){
            if(Fetch.actor.deathday != null)
            {
                DateTime deathDay = DateTime.Parse(Fetch.actor.deathday);
                DateTime birthDay = DateTime.Parse(Fetch.actor.birthday);
                deathDate = Fetch.actor.deathday.Split('-');
                age = Tools.calulateAge(birthDay,deathDay); 
                actorDeathday = Tools.formatDate(deathDate) + " (" + age + " years old)";
            }else
            {
                DateTime now = DateTime.Now;
                DateTime birthDay = DateTime.Parse(Fetch.actor.birthday);
                age = Tools.calulateAge(birthDay,now); 
                actorDeathday = null;
                actorBirthday += " (" + age + " years old)";
            }
        }

        // Get actor's place of birth
        if(Fetch.actor.place_of_birth != null){
            actorBirthPlace = Fetch.actor.place_of_birth;
        }else{
            actorBirthPlace = "No Information Available";
        }
        
        // Get actor's department
        if(Fetch.actor.known_for_department != null){
            actorKnownFor = Fetch.actor.known_for_department;
        }else{
            actorKnownFor = "No Information Available";
        }
        
        // Get actor aliases
        if(Fetch.actor.also_known_as != null){
            actorKnownAs = Fetch.actor.also_known_as;
        }
        else{
            actorKnownAs = null;
        }
        // Get actor aliases
        if(Fetch.actor.homepage != null){
            actorHomepage = Fetch.actor.homepage;
        }
        else{
            actorHomepage = null;
        }
        
        // Get actor profile path and images
        if(Fetch.actor.profile_path != null){
             actorProfilePath = profile_path + Fetch.actor.profile_path;
        }
        else{
             actorProfilePath = "./assets/images/noActorProfilePic.png";
        }
       
        // Get actor Instagram Profile
        if((Fetch.actorSocialMediaSet.instagram_id != null) && (Fetch.actorSocialMediaSet.instagram_id != "")){
            actorInstaId = "https://www.instagram.com/" + Fetch.actorSocialMediaSet.instagram_id;
        }
        else{
            actorInstaId = null;
        }

        // Get actor Facebook Profile
        if((Fetch.actorSocialMediaSet.facebook_id != null)  && (Fetch.actorSocialMediaSet.facebook_id.ToString() != "")){
            actorFacebookId = "https://www.facebook.com/" + Fetch.actorSocialMediaSet.facebook_id.ToString();
        }
        else{
            actorFacebookId = null;
        }

        // Get actor Twitter Profile
        if((Fetch.actorSocialMediaSet.twitter_id != null) && (Fetch.actorSocialMediaSet.twitter_id != "")){
            actorTwitterId = "https://www.twitter.com/" + Fetch.actorSocialMediaSet.twitter_id;
        }
        else{
            actorTwitterId = null;
        } 

        // loop through actor images 
        if(Fetch.actorImageGallery.profiles.Count < 1){
            actorIMGs.Add("./assets/images/noActorProfilePic.png");
        }
        else{
            for(int i = 0; i < Fetch.actorImageGallery.profiles.Count; i++) {
                if(Fetch.actorImageGallery.profiles[i].file_path != null) {
                    actorIMGs.Add(file_path + Fetch.actorImageGallery.profiles[i].file_path);
                }
            }
        }// actor images

        // Get Actor Also known Posters 

        foreach(Cast cast in Fetch.actorKnownPosters.cast)
        {
            movieTitles.Add(cast.title);
            if(cast.overview.Length >= overview_cutoff) {
                overviews.Add(cast.overview.Substring(0, overview_cutoff) + "...");
            }
            else {
                overview_cutoff = cast.overview.Length;
                overviews.Add(cast.overview.Substring(0, overview_cutoff) + "...");
            }
            if(cast.poster_path != null){
                string path = imagePath + cast.poster_path;
                posterURLs.Add(path);
            }
            else{
                string path = "./assets/images/noPoster.jpg";
                posterURLs.Add(path);
            }  
            if((cast.release_date != null) && (cast.release_date != "")){
                string[] rawdate = cast.release_date.Split('-');
                string formattedDate = Tools.formatDate(rawdate);
                releaseDate.Add(formattedDate);
            }
            movieIDs.Add(cast.id.ToString());
            List<int> codedGenres = cast.genre_ids;
            List<string> decodedGenres = Tools.decodeGenres(codedGenres);
            genreSet.Add(decodedGenres);
        }   
    } // OnGet()

     public async Task OnPostDetails(int movieID){
        // redirect to the movie page
        // pass along the movieID
        Response.Redirect("./Movie?_id=" + movieID);
    } // OnPostDetails()
}// class
