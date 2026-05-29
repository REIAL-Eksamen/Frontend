namespace Frontend.Models;

public class CenterModel
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Address  { get; set; }
    public List<CenterAdmin> Admins { get; set; } = new();
    public List<ClassroomDto> Classrooms { get; set; } = new();
}

public class CenterAdmin
{
    public string? AdminId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Role { get; set; }
}

public class ClassroomDto
{
    public string? ClassroomId { get; set; }
    public string? ClassroomName { get; set; }
    public int? Capacity  { get; set; }
}