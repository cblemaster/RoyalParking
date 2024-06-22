namespace RoyalParking.API.Entities;

public partial class Valet
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<ParkingSlip> ParkingSlips { get; set; } = new List<ParkingSlip>();

    public virtual User User { get; set; } = null!;
}
