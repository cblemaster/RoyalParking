namespace RoyalParking.API.Entities;

public partial class ParkingSlip
{
    public int Id { get; set; }

    public int VehicleId { get; set; }

    public int? ValetId { get; set; }

    public int? ParkingSpotId { get; set; }

    public int ParkingStatusId { get; set; }

    public int RateId { get; set; }

    public int? DiscountId { get; set; }

    public DateTime TimeIn { get; set; }

    public DateTime? TimeOut { get; set; }

    public decimal? AmountDue { get; set; }

    public decimal? AmountPaid { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? SavedDetails { get; set; }

    public virtual Discount? Discount { get; set; }

    public virtual ParkingSpot? ParkingSpot { get; set; }

    public virtual ParkingStatus ParkingStatus { get; set; } = null!;

    public virtual Rate Rate { get; set; } = null!;

    public virtual Valet? Valet { get; set; }

    public virtual Vehicle Vehicle { get; set; } = null!;
}
