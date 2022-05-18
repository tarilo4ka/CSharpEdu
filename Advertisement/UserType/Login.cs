using System.Net.Mail;

namespace UserType;

class Login
{
    private List <User> _users;

    public Login()
    {
        _users = new ();
        ReadUsersDB();
    }

    private void ReadUsersDB()
    {
        string path = "Data/all_USERS.txt";
        string[] lines = File.ReadAllLines(path);
        string[] values;
        foreach (var line in lines)
        {
            values = line.Split("; ");
            if (values[0] == "Admin")
            {
                _users.Add(new Admin(values[1], values[2], values[3], values[4], true));

            }
            else if (values[0] == "Staff")
            {
                _users.Add(new Staff(values[1], values[2], values[3], values[4], int.Parse(values[5]), values[6], true));
            }
        }
    }

    private void WriteUsersDB()
    {
        string path = "Data/all_USERS.txt";
        List<string> lines = new ();
        foreach (var user in _users)
        {
            lines.Add(user.ToString());
        }
        File.WriteAllLines(path, lines);
    }

    public User? LoginUser(string email, string password)
    {
        foreach (var user in _users)
        {
            if (user.Email.ToString() == email && user.Password == Hashing.ComputeSha256Hash(password))
            {
                return user;
            }
        }
        return null;
    }

    public User? LoginUser(User user)
    {
        foreach (var _user in _users)
        {
            if (_user.Email.ToString() == user.Email.ToString() && _user.Password == user.Password)
            {
                return user;
            }
        }
        return null;
    }

    public bool IsUniqueEmail(string email)
    {
        foreach (var user in _users)
        {
            if (user.Email.ToString() == email)
            {
                return false;
            }
        }
        return true;
    }

    public Staff? SignUp(string firstName, string lastName, string email, string password, int salary, string dateOfHire)
    {
        if (IsUniqueEmail(email))
        {
            Staff newUser = new Staff(firstName, lastName, email, password, salary, dateOfHire);
            _users.Add(newUser);
            WriteUsersDB();
            return newUser;
        }
        return null;
    }

    public Staff? SignUp(Staff staff)
    {
        if (IsUniqueEmail(staff.Email.ToString()))
        {
            _users.Add(staff);
            WriteUsersDB();
            return staff;
        }
        return null;
    }

    public Admin? NewAdmin(string firstName, string lastName, string email, string password)
    {
        if (IsUniqueEmail(email))
        {
            Admin newUser = new Admin(firstName, lastName, email, password);
            _users.Add(newUser);
            WriteUsersDB();
            return newUser;
        }
        return null;
    }

    public Admin? NewAdmin(Admin admin)
    {
        if (IsUniqueEmail(admin.Email.ToString()))
        {
            _users.Add(admin);
            WriteUsersDB();
            return admin;
        }
        return null;
    }
}