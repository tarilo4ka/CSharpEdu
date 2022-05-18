using System.ComponentModel.DataAnnotations;  
using AdvertisementApi.Models;
namespace AdvertisementApi.CustomValidation;

public class CustomStartEndDateAnnotation : ValidationAttribute  
{  
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)  
    {  
        if (value == null)
        {
            return new ValidationResult("StartDate is required");
        }
        var advertisement = (Advertisement)value;  
        if (advertisement.StartDate > advertisement.EndDate)  
        {  
            return new ValidationResult("EndDate must be greater than StartDate");  
        }  
        return ValidationResult.Success;  
    }  
}  

public class CustomStartDateAnnotation : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)  
    {  
        if (value == null)
        {
            return new ValidationResult("StartDate is required");
        }
        var StartDate = (DateTime)value;  
        if (StartDate > DateTime.Today)  
        {  
            return new ValidationResult("StartDate must be less than or equal to today");  
        }
        return ValidationResult.Success;  
    }  
}  
