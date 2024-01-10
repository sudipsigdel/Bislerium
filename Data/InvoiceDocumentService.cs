using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Colors = QuestPDF.Helpers.Colors;
using IContainer = QuestPDF.Infrastructure.IContainer;

namespace Bislerium.Data
{
    public class InvoiceDocumentService : IDocument
    { 
        public List<ProductSalesQuantity> addIns = new List<ProductSalesQuantity>();
        public List<ProductSalesQuantity> coffee = new List<ProductSalesQuantity>();
        public List<Order> order = new List<Order>();
        public string timeFrame { get; set; }
        public double totalAmount { get; set; } = 0;

        public InvoiceDocumentService(List<ProductSalesQuantity> addInsList, List<ProductSalesQuantity> coffeeList, List<Order> orderList, string timeFrameParam)
        {
            addIns = addInsList;
            coffee = coffeeList;
            order = orderList;
            timeFrame = timeFrameParam;
        }  
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;


        public void Compose(IDocumentContainer container)

        {

            container
            .Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);
                page.DefaultTextStyle(x => x.FontSize(10));
                page.Header().Element(HeaderCompose);
                page.Content().Element(ComposeContent);
            });
        }



        //For Composing the header of PDF
        void HeaderCompose(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(18).SemiBold().FontColor(Colors.Blue.Medium);
            
            string pdfTitle = $"Bislerium Sales Transaction Report {timeFrame} - {DateTime.Now}";


            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"{pdfTitle}").Style(titleStyle);

                    column.Item().Text(text =>
                    {
                        text.Span("Issue Date: ").Medium();
                        text.Span($"{DateTime.Now}").Medium();
                    });
                });
            });
        }




        // For Composing content of PDF
        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(20).Column(column =>
            {

                var titleStyle = TextStyle.Default.FontSize(16).SemiBold();

                string pdfTitle = $"The Top 5 Most Purchased Items are - ({DateTime.Now})";

                column.Item().Text(pdfTitle).Style(titleStyle);

                column.Item().PaddingTop(7).Element(ComposeForMostPurchasedTable);

                // For Sales Transactions
                column.Item().PaddingTop(30).Element(ComposeHeaderForSalesTransaction);
                column.Item().PaddingTop(10).Element(ComposeTransactionsTableForSales);

            });
        }


        //Most purchase table
        void ComposeForMostPurchasedTable(IContainer Container)
        {
            Container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(250);
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    //For Add ins heading
                    header.Cell().Element(ComposeHeadingForTopAddins);

                    //For Coffee heading
                    header.Cell().Element(ComposeHeadingForTopCoffee);
                });

                //Most purchase coffee
                table.Cell().Element(ComposeMostPurchasedCoffee);

                //Most purchased Add ins 
                table.Cell().Element(ComposeMostPurchasedAddIns);

            });
        }




        //Composes the header for sales transactions table
        void ComposeHeaderForSalesTransaction(IContainer container)
        {
            var textStyle = TextStyle.Default.FontSize(18).SemiBold();

            string title = $"{timeFrame} Sales Transaction Report - ({DateTime.Now.Date})";

            

            foreach(Order item in order)
            {
                totalAmount += item.OrderTotalAmount;
            }
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"{title}").Style(textStyle);

                    column.Item().PaddingTop(2).Text(text =>
                    {
                        text.Span("Total Revenue: ").FontSize(15);
                        text.Span($"NRs. {totalAmount}").FontSize(15);
                    });
                });
            });
        }


        // This method generates the Sales Transactions table.
        void ComposeTransactionsTableForSales(IContainer container)
        {
            container.Table(table =>
            {
                // First process
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(20);
                    columns.ConstantColumn(130);
                    columns.ConstantColumn(90);
                    columns.ConstantColumn(80);
                    columns.RelativeColumn();
                    columns.RelativeColumn();

                });

                // Second process
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("#");
                    header.Cell().Element(CellStyle).Text("Customer Name");
                    header.Cell().Element(CellStyle).Text("Phone");
                    header.Cell().Element(CellStyle).Text("Date time");
                    header.Cell().Element(CellStyle).Text("Employee");
                    header.Cell().Element(CellStyle).Text("Total Amount");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });
                int counter = 1;
                foreach(Order item in order)
                {
                    
                    table.Cell().Element(CellStyle).Text(counter.ToString());
                    table.Cell().Element(CellStyle).Text(item.CustomerName);
                    table.Cell().Element(CellStyle).Text(item.CustomerPhone);
                    table.Cell().Element(CellStyle).Text(item.OrderDateTime.ToString());
                    table.Cell().Element(CellStyle).Text(item.EmployeeRole);
                    table.Cell().Element(CellStyle).Text(item.OrderTotalAmount.ToString());

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                    counter++;
                }
            });
        }


        // For Add Ins
        //Composes HEADER for top 5 most purchased addins
        void ComposeHeadingForTopAddins(IContainer container)
        {
            var textStyle = TextStyle.Default.FontSize(12).SemiBold();

            string title = "Most Purchased Add Ins";

            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"{title}").Style(textStyle);

                });
            });


        }




        // For Coffee
        //Composes HEADER for top 5 most purchased Coffee
        void ComposeHeadingForTopCoffee(IContainer container)
        {
            var textStyle = TextStyle.Default.FontSize(12).SemiBold();

            string title = "Most Purchased Coffee";

            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"{title}").Style(textStyle);

                });
            });

        }




        //Generates Top 5 Most Purchased Coffees
        void ComposeMostPurchasedCoffee(IContainer container)
        {
            container.Table(table =>
            {
                // First process
                table.ColumnsDefinition(columns =>
                {

                    columns.ConstantColumn(20);
                    columns.ConstantColumn(150);
                    columns.ConstantColumn(70);

                });

                //Second process
                table.Header(header =>
                {

                    header.Cell().Element(CellStyle).Text("#");
                    header.Cell().Element(CellStyle).Text("Coffee Name");
                    header.Cell().Element(CellStyle).Text("Quantity");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });
                int counter = 1;
                foreach (ProductSalesQuantity product in coffee)
                {
                    table.Cell().Element(CellStyle).Text(counter.ToString());
                    table.Cell().Element(CellStyle).Text(product.ProductName);
                    table.Cell().Element(CellStyle).Text(product.Quantity.ToString());

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                    counter++;
                }
            });
        }

        void ComposeMostPurchasedAddIns(IContainer container)
        {
            container.Table(table =>
            {
                // step 1
                table.ColumnsDefinition(columns =>
                {

                    columns.ConstantColumn(20);
                    columns.ConstantColumn(150);
                    columns.ConstantColumn(70);

                });

                // step 2
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("#");
                    header.Cell().Element(CellStyle).Text("AddIns Name");
                    header.Cell().Element(CellStyle).Text("Quantity");
                    
                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });
                int counter = 1;
                foreach (ProductSalesQuantity product in addIns)
                {
                    table.Cell().Element(CellStyle).Text(counter.ToString());
                    table.Cell().Element(CellStyle).Text(product.ProductName);
                    table.Cell().Element(CellStyle).Text(product.Quantity.ToString());

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                    counter++;
                }
            });

        }
    }
}
