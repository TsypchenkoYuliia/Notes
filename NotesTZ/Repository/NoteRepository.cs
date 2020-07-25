using Microsoft.EntityFrameworkCore;
using NotesTZ.Context;
using NotesTZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NotesTZ.Repository
{
    public class NoteRepository:IRepository<Note>
    {
        private readonly NoteContext _context;
        public NoteRepository(NoteContext context) {_context = context;}

        public async Task CreateAsync(Note note)
        {
            await _context.AddAsync(note);
            await _context.SaveChangesAsync();
        }
       
        public async Task<IReadOnlyCollection<Note>> GetAllAsync()
        {
            return await _context.Notes.ToListAsync();
        }

        public async Task<Note> GetByIdAsync(Guid id)
        {
            return await _context.Notes.FindAsync(id);
        }

        public async Task RemoveAsync(Note note)
        {
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Note note)
        {
            _context.Notes.Update(note);
            await _context.SaveChangesAsync();
        }

        
    }
}
