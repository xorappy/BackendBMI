using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yeaa2.Entities;

public class User
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    [Index(IsUnique = true)]
    public string Username { get; set; }

    [Required]
    public byte[] Password { get; set; }

    [Required]
    public byte[] Salt { get; set; }

    public virtual List<ImperialMeasurement> ImperialMeasurements { get; set; }

    public virtual List<MetricMeasurement> MetricMeasurements { get; set; }


}