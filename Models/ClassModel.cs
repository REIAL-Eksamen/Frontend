namespace Frontend.Models;

public enum ClassStatus { Scheduled, Active, Cancelled, Done }
public class ClassModel
{
    public string? Id { get; set; }

    public string? ClassName { get; set; }
    public string? ClassDescription { get; set; }
    public string? ClassType { get; set; }
    public string? InstructorId { get; set; }
    public string? CenterId { get; set; } 
    public ClassroomModel? Classroom { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public ClassStatus? Status{ get; set; }
}