using Microsoft.EntityFrameworkCore;
using AdvertisementApi.Models;


namespace AdvertisementApi.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }


    public DbSet<User>? CurrentUser { get; set; } = null;
    public DbSet<User>? Users { get; set; }
    public DbSet<Advertisement>? Advertisements { get; set; }
}