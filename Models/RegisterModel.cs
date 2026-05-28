namespace Frontend.Models;

public class RegisterModel
{
    public string? Email  { get; set; }
    
    public string? Password { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public MembershipType Membership { get; set; }
    public MembershipStatus MembershipStatus { get; set; }
}

public enum MembershipType
{
    Student,
    Standard,
    Premium
}

public enum MembershipStatus
{
    Active,
    Inactive,
}