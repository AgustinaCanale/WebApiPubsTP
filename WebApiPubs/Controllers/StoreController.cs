using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApiPubs.Models;

namespace WebApiPubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {

        private readonly pubsContext context;
        public StoreController(pubsContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Store>> Get()
        {
            return context.Stores.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Store> GetById(string id)
        {
            Store store = (from s in context.Stores
                           where s.StorId == id
                           select s).FirstOrDefault();
            if (store == null)
            {
                return NotFound();
            }
            return store;
        }


      

        [HttpPost]
        public ActionResult<Store> Post(Store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Stores.Add(store);
            context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Store store)
        {
            if (store.StorId != id)
            {
                return BadRequest();
            }
            context.Entry(store).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Store> Delete(string id)
        {
            Store store = (from s in context.Stores
                           where s.StorId == id
                           select s).FirstOrDefault();
            if (store == null)
            {
                return NotFound();
            }
            context.Stores.Remove(store);
            context.SaveChanges();
            return store;
        }

        [HttpGet("name/{name}")]
        public ActionResult<IEnumerable<Store>> GetByName(string name)
        {
            List<Store> stores = (from s in context.Stores
                                  where s.StorName == name
                                  select s).ToList();
            if (stores.Count == 0)
            {
                return NotFound();
            }
            return stores;
        }

        [HttpGet("zip/{zip}")]
        public ActionResult<IEnumerable<Store>> GetByZip(string zip)
        {
            List<Store> stores = (from s in context.Stores
                                  where s.Zip == zip
                                  select s).ToList();
            if (stores.Count == 0)
            {
                return NotFound();
            }
            return stores;
        }

        [HttpGet("{city}/{state}")]
        public ActionResult<IEnumerable<Store>> GetByCityState(string city, string state)
        {
            List<Store> stores = (from s in context.Stores
                                  where s.City == city && s.State == state
                                  select s).ToList();
            if (stores.Count == 0)
            {
                return NotFound();
            }
            return stores;
        }
    }
}
