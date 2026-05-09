# Sales Summary Assignment Notes

## Additional Function Added

An additional function named `GenerateSalesSummaryReport()` was added to the application to generate a sales summary report file.

The function:
- Calculates the overall sales total
- Creates a detailed breakdown of each store sales file
- Writes the report to `sales-summary.txt`

## Example Output

### text
Sales Summary
----------------------------
Total Sales: £1,923.32

Details:
stores/204/sales.json: £88.88
stores/203/sales.json: £99.00
stores/202/sales.json: £1,234.22
stores/201/sales.json: £501.22

## Updated FindFiles Logic

The `FindFiles()` method was updated so that only valid `sales.json` files from the store directories are included in the report.

### csharp
if (extension == ".json" &&
    Path.GetFileName(file) == "sales.json" &&
    file.Contains($"{Path.DirectorySeparatorChar}20"))
{
    salesFiles.Add(file);
}


## Added Function

### csharp
void GenerateSalesSummaryReport(
    IEnumerable<string> salesFiles,
    string reportPath)
{
    var report = new StringBuilder();

    double grandTotal = 0;

    var fileTotals = new List<(string FileName, double Total)>();

    foreach (var file in salesFiles)
    {
        string salesJson = File.ReadAllText(file);

        SalesData? data =
            JsonConvert.DeserializeObject<SalesData>(salesJson);

        if (data is not null)
        {
            grandTotal += data.Total;

            fileTotals.Add(
                (Path.GetRelativePath(currentDirectory, file), data.Total));
        }
    }

    report.AppendLine("Sales Summary");
    report.AppendLine("----------------------------");
    report.AppendLine($"Total Sales: {grandTotal:C}");
    report.AppendLine();
    report.AppendLine("Details:");

    foreach (var item in fileTotals)
    {
        report.AppendLine(
            $"{item.FileName}: {item.Total:C}");
    }

    File.WriteAllText(reportPath, report.ToString());
}
