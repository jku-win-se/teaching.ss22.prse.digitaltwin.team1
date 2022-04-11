// See https://aka.ms/new-console-template for more information


using SmartRoom.CommonBase.Utils;
using SmartRoom.CSVConsole.Data;
using SmartRoom.CSVConsole.Models;



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


using (GenericCSVReader<WindowOpen> reader = new GenericCSVReader<WindowOpen>(@"C:\Daten\Git\teaching.ss22.prse.digitaltwin.team1\Backend\SmartRoom\SmartRoom.CSVConsole\Data\WindowOpen.csv"))
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

using (GenericCSVReader<Ventilator> reader = new GenericCSVReader<Ventilator>(@"C:\Daten\Git\teaching.ss22.prse.digitaltwin.team1\Backend\SmartVentilator\SmartVentilator.CSVConsole\Data\Ventilator.csv"))
{
    ventilator = reader.Read();
}

using (GenericCSVReader<VentilatorOn> reader = new GenericCSVReader<VentilatorOn>(@"C:\Daten\Git\teaching.ss22.prse.digitaltwin.team1\Backend\SmartVentilatorOn\SmartVentilatorOn.CSVConsole\Data\VentilatorOn.csv"))
{
    ventilatorOn = reader.Read();
}

using (GenericCSVReader<Window> reader = new GenericCSVReader<Window>(@"C:\Daten\Git\teaching.ss22.prse.digitaltwin.team1\Backend\SmartWindow\SmartWindow.CSVConsole\Data\Window.csv"))
{
    window = reader.Read();
}
//ToDo Create Props and readers for all csv