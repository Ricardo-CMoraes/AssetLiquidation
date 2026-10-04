namespace AssetLiquidation.Api.DTOs;

public record CreateAssetRequest(
    string AssetId,
    decimal InitialAmount
);

public record AssetResponse(
    Guid Id,
    string AssetId,
    decimal InitialAmount,
    decimal CurrentAmount,
    String Status,
    DateTime CreatedAt
);