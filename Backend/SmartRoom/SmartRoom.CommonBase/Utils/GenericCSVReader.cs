using Microsoft.Win32.SafeHandles;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SmartRoom.CommonBase.Utils
{
    public class GenericCSVReader<T> : IDisposable where T : new()
    {
        List<T> _data = new List<T>();
        private string _fileName;
        private char _sep;

        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        public GenericCSVReader(string fileName, char sep = ';')
        {
            if (!fileName.Contains(".csv")) throw new FormatException("No CSV File!");
            else _fileName = fileName;
            _sep = sep;
        }

        public IEnumerable<T> Read()
        {
            IEnumerable<string> lines = File.ReadLines(_fileName);
            string[] headers = lines.First().Split(_sep);
            NumberFormatInfo provider = new NumberFormatInfo();
            Console.WriteLine($"Reading {lines.Count()} lines from {typeof(T).Name}");
            foreach (var line in lines.Skip(1))
            {
                var t = new T();
                foreach (var header in headers)
                {
                    int i = Array.IndexOf(headers, header);
                    PropertyInfo? prop = typeof(T).GetProperty(header);
                    Type? type = prop?.PropertyType;

                    if (line.Split(_sep).Length > i)
                    {
                        var val = line.Split(_sep)[i];

                        if (typeof(int).Equals(type))
                        {
                            prop?.SetValue(t, Convert.ToInt32(val));
                        }
                        else if (typeof(DateTime).Equals(type))
                        {
                            prop?.SetValue(t, Convert.ToDateTime(val));
                        }
                        else if (typeof(bool).Equals(type))
                        {
                            prop?.SetValue(t, Convert.ToBoolean(val));
                        }
                        else if (typeof(double).Equals(type))
                        {
                            if (val.Contains(".")) provider.NumberDecimalSeparator = ".";
                            else provider.NumberDecimalSeparator = ",";

                            prop?.SetValue(t, Convert.ToDouble(val, provider));
                        }
                        else prop?.SetValue(t, line.Split(_sep)[i]);
                    }
                }
                _data.Add(t);

            }

            return _data;
        }

        #region IDisposable Support
        private bool _disposedValue = false; 

        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _safeHandle.Dispose();
                }

                _data = new List<T>();
                _disposedValue = true;
            }
        }
        #endregion
    }
}
