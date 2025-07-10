CREATE TABLE "order_items"
(
    "Id" NVARCHAR(MAX) NOT NULL,
    "OrderId" NVARCHAR(MAX),
    "ProductId" NVARCHAR(MAX),
    "Quantity" NVARCHAR(MAX),
    "UnitPrice" NVARCHAR(MAX),
    "TotalPrice" NVARCHAR(MAX),
    "CreatedAt" NVARCHAR(MAX),
    "UpdatedAt" NVARCHAR(MAX),
    "Id" NVARCHAR(MAX),
    CONSTRAINT "order_items_pkey" PRIMARY KEY ("Id")
)

