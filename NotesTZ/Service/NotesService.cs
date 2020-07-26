using NotesTZ.Models;
using NotesTZ.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesTZ.Service
{
    public class NotesService : IService<Note>
    {
        IRepository<Note> _repository { get; set; }
        public NotesService(IRepository<Note> repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(Note note)
        {
            await _repository.CreateAsync(note);
        }

        public async Task<IReadOnlyCollection<Note>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Note> GetByIdAsync(Guid id)
        {
            var note = await _repository.GetByIdAsync(id);
            if (!(note is null))
                return note;
            return null;
        }

        public async Task RemoveAsync(Note note)
        {
            await _repository.RemoveAsync(note);
        }

        public async Task UpdateAsync(Guid Id, Note newNote)
        {
            var note = await _repository.GetByIdAsync(Id);
            if (!(note is null))
            {
                note.Content = newNote.Content;
                note.Heading = newNote.Heading;
                await _repository.UpdateAsync(note);
            }
        }
    }
}
