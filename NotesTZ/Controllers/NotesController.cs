using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotesTZ.Models;
using NotesTZ.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NotesTZ.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {

        IService<Note> _service;
        public NotesController(IService<Note> service)
        {
            _service = service;
        }

        // GET: api/<NotesController>
        [HttpGet]
        public async Task<IEnumerable<Note>> Get()
        {
           return await _service.GetAllAsync();
        }

        // GET api/<NotesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<NotesController>
        [HttpPost]
        public void Post(Note note)
        {

        }

        // PUT api/<NotesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NotesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
