using System;
using conscious.api.Models;
using Microsoft.EntityFrameworkCore;

namespace conscious.api.Data;

public class BrandsDbContext : DbContext
{
    public BrandsDbContext(DbContextOptions<BrandsDbContext> options) : base(options) { }

    public DbSet<Brand> Brands { get; set; }
}
