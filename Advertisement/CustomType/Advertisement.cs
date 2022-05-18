using System.Text.RegularExpressions;
using System.ComponentModel;


namespace CustomType;

public class Advertisement
{
    public string Title { get; set; } = "";

    private string _transactionNumber = "aa-000-aa/00";
    public string TransactionNumber
    {
        get => _transactionNumber;
        
        set
        {
            if (!Regex.IsMatch(value, @"^[a-zA-Z]{2}-[0-9]{3}-[a-zA-Z]{2}/[0-9]{2}$"))
            {
                Console.WriteLine("Invalid transaction number\n");
                throw new Exception("Invalid transaction number");
            }
            else
            {
                _transactionNumber = value;
            }
        }
    }
    private Uri _websiteUrl = new Uri("https://www.google.com");
    public string WebsiteUrl
    { 
        get => _websiteUrl.ToString(); 
        set
        {
            try
            {
                _websiteUrl = new Uri(value);
            }
            catch (UriFormatException)
            {
                Console.WriteLine("Invalid site URL\n");
                throw new Exception("Invalid site URL");
            }
        } 
    }
    private Uri _photoUrl = new Uri("https://www.google.com/images");
    public string PhotoUrl
    { 
        get => _photoUrl.ToString(); 
        set 
        {
            try
            {
                _photoUrl = new Uri(value);
            }
            catch (UriFormatException)
            {
                Console.WriteLine("Invalid photo URL\n");
                throw new Exception("Invalid photo URL");
            }
        }
    }
    private DateTime _start_date = DateTime.Today;
    public DateTime StartDate
    {
        get => _start_date;
        set
        {
            if (value > DateTime.Now)
            {
                Console.WriteLine("Invalid start date");
                throw new Exception("Invalid start date");
            }
            else
            {
                _start_date = value;
            }
        }
    }
    private DateTime _end_date = DateTime.Today + TimeSpan.FromDays(1);
    public DateTime EndDate
    { 
        get => _end_date;
        set
        {
            if (StartDate >= value)
            {
                Console.WriteLine("Invalid end date");
                throw new Exception("Invalid end date");
            }
            else
            {
                _end_date = value;
            }
        }
    }
    private int _price = 0;
    public int Price
    { 
        get => _price; 
        set
        {
            if (value < 0)
            {
                Console.WriteLine("Invalid price");
                throw new Exception("Invalid price");
            }
            else
            {
                _price = value;
            }
        } 
    } 
    private int _id = 0;
    public int Id 
    { 
        get => _id; 
        set
        {
            if (value < 0)
            {
                Console.WriteLine("Invalid id");
                throw new Exception("Invalid id");
            }
            else
            {
                _id = value;
            }
        } 
    }
    


    public Advertisement()
    {
    }
    public Advertisement(Advertisement ad)
    {
        Title = ad.Title;
        TransactionNumber = ad.TransactionNumber;
        WebsiteUrl = ad.WebsiteUrl;
        PhotoUrl = ad.PhotoUrl;
        StartDate = ad.StartDate;
        EndDate = ad.EndDate;
        Price = ad.Price;
        Id = ad.Id;
    }

    public Advertisement(params string[] props)
    {
        Title = props[0];
        TransactionNumber = props[1];
        WebsiteUrl = props[2];
        PhotoUrl = props[3];
        StartDate = DateTime.Parse(props[4]);
        EndDate = DateTime.Parse(props[5]);
        Price = int.Parse(props[6]);
        Id = int.Parse(props[7]);
    }

    public Advertisement(string title, string transactionNumber, string websiteUrl, string photoUrl, string startDate, string endDate, int price, int id)
    {
        Title = title;
        TransactionNumber = transactionNumber;
        WebsiteUrl = websiteUrl;
        PhotoUrl = photoUrl;
        StartDate = DateTime.Parse(startDate);
        EndDate = DateTime.Parse(endDate);
        Price = price;
        Id = id;
    }

    private static string valueFromConsole(string message)
    {
        while (true)
        {
            Console.WriteLine(message);
            string? st = Console.ReadLine();
            if (st == null || st == "")
            {
                Console.WriteLine("Invalid value");
            }
            else
            {
                return st;
            }
        }
    }
    public void FromConsole()
    {
        foreach(var prop in this.GetType().GetProperties())
        {
            tryAgain:
            if (prop.Name == "StartDate" || prop.Name == "EndDate")
            {
                try
                {
                    prop.SetValue(this, DateTime.Parse(valueFromConsole($"Enter {prop.Name}")));
                }
                catch (System.Exception)
                {
                    Console.WriteLine("Invalid value");
                    goto tryAgain;
                }
            }
            else if (prop.Name == "Price" || prop.Name == "Id")
            {
                try
                {
                    int val = int.Parse(valueFromConsole($"Enter {prop.Name}"));
                    prop.SetValue(this, val);
                }
                catch (System.Exception)
                {
                    Console.WriteLine("Invalid value");
                    goto tryAgain;
                }
            }
            else
            {
                try
                {
                    prop.SetValue(this, valueFromConsole($"Enter {prop.Name}"));
                }
                catch (System.Exception)
                {
                    Console.WriteLine("Invalid value");
                    goto tryAgain;
                }
            }
            
        }
    }

    public override string ToString() 
    {
            string adStr = String.Empty;
            int i = 0;
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(this))
            {
                adStr += property.GetValue(this);
                if (i != 7)
                {
                    adStr += "; ";
                }
                i++;
            }
            return adStr;
    }
}
