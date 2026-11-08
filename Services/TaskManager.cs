using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StudentTaskTracker.Data;
using StudentTaskTracker.Models;

namespace StudentTaskTracker.Services;

public class TaskManager
{
    // 1. Ekip Üyesi Ekleme (Başarılıysa true, ID zaten varsa false)
    public bool AddMember(int id, string name, string email, string role)
    {
        using var context = new AppDbContext();

        bool exists = context.Members.Any(m => m.Id == id);
        if (exists) return false;

        TeamMember newMember = new TeamMember(id, name, email, role);
        context.Members.Add(newMember);
        context.SaveChanges();
        return true;
    }

    // 2. Ekip Üyelerini Getirme
    public List<TeamMember> GetAllMembers()
    {
        using var context = new AppDbContext();
        return context.Members.ToList();
    }

    // 3. Yeni Görev Ekleme (Başarılıysa true, ID zaten varsa false)
    public bool AddTask(int id, string title, string description, DateTime dueDate)
    {
        using var context = new AppDbContext();

        bool exists = context.Tasks.Any(t => t.Id == id);
        if (exists) return false;

        ProjectTask newTask = new ProjectTask(id, title, description, dueDate);
        context.Tasks.Add(newTask);
        context.SaveChanges();
        return true;
    }

    // 4. Tüm Görevleri Getirme (Atanan üyeyle birlikte LEFT JOIN)
    public List<ProjectTask> GetAllTasks()
    {
        using var context = new AppDbContext();
        return context.Tasks.Include(t => t.AssignedMember).ToList();
    }

    // 5. Göreve Üye Atama
    public AssignTaskResult AssignTask(int taskId, int memberId)
    {
        using var context = new AppDbContext();

        var task = context.Tasks.FirstOrDefault(t => t.Id == taskId);
        if (task == null) return AssignTaskResult.TaskNotFound;

        var member = context.Members.FirstOrDefault(m => m.Id == memberId);
        if (member == null) return AssignTaskResult.MemberNotFound;

        task.AssignedMember = member;
        context.SaveChanges();
        return AssignTaskResult.Success;
    }

    // 6. Belirli Bir Öğrencinin Görevlerini Getirme
    public List<ProjectTask>? GetTasksByMemberId(int memberId, out string? memberName)
    {
        using var context = new AppDbContext();

        var member = context.Members.FirstOrDefault(m => m.Id == memberId);
        if (member == null)
        {
            memberName = null;
            return null; // Üye bulunamadı
        }

        memberName = member.Name;
        return context.Tasks
            .Where(t => t.AssignedMemberId == memberId)
            .ToList();
    }

    // 7. Görev Durumu Güncelleme (Görev bulunamazsa false)
    public bool UpdateTaskStatus(int taskId, StudentTaskTracker.Models.TaskStatus newStatus)
    {
        using var context = new AppDbContext();

        var task = context.Tasks.FirstOrDefault(t => t.Id == taskId);
        if (task == null) return false;

        task.Status = newStatus;
        context.SaveChanges();
        return true;
    }
}