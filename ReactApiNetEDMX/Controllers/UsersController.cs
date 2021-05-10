using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReactApiNetEDMX;
using ReactApiNetEDMX.Store.Database;
using ReactApiNetEDMX.Store.Interfaces;
using ReactCoreApiApp;
using ReactCoreApiApp.Filters;
using ReactApiNetEDMX.Store.DataAccessObjects.Enum;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Text;

namespace ReactCoreApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller//BaseApiController<Users>
    {
        private IUsersRepository _usersRepository;
        //private IConfiguration _configuration;
        private JWTConfig _JWTConfiguration { get; }
        public UsersController(IUsersRepository usersRepository, IOptions<JWTConfig> JWTConfiguration)//IConfiguration configuration)
        {
            _usersRepository = usersRepository;
            //_configuration = configuration;
            _JWTConfiguration = JWTConfiguration.Value;
        }

        [Authorize]
        [Route("getlogin")]
        public IActionResult GetLogin()
        {
            return Ok($"Ваш логин: {User.Identity.Name}");
        }

        [Authorize(Roles="Admin")]
        [Route("getrole")]
        public IActionResult GetRole()
        {
            return Ok("Ваша роль: администратор");
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok();
        }

        //[HttpPost("/token")]
        [Route("token")]
        public IActionResult Token([FromBody] Users user)
        {

            var identity = GetIdentity(user.Login, user.Password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: _JWTConfiguration.ISSUER,
                    audience: _JWTConfiguration.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(_JWTConfiguration.LIFETIME)),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_JWTConfiguration.KEY)), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Ok(response);
        }

        private ClaimsIdentity GetIdentity(string login, string password)
        {
            Users users = _usersRepository.FindByLoginAndPassword(login, password);
            if (users != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, users.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, ((Role)users.Role).ToString())
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
        //public UsersController(IGenericRepository<Users> repository) : base(repository)
        //{
        //}
        // GET: api/Users
    //    [HttpGet]
    //    public override IList<Users> Get(int? _page = null, int? _perPage = null, string _sortDir = null, string _sortField = null, string filter = null)
    //    {
    //        return base.Get(_page, _perPage, _sortDir, _sortField, filter);
    //    }

    //    // GET: api/Users/5
    //    [HttpGet("{id}")]
    //    public override IActionResult Get(int id)
    //    {
    //        return base.Get(id);
    //    }

    //    //// PUT: api/Users/5
    //    [HttpPut("{id}")]
    //    public override IActionResult Put(Users Users)
    //    {
    //        Response.Headers.Add("Access-Control-Expose-Headers", "*");
    //        return base.Put(Users);
    //    }

    //    //// POST: api/Users
    //    [HttpPost]
    //    public override IActionResult Post(Users Users)
    //    {
    //        return base.Post(Users);
    //    }

    //    //// DELETE: api/Users/5
    //    [HttpDelete("{id}")]
    //    public override IActionResult Delete(Users Users)
    //    {
    //        return base.Delete(Users);
    //    }

    //    // DELETE: api/Users/filter={,,,}
    //    [HttpDelete]
    //    public override IActionResult Delete(string filter)
    //    {
    //        return base.Delete(filter);
    //    }
    //}
}
