using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SacramentPlanner.Models
{
    public class SacramentPlannerContext : DbContext
    {
        public SacramentPlannerContext (DbContextOptions<SacramentPlannerContext> options)
            : base(options)
        {
        }

        public DbSet<SacramentPlanner.Models.Sunday> Sunday { get; set; }
        public DbSet<SacramentPlanner.Models.Speaker> Speaker { get; set; }
    }
}
