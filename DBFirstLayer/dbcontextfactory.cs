using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFirstLayer
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<SchoolSystemContext>
    {
        public SchoolSystemContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SchoolSystemContext>();
            optionsBuilder.UseSqlServer("Data Source=MR-AG\\SQLEXPRESS;Initial Catalog=SchoolSystem;Integrated Security=True;MultipleActiveResultSets=true");

            return new SchoolSystemContext(optionsBuilder.Options);
        }
    }
}
