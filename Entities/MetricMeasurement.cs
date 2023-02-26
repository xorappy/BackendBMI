namespace yeaa2.Entities;

public class MetricMeasurement
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int HeightCm { get; set; }
    public int WeightKg { get; set; }
    public double Bmi { get; set; }
    public DateTime CreatedAt { get; set; }

    public virtual User User { get; set; }
}