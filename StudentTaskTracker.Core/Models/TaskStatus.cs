namespace StudentTaskTracker.Models;

public enum TaskStatus
{
    Backlog, // Bekleyen görevler için nesnem.
    InProgress, // Devam eden görevler için nesnem.
    InReview, // İnceleme aşamasında olan görevler için nesnem.
    Done // Tamamlanan görevler için nesnem.
}
