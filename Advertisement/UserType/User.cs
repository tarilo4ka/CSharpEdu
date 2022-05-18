using System.Text.RegularExpressions;
using System.Net.Mail;


namespace UserType;
public abstract class User
{ 
    public RoleInSystem Role { get; set; } = RoleInSystem.Staff;
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

    protected string _password = String.Empty;
    public string Password
    {
        get => _password;

        set
        {
            if (!Regex.IsMatch(value, "^(?=.*[a-z])(?=.*[A-Z]).{8,}$"))
            {
                throw new InvalidDataException($"Invalid password");
            }
            else
            {
                _password = Hashing.ComputeSha256Hash(value);
            }
        }
    }

    public enum RoleInSystem
    {
        Admin,
        Staff
    }

    public User(RoleInSystem role, string firstName, string lastName, string email, string password, bool hashed=false)
    {
        FirstName = firstName;
        LastName = lastName;
        Role = role;
        Email = new MailAddress(email);
        if (!hashed)
        {
            Password = password;
        }
        else
        {
            _password = password;
        }
    }

    

    public override string ToString()
    {
        return $"{Role}; {FirstName}; {LastName}; {Email}; {Password}";
    }
}
