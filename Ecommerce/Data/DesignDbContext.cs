namespace Ecommerce.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

public class DesignDbContext : IDesignTimeDbContextFactory<Context>
{
    public Context CreateDbContext(string[] args)
    {
    

        var optionsBuilder = new DbContextOptionsBuilder<Context>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ecommerce;Username=admin;Password=123");

        return new Context(optionsBuilder.Options);
    }
}
