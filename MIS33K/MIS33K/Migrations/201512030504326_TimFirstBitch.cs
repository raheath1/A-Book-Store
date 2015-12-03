namespace MIS33K.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimFirstBitch : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookID = c.Int(nullable: false, identity: true),
                        BookSKU = c.String(),
                        Title = c.String(),
                        AuthorFName = c.String(),
                        AuthorLName = c.String(),
                        PurchaseHistory = c.Int(nullable: false),
                        ProfitMargin = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomerPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PublicationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Genre = c.String(),
                        CountonHand = c.Int(nullable: false),
                        ReoderPoint = c.Int(nullable: false),
                        ProcurementShoppingCart = c.Int(nullable: false),
                        PendingOrder = c.Int(nullable: false),
                        OrderAmountRecieved = c.Int(nullable: false),
                        CustomerRegularPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomerDiscountedPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Coupon_CouponID = c.String(maxLength: 20),
                        Order_OrderID = c.Int(),
                        ShoppingCart_ShoppingCartID = c.Int(),
                    })
                .PrimaryKey(t => t.BookID)
                .ForeignKey("dbo.Coupons", t => t.Coupon_CouponID)
                .ForeignKey("dbo.Orders", t => t.Order_OrderID)
                .ForeignKey("dbo.ShoppingCarts", t => t.ShoppingCart_ShoppingCartID)
                .Index(t => t.Coupon_CouponID)
                .Index(t => t.Order_OrderID)
                .Index(t => t.ShoppingCart_ShoppingCartID);
            
            CreateTable(
                "dbo.Coupons",
                c => new
                    {
                        CouponID = c.String(nullable: false, maxLength: 20),
                        FreeShipMinimum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PercentOff = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountEnabled = c.Boolean(nullable: false),
                        CouponTypeVar = c.Int(nullable: false),
                        CouponEntered = c.String(),
                        theCouponType = c.Int(nullable: false),
                        CouponEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CouponID);
            
            CreateTable(
                "dbo.CreditCards",
                c => new
                    {
                        CreditCardID = c.Int(nullable: false, identity: true),
                        DefaultPayment = c.String(),
                        Card1 = c.Boolean(nullable: false),
                        Card1Type = c.Int(nullable: false),
                        Card1Num = c.String(),
                        Card2 = c.Boolean(nullable: false),
                        Card2Type = c.Int(nullable: false),
                        Card2Num = c.String(),
                        Card3 = c.Boolean(nullable: false),
                        Card3Type = c.Int(nullable: false),
                        Card3Num = c.String(),
                    })
                .PrimaryKey(t => t.CreditCardID);
            
            CreateTable(
                "dbo.Discounts",
                c => new
                    {
                        DiscountID = c.Int(nullable: false, identity: true),
                        DiscountedBook = c.Int(nullable: false),
                        SelectedBook = c.Int(nullable: false),
                        DiscountEnabled = c.Boolean(nullable: false),
                        CustomerDiscountedPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Book_BookID = c.Int(),
                    })
                .PrimaryKey(t => t.DiscountID)
                .ForeignKey("dbo.Books", t => t.Book_BookID)
                .Index(t => t.Book_BookID);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailID = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        CustomerPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PriceLastPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProcurementCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Book_BookID = c.Int(),
                        Order_OrderID = c.Int(),
                    })
                .PrimaryKey(t => t.OrderDetailID)
                .ForeignKey("dbo.Books", t => t.Book_BookID)
                .ForeignKey("dbo.Orders", t => t.Order_OrderID)
                .Index(t => t.Book_BookID)
                .Index(t => t.Order_OrderID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        ShoppingCartSubTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CartTotalWShipping = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProcurementCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Customer_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.AspNetUsers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.ShoppingCartDetails",
                c => new
                    {
                        ShoppingCartDetailID = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        CustomerPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PriceLastPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Book_BookID = c.Int(),
                        Customer_Id = c.String(maxLength: 128),
                        Order_OrderID = c.Int(),
                        ShoppingCart_ShoppingCartID = c.Int(),
                    })
                .PrimaryKey(t => t.ShoppingCartDetailID)
                .ForeignKey("dbo.Books", t => t.Book_BookID)
                .ForeignKey("dbo.AspNetUsers", t => t.Customer_Id)
                .ForeignKey("dbo.Orders", t => t.Order_OrderID)
                .ForeignKey("dbo.ShoppingCarts", t => t.ShoppingCart_ShoppingCartID)
                .Index(t => t.Book_BookID)
                .Index(t => t.Customer_Id)
                .Index(t => t.Order_OrderID)
                .Index(t => t.ShoppingCart_ShoppingCartID);
            
            CreateTable(
                "dbo.ShoppingCarts",
                c => new
                    {
                        ShoppingCartID = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(nullable: false),
                        PriceLastPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GetCount = c.Int(nullable: false),
                        CalcCartSubTotal = c.Decimal(precision: 18, scale: 2),
                        CalcShipping = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CalcOrderTotal = c.Decimal(precision: 18, scale: 2),
                        Customer_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ShoppingCartID)
                .ForeignKey("dbo.AspNetUsers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingCartDetails", "ShoppingCart_ShoppingCartID", "dbo.ShoppingCarts");
            DropForeignKey("dbo.ShoppingCarts", "Customer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Books", "ShoppingCart_ShoppingCartID", "dbo.ShoppingCarts");
            DropForeignKey("dbo.ShoppingCartDetails", "Order_OrderID", "dbo.Orders");
            DropForeignKey("dbo.ShoppingCartDetails", "Customer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ShoppingCartDetails", "Book_BookID", "dbo.Books");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.OrderDetails", "Order_OrderID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "Customer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Books", "Order_OrderID", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "Book_BookID", "dbo.Books");
            DropForeignKey("dbo.Discounts", "Book_BookID", "dbo.Books");
            DropForeignKey("dbo.Books", "Coupon_CouponID", "dbo.Coupons");
            DropIndex("dbo.ShoppingCarts", new[] { "Customer_Id" });
            DropIndex("dbo.ShoppingCartDetails", new[] { "ShoppingCart_ShoppingCartID" });
            DropIndex("dbo.ShoppingCartDetails", new[] { "Order_OrderID" });
            DropIndex("dbo.ShoppingCartDetails", new[] { "Customer_Id" });
            DropIndex("dbo.ShoppingCartDetails", new[] { "Book_BookID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Orders", new[] { "Customer_Id" });
            DropIndex("dbo.OrderDetails", new[] { "Order_OrderID" });
            DropIndex("dbo.OrderDetails", new[] { "Book_BookID" });
            DropIndex("dbo.Discounts", new[] { "Book_BookID" });
            DropIndex("dbo.Books", new[] { "ShoppingCart_ShoppingCartID" });
            DropIndex("dbo.Books", new[] { "Order_OrderID" });
            DropIndex("dbo.Books", new[] { "Coupon_CouponID" });
            DropTable("dbo.ShoppingCarts");
            DropTable("dbo.ShoppingCartDetails");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Discounts");
            DropTable("dbo.CreditCards");
            DropTable("dbo.Coupons");
            DropTable("dbo.Books");
        }
    }
}
