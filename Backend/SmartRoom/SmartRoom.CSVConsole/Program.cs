// See https://aka.ms/new-console-template for more information


using SmartRoom.CommonBase.Utils;
using SmartRoom.CSVConsole.Models;



Console.WriteLine("Hello, World!");
IEnumerable<WindowOpen> data;



using (GenericCSVReader<WindowOpen> reader = new GenericCSVReader<WindowOpen>(@"C:\Daten\Git\teaching.ss22.prse.digitaltwin.team1\Backend\SmartRoom\SmartRoom.CSVConsole\Data\WindowOpen.csv"))
{ 
   data = reader.Read();
}