// See https://aka.ms/new-console-template for more information


using SmartRoom.CommonBase.Utils;
using SmartRoom.CSVConsole.Models;
using System.Reflection;

string path = @"C:\Daten\Git\teaching.ss22.prse.digitaltwin.team1\Backend\SmartRoom\SmartRoom.CSVConsole\Data";

Console.WriteLine("Hello, World!");
IEnumerable<WindowOpen> windowStates;
IEnumerable<Door> doorID;
IEnumerable<DoorConnectsRoom> doorConnectsRoom;
IEnumerable<DoorOpen> doorOpen;
IEnumerable<PeopleInRoom> peopleInRoom;
IEnumerable<Room> roomCap;
IEnumerable<Ventilator> ventilator;
IEnumerable<VentilatorOn> ventilatorOn;
IEnumerable<Window> window;

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
    
    using (GenericCSVReader<WindowOpen> reader = new GenericCSVReader<WindowOpen>(@$"{path}\WindowOpen.csv"))
    {
        windowStates = reader.Read();
    }

    using (GenericCSVReader<Door> reader = new GenericCSVReader<Door>(@$"{path}\Door.csv"))
    {
        doorID = reader.Read();
    }

    using (GenericCSVReader<DoorConnectsRoom> reader = new GenericCSVReader<DoorConnectsRoom>(@$"{path}\Door_Connects_Room.csv"))
    {
        doorConnectsRoom = reader.Read();
    }

    using (GenericCSVReader<DoorOpen> reader = new GenericCSVReader<DoorOpen>(@$"{path}\DoorOpen.csv"))
    {
        doorOpen = reader.Read();
    }

    using (GenericCSVReader<PeopleInRoom> reader = new GenericCSVReader<PeopleInRoom>(@$"{path}\PeopleInRoom.csv"))
    {
        peopleInRoom = reader.Read();
    }

    using (GenericCSVReader<Room> reader = new GenericCSVReader<Room>(@$"{path}\Room.csv"))
    {
        roomCap = reader.Read();
    }

    using (GenericCSVReader<Ventilator> reader = new GenericCSVReader<Ventilator>(@$"{path}\Ventilator.csv"))
    {
        ventilator = reader.Read();
    }

    using (GenericCSVReader<VentilatorOn> reader = new GenericCSVReader<VentilatorOn>(@$"{path}\VentilatorOn.csv"))
    {
        ventilatorOn = reader.Read();
    }

    using (GenericCSVReader<Window> reader = new GenericCSVReader<Window>(@$"{path}\Window.csv"))
    {
        window = reader.Read();
    }
    await SmartRoom.CommonBase.Utils.WebApiTrans.PostAPI("", window.First(), "");


 Console.Write($"Import beendet ");

}


