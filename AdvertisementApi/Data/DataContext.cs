using Microsoft.EntityFrameworkCore;
using AdvertisementApi.Models;


namespace AdvertisementApi.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public DbSet<Advertisement>? Advertisements { get; set; }
}