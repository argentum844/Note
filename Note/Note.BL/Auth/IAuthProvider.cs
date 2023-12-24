using Note.BL.Auth.Entities;

namespace Note.BL.Auth;

public interface IAuthProvider
{
    Task<TokensResponse> AuthorizeUser(string email, string password);
    //register - do by yourself
}