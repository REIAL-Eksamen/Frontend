namespace Frontend.Models;

public class ClassOverviewModel
{
    public string Id { get; set; } = "";
    public string CenterName { get; set; } = "";
    public string ClassName { get; set; } = "";
    public string ClassDescription { get; set; } = "";
    public string ClassType { get; set; } = "";
    public string InstructorName { get; set; } = "";
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string Status { get; set; } = "";
    public string ClassroomName { get; set; } = "";
    public int Capacity { get; set; }
}