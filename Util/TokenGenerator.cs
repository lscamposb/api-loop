using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LoopApi.Util
{
    public static class TokenGenerator
    {
        public static string GenerateTokenJwt(IConfiguration config, string username, bool rememberMe)
        {
            var secretKey = config["Jwt:Key"];
            var audienceToken = config["Jwt:Audience"];
            var issuerToken = config["Jwt:Issuer"];
            var expireTime = config["Jwt:Expire_Minute"];

            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // create a claimsIdentity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) });

            int expireToken = 0;

            if (rememberMe)
                expireToken = 43800;
            else
                expireToken = Convert.ToInt32(expireTime);

            // create token to the user
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audienceToken,
                issuer: issuerToken,
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(expireToken),
                signingCredentials: signingCredentials);

            return tokenHandler.WriteToken(jwtSecurityToken);
        }
    }
}
