namespace RoyalParking.Core.Entities;

public partial class ParkingSpot
{
    public int Id { get; set; }

    public string Number { get; set; } = null!;

    public virtual ICollection<ParkingSlip> ParkingSlips { get; set; } = new List<ParkingSlip>();
}
