namespace UserType;

public class Admin : User
{
    public Admin(string firstName, string lastName, string email, string password, bool hashed=false)
        : base(User.RoleInSystem.Admin, firstName, lastName, email, password, hashed)
    {
    }
}
