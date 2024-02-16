/******************************************************************************
https://onlinegdb.com/WV_4QpUkn
Codestyle: https://learn.microsoft.com/ru-ru/dotnet/csharp/fundamentals/coding-style/coding-conventions
Название: ООП на С#
Автор: Тимофеев Гордей Евгеньевич
Версия: 1
*******************************************************************************/
using System;

class Document
{
    public string Name { get; set; }
    public string Author { get; set; }
    public string[] Keywords { get; set; }
    public string Theme { get; set; }
    public string FilePath { get; set; }

    public virtual void GetInfo()
    {
        Console.WriteLine($"Document: {Name}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine("Keywords: " + string.Join(", ", Keywords));
        Console.WriteLine($"Theme: {Theme}");
        Console.WriteLine($"File Path: {FilePath}");
    }
}

class WordDocument : Document
{
    public override void GetInfo()
    {
        Console.WriteLine("MS Word document:");
        base.GetInfo();
    }
}

class PdfDocument : Document
{
    public override void GetInfo()
    {
        Console.WriteLine("PDF document:");
        base.GetInfo();
    }
}

class ExcelDocument : Document
{
    public override void GetInfo()
    {
        Console.WriteLine("MS Excel document:");
        base.GetInfo();
    }
}

class TxtDocument : Document
{
    public override void GetInfo()
    {
        Console.WriteLine("TXT document:");
        base.GetInfo();
    }
}

class HtmlDocument : Document
{
    public override void GetInfo()
    {
        Console.WriteLine("HTML document:");
        base.GetInfo();
    }
}

class DocumentInfoMenu
{
    private Document document;

    private DocumentInfoMenu()
    {
    }

    public static DocumentInfoMenu Instance { get; } = new DocumentInfoMenu();

    public void ShowMenu(Document doc)
    {
        document = doc;
        Console.WriteLine("1. Show document info");
        Console.WriteLine("2. Exit");
        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                document.GetInfo();
                break;
            case "2":
                return;
            default:
                Console.WriteLine("Invalid choice");
                break;
        }
        ShowMenu(doc);
    }
}

class Program
{
    static void Main()
    {
        var wordDoc = new WordDocument
        {
            Name = "Notes",
            Author = "John Ivanovich",
            Keywords = new string[] { "Dostoevsky", "Solovъev", "Shestov" },
            Theme = "The main problem of the world's evils living in our minds",
            FilePath = "C:\\SampleDoc.docx"
        };

        var documentMenu = DocumentInfoMenu.Instance;
        documentMenu.ShowMenu(wordDoc);
    }
}
