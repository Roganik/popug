namespace sso.db.Models;

public class User
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string FullName { get; set; }
    public UserRole Role { get; set; }
}

public enum UserRole
{
    Unknown = 0,
    Admin = 1,
    Manager = 2,
    TODO = 3,
    User = 4,
}