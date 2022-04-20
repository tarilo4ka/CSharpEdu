namespace User;

public class Staff : User
{
    public int Salary { get; set; } = 0;
    public DateTime DateOfHire { get; set; } = DateTime.Now;
    
    public Staff(string firstName, string lastName, string email, string password, int salary, string dateOfHire)
        : base(firstName, lastName, User.RoleInSystem.Staff, email, password)
    {
        Salary = salary;
        DateOfHire = DateTime.Parse(dateOfHire);
    }

    public override string ToString()
    {
        return base.ToString() + $" {Salary} {DateOfHire}";
    }
}
