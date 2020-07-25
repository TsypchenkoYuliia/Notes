using NotesTZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NotesTZ.Repository
{
    public interface IRepository<T> where T:class
    {
        Task<IReadOnlyCollection<Note>> GetAllAsync();
        Task<Note> GetByIdAsync(Guid id);
        Task CreateAsync(T obj);
        Task UpdateAsync(T obj);
        Task RemoveAsync(T obj);
    }
}
