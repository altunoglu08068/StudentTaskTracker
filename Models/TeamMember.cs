namespace StudentTaskTracker.Models;

public class TeamMember
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;

    // EF Core için parametresiz constructor
    public TeamMember()
    {
    }

    public TeamMember(int id, string name, string email, string role)
    {
        Id = id;
        Name = name;
        Email = email;
        Role = role;
    }
}