// See https://aka.ms/new-console-template for more information


using SmartRoom.CSVConsole.Logic;

string path = @"C:\Daten\Git\teaching.ss22.prse.digitaltwin.team1\Backend\SmartRoom\SmartRoom.CSVConsole\Data";

Console.Write("Import-File-Pfad angeben: ");
var input = Console.ReadLine();

if (!string.IsNullOrEmpty(input)) path = input;
var dirs = Directory.GetFiles(path);
Console.WriteLine("Folgende Files wurden im angegebenen Verzeichnis gefunden ");
Console.WriteLine("--------------------- ");
int i = 0;
foreach (var dir in dirs)
{
    i++;
    Console.WriteLine($"{i}: {dir}");
}
Console.WriteLine("--------------------- ");


var choose = "";
do
{
    Console.WriteLine($"Was moechten Sie machen?");

    Console.WriteLine($"i fuer Import");
    Console.WriteLine($"e fuer Export");
    Console.WriteLine($"stop fuer beenden");
    choose = Console.ReadLine();

    switch (choose)
    {
        case "i":
            var import = new ImportManager(path);
            import.ImportCSV();

            Console.WriteLine($"Import beendet ");
            break;

        case "e":
            var export = new ExportManager(path);
            export.ExportCSV();

            Console.WriteLine($"Export beendet ");
            break;

        default:
            Console.WriteLine($"keine gueltige Auswahl ");
            break;
    }

} while (choose !="stop");


