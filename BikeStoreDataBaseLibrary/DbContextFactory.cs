using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStoreDataBaseLibrary
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<BikestoreContext>
    {
        public BikestoreContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BikestoreContext>();
            optionsBuilder.UseSqlServer("Data Source=MR-AG\\SQLEXPRESS;Initial Catalog=Bikestore;Integrated Security=True;MultipleActiveResultSets=true");

            return new BikestoreContext(optionsBuilder.Options);
        }
    }
}
