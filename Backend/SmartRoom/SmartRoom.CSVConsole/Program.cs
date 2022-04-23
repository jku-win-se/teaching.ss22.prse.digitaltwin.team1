// See https://aka.ms/new-console-template for more information


using SmartRoom.CSVConsole.Logic;

string path = @"C:\Daten\Git\teaching.ss22.prse.digitaltwin.team1\Backend\SmartRoom\SmartRoom.CSVConsole\Data";

Console.WriteLine("Hello, World!");

Console.Write("Import-File-Pfad angeben: ");
var input = Console.ReadLine();

if (!string.IsNullOrEmpty(input)) path = input;
var dirs = Directory.GetFiles(path);
int i = 0;
foreach (var dir in dirs)
{
    i++;
    Console.WriteLine($"{i}: {dir}");
}

Console.Write($"[J]a wenn der Input gstartet werden soll: ");
input = Console.ReadLine();

if (input.Equals("j", StringComparison.CurrentCultureIgnoreCase))
{
    var import = new ImportManager(path);
    import.ImportCSV();

    //await SmartRoom.CommonBase.Utils.WebApiTrans.PostAPI("", window.First(), "");
    Console.Write($"Import beendet ");
}

//Export
Console.Write($"[e] wenn der Export gstartet werden soll: ");
var exp = Console.ReadLine();
exp = Console.ReadLine();


if (exp.Equals("e", StringComparison.CurrentCultureIgnoreCase))
{
    var export = new ExportManager();
    export.ExportCSV();
    
}
Console.Write($"Export beendet ");