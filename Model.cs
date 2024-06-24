using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

public class OreqmContext : DbContext
{
    //public DbSet<Blog> Blogs { get; set; }
    //public DbSet<Post> Posts { get; set; }
    public DbSet<Specdocument> Specdocuments { get; set; }

    public string DbPath { get; }

    public OreqmContext()
    {
        //var folder = Environment.SpecialFolder.LocalApplicationData;
        //var path = Environment.GetFolderPath(folder);
        //DbPath = System.IO.Path.Join(path, "blogging.db");


        var folder = Directory.GetCurrentDirectory();
        //var path = Environment.GetFolderPath(folder);
        //DbPath = System.IO.Path.Join(folder, "blogging.db");

        DbPath = System.IO.Path.Join(folder, "oreqm.db");


    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}


public class Specdocument
{
    [Key]
    public int SpecdocumentId { get; set; }
    public string doctype { get; set; }
    public string moduleName { get; set; }
    public string id { get; set; }
    public string status { get; set; }
    public string source { get; set; }
    public string version { get; set; }
    public string violations { get; set; }
    public string oreqmViolations { get; set; }
    public string covstatus { get; set; }
    public string internalId { get; set; }
}


public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }

    public List<Post> Posts { get; } = new();
}

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}