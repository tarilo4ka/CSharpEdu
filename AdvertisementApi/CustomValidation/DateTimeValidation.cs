using System.ComponentModel.DataAnnotations;  
using AdvertisementApi.Models;
namespace AdvertisementApi.CustomValidation;

public class CustomEndDateAnnotation : ValidationAttribute  
{  
    public DateTime StartDate { get; set; }

    public override bool IsValid(object? endDate)
    {
        DateTime EndDate = Convert.ToDateTime(endDate);

        if (EndDate.Year > StartDate.Year)
        {
            return true;
        }
        else if (EndDate.Year == StartDate.Year)
        {
            if (EndDate.Month > StartDate.Month)
            {
                return true;
            }
            else if (EndDate.Month == StartDate.Month)
            {
                if (EndDate.Day > StartDate.Day)
                {
                    return true;
                }
            }
        }
        return false;
    }
}  
  