using System.Text.RegularExpressions;

namespace AssetLiquidation.Core;

public class Asset
{
    public Guid Id { get; private set; }
    public string AssetId { get; private set;} = string.Empty;
    public decimal InitialAmount { get; private set; }
    public decimal CurrentAmount { get; private set; }
    public AssetStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    protected Asset() {}

    public Asset(string assetId, decimal initialAmount)
    {
        if (string.IsNullOrWhiteSpace(assetId) || !Regex.IsMatch(assetId, @"^\d{11}$"))
            throw new ArgumentException("AssetId must contain exactly 11 numeric digits.", nameof(assetId));
        if (initialAmount <= 0)
            throw new ArgumentException("Initial amount must be greater than zero.", nameof(initialAmount));

        this.Id = Guid.NewGuid();
        this.AssetId = assetId;
        this.InitialAmount = initialAmount;
        this.CurrentAmount = initialAmount;
        this.Status = AssetStatus.Active;
        this.CreatedAt = DateTime.UtcNow;
    }

    public void ProcessPayment(decimal amount)
    {
        if (this.Status != AssetStatus.Active)
            throw new InvalidOperationException("Cannot process payment for an inactive or canceled asset.");
        
        if (amount <= 0 || amount > this.CurrentAmount)
            throw new ArgumentException("Payment amount must be greater than zero and less than or equal to the current amount.", nameof(amount));
    
        this.CurrentAmount -= amount;
        if (this.CurrentAmount == 0)
        {
            this.Status = AssetStatus.Inactive;
        }
    }

    public void ProcessRefund(decimal amount)
    {
        if (this.Status == AssetStatus.Canceled)
            throw new InvalidOperationException("Cannot process refund for an canceled asset.");
        
        if (amount <= 0 || (this.CurrentAmount + amount) > this.InitialAmount)
            throw new ArgumentException("Refund amount must be greater than zero and less than or equal to the initial amount.", nameof(amount));
    
        this.CurrentAmount += amount;
        if (this.Status == AssetStatus.Inactive && this.CurrentAmount > 0)
        {
            this.Status = AssetStatus.Active;
        }
    }

    public void ProcessCancellation()
    {
        if (this.Status == AssetStatus.Canceled)
            throw new InvalidOperationException("Cannot process a canceled asset.");
        else
        {
            this.Status = AssetStatus.Canceled;
        }
    }
}
