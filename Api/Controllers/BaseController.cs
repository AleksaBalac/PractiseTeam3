using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            
        }

        public string GetUserIdFromToken()
        {
            var token = Request.Headers["Authorization"];
            
            if (token == "null") return null;

            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken tokenS = handler.ReadToken(token) as JwtSecurityToken;

            var id = tokenS?.Claims?.FirstOrDefault(a => a.Type == "id")?.Value;

            return id;
        }
    }
}