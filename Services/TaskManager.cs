using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StudentTaskTracker.Data;
using StudentTaskTracker.Models;

namespace StudentTaskTracker.Services;

public class TaskManager
{
    // 1. Ekip Üyesi Ekle
    public void AddMember(int id, string name, string email, string role)
    {
        using var context = new AppDbContext();

        // Yinelenen ID kontrolü (Veritabanında arama)
        bool exists = context.Members.Any(m => m.Id == id);
        if (exists)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❗️ Hata: {id} ID'li üye zaten sistemde mevcut!");
            Console.ResetColor();
            return;
        }

        TeamMember newMember = new TeamMember(id, name, email, role);
        context.Members.Add(newMember);
        context.SaveChanges();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"'{name}' başarıyla veritabanına eklendi.");
        Console.ResetColor();
    }

    // 2. Ekip Üyelerini Listele
    public void ListMembers()
    {
        using var context = new AppDbContext();
        var members = context.Members.ToList();

        if (members.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Kayıtlı hiçbir ekip üyesi bulunamadı.");
            Console.ResetColor();
            return;
        }

        Console.WriteLine("\n--- EKİP ÜYELERİ ---");
        foreach (var member in members)
        {
            Console.WriteLine($"ID: {member.Id} | Ad Soyad: {member.Name} | E-posta: {member.Email} | Rol: {member.Role}");
        }
    }

    // 3. Yeni Görev Ekle
    public void AddTask(int id, string title, string description, DateTime dueDate)
    {
        using var context = new AppDbContext();

        bool exists = context.Tasks.Any(t => t.Id == id);
        if (exists)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❗️ Hata: {id} ID'li görev zaten mevcut!");
            Console.ResetColor();
            return;
        }

        ProjectTask newTask = new ProjectTask(id, title, description, dueDate);
        context.Tasks.Add(newTask);
        context.SaveChanges();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"'{title}' görevi başarıyla veritabanına kaydedildi.");
        Console.ResetColor();
    }

    // 4. Tüm Görevleri Listele (JOIN ile atanan üyeyi getirme)
    public void ListTasks()
    {
        using var context = new AppDbContext();

        // Include: SQL'deki LEFT JOIN karşılığıdır
        var tasks = context.Tasks.Include(t => t.AssignedMember).ToList();

        if (tasks.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Kayıtlı hiçbir görev bulunamadı.");
            Console.ResetColor();
            return;
        }

        Console.WriteLine("\n--- TÜM GÖREVLER ---");
        foreach (var task in tasks)
        {
            string memberInfo = task.AssignedMember != null ? task.AssignedMember.Name : "Atama Yapılmadı";
            Console.WriteLine($"Görev ID: {task.Id} | Başlık: {task.Title} | Durum: {task.Status} | Bitiş: {task.DueDate:dd.MM.yyyy} | Atanan: {memberInfo}");
        }
    }

    // 5. Göreve Üye Ata
    public void AssignTask(int taskId, int memberId)
    {
        using var context = new AppDbContext();

        var task = context.Tasks.FirstOrDefault(t => t.Id == taskId);
        if (task == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❗️ Hata: {taskId} ID'li görev bulunamadı.");
            Console.ResetColor();
            return;
        }

        var member = context.Members.FirstOrDefault(m => m.Id == memberId);
        if (member == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❗️ Hata: {memberId} ID'li üye bulunamadı.");
            Console.ResetColor();
            return;
        }

        task.AssignedMember = member;
        context.SaveChanges();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"'{task.Title}' görevi başarıyla '{member.Name}' adlı üyeye atandı.");
        Console.ResetColor();
    }

    // 6. Bir Öğrencinin Görevlerini Listele
    public void ListTasksByMemberId(int memberId)
    {
        using var context = new AppDbContext();

        var member = context.Members.FirstOrDefault(m => m.Id == memberId);
        if (member == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❗️ Hata: {memberId} ID'li üye bulunamadı.");
            Console.ResetColor();
            return;
        }

        var memberTasks = context.Tasks
            .Where(t => t.AssignedMemberId == memberId)
            .ToList();

        Console.WriteLine($"\n--- {member.Name} ÜZERİNDEKİ GÖREVLER ---");
        if (memberTasks.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Bu üyeye atanmış görev bulunmuyor.");
            Console.ResetColor();
            return;
        }

        foreach (var task in memberTasks)
        {
            Console.WriteLine($"Görev ID: {task.Id} | Başlık: {task.Title} | Durum: {task.Status} | Bitiş: {task.DueDate:dd.MM.yyyy}");
        }
    }

    // 7. Görev Durumu Güncelle
    public void UpdateTaskStatus(int taskId, StudentTaskTracker.Models.TaskStatus newStatus)
    {
        using var context = new AppDbContext();

        var task = context.Tasks.FirstOrDefault(t => t.Id == taskId);
        if (task == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❗️ Hata: {taskId} ID'li görev bulunamadı.");
            Console.ResetColor();
            return;
        }

        task.Status = newStatus;
        context.SaveChanges();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Görev '{task.Title}' durumu başarıyla '{newStatus}' olarak güncellendi.");
        Console.ResetColor();
    }
}