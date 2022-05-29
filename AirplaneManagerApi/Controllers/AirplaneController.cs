using System;
using System.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projekt.Model;
using projekt.Repository;
using projekt.Extensions;


namespace projekt.Controllers
{
    [Route("api/[controller]")]
    public class AirplaneController : Controller
    {
        private readonly IAirplaneRepository airplaneRepo;

        public AirplaneController(IAirplaneRepository airplaneRepo)
        {
            this.airplaneRepo = airplaneRepo;
        }

        [HttpGet]
        public async Task<ActionResult<Object?>> Get()
        {

            var res = await airplaneRepo.Get();
            if (res.Item1 == null) return Ok((Object?)res.Item2);
            return StatusCode(500);
        }

        [HttpGet("search/{keyword}")]
        public async Task<ActionResult<Object?>> Search(string keyword)
        {
            try
            {
                var res = await airplaneRepo.Search(keyword);
                if (res.Item1 == null) return Ok((Object?)res.Item2);
                return StatusCode(500);
            }
            catch (Exception)
            {
                return StatusCode(400);
            }


        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Object?>> GetAirplaneById(Guid id)
        {
            try
            {
                var res = await airplaneRepo.Get(id);
                if (res.Item1 == null && res.Item2 == null) return NotFound();
                if (res.Item1 != null) return StatusCode(500);
                return Ok(res.Item2);
            }
            catch (Exception)
            {
                return StatusCode(400);
            }

        }


        [HttpPost]
        public async Task<ActionResult<string>> CreateAirplane([FromBody] IAirplane airplane)
        {
            try
            {
                airplane.Id = Guid.NewGuid();
                if (airplane.Verify() != null) return StatusCode(400);
                var res = await airplaneRepo.Post(airplane);
                if (res != null) return StatusCode(500);
                //return CreatedAtAction(nameof(GetAirplaneById), new { Id = airplane.Id }, airplane);
                return Ok();
            }
            catch (Exception) {
                return StatusCode(400);
            }

            //return HttpContext.Request.Path.ToString()+ "/api/Airplane/img.png";
        }

        [HttpPut]
        public async Task<ActionResult<string>> UpdateAirplane([FromBody] IAirplane airplane)
        {
            try
            {
                if (airplane.Verify() != null) return StatusCode(400);
                var arpl = await airplaneRepo.Get(airplane.Id);
                if (arpl.Item1 == null && arpl.Item2 == null) return NotFound();
                var res = await airplaneRepo.Update(airplane);
                if (res == null) return Ok();
                return StatusCode(500);
            }
            catch (Exception)
            {
                return StatusCode(400);
            }

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<string>> DeleteAirplane(Guid Id)
        {
            try
            {
                var airplane = await airplaneRepo.Get(Id);
                if (airplane.Item1 == null && airplane.Item2 == null) return NotFound();
                var res = await airplaneRepo.Delete(Id);
                if (res == null) return Ok();
                return StatusCode(500);
            }
            catch(Exception)
            {
                return StatusCode(400);
            }
            
        }

    }
}

