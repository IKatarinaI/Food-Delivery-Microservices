using MenuMicroservice.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenuMicroservice.DBAccess
{
    public class MenuContext: DbContext
    {
        public MenuContext(DbContextOptions<MenuContext> options):base(options)
        {

        }

        public DbSet<Food> Foods { get; set; }
    }
}
