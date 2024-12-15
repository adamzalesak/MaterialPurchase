-- Seed example data for demonstration purposes

-- create products
insert into dbo.Products (Id, Code, Name, Description, IsActive)
values (1, 'P1', 'bubble wrap 1x15m', 'bubble wrap 1x15m', 1);

insert into dbo.Products (Id, Code, Name, Description, IsActive)
values (2, 'P2', 'bubble wrap 1x30m', 'bubble wrap 1x30m', 1);

insert into dbo.Products (Id, Code, Name, Description, IsActive)
values (3, 'P3', 'bubble wrap 1x50m', 'bubble wrap 1x50m', 1);

declare @offerId uniqueidentifier = newid();

-- create offer
insert into offers.OfferHeaders (Id, Version, SupplierId, Status, ValidFrom, ValidTo, Note)
values (@offerId, newid(), 1, 1, '2021-01-01', '2021-12-31', 'Offer 1');

declare @offerItem1Id uniqueidentifier = newid();
declare @offerItem2Id uniqueidentifier = newid();

-- create offer items
insert into offers.OfferItems (Id, OfferId, ProductId, AvailableQuantity, PriceAmount, PriceCurrency)
values (@offerItem1Id, @offerId, 1, 100, 10.00, 'EUR');

insert into offers.OfferItems (Id, OfferId, ProductId, AvailableQuantity, PriceAmount, PriceCurrency)
values (@offerItem2Id, @offerId, 2, 100, 20.00, 'EUR');

declare @orderCartId uniqueidentifier = newid();

-- create order cart
insert into orderCarts.OrderCartHeaders (Id, Version, Name, Status)
values (@orderCartId, newid(), 'Cart 1', 0);

insert into orderCarts.OrderCartStatsReadModels (Id, CreatedCount, FinishedCount)
values (newid(), 1, 0);

-- crate order cart item
insert into orderCarts.OrderCartItems (Id, Name, OrderCartId, ProductId, OfferId, SupplierId, Quantity, PriceAmount, PriceCurrency)
values (newid(), 'bubble wrap 1x15m', @orderCartId, 1, @offerId, 1, 10, 10.00, 'EUR');
