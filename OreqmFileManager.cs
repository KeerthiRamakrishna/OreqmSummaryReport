using System;
using System.IO;
using System.Collections.Generic;

public class OreqmFile
{
    public string Filename { get; set; }
    public string Content { get; set; }
}

public class OreqmFileManager
{
    public List<OreqmFile> GetOreqmFiles(string directoryPath)
    {
        var oreqmFiles = new List<OreqmFile>();

        try
        {
            var files = Directory.GetFiles(directoryPath, "*.oreqm");

            foreach (var filePath in files)
            {
                var filename = Path.GetFileName(filePath);
                var content = File.ReadAllText(filePath);

                oreqmFiles.Add(new OreqmFile { Filename = filename, Content = content });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading Oreqm files: {ex.Message}");
        }

        return oreqmFiles;
    }
}


