# StudentTaskTracker

Öğrenci ekipleri için geliştirilmiş, görev takibi ve ekip yönetimi amacıyla tasarlanmış bir .NET konsol uygulamasıdır.

## Proje Hakkında
StudentTaskTracker, öğrencilerin görevleri takip etmesini, ekip üyelerini yönetmesini ve her görevin durumunu düzenli şekilde izleyebilmesini sağlar. Uygulama, üyeler ve görevler için SQLite veritabanı kullanır ve Entity Framework Core ile verilerin kalıcı olarak saklanmasını sağlar.

## Özellikler
- Ekip üyelerini ekleme
- Tüm ekip üyelerini listeleme
- Görev ekleme
- Tüm görevleri listeleme
- Göreve üye atama
- Belirli bir üye üzerindeki görevleri listeleme
- Görev durumunu güncelleme (`Backlog`, `InProgress`, `InReview`, `Done`)
- Kullanıcı girişlerinde sayı ve durum doğrulama
- SQLite ile veritabanı tabanlı persistence

## Uygulama Menüleri
Program akışı şu seçenekleri sunar:
1. Ekip üyesi ekle
2. Ekip üyelerini listele
3. Yeni görev ekle
4. Tüm görevleri listele
5. Göreve üye ata
6. Öğrencinin görevlerini listele
7. Görev durumu güncelle
0. Çıkış

## Proje Yapısı
- `StudentTaskTracker.ConsoleApp/`
  - `Program.cs`: Ana kullanıcı arayüzü ve menü akışı
  - `StudentTaskTracker.ConsoleApp.csproj`: Konsol uygulaması proje dosyası
- `StudentTaskTracker.Core/`
  - `Models/`: Uygulama modelleri
    - `TeamMember.cs`
    - `ProjectTask.cs`
    - `TaskStatus.cs`
    - `AssignTaskResult.cs`
  - `Data/`: EF Core veritabanı bağlamı
    - `AppDbContext.cs`
  - `Services/`: İş mantığı katmanı
    - `TaskManager.cs`
  - `Migrations/`: EF Core migration dosyaları
  - `StudentTaskTracker.Core.csproj`: Core katmanı proje dosyası
- `StudentTaskTracker.slnx`: Çözüm dosyası

## Teknoloji Yığını
- C#
- .NET 10
- Console Application
- SQLite
- Entity Framework Core
- Layered Architecture (Katmanlı mimari)

## Çalıştırma
.NET 10 SDK yüklü olmalıdır.

```bash
dotnet restore
dotnet build
dotnet run --project StudentTaskTracker.ConsoleApp/StudentTaskTracker.ConsoleApp.csproj
```

Alternatif olarak çözüm klasöründen aşağıdaki komut da kullanılabilir:

```bash
dotnet run
```

Uygulama ilk kez çalıştığında SQLite veritabanı dosyası otomatik olarak oluşturulur. Veritabanı dosyası çalışma dizininde `student_tasks.db` olarak saklanır.

## Veritabanı
Uygulama SQLite kullanır ve `StudentTaskTracker.Core/Data/AppDbContext.cs` dosyasında tanımlı `AppDbContext` üzerinden veriye erişir.

Migration dosyaları `StudentTaskTracker.Core/Migrations/` klasöründe yer alır. İsterseniz manuel olarak şu komutla veritabanını güncelleyebilirsiniz:

```bash
dotnet ef database update --project StudentTaskTracker.Core/StudentTaskTracker.Core.csproj --startup-project StudentTaskTracker.ConsoleApp/StudentTaskTracker.ConsoleApp.csproj
```

## Durumlar ve İlişkiler
- Görev durumları: `Backlog`, `InProgress`, `InReview`, `Done`
- Atama sonuçları: `Success`, `TaskNotFound`, `MemberNotFound`
- Her görev bir ekip üyesine atanabilir
- Bir üye birden fazla göreve atanabilir

## Geliştirme Notları
Projede iş mantığı, veri erişimi ve kullanıcı arayüzü ayrı katmanlarda ayrılmıştır. Bu yapı, kodun daha okunabilir, test edilebilir ve bakımı daha kolay olmasını sağlar.

## Mevcut Durum
- [x] Proje yapısı oluşturuldu
- [x] Domain modelleri tamamlandı
- [x] Görev ve üye yöneticisi eklendi
- [x] Konsol arayüzü hazırlandı
- [x] SQLite veritabanı entegrasyonu tamamlandı
