namespace TodoProjectUsingCleanArchitecture.Application.Models;
public class TaskItem
{
    public required Guid Id { get; set; }
    public string Title { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
}
