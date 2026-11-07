using System;
using System.Collections.Generic;
using StudentTaskTracker.Models;

namespace StudentTaskTracker.Services;

public class TaskManager
{
    private List<ProjectTask> _tasks; // Görevler
    private List<TeamMember> _members; // Üyeler

    public TaskManager()
    {
        _tasks = new List<ProjectTask>();
        _members = new List<TeamMember>();
    }

    // 1. ÜYE EKLEME İŞİ
    public void AddMember(int id, string name, string email, string role)
    {
        foreach (var member in _members)
        {
            if (member.Id == id)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Bu öğrenci zaten kayıtlı.");
                Console.ResetColor();
                return;
            }
        }

        TeamMember newMember = new TeamMember(id, name, email, role);
        _members.Add(newMember);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{name} başarıyla ekibe eklendi.");
        Console.ResetColor();
    }

    // 2. YENİ İŞ: TÜM ÜYELERİ EKRANA YAZDIRMA
    public void ListMembers()
    {
        // 1. Adım: Önce listenin boş olup olmadığını kontrol et
        if (_members.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ekipte henüz kayıtlı üye yok.");
            Console.ResetColor();
            return;
        }

        Console.BackgroundColor = ConsoleColor.Cyan;
        Console.WriteLine("--- Ekip Üyeleri Listesi ---");
        Console.ResetColor();

        // Eğer yukarıdaki if çalışmadıysa, demek ki içeride öğrenciler var.
        foreach (var member in _members)
        {
            Console.WriteLine($"ID: {member.Id}, Ad: {member.Name}, Email: {member.Email}, Rol: {member.Role}");
        }
    }

    // 3. YENİ İŞ: BELİRLİ BİR ÜYENİN GÖREVLERİNİ LİSTELEME
    public void ListTasksByMemberId(int memberId)
    {
        bool foundAnyTask = false;

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n--- {memberId} NUMARALI ÖĞRENCİNİN GÖREVLERİ ---");
        Console.ResetColor();

        foreach (var task in _tasks)
        {
            if (task.AssignedMember != null && task.AssignedMember.Id == memberId)
            {
                foundAnyTask = true;
                Console.WriteLine($"Görev ID: {task.Id}, Başlık: {task.Title}, Durum: {task.Status}, Bitiş Tarihi: {task.DueDate:dd.MM.yyyy}");
            }
        }

        if (!foundAnyTask)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Bu öğrenciye atanmış hiçbir görev bulunamadı.");
            Console.ResetColor();
        }
    }

    // 4. YENİ İŞ: GÖREV EKLEME
    public void AddTask(int id, string title, string description, DateTime dueDate)
    {
        foreach (var task in _tasks)
        {
            if (task.Id == id)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Bu görev zaten mevcut.");
                Console.ResetColor();
                return;
            }
        }

        ProjectTask newTask = new ProjectTask(id, title, description, dueDate);
        _tasks.Add(newTask);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{title} başarıyla eklendi.");
        Console.ResetColor();
    }

    // 5. YENİ İŞ: TÜM GÖREVLERİ EKRANA YAZDIRMA
    public void ListTasks()
    {
        if (_tasks.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Henüz eklenmiş görev yok.");
            Console.ResetColor();
            return;
        }

        Console.BackgroundColor = ConsoleColor.Cyan;
        Console.WriteLine("--- Görev Listesi ---");
        Console.ResetColor();

        foreach (var task in _tasks)
        {
            string assignedMemberInfo = task.AssignedMember != null ? $"Atanan Üye: {task.AssignedMember.Name}" : "Atanan Üye: Yok";
            Console.WriteLine($"Görev ID: {task.Id}, Başlık: {task.Title}, Durum: {task.Status}, Bitiş Tarihi: {task.DueDate:dd.MM.yyyy}, {assignedMemberInfo}");
        }
    }

    // 6. YENİ İŞ: GÖREV ATAMA
    public void AssignTask(int taskId, int memberId)
    {
        foreach (var task in _tasks)
        {
            if (task.Id == taskId)
            {
                foreach (var member in _members)
                {
                    if (member.Id == memberId)
                    {
                        task.AssignedMember = member;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Görev '{task.Title}' başarıyla {member.Name} adlı üyeye atandı.");
                        Console.ResetColor();
                        return;
                    }
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Belirtilen ID'ye sahip bir üye bulunamadı.");
                Console.ResetColor();
                return;
            }
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Belirtilen ID'ye sahip bir görev bulunamadı.");
        Console.ResetColor();
    }

    // 7. YENİ İŞ: GÖREV DURUMUNU GÜNCELLEME
    public void UpdateTaskStatus(int taskId, StudentTaskTracker.Models.TaskStatus newStatus)
    {
        foreach (var task in _tasks)
        {
            if (task.Id == taskId)
            {
                task.Status = newStatus;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Görev '{task.Title}' durumu başarıyla '{newStatus}' olarak güncellendi.");
                Console.ResetColor();
                return;
            }
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Belirtilen ID'ye sahip bir görev bulunamadı.");
        Console.ResetColor();
    }
}