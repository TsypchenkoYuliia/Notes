using NotesTZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesTZ.Service
{
    public interface IService<T> where T : class
    {
        Task<IReadOnlyCollection<Note>> GetAllAsync();
        Task<Note> GetByIdAsync(Guid id);
        Task CreateAsync(T obj);
        Task UpdateAsync(Guid Id, T obj);
        Task RemoveAsync(T obj);
    }
}
