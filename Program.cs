using System;
using StudentTaskTracker.Data;
using StudentTaskTracker.Models;
using StudentTaskTracker.Services;

namespace StudentTaskTracker
{
    class Program
    {
        static void Menu()
        {
            Console.WriteLine("\n=== ÖĞRENCİ GÖREV TAKİP SİSTEMİ ===");
            Console.WriteLine("1. Ekip Üyesi Ekle");
            Console.WriteLine("2. Ekip Üyelerini Listele");
            Console.WriteLine("3. Yeni Görev Ekle");
            Console.WriteLine("4. Tüm Görevleri Listele");
            Console.WriteLine("5. Göreve Üye Ata");
            Console.WriteLine("6. Öğrencinin Görevlerini Listele");
            Console.WriteLine("7. Görev Durumu Güncelle");
            Console.WriteLine("0. Çıkış");
        }

        static string GetInput(string prompt)
        {
            string input = "";
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine()?.Trim() ?? "";
            } while (input == "");

            return input;
        }

        static void Main(string[] args)
        {
            using var context = new AppDbContext();
            TaskManager taskManager = new TaskManager(context);

            while (true)
            {
                Menu();
                string choice = GetInput("Seçiminiz: ");

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("\n=== Ekip Üyesi Ekle ===");
                        int memberId;

                        while (true)
                        {
                            string idInput = GetInput("Üye ID (pozitif bir sayı): ");
                            if (int.TryParse(idInput, out memberId) && memberId > 0)
                                break;
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("❗️ Geçersiz ID. Lütfen pozitif bir sayı girin.");
                                Console.ResetColor();
                            }
                        }

                        string memberName = GetInput("Üye Adı/Soyadı: ");
                        string memberEmail = GetInput("Üye E-posta: ");
                        string memberRole = GetInput("Üye Rolü (Geliştirici/Test Uzmanı): ");

                        bool memberAdded = taskManager.AddMember(memberId, memberName, memberEmail, memberRole);

                        if (memberAdded)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"✅ {memberName} ekibe başarıyla eklendi.");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"❗️ Hata: {memberId} ID'li üye zaten mevcut.");
                            Console.ResetColor();
                        }
                        break;
                    case "2":
                        var members = taskManager.GetAllMembers();
                        if (members.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Kayıtlı hiçbir ekip üyesi bulunamadı.");
                            Console.ResetColor();
                            break;
                        }

                        Console.WriteLine("\n--- EKİP ÜYELERİ ---");
                        foreach (var member in members)
                        {
                            Console.WriteLine($"ID: {member.Id} | Ad Soyad: {member.Name} | E-posta: {member.Email} | Rol: {member.Role}");
                        }
                        break;
                    case "3":
                        Console.WriteLine("\n=== Yeni Görev Ekle ===");
                        int taskId;

                        while (true)
                        {
                            string idInput = GetInput("Görev ID (pozitif bir sayı): ");
                            if (int.TryParse(idInput, out taskId) && taskId > 0)
                                break;
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("❗️ Geçersiz ID. Lütfen pozitif bir sayı girin.");
                                Console.ResetColor();
                            }
                        }

                        string taskName = GetInput("Görev Adı: ");
                        string taskDescription = GetInput("Görev Açıklaması: ");

                        DateTime dueDate;
                        while (true)
                        {
                            string dateInput = GetInput("Bitiş Tarihi (Gün.Ay.Yıl - Örn: 15.05.2026): ");
                            if (DateTime.TryParse(dateInput, out dueDate))
                                break;
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("❗️ Geçersiz tarih formatı. Lütfen Gün.Ay.Yıl biçiminde girin.");
                                Console.ResetColor();
                            }
                        }

                        bool taskAdded = taskManager.AddTask(taskId, taskName, taskDescription, dueDate);

                        if (taskAdded)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"✅ '{taskName}' görevi başarıyla eklendi.");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"❗️ Hata: {taskId} ID'li görev zaten mevcut.");
                            Console.ResetColor();
                        }
                        break;
                    case "4":
                        var tasks = taskManager.GetAllTasks();
                        if (tasks.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Kayıtlı hiçbir görev bulunamadı.");
                            Console.ResetColor();
                            break;
                        }

                        Console.WriteLine("\n--- TÜM GÖREVLER ---");
                        foreach (var task in tasks)
                        {
                            string memberInfo = task.AssignedMember != null ? task.AssignedMember.Name : "Atama Yapılmadı";
                            Console.WriteLine($"Görev ID: {task.Id} | Başlık: {task.Title} | Durum: {task.Status} | Bitiş: {task.DueDate:dd.MM.yyyy} | Atanan: {memberInfo}");
                        }
                        break;
                    case "5":
                        Console.WriteLine("\n=== Göreve Üye Ata ===");
                        int assignTaskId;
                        while (true)
                        {
                            string idInput = GetInput("Görev ID (pozitif bir sayı): ");
                            if (int.TryParse(idInput, out assignTaskId) && assignTaskId > 0)
                                break;
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("❗️ Geçersiz ID. Lütfen pozitif bir sayı girin.");
                                Console.ResetColor();
                            }
                        }

                        int assignMemberId;
                        while (true)
                        {
                            string idInput = GetInput("Atanacak Üye ID (pozitif bir sayı): ");
                            if (int.TryParse(idInput, out assignMemberId) && assignMemberId > 0)
                                break;
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("❗️ Geçersiz ID. Lütfen pozitif bir sayı girin.");
                                Console.ResetColor();
                            }
                        }

                        var assignResult = taskManager.AssignTask(assignTaskId, assignMemberId);
                        switch (assignResult)
                        {
                            case AssignTaskResult.Success:
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"✅ {assignTaskId} ID'li görev başarıyla üyeye atandı.");
                                Console.ResetColor();
                                break;
                            case AssignTaskResult.TaskNotFound:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"❗️ Hata: {assignTaskId} ID'li görev bulunamadı.");
                                Console.ResetColor();
                                break;
                            case AssignTaskResult.MemberNotFound:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"❗️ Hata: {assignMemberId} ID'li üye bulunamadı.");
                                Console.ResetColor();
                                break;
                            default:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("❗️ Bilinmeyen bir hata oluştu.");
                                Console.ResetColor();
                                break;
                        }
                        break;
                    case "6":
                        Console.WriteLine("\n=== Öğrencinin Görevlerini Listele ===");
                        int memberIdForTasks;
                        while (true)
                        {
                            string idInput = GetInput("Üye ID (pozitif bir sayı): ");
                            if (int.TryParse(idInput, out memberIdForTasks) && memberIdForTasks > 0)
                                break;
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("❗️ Geçersiz ID. Lütfen pozitif bir sayı girin.");
                                Console.ResetColor();
                            }
                        }

                        var memberTasks = taskManager.GetTasksByMemberId(memberIdForTasks, out string? assignedMemberName);
                        if (memberTasks == null)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"❗️ Hata: {memberIdForTasks} ID'li üye bulunamadı.");
                            Console.ResetColor();
                            break;
                        }

                        Console.WriteLine($"\n--- {assignedMemberName} ÜZERİNDEKİ GÖREVLER ---");
                        if (memberTasks.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Bu üyeye atanmış görev bulunmuyor.");
                            Console.ResetColor();
                            break;
                        }

                        foreach (var task in memberTasks)
                        {
                            Console.WriteLine($"Görev ID: {task.Id} | Başlık: {task.Title} | Durum: {task.Status} | Bitiş: {task.DueDate:dd.MM.yyyy}");
                        }
                        break;
                    case "7":
                        Console.WriteLine("\n=== Görev Durumu Güncelle ===");
                        int taskIdForStatus;
                        while (true)
                        {
                            string idInput = GetInput("Görev ID (pozitif bir sayı): ");
                            if (int.TryParse(idInput, out taskIdForStatus) && taskIdForStatus > 0)
                                break;
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("❗️ Geçersiz ID. Lütfen pozitif bir sayı girin.");
                                Console.ResetColor();
                            }
                        }

                        Console.WriteLine("Yeni Durumu Seçin:");
                        Console.WriteLine("1. Backlog (Beklemede)");
                        Console.WriteLine("2. InProgress (Devam Ediyor)");
                        Console.WriteLine("3. InReview (İncelemede)");
                        Console.WriteLine("4. Done (Tamamlandı)");

                        StudentTaskTracker.Models.TaskStatus newStatus;
                        while (true)
                        {
                            string statusChoice = GetInput("Seçiminiz (1, 2, 3, 4): ");
                            if (statusChoice == "1")
                            {
                                newStatus = StudentTaskTracker.Models.TaskStatus.Backlog;
                                break;
                            }
                            else if (statusChoice == "2")
                            {
                                newStatus = StudentTaskTracker.Models.TaskStatus.InProgress;
                                break;
                            }
                            else if (statusChoice == "3")
                            {
                                newStatus = StudentTaskTracker.Models.TaskStatus.InReview;
                                break;
                            }
                            else if (statusChoice == "4")
                            {
                                newStatus = StudentTaskTracker.Models.TaskStatus.Done;
                                break;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("❗️ Geçersiz seçim. Lütfen 1, 2, 3 veya 4 girin.");
                                Console.ResetColor();
                            }
                        }

                        bool statusUpdated = taskManager.UpdateTaskStatus(taskIdForStatus, newStatus);
                        if (statusUpdated)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("✅ Görev durumu başarıyla güncellendi.");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"❗️ Hata: {taskIdForStatus} ID'li görev bulunamadı.");
                            Console.ResetColor();
                        }
                        break;
                    case "0":
                        Console.WriteLine("Programdan çıkılıyor. İyi çalışmalar!");
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                        Console.ResetColor();
                        break;
                }
            }
        }
    }
}