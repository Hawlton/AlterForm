using Microsoft.AspNetCore.Identity;

namespace AlterForm;

public class AlterUser : IdentityUser<Guid>
{
    public string? display_name {get;set;} 
    public string? current_species {get;set;}
    public string? bio {get;set;}
    public byte[] avatar_bytes {get;set;} = []; // I know, I don't feel like dealing with s3 buckets rn.
    public byte[] banner_bytes {get;set;} = []; // You've done worse don't judge.
    public MaturityRating maturity_ceiling {get;set;} = MaturityRating.General;

}