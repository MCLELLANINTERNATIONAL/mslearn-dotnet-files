using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

// creating a new directory
/*Directory.CreateDirectory(
    Path.Combine(
        Directory.GetCurrentDirectory(),
        "stores",
        "201",
        "newDir"));*/

//Console.WriteLine("Directory created.");

// does file exist
/*string filePath =
    Path.Combine(
        Directory.GetCurrentDirectory(),
        "stores",
        "201");
bool doesDirectoryExist = Directory.Exists(filePath);

Console.WriteLine(doesDirectoryExist);*/

// special directories
// Console.WriteLine(Directory.GetCurrentDirectory());

// special path characters
//Console.WriteLine($"stores{Path.DirectorySeparatorChar}201");

// join paths
//Console.WriteLine(Path.Combine("stores","201"));

// path extensions
//Console.WriteLine(Path.GetExtension("sales.json"));

// everything one needs to know about a file or path
/*string fileName = $"stores{Path.DirectorySeparatorChar}201{Path.DirectorySeparatorChar}sales{Path.DirectorySeparatorChar}sales.json";
FileInfo info = new FileInfo(fileName);
Console.WriteLine($"Full Name: {info.FullName}{Environment.NewLine}Directory: {info.Directory}{Environment.NewLine}Extension: {info.Extension}{Environment.NewLine}Create Date: {info.CreationTime}");*/

var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory, "stores");

var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
Directory.CreateDirectory(salesTotalDir);

var salesFiles = FindFiles(storesDirectory);

var salesTotal = CalculateSalesTotal(salesFiles);

//var salesJson = File.ReadAllText($"stores{Path.DirectorySeparatorChar}201{Path.DirectorySeparatorChar}sales.json");

//var data = JsonConvert.DeserializeObject<SalesTotal>(salesJson);

//Console.WriteLine(salesData.Total);

File.AppendAllText(Path.Combine(salesTotalDir, "totals.txt"), $"{salesTotal}{Environment.NewLine}");

/*foreach (var file in salesFiles)
{
    Console.WriteLine(file);
}*/

IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();

    var foundFiles = Directory.EnumerateFiles(
        folderName,
        "*",
        SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        // The file name will contain the full path,
        var extension = Path.GetExtension(file);
        if (extension == ".json")
        {
            salesFiles.Add(file);
        }
    }

    return salesFiles;
}

/*class SalesTotal
{
    public double Total { get; set; }
}*/

double CalculateSalesTotal(IEnumerable<string> salesFiles)
{
    double salesTotal = 0;
    
    // Loop over each file path in salesFiles
    foreach (var file in salesFiles)
    {      
        // Read the contents of the file
        string salesJson = File.ReadAllText(file);
    
        // Parse the contents as JSON
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);
    
        // Add the amount found in the Total field to the salesTotal variable
        salesTotal += data?.Total ?? 0;
    }
    
    return salesTotal;
}

record SalesData (double Total);