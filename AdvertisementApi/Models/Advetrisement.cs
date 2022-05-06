#nullable disable
using System.ComponentModel.DataAnnotations;
using AdvertisementApi.CustomValidation;
namespace AdvertisementApi.Models;


[CustomStartEndDateAnnotation]
public class Advertisement
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100, ErrorMessage = "Title must be less than 100 characters")]
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }
    [RegularExpression(@"^[a-zA-Z]{2}-[0-9]{3}-[a-zA-Z]{2}/[0-9]{2}$", ErrorMessage = "Invalid format")]
    [Required(ErrorMessage = "TransactionNumber is required")]
    public string TransactionNumber { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Price must be greater than 0")]
    [Required(ErrorMessage = "Price is required")]
    public int Price { get; set; }
    [Required(ErrorMessage = "StartDate is required")]
    [CustomStartDateAnnotation]
    public DateTime StartDate { get; set; }
    [Required(ErrorMessage = "EndDate is required")]
    
    public DateTime EndDate { get; set; }
    [Url(ErrorMessage = "Invalid ImageUrl")]
    public string ImageUrl { get; set; }
    [Url(ErrorMessage = "Invalid WebsiteUrl")]
    public string WebsiteUrl { get; set; }
}
