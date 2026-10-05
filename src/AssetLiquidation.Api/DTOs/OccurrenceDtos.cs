using AssetLiquidation.Core;

namespace AssetLiquidation.Api.DTOs;

public record ProcessOccurrenceRequest(
    string AssetId,
    decimal Amount,
    OccurrenceType Type
);

public record OccurrenceResponse(
    Guid Id,
    string AssetId,
    OccurrenceType Type,
    decimal Amount,
    ProcessingStatus Status,
    DateTime CreatedAt,
    string? Reason
);