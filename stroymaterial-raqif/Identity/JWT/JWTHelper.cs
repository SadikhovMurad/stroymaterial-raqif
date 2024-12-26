using Core.Utilities.Security.Encyption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using stroymaterial_raqif.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace stroymaterial_raqif.Identity.JWT
{
    public class JWTHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;

        public JWTHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public AccessToken CreateToken(User user, List<string> roles)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, roles);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<string> roles)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, roles), // Claims əlavə edilir
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user, List<string> roles)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, $"{user.Firstname} {user.Lastname}")
        };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role)); // Rollar əlavə edilir
            }

            return claims;
        }
    }
    //public class JWTHelper : ITokenHelper
    //{
    //    public IConfiguration Configuration { get; }
    //    private TokenOptions _tokenOptions;
    //    private DateTime _accessTokenExpiration;

    //    public JWTHelper(IConfiguration configuration)
    //    {
    //        Configuration = configuration;
    //        _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
    //    }

    //    public AccessToken CreateToken(User user, List<string> roles)
    //    {
    //        _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
    //        var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
    //        var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
    //        var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, roles);
    //        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
    //        var token = jwtSecurityTokenHandler.WriteToken(jwt);

    //        return new AccessToken
    //        {
    //            Token = token,
    //            Expiration = _accessTokenExpiration
    //        };
    //    }

    //    public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
    //        SigningCredentials signingCredentials, List<string> roles)
    //    {
    //        var jwt = new JwtSecurityToken(
    //            issuer: tokenOptions.Issuer,
    //            audience: tokenOptions.Audience,
    //            expires: _accessTokenExpiration,
    //            notBefore: DateTime.Now,
    //            signingCredentials: signingCredentials
    //        );
    //        return jwt;
    //    }

    //    //private IEnumerable<Claim> SetClaims(User user, List<string> roles)
    //    //{
    //    //    var claims = new List<Claim>();
    //    //    claims.AddNameIdentifier(user.Id.ToString());
    //    //    claims.AddEmail(user.Email);
    //    //    claims.AddName($"{user.FirstName} {user.LastName}");
    //    //    claims.AddRoles(roles.ToArray()); // Burada yalnız rollar əlavə edilir.

    //    //    return claims;
    //    //}
    //}
}
