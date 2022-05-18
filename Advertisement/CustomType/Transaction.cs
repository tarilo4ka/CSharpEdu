using System.Text.RegularExpressions;
using System.ComponentModel;


namespace CustomType;

public class Transaction
{
    private string _name = "";
    public string Name
    {
        get => _name;
        set
        {
            if (!Regex.IsMatch(value, @"^[a-zA-Z]{2,40}$"))
            {
                Console.WriteLine("Invalid name\n");
                throw new Exception("Invalid name");
            }
            else
            {
                _name = value;
            }
        }
    }
    private string _cardNumber = "0000000000000000";
    public string CardNumber
    {
        get => _cardNumber;
        set
        {
            if (!Regex.IsMatch(value, @"^[0-9]{16}$"))
            {
                Console.WriteLine("Invalid card number\n");
                throw new Exception("Invalid card number");
            }
            else
            {
                _cardNumber = value;
            }
        }
    }
    private string _cvc = "000";
    public string Cvc
    {
        get => _cvc;
        set
        {
            if (!Regex.IsMatch(value, @"^[0-9]{3,4}$"))
            {
                Console.WriteLine("Invalid CVC\n");
                throw new Exception("Invalid CVC");
            }
            else
            {
                _cvc = value;
            }
        }
    }
    private int _month = 0;
    public int Month
    {
        get => _month;
        set
        {
            if (value < 1 || value > 13)
            {
                Console.WriteLine("Invalid month\n");
                throw new Exception("Invalid month");
            }
            else
            {
                _month = value;
            }
        }
    }
    private int _year = 0;
    public int Year
    {
        get => _year;
        set
        {
            if (value < 1950 || value > 2031)
            {
                Console.WriteLine("Invalid year\n");
                throw new Exception("Invalid year");
            }
            else
            {
                _year = value;
            }
        }
    }
    private DateTime _date = DateTime.Now;
    public DateTime TDate
    {
        get => _date;
        set
        {
            _date = value;
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
                Console.WriteLine("Invalid price\n");
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
                Console.WriteLine("Invalid id\n");
                throw new Exception("Invalid id");
            }
            else
            {
                _id = value;
            }
        }
    }

    public Transaction() 
    { 
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
        foreach (var prop in this.GetType().GetProperties())
        {
            tryAgain:
            if (prop.Name == "TDate")
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
            else if (prop.Name == "Price" || prop.Name == "Id" || prop.Name == "Month" || prop.Name == "Year")
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
}
