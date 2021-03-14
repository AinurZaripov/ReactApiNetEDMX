using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApiNetEDMX.Store.Database;
using ReactApiNetEDMX.Store.Interfaces;
using ReactCoreApiApp;
using ReactCoreApiApp.Filters;

namespace ReactCoreApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseApiController<Users>
    {
        public UsersController(IGenericRepository<Users> repository) : base(repository)
        {
        }

        // GET: api/Users
        [HttpGet]
        public override IList<Users> Get(int? _page = null, int? _perPage = null, string _sortDir = null, string _sortField = null, string filter = null)
        {
            return base.Get(_page, _perPage, _sortDir, _sortField, filter);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public override IActionResult Get(int id)
        {
            return base.Get(id);
        }

        //// PUT: api/Users/5
        [HttpPut("{id}")]
        public override IActionResult Put(Users Users)
        {
            Response.Headers.Add("Access-Control-Expose-Headers", "*");
            return base.Put(Users);
        }

        //// POST: api/Users
        [HttpPost]
        public override IActionResult Post(Users Users)
        {
            return base.Post(Users);
        }

        //// DELETE: api/Users/5
        [HttpDelete("{id}")]
        public override IActionResult Delete(Users Users)
        {
            return base.Delete(Users);
        }

        // DELETE: api/Users/filter={,,,}
        [HttpDelete]
        public override IActionResult Delete(string filter)
        {
            return base.Delete(filter);
        }
    }
}
