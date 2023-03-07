using HortasIndoor.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HortasIndoor.DataAccess
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<HIContext>
    {
        public HIContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HIContext>();

            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=HortasIndoor;Trusted_Connection=True;MultipleActiveResultSets=true"
                );

            return new HIContext(optionsBuilder.Options);
        }
    }
}
