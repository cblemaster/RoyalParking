namespace RoyalParking.Core.Entities;

public partial class ParkingStatus
{
    public int Id { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<ParkingSlip> ParkingSlips { get; set; } = new List<ParkingSlip>();
}
