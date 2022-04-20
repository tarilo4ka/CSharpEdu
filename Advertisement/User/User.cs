using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography;

namespace User;
public abstract class User
{
    public User(string firstName, string lastName, RoleInSystem role, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Role = role;
        Email = new MailAddress(email);
        Password = password;
    }
    
    protected string _firstName = String.Empty;
    public string FirstName
    {
        get => _firstName;

        set
        {
            if (value.Length < 2 || !Regex.IsMatch(value, @"^[a-zA-Z]+$"))
            {
                throw new InvalidDataException($"Invalid {nameof(value)}");
            }
            _firstName = value;
        }
    }


    protected string _lastName = String.Empty;
    public string LastName
    {
        get => _lastName;

        set
        {
            if (value.Length < 2 || !Regex.IsMatch(value, @"^[a-zA-Z]+$"))
            {
                throw new InvalidDataException($"Invalid {nameof(value)}");
            }
            _lastName = value;
        }
    }

    public MailAddress Email { get; set; } = new("default@email.com");

    public enum RoleInSystem
    {
        Admin,
        Staff
    }

    // field password
    // min length = 8
    // contains at least one Uppercase letter and at least one lowercase letter

    protected string _password = String.Empty;
    public string Password
    {
        get => _password;

        set
        {
            if (!Regex.IsMatch(value, "^(?=.*[a-z])(?=.*[A-Z]).{8,}$"))
            {
                throw new InvalidDataException($"Invalid {nameof(value)}");
            }
            _password = ComputeSha256Hash(value);
        }
    }



    public RoleInSystem Role { get; set; } = RoleInSystem.Staff;
    // private method thar returns hashed string
    static protected string ComputeSha256Hash(string rawData)  
    {  
        // Create a SHA256
        using (SHA256 sha256Hash = SHA256.Create())  
        {  
            // ComputeHash - returns byte array
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));  

            // Convert byte array to a string
            StringBuilder builder = new StringBuilder();  
            for (int i = 0; i < bytes.Length; i++)  
            {  
                builder.Append(bytes[i].ToString("x2"));  
            }  
            return builder.ToString();  
        }
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName} {Email}";
    }
}
