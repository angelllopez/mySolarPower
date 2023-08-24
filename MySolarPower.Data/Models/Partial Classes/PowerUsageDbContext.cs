using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySolarPower.Data.Models.Partial_Classes
{
    public partial class PowerUsageDbContext : DbContext
    {
        public PowerUsageDbContext(DbContextOptions<PowerUsageDbContext> options) : base(options)
        {
        }


    }
}
