using AssetLiquidation.Core;
using FluentAssertions;
using Xunit;

namespace AssetLiquidation.Tests;

public class OccurrenceTests
{
    [Fact]
    public void Constructor_WithValidParameters_ShouldInitializeAsPending()
    {
        var assetGuid = Guid.NewGuid();
        var amount = 250.50m;
        var type = OccurrenceType.Payment;

        var occurrence = new Occurrence(assetGuid, type, amount);

        occurrence.Id.Should().NotBeEmpty();
        occurrence.AssetId.Should().Be(assetGuid);
        occurrence.Amount.Should().Be(amount);
        occurrence.Type.Should().Be(type);
        occurrence.Status.Should().Be(ProcessingStatus.Pending);
        occurrence.Reason.Should().BeNull();
        occurrence.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void MarkAsCompleted_ShouldUpdateStatusToCompleted()
    {
        var occurrence = new Occurrence(Guid.NewGuid(), OccurrenceType.Payment, 100.00m);

        occurrence.MarkAsCompleted();

        occurrence.Status.Should().Be(ProcessingStatus.Completed);
    }

    [Fact]
    public void MarkAsRejected_ShouldUpdateStatusAndSetReason()
    {
        var occurrence = new Occurrence(Guid.NewGuid(), OccurrenceType.Payment, 500.00m);
        var reasonMessage = "Payment amount exceeds current asset balance.";

        occurrence.MarkAsRejected(reasonMessage);

        occurrence.Status.Should().Be(ProcessingStatus.Rejected);
        occurrence.Reason.Should().Be(reasonMessage);
    }
}