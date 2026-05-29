namespace Frontend.Services;

//holder jwt tokenet i hukkomelse så det bruges på tværs af komponenter. 
//nulstilles når brugere lukker browseren. 
public class TokenProvider
{
    public string? Token { get; set; }
}