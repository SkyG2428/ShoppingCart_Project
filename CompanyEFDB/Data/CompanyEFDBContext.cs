using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EFDataFirstLIbrary.Models;

namespace CompanyEFDB.Data
{
    public class CompanyEFDBContext : DbContext
    {
        public CompanyEFDBContext (DbContextOptions<CompanyEFDBContext> options)
            : base(options)
        {
        }

        public DbSet<EFDataFirstLIbrary.Models.Employee> Employee { get; set; } = default!;
    }
}
