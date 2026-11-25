public sealed class UserInfo
{
    public required string UserID {get;set;}
    public required string UserName {get;set;}
    public string? email {get;set;}
    public string? display_name {get;set;}
    public string[] roles {get;set;} = [];

}