using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public async Task<string> Get()
        {
            var res = JsonConvert.SerializeObject(await _service.GetAllAsync());
            return JsonConvert.SerializeObject(await _service.GetAllAsync());
        }

        // GET api/<NotesController>/5
        [HttpGet("{id}")]
        public async Task<string> Get(Guid id)
        {
            return JsonConvert.SerializeObject(await _service.GetByIdAsync(id));
        }

        // POST api/<NotesController>
        [HttpPost]
        public async Task Post([FromForm] Note note)
        {
            note.CreationDate = DateTime.Now.Date;
            await _service.CreateAsync(note);
        }

        // PUT api/<NotesController>/5
        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromForm] Note note)
        {          
            await _service.UpdateAsync(id, note);
        }

        // DELETE api/<NotesController>/5
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _service.RemoveAsync(await _service.GetByIdAsync(id));
        }
    }
}
