namespace RoyalParking.API.Entities;

public partial class Rate
{
    public int Id { get; set; }

    public decimal WeekdayRate { get; set; }

    public decimal SaturdaySundayRate { get; set; }

    public decimal SpecialEventRate { get; set; }

    public virtual ICollection<ParkingSlip> ParkingSlips { get; set; } = new List<ParkingSlip>();
}
