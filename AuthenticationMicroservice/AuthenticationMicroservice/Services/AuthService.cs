using AuthenticationMicroservice.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationMicroservice.Services
{
    public interface IAuthService
    {
        public bool IsRoleValid(string role);
        public bool IsUserValid(User user, out AuthUser authUser);
        public string GenerateToken(AuthUser authUser);
        public AuthUser VerifyToken(TokenString token);
    }

    public class AuthService : IAuthService
    {
        private const string SECRET_KEY = "secret-key-value";

        public bool IsRoleValid(string role)
        {
            return Role.Exists(role);
        }

        public bool IsUserValid(User user, out AuthUser authUser)
        {
            try
            {
                authUser = null;
                string reqAddress = "https://localhost:";
                switch (user.Role)
                {
                    case Role.Manager:
                        reqAddress += "44348/api/Manager/validateManager";
                        break;
                    case Role.Executive:
                        reqAddress += "44348/api/Manager/validateExecutive";
                        break;
                    case Role.Customer:
                        reqAddress += "44366/api/Customer/validate";
                        break;
                    default:
                        break;
                }
                HttpResponseMessage response = new HttpResponseMessage();
                HttpClientHandler clientHandler = new HttpClientHandler();
                HttpContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    response = client.PostAsync(reqAddress, content).Result;
                    if ((int)response.StatusCode != 200) return false;
                    authUser = JsonConvert.DeserializeObject<AuthUser>(response.Content.ReadAsStringAsync().Result);
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GenerateToken(AuthUser authUser)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(SECRET_KEY);
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[] { new Claim("id", authUser.Id.ToString()), new Claim("role", authUser.Role) }),
                    Expires = DateTime.UtcNow.AddSeconds(2000),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public AuthUser VerifyToken(TokenString tokenObj)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(SECRET_KEY);
                tokenHandler.ValidateToken(tokenObj.Token, new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                AuthUser authUser = new AuthUser();
                authUser.Id = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                authUser.Role = jwtToken.Claims.First(x => x.Type == "role").Value;
                return authUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
