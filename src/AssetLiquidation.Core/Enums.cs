namespace AssetLiquidation.Core;

public enum AssetStatus
{
    Active = 1,
    Inactive = 2,
    Canceled = 3
}

public enum OccurrenceType
{
    Payment = 1,
    Refund = 2,
    Cancellation = 3
}

public enum ProcessingStatus
{
    Pending = 1,
    Completed = 2,
    Rejected = 3
}