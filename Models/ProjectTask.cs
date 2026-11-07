using System;

namespace StudentTaskTracker.Models;

public class ProjectTask
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskStatus Status { get; set; }
    public DateTime DueDate { get; set; }

    // Veritabanı Yabancı Anahtarı (Foreign Key)
    public int? AssignedMemberId { get; set; }
    public TeamMember? AssignedMember { get; set; }

    // EF Core'un nesneleri veritabanından çekerken kullanacağı boş constructor
    public ProjectTask()
    {
    }

    // Kod içinde yeni görev oluştururken kullandığımız constructor
    public ProjectTask(int id, string title, string description, DateTime dueDate)
    {
        Id = id;
        Title = title;
        Description = description;
        DueDate = dueDate;
        Status = TaskStatus.Backlog;
    }
}