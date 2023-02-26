namespace yeaa2.Entities;

public class ImperialMeasurement
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int HeightFt { get; set; }
    public int HeightIn { get; set; }
    public int WeightLbs { get; set; }
    public double Bmi { get; set; }
    public DateTime CreatedAt { get; set; }

    public virtual User User { get; set; }
}