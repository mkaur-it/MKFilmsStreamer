   
   public static class Tools{

    public static string formatDate(string[] rawdate){
        string month = "";
        string finalDate = "";
        switch(rawdate[1]){
            case "1":
            case "01":{ 
                month = "January";
                break;
            }
            case "2":
            case "02":{ 
                month = "Feburary";
                break;
            }
            case "3":
            case "03":{ 
                month = "March";
                break;
            }
            case "4":
            case "04":{ 
                month = "April";
                break;
            }
            case "5":
            case "05":{ 
                month = "May";
                break;
            }
            case "6":
            case "06":{ 
                month = "June";
                break;
            }
            case "7":
            case "07":{ 
                month = "July";
                break;
            }
            case "8":
            case "08":{ 
                month = "August";
                break;
            }
            case "9":
            case "09":{ 
                month = "September";
                break;
            }
            case "10":{ 
                month = "October";
                break;
            }
            case "11":{ 
                month = "November";
                break;
            }
            case "12":{ 
                month = "December";
                break;
            }

            default:
                month ="Unknown";
                break;

        }
        //"raw date" YYYY-mm-dd e.g.2022-12-28
        finalDate = month + " " + rawdate[2] + ", " + rawdate[0];
        return finalDate;
    } // dateFormat(string[] rawdate)
  
    public static int calulateAge(DateTime birthDate,DateTime referenceDate) { 
        DateTime birthDay = birthDate;
        DateTime refDay = referenceDate;
        int refYear = refDay.Year;
        int birthYear = birthDay.Year;
        int age = refYear - birthYear;
        if (birthDay > refDay.AddYears(-age)){
             age--;
        }    
        return age;
    } // calulateAge(string[] birthDate,string[] referenceDate)
  
    public static List<string> decodeGenres(List<int> codedGenres){
         List<string> decodedGenres = new List<string>();
         for(int i = 0; i < codedGenres.Count; i++){
            switch(codedGenres[i]){
                case 12:{
                    decodedGenres.Add("Adventure");
                    break;
                }
                case 14:{
                    decodedGenres.Add("Fantasy");
                    break;
                }
                case 16:{
                    decodedGenres.Add("Animation");
                    break;
                }
                case 18:{
                    decodedGenres.Add("Drama");
                    break;
                }
                case 27:{
                    decodedGenres.Add("Horror");
                    break;
                }
                case 28:{
                    decodedGenres.Add("Action");
                    break;
                }
                case 35:{
                    decodedGenres.Add("Comedy");
                    break;
                }
                case 36:{
                    decodedGenres.Add("History");
                    break;
                }
                case 37:{
                    decodedGenres.Add("Western");
                    break;
                }
                case 53:{
                    decodedGenres.Add("Thriller");
                    break;
                }
                case 80:{
                    decodedGenres.Add("Crime");
                    break;
                }
                case 99:{
                    decodedGenres.Add("Documentary");
                    break;
                }
                case 878:{
                    decodedGenres.Add("Sci-Fi");
                    break;
                }
                case 9648:{
                    decodedGenres.Add("Mystery");
                    break;
                }
                case 10402:{
                    decodedGenres.Add("Music");
                    break;
                }
                case 10749:{
                    decodedGenres.Add("Romance");
                    break;
                }
                case 10751:{
                    decodedGenres.Add("Family");
                    break;
                }
                case 10752:{
                    decodedGenres.Add("War");
                    break;
                }
                case 10770:{
                    decodedGenres.Add("TV Movie");
                    break;
                }
               
                default:
                    decodedGenres.Add("No information");
                    break;

            } //switch()
         }//for loop
         return decodedGenres;
    } //decodeGenres(List<int> codedGenres)
   } //Common Class