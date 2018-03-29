using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LimeBox.Models.Entities
{
    public partial class LimeContext : DbContext
    {

        //optionsBuilder.UseSqlServer(Startup.connString);
        public LimeContext(DbContextOptions<LimeContext> options) : base(options)
        {

        }
    }
}
