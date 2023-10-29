namespace WebFootballApp.Entities;

public class StaffMember
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime? Birthday { get; set; }
    public string? Picture { get; set; }
    public Job? Job { get; set; }
    public Team? Team { get; set; }
}