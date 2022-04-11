// See https://aka.ms/new-console-template for more information


using SmartRoom.CommonBase.Utils;
using SmartRoom.CSVConsole.Data;
using SmartRoom.CSVConsole.Models;

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


using (GenericCSVReader<WindowOpen> reader = new GenericCSVReader<WindowOpen>(@$"{path}\WindowOpen.csv"))
{ 
   windowStates = reader.Read();
}

using (GenericCSVReader<Door> reader = new GenericCSVReader<Door>(@"C:\Daten\Git\teaching.ss22.prse.digitaltwin.team1\Backend\SmartRoom\SmartRoom.CSVConsole\Data\Door.csv"))
{
    doorID = reader.Read();
}

using (GenericCSVReader<DoorConnectsRoom> reader = new GenericCSVReader<DoorConnectsRoom>(@"C:\Daten\Git\teaching.ss22.prse.digitaltwin.team1\Backend\SmartRoom\SmartRoom.CSVConsole\Data\Door_Connects_Room.csv"))
{
    doorConnectsRoom = reader.Read();
}

using (GenericCSVReader<DoorOpen> reader = new GenericCSVReader<DoorOpen>(@"C:\Daten\Git\teaching.ss22.prse.digitaltwin.team1\Backend\SmartRoom\SmartRoom.CSVConsole\Data\DoorOpen.csv"))
{
    doorOpen = reader.Read();
}

using (GenericCSVReader<PeopleInRoom> reader = new GenericCSVReader<PeopleInRoom>(@"C:\Daten\Git\teaching.ss22.prse.digitaltwin.team1\Backend\SmartRoom\SmartRoom.CSVConsole\Data\PeopleInRoom.csv"))
{
    peopleInRoom = reader.Read();
}

using (GenericCSVReader<Room> reader = new GenericCSVReader<Room>(@"C:\Daten\Git\teaching.ss22.prse.digitaltwin.team1\Backend\SmartRoom\SmartRoom.CSVConsole\Data\Room.csv"))
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
