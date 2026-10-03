using System;
using System.Collections.Generic;
using StudentTaskTracker.Models;

namespace StudentTaskTracker.Services;

public class TaskManager
{
    private List<ProjectTask> _tasks;
    private List<TeamMember> _members;

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
                Console.WriteLine($"Görev ID: {task.Id}, Başlık: {task.Title}, Durum: {task.Status}, Bitiş Tarihi: {task.DueDate.ToShortDateString()}");
            }
        }

        if (!foundAnyTask)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Bu öğrenciye atanmış hiçbir görev bulunamadı.");
            Console.ResetColor();
        }
    }

}