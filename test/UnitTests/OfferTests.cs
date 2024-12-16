using FluentAssertions;
using MaterialPurchase.Common.Domain.ValueObjects;
using MaterialPurchase.Offers.Domain.Offer;
using MaterialPurchase.OffersContracts.DomainEvents;
using Xunit;

namespace UnitTests;

public class OfferTests
{
    [Fact]
    public void Create_ValidInput_Succeeds()
    {
        //Arrange
        var supplyerId = 1;
        var validFrom = DateTimeOffset.Now;
        var validTo = DateTimeOffset.Now.AddDays(1);
        var note = "Test Offer";

        //Act
        var offer = Offer.Create(supplyerId, validFrom, validTo, note);

        //Assert
        offer.Should().NotBeNull();
        offer.SupplierId.Should().Be(supplyerId);
        offer.ValidFrom.Should().Be(validFrom);
        offer.ValidTo.Should().Be(validTo);
        offer.Note.Should().Be(note);
        offer.Status.Should().Be(OfferStatus.Draft);
        offer.DomainEvents.Should().HaveCount(1);
        offer.DomainEvents[0].Should().BeOfType<OfferCreatedDomainEvent>();
    }
    
    [Fact]
    public void AddOfferItem_ValidInput_Succeeds()
    {
        //Arrange
        var offer = Offer.Create(1, DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1), "Test Offer");
        var productId = 1;
        var price = new Money(1, "EUR");
        var availableQuantity = 10;

        //Act
        offer.AddOfferItem(productId, price, availableQuantity);

        //Assert
        offer.OfferItems.Should().HaveCount(1);
        var offerItem = offer.OfferItems.First();
        offerItem.ProductId.Should().Be(productId);
        offerItem.Price.Should().Be(price);
        offerItem.AvailableQuantity.Should().Be(availableQuantity);
        offer.DomainEvents.Should().HaveCount(2);
        offer.DomainEvents[1].Should().BeOfType<OfferItemAddedDomainEvent>();
    }
    
    [Fact]
    public void AddOfferItem_ProductAlreadyAdded_ThrowsException()
    {
        //Arrange
        var offer = Offer.Create(1, DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1), "Test Offer");
        var productId = 1;
        var price = new Money(1, "EUR");
        var availableQuantity = 10;
        offer.AddOfferItem(productId, price, availableQuantity);

        //Act
        var act = () => offer.AddOfferItem(productId, price, availableQuantity);

        //Assert
        act.Should().Throw<InvalidOperationException>().WithMessage("Product already added to offer");
    }
    
    [Fact]
    public void AddOfferItem_OfferNotInDraft_ThrowsException()
    {
        //Arrange
        var offer = Offer.Create(1, DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1), "Test Offer");
        offer.Confirm();

        //Act
        var act = () => offer.AddOfferItem(1, new Money(1, "EUR"), 10);

        //Assert
        act.Should().Throw<InvalidOperationException>().WithMessage("Offer is not in draft status");
    }
    
    [Fact]
    public void Confirm_ValidInput_Succeeds()
    {
        //Arrange
        var offer = Offer.Create(1, DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1), "Test Offer");

        //Act
        offer.Confirm();

        //Assert
        offer.Status.Should().Be(OfferStatus.Confirmed);
        offer.DomainEvents.Should().HaveCount(2);
        offer.DomainEvents[1].Should().BeOfType<OfferConfirmedDomainEvent>();
    }
    
    [Fact]
    public void Confirm_OfferNotInDraft_ThrowsException()
    {
        //Arrange
        var offer = Offer.Create(1, DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1), "Test Offer");
        offer.Confirm();

        //Act
        var act = () => offer.Confirm();

        //Assert
        act.Should().Throw<InvalidOperationException>().WithMessage("Offer is not in draft status");
    }
    
    [Fact]
    public void ReserveItemQuantity_ValidInput_Succeeds()
    {
        //Arrange
        var offer = Offer.Create(1, DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1), "Test Offer");
        offer.AddOfferItem(1, new Money(1, "EUR"), 10);
        offer.Confirm();

        //Act
        offer.ReserveItemQuantity(1, 1);

        //Assert
        offer.DomainEvents.Should().HaveCount(4);
        offer.DomainEvents[^1].Should().BeOfType<OfferItemAvailableQuantityChangedDomainEvent>();
    }
    
    [Fact]
    public void ReserveItemQuantity_OfferNotConfirmed_ThrowsException()
    {
        //Arrange
        var offer = Offer.Create(1, DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1), "Test Offer");

        //Act
        var act = () => offer.ReserveItemQuantity(1, 1);

        //Assert
        act.Should().Throw<InvalidOperationException>().WithMessage("Offer is not in confirmed status");
    }
    
    [Fact]
    public void ReserveItemQuantity_ProductNotAdded_ThrowsException()
    {
        //Arrange
        var offer = Offer.Create(1, DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1), "Test Offer");
        offer.Confirm();

        //Act
        var act = () => offer.ReserveItemQuantity(1, 1);

        //Assert
        act.Should().Throw<InvalidOperationException>().WithMessage("Product not found in offer");
    }
}