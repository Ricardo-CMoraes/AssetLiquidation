namespace AssetLiquidation.Core;

public class Occurrence
{
    public Guid Id { get; private set; }
    public Guid AssetId { get; private set; }
    public OccurrenceType Type { get; private set; }
    public decimal Amount { get; private set; }
    public ProcessingStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string? Reason { get; private set; }

    protected Occurrence() {}

    public Occurrence(Guid assetId, OccurrenceType type, decimal amount)
    {
        this.Id = Guid.NewGuid();
        this.AssetId = assetId;
        this.Type = type;
        this.Amount = amount;
        this.Status = ProcessingStatus.Pending;
        this.CreatedAt = DateTime.UtcNow;
    }

    public void MarkAsCompleted()
    {
        this.Status = ProcessingStatus.Completed;
    }

    public void MarkAsRejected(string reason)
    {
        this.Status = ProcessingStatus.Rejected;
        this.Reason = reason;
    }
}