using System.ComponentModel.DataAnnotations;

namespace AdvertisementApi.Models;

public class userAds
{
    [Key]
    public User? CurrentUser { get; set; } = null;
    public List<Advertisement>? Advertisement { get; set; } 
}