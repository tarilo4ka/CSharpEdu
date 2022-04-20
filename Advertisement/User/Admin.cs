namespace User;

public class Admin : User
{
    public Admin(string firstName, string lastName, string email, string password)
        : base(firstName, lastName, User.RoleInSystem.Admin, email, password)
    {
    }
}
