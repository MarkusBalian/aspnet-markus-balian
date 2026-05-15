namespace CoreFitnessClub.Domain.Entities;

public class GymClass
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Instructor { get; set; } = string.Empty;
    public DateTime StartTimeUtc { get; set; }
    public int Capacity { get; set; } = 20;
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}