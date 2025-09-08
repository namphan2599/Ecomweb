using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecomweb.Migrations
{
    public partial class UseDecimalInProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 1, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", false, "Chicken", 763.91305573617550m, 1 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 2, "The Football Is Good For Training And Recreational Purposes", false, "Chips", 124.659435783718540m, 24 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 3, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", false, "Pants", 863.009126787459070m, 67 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 4, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", false, "Sausages", 923.170967699074480m, 38 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 5, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", false, "Chips", 511.836113273319550m, 88 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 6, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", false, "Sausages", 507.188042542413640m, 84 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 7, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", false, "Shoes", 453.079208636361550m, 32 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 8, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", false, "Table", 831.119924223964090m, 17 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 9, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", false, "Shirt", 239.936905441784530m, 3 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 10, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", false, "Hat", 353.317802291947450m, 10 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 11, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", false, "Hat", 624.826904784198040m, 69 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 12, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", false, "Sausages", 359.825160246946120m, 24 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 13, "The Football Is Good For Training And Recreational Purposes", false, "Towels", 77.2318219254134860m, 47 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 14, "The Football Is Good For Training And Recreational Purposes", false, "Chair", 347.165453792206270m, 23 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 15, "The Football Is Good For Training And Recreational Purposes", false, "Sausages", 373.823895582387460m, 82 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 16, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", false, "Bike", 449.209915343919280m, 10 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 17, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", false, "Car", 92.6197154972223760m, 23 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 18, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", false, "Fish", 756.783508992516820m, 20 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 19, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", false, "Chair", 606.780906097629880m, 32 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 20, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", false, "Car", 382.098694382297740m, 88 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 21, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", false, "Salad", 112.582535657277610m, 67 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 22, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", false, "Cheese", 240.165159407036920m, 24 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 23, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", false, "Cheese", 255.824672934978280m, 53 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 24, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", false, "Shoes", 571.426433753161240m, 56 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 25, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", false, "Sausages", 70.8299473239945190m, 42 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 26, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", false, "Chair", 193.485860931503350m, 23 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 27, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", false, "Table", 607.947114781722970m, 96 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 28, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", false, "Computer", 738.906777936469990m, 7 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 29, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", false, "Soap", 509.595458615662420m, 60 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 30, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", false, "Soap", 533.344479987857770m, 10 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 31, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", false, "Pizza", 740.010637800773560m, 65 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 32, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", false, "Chicken", 360.180157575260980m, 13 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 33, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", false, "Shirt", 903.327134152225960m, 24 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 34, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", false, "Mouse", 742.154838588043750m, 2 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 35, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", false, "Keyboard", 676.352479238822530m, 41 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 36, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", false, "Fish", 283.354482918381040m, 20 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 37, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", false, "Car", 745.857224602748380m, 31 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 38, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", false, "Salad", 496.438981056274510m, 41 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 39, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", false, "Pizza", 390.086009132973220m, 40 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 40, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", false, "Pizza", 300.619517252489920m, 73 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 41, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", false, "Tuna", 775.969026690879550m, 71 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 42, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", false, "Pants", 272.270010221853040m, 97 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 43, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", false, "Bike", 408.398770269238420m, 35 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 44, "The Football Is Good For Training And Recreational Purposes", false, "Shoes", 544.537985716511920m, 12 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 45, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", false, "Chips", 115.182451019783170m, 95 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 46, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", false, "Chair", 392.419530623282680m, 77 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 47, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", false, "Table", 753.148169969798890m, 83 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 48, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", false, "Hat", 91.777663245442780m, 32 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 49, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", false, "Bacon", 762.928904247606460m, 4 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 50, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", false, "Pizza", 912.19084370873920m, 68 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Products",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");
        }
    }
}
