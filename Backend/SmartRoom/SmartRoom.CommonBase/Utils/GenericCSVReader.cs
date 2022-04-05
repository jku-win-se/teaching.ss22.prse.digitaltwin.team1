using Microsoft.Win32.SafeHandles;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SmartRoom.CommonBase.Utils
{
    public class GenericCSVReader<T> : IDisposable where T : new()
    {
        List<T> _data = new List<T>();
        private string _fileName;

        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        public GenericCSVReader(string fileName)
        {
            if (!fileName.Contains(".csv")) throw new FormatException("No CSV File!");
            else _fileName = fileName;
        }

        public IEnumerable<T> Read()
        {
            IEnumerable<string> lines = File.ReadLines(_fileName);
            string[] headers = lines.First().Split(";");

            foreach (var line in lines.Skip(1))
            {
                var t = new T();
                foreach (var header in headers)
                {
                    int i = Array.IndexOf(headers, headers.Where(h => h.Equals(header)));
                    PropertyInfo? prop = typeof(T).GetProperty(header);
                    Type? type = prop?.PropertyType;

                    if (typeof(int).Equals(type))
                    {
                        prop?.SetValue(t, Convert.ToInt32(line.Split(";")[i]));
                    }
                    else if (typeof(DateTime).Equals(type))
                    {
                        prop?.SetValue(t, Convert.ToDateTime(line.Split(";")[i]));
                    }
                    else if (typeof(bool).Equals(type))
                    {
                        prop?.SetValue(t, Convert.ToBoolean(line.Split(";")[i]));
                    }
                    else prop?.SetValue(t, line.Split(";")[i]);
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
