using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NotesTZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesTZ.Context
{
    public class NoteContext : DbContext
    {
        public NoteContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<Note> Notes { get; set; }
    }
}
