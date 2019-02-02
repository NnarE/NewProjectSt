using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NewProject.Models
{
    public class NewProjectContext : DbContext
    {
        public NewProjectContext (DbContextOptions<NewProjectContext> options)
            : base(options)
        {
        }

        public DbSet<NewProject.Models.Grade> Grade { get; set; }
        public DbSet<NewProject.Models.Ambion> Ambion { get; set; }
        public DbSet<NewProject.Models.Student> Student { get; set; }
    }
}
