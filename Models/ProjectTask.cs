namespace StudentTaskTracker.Models;

public class ProjectTask
{
    public int Id { get; set; } // Proje ID'sini tutan nesnem.
    public string Title { get; set; } = ""; // Proje başlığını tutan nesnem. Boş olma durumunu önlemek için varsayılan olarak boş string ile başlattım.
    public string Description { get; set; } = ""; // Proje açıklamasını tutan nesnem. Boş olma durumunu önlemek için bunu da varsayılan olarak boş string ile başlattım.
    public DateTime DueDate { get; set; } // Proje bitiş tarihini tutan nesnem.
    public TaskStatus Status { get; set; } // Proje durumunu tutan nesnem.

    public TeamMember? AssignedMember { get; set; } // Göreve atanmış olan takım üyesini tutan nesnem. Boş olma durumuna karşı ? ekledim.

    public ProjectTask(int id, string title, string description, DateTime dueDate, TeamMember? assignedMember = null)
    {
        Id = id;
        Title = title;
        Description = description;
        DueDate = dueDate;
        Status = TaskStatus.Backlog; // Tüm görevler Backlog (Bekeleyen görev) olarak başlattım.
        AssignedMember = assignedMember;
    }
}