using System;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml;
using System.Data;
using System.IO;

using var db = new OreqmContext();

// Note: This sample requires the database to be created before running.
Console.WriteLine($"Database path: {db.DbPath}.");
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

// Load your template (existing Excel file)
//var templatePath = @"C:\Users\kera271308\Desktop\PFDSYS-11617\Project\ImportOreqM\Template.\SummaryTemplate.xlsx";
var templatePath = Environment.CurrentDirectory + "/Template/SummaryTemplate.xlsx";


using (var dbContext = new OreqmContext())
{

    var spec = dbContext.Specdocuments.ExecuteDeleteAsync(); 
    dbContext.SaveChanges();

    var oreqmManager = new OreqmFileManager();
    //var directoryPath = @"C:\Users\kera271308\Desktop\PFDSYS-11617\Project\ImportOreqM\InputOreqm\";
    var directoryPath = Environment.CurrentDirectory + "/InputOreqm/";
    string moduleName = "";
    
    foreach (var oreqmFile in oreqmManager.GetOreqmFiles(directoryPath)) // Implement GetOreqmFiles() to return a list of files
    {
        moduleName = oreqmFile.Filename.Replace("asc_", "");
        moduleName = moduleName.Substring(0, moduleName.IndexOf("_"));

        XmlDocument doc = new XmlDocument();
        doc.LoadXml(oreqmFile.Content);
        XmlNodeList EmpNodes = doc.SelectNodes("//specdocument//specobject");
        XmlElement root = doc.DocumentElement; //get the root element.
        foreach (XmlNode nodes in EmpNodes)
        {
            dbContext.Add(new Specdocument
            {
                id = nodes["id"].InnerText,
                status = nodes["status"].InnerText,
                doctype ="",
                moduleName = moduleName,
                //doctype = nodes["doctype"].InnerText,
                //doctype = nodes.Attributes.GetNamedItem("doctype");
                covstatus = nodes["covstatus"].InnerText,
                oreqmViolations = nodes["oreqmViolations"].InnerText,
                violations = nodes["violations"].InnerText,
                internalId = nodes["internalId"].InnerText,
                source = nodes["source"] == null ? "" : nodes["source"].InnerText,
                version = nodes["version"].InnerText,
            });
        }
        dbContext.SaveChanges();
    }
}


using (var package = new ExcelPackage(new FileInfo(templatePath)))
{
    // Retrieve data from the database
    var specExcel = await db.Specdocuments.ToListAsync();
    var worksheet = package.Workbook.Worksheets["RawData"];

    for (int i = 0; i < specExcel.Count; i++)
    {
        worksheet.Cells[i + 2, 1].Value = specExcel[i].SpecdocumentId;
        worksheet.Cells[i + 2, 2].Value = specExcel[i].doctype;
        worksheet.Cells[i + 2, 3].Value = specExcel[i].moduleName;
        worksheet.Cells[i + 2, 4].Value = specExcel[i].id;
        worksheet.Cells[i + 2, 5].Value = specExcel[i].status;
        worksheet.Cells[i + 2, 6].Value = specExcel[i].source;
        worksheet.Cells[i + 2, 7].Value = specExcel[i].version;
        worksheet.Cells[i + 2, 8].Value = specExcel[i].violations;
        worksheet.Cells[i + 2, 9].Value = specExcel[i].oreqmViolations;
        worksheet.Cells[i + 2, 10].Value = specExcel[i].covstatus;
        worksheet.Cells[i + 2, 11].Value = specExcel[i].internalId;
    }

    // Fill data into specific cells (e.g., A1, B2, etc.)
    // Add more data as needed

    // Save the modified template to a new file
    //var outputPath = @"C:\Users\kera271308\Desktop\PFDSYS-11617\Project\ImportOreqM\Results\output.xlsx";
    var outputPath = Path.Combine(Environment.CurrentDirectory + "/Results", "output.xlsx");
    package.SaveAs(new FileInfo(outputPath));
}


//// Create
//Console.WriteLine("Inserting a new blog");
//db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
//db.SaveChanges();

//// Read
//Console.WriteLine("Querying for a blog");
//var blog = db.Blogs
//    .OrderBy(b => b.BlogId)
//    .First();

//// Update
//Console.WriteLine("Updating the blog and adding a post");
//blog.Url = "https://devblogs.microsoft.com/dotnet";
//blog.Posts.Add(
//    new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
//db.SaveChanges();

//// Delete
//Console.WriteLine("Delete the blog");
//db.Remove(blog);
//db.SaveChanges();
