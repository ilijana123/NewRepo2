using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Models;

namespace Store.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext (DbContextOptions<StoreContext> options)
            : base(options)
        {
        }

        public DbSet<Store.Models.Kniga> Kniga { get; set; } = default!;
        public DbSet<Store.Models.Avtor> Avtor { get; set; } = default!;
        public DbSet<Store.Models.AvtorKniga> AvtorKniga { get; set; } = default!;
    }
}
