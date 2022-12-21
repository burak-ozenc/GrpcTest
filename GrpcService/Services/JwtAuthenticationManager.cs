using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GrpcService1;
using Microsoft.IdentityModel.Tokens;

namespace GrpcService.Protos;

public static class JwtAuthenticationManager
{
    public const string JWT_TOKEN_KEY = "TesteeeeeeeeeeasKey@2023"; 
    private const int JWT_TOKEN_VALIDITY = 39; 
    
    public static AuthenticationResponse Authenticate(AuthenticationRequest request)
    {
        string userRole = String.Empty;
        
        if (request.UserName == "test"  && request.Password == "test3")
        {
            userRole = "Administrator";
        }
        else if (request.UserName == "user"  && request.Password == "user")
        {
            userRole = "User";
        }
        else
        {
            return null;
        }

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.ASCII.GetBytes(JWT_TOKEN_KEY);
        var tokenExpiryDateTime = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY);
        var securityTokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new List<Claim>
            {
                new("username", request.UserName),
                new(ClaimTypes.Role, userRole)
            }),
            Expires = tokenExpiryDateTime,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
        var token = jwtSecurityTokenHandler.WriteToken(securityToken);

        return new AuthenticationResponse()
        {
            AccessToken = token,
            Expires = (int)tokenExpiryDateTime.Subtract(DateTime.Now).TotalSeconds
        };

    }
}