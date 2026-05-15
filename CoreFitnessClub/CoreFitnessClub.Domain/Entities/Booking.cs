namespace CoreFitnessClub.Domain.Entities;

public class Booking
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserId { get; set; } = string.Empty;
    public Guid GymClassId { get; set; }
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

    public GymClass? GymClass { get; set; }
}