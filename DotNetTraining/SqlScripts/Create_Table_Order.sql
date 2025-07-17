CREATE TABLE "orders"
(
    "Id" NVARCHAR(MAX) NOT NULL,
    "UserId" NVARCHAR(MAX),
    "OrderDate" NVARCHAR(MAX),
    "TotalAmount" NVARCHAR(MAX),
    "CreatedAt" NVARCHAR(MAX),
    "UpdatedAt" NVARCHAR(MAX),
    "Id" NVARCHAR(MAX),
    CONSTRAINT "orders_pkey" PRIMARY KEY ("Id")
)

