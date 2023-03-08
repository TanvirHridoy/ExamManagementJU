using CertificationMS.ContextModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CertificationMS.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherAPIController : ControllerBase
    {
        private readonly CertificateMSV2Context _Db;
        public TeacherAPIController(CertificateMSV2Context Db)
        {
            _Db = Db;
        }
        // GET: api/<TeacherAPIController>
        [HttpGet("{TeacherId}/{Date}")]
        public async Task<ActionResult> Get(int TeacherId,string Date)
        {

            try
            {
                var Examdate = Convert.ToDateTime(Date);
                var data = await _Db.VwTeacherExamDuties.Where(e => e.ExamDate.Value.Date == Examdate.Date && e.TeacherId == TeacherId).ToListAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {

                return BadRequest($"Error: {ex.Message}");
            }
            
        }

        // GET api/<TeacherAPIController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TeacherAPIController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TeacherAPIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TeacherAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
