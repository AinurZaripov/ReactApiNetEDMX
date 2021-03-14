using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
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
    public class ProductsController : BaseApiController<Products>
    {
        public ProductsController(IGenericRepository<Products> repository) : base(repository)
        {
        }

        // GET: api/Products
        [HttpGet]
        public override IList<Products> Get(int? _page = null, int? _perPage = null, string _sortDir = null, string _sortField = null, string filter = null)
        {
            return base.Get(_page, _perPage, _sortDir, _sortField, filter);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public override IActionResult Get(int id)
        {
            return base.Get(id);
        }

        //// PUT: api/Products/5
        [HttpPut("{id}")]
        public override IActionResult Put(Products Products)
        {
            return base.Put(Products);
        }

        //// POST: api/Products
        [HttpPost]
        public override IActionResult Post(Products Products)
        {
            return base.Post(Products);
        }

        //// DELETE: api/Products/5
        [HttpDelete("{id}")]
        public override IActionResult Delete(Products Products)
        {
            return base.Delete(Products);
        }

        // DELETE: api/Products/filter={,,,}
        [HttpDelete]
        public override IActionResult Delete(string filter)
        {
            return base.Delete(filter);
        }
    }
}
