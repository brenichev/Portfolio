using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class SignsController : ControllerBase
    {
        SignsContext db;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        /*public SignsController(IConfiguration configuration, IWebHostEnvironment env)
        {
            
        }*/

        public SignsController(SignsContext context, IConfiguration configuration, IWebHostEnvironment env)
        {
            db = context;
            if (!db.Signs.Any())
            {
                db.Signs.Add(new Sign { SignName = "Sign1", PhotoFileName = "1.png"});
                db.Signs.Add(new Sign { SignName = "Sign2" });
                db.Signs.Add(new Sign { SignName = "Sign3" });
                db.Signs.Add(new Sign { SignName = "Sign4" });
                db.Signs.Add(new Sign { SignName = "Sign5" });
                db.Signs.Add(new Sign { SignName = "Sign6" });
                db.Signs.Add(new Sign { SignName = "Sign7" });
                db.Signs.Add(new Sign { SignName = "Sign8" });
                db.Signs.Add(new Sign { SignName = "Sign9" });
                db.SaveChanges();
            }
            _configuration = configuration;
            _env = env;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sign>>> Get()
        {
            return await db.Signs.ToListAsync();
        }

        // GET api/signs/5
        [AllowAnonymous]
        [HttpGet("{id}")]        
        public async Task<ActionResult<Sign>> Get(int id)
        {
            Sign sign = await db.Signs.FirstOrDefaultAsync(x => x.SignId == id);
            if (sign == null)
                return NotFound();
            return new ObjectResult(sign);
        }

        // POST api/signs
        [HttpPost]
        public async Task<ActionResult<Sign>> Post(Sign sign)
        {
            if (sign == null)
            {
                return BadRequest();
            }

            db.Signs.Add(sign);
            await db.SaveChangesAsync();
            return Ok(sign);
        }

        // PUT api/signs/
        [HttpPut]
        public async Task<ActionResult<Sign>> Put(Sign sign)
        {
            if (sign == null)
            {
                return BadRequest();
            }
            if (!db.Signs.Any(x => x.SignId == sign.SignId))
            {
                return NotFound();
            }

            db.Update(sign);
            await db.SaveChangesAsync();
            return Ok(sign);
        }

        // DELETE api/signs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Sign>> Delete(int id)
        {

            Sign sign = db.Signs.FirstOrDefault(x => x.SignId == id);
            if (sign == null)
            {
                return NotFound();
            }
            db.Signs.Remove(sign);
            await db.SaveChangesAsync();
            return Ok(sign);
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "\\Photos\\" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {

                return new JsonResult("anonymous.png");
            }
        }
    }

    /*
    [Route("api/[controller]")]
    [ApiController]
    public class SignsController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
    */
}
