using System.Text.RegularExpressions;
using System.ComponentModel;

namespace CustomType;
public class Advertisement
{
    private string _title = String.Empty;
    public string Title
    {
        get => _title;

        set
        {
            _title = value;
        }
    }

    private string _transactionNumber = String.Empty;
    public string TransactionNumber
    {
        get => _transactionNumber;
        
        set
        {
            // format of the transaction number is "LL-NNN-LL/NN"
            // where L is letter and N is number
            if (Regex.IsMatch(value, @"^[A-Z]{2}-[0-9]{3}-[A-Z]{2}/[0-9]{2}$"))
            {
                _transactionNumber = value;
            }
            else
            {
                throw new FormatException("Invalid transaction number format");
            }
            _transactionNumber = value;
        }
    }
    public Uri WebsiteUrl
    { get; set; }
    public Uri PhotoUrl
    { get; set; }
    public DateTime StartDate
    { get; set; }
    private DateTime _end_date;
    public DateTime EndDate
    { 
        get => _end_date;
        set
        {
            if (StartDate >= value)
            {
                throw new InvalidDataException($"Invalid {nameof(StartDate)} or {nameof(EndDate)}");
            }
            _end_date = value;
        }
    }
    private int _price;
    public int Price
    {
        get => _price;
        set
        {
            if (_price < 0)
            {
                throw new InvalidDataException($"Invalid {nameof(value)}");
            }
            _price = value;
        }
    }
    public int Id
    { get; set; }
    


    // default constructor
    public Advertisement()
    {
        Title = "Default Title";
        TransactionNumber = "LL-000-LL/00";
        WebsiteUrl = new Uri("https://www.default.url");
        PhotoUrl = new Uri("https://www.default.url");
        StartDate = DateTime.MinValue;
        EndDate = DateTime.Now;
        Price = 0;
        Id = 0;
    }

    public Advertisement(string title, string transactionNumber, string websiteUrl, string photoUrl, string startDate, string endDate, int price, int id)
    {
        if (title == null || transactionNumber == null)
        {
            throw new InvalidDataException($"Invalid invalid");
        }
        
        Title = title;
        TransactionNumber = transactionNumber;
        WebsiteUrl = new  Uri(websiteUrl);
        PhotoUrl = new Uri(photoUrl);
        StartDate = DateTime.Parse(startDate);
        EndDate = DateTime.Parse(endDate);
        Price = price;
        Id = id;
    }

    

    public override string ToString() 
    {
            string adStr = String.Empty;
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(this))
            {
                adStr += ($"{property.Name}: { property.GetValue(this)}\n" );
            }
            return adStr.Substring(0, adStr.Length - 1);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        Advertisement? objAsAdvertisement = obj as Advertisement;
        if (objAsAdvertisement == null) return false;
        else return Equals(objAsAdvertisement);
    }

    public bool Equals(Advertisement? other)
    {
        if (other == null)
        {
            return false;
        }
        return (this.Id.Equals(other.Id) && this.Title.Equals(other.Title) 
                && this.TransactionNumber.Equals(other.TransactionNumber) && this.StartDate.Equals(other.StartDate)
                && this.EndDate.Equals(other.EndDate) && this.WebsiteUrl.Equals(other.WebsiteUrl) 
                && this.PhotoUrl.Equals(other.PhotoUrl) && this.Price.Equals(other.Price));
    }

    public bool NotEquals(Advertisement? other)
    {
        return !(this.Equals(other));
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==(Advertisement? a1, Advertisement? a2)
    {
        if (ReferenceEquals(a1, a2))
        {
            return true;
        }
        if (ReferenceEquals(a1, null) || ReferenceEquals(a2, null))
        {
            return false;
        }
        return a1.Equals(a2);
    }

    public static bool operator !=(Advertisement? a1, Advertisement? a2)
    {
        return !(a1 == a2);
    }
}
