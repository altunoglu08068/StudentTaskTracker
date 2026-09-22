namespace StudentTaskTracker.Models;

public class TeamMember
{
    public int Id { get; set; } // Öğrenci Numarasını tutan nesnem.

    // Boş olma durumunu önlemek için Name, Email ve Role özelliklerini varsayılan olarak boş string ile başlattım.
    public string Name { get; set; } = ""; // Öğrenci Adını tutan nesnem.
    public string Email { get; set; } = ""; // Öğrenci E-posta adresini tutan nesnem.
    public string Role { get; set; } = ""; // Öğrenci Rolünü tutan nesnem.

    public TeamMember(int id, string name, string email, string role)
    {
        Id = id;
        Name = name;
        Email = email;
        Role = role;
    }
}