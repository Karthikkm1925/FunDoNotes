using FunDoNotes.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunDoNotes.DataAccess.Context
{
    public class FunDoNotesDbContext : DbContext
    {
        public FunDoNotesDbContext(DbContextOptions<FunDoNotesDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
