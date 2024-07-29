namespace DotnetCore.Entities.DbSet;

public class Driver : BaseEntity
{
    public Driver()
    {
        Achievements = new HashSet<Achievement>();
    }
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public int DriverNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public virtual ICollection<Achievement> Achievements { get; set; }
}