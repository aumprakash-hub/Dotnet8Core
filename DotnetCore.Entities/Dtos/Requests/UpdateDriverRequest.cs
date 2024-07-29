namespace DotnetCore.Entities.Dtos.Requests;

public class UpdateDriverRequest
{
    public Guid DriverId { get; set; }
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public int DriverNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
}