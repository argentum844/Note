using Duende.IdentityServer.Models;
using Note.BL.Auth.Entities;
using Note.DataAccess.Entities;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Note.BL.Auth;

namespace Note.BL.Auth;

public class AuthProvider : IAuthProvider
{
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly UserManager<UserEntity> _UserManager;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _identityServerUri;
    private readonly string _clientId;
    private readonly string _clientSecret;

    public AuthProvider(SignInManager<UserEntity> signInManager, UserManager<UserEntity> UserManager,
        IHttpClientFactory httpClientFactory,
        string identityServerUri,
        string clientId,
        string clientSecret)
    {
        _signInManager = signInManager;
        _UserManager = UserManager;
        _identityServerUri = identityServerUri;
        _httpClientFactory = httpClientFactory;
        _clientId = clientId;
        _clientSecret = clientSecret;
    }

    public async Task<TokensResponse> AuthorizeUser(string email, string password)
    {
        var User = await _UserManager.FindByEmailAsync(email); //IRepository<UserEntity> get User by email
        if (User is null)
        {
            throw new Exception(); //UserNotFoundException, BusinessLogicException(Code.UserNotFound);
        }

        var verificationPasswordResult = await _signInManager.CheckPasswordSignInAsync(User, password, false);
        if (!verificationPasswordResult.Succeeded)
        {
            throw new Exception(); //AuthorizationException, BusinessLogicException(Code.PasswordOrLoginIsIncorrect);
        }

        var client = _httpClientFactory.CreateClient();
        var discoveryDoc = await client.GetDiscoveryDocumentAsync(_identityServerUri);
        if (discoveryDoc.IsError)
        {
            throw new Exception();
        }

        var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest()
        {
            Address = discoveryDoc.TokenEndpoint,
            GrantType = GrantType.ResourceOwnerPassword,
            ClientId = _clientId,
            ClientSecret = _clientSecret,
            UserName = User.Name,
            Password = password,
            Scope = "api offline_access"
        });

        if (tokenResponse.IsError)
        {
            throw new Exception();
        }

        return new TokensResponse()
        {
            AccessToken = tokenResponse.AccessToken,
            RefreshToken = tokenResponse.RefreshToken
        };
    }

    public async Task RegisterUser(string email, string password)
    {
        var User = await _UserManager.FindByNameAsync(email);
        if (!(User is null))
        {
            throw new Exception("User already exists");
        }

        UserEntity userEntity = new UserEntity()
        {
            Login = email,
            Name = email
        };

        var createUserResult = await _UserManager.CreateAsync(userEntity, password);

        if (!createUserResult.Succeeded)
        {
            throw new Exception("Error create User");
        }
    }
}