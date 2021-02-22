using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhonesController : ControllerBase
    {
        PhoneContext db;
        public PhonesController(PhoneContext _db)
        {
            db = _db;
            if (!db.Phones.Any())
            {
                db.Phones.Add(new Phone { Model = "Galaxy S11", Name = "Samsung", Price = 11000 });
                db.Phones.Add(new Phone { Model = "S9", Name = "IPhone", Price = 31000 });
                db.Phones.Add(new Phone { Model = "6300", Name = "Nokia", Price = 100000 });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Phone>>> Get()
        {
            return await db.Phones.ToListAsync();
        }

        // GET api/phones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Phone>> Get(int id)
        {
            var phone = await db.Phones.FirstOrDefaultAsync(x => x.Id == id);
            if (phone != null)
            {
                return new ObjectResult(phone);
            }
            else return NotFound();
        }

        // POST api/phones/
        [HttpPost]
        public async Task<ActionResult<Phone>> Post(Phone phone)
        {
            if (phone != null)
            {
                db.Phones.Add(phone);
                await db.SaveChangesAsync();
                return Ok(phone);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/phones/
        [HttpPut]
        public async Task<ActionResult<Phone>> Put(Phone phone)
        {
            if (phone != null)
            {
                if (!db.Phones.Any(x => x.Id == phone.Id))
                {
                    return NotFound();
                }
                else
                {
                    db.Update(phone);
                    await db.SaveChangesAsync();
                    return Ok(phone);
                }
            }
            else return BadRequest();
        }

        // DELETE api/phones/5
        [HttpDelete]
        public async Task<ActionResult<Phone>> Delete(int id)
        {
            var phone = await db.Phones.FirstOrDefaultAsync(x => x.Id == id);
            if (phone != null)
            {
                db.Phones.Remove(phone);
                await db.SaveChangesAsync();
                return Ok(phone);
            }
            else return NotFound();

        }
    }
}
