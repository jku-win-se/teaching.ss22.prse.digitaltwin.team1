using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace SmartRoom.CommonBase.Utils
{
    public class GenericCSVWriter<T> : IDisposable
    {
        private IEnumerable<T> _data;
        private readonly SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);
        private string? _fileName;
        public string? FileName
        {
            get { return _fileName; }
            private set 
            {
                if (string.IsNullOrEmpty(value)) _fileName =$"{_data.First()?.GetType().Name}_{DateTime.Now.ToShortDateString()}.csv";
                else _fileName = value;
            }
        } 

        public GenericCSVWriter(IEnumerable<T> data, string fileName)
        {
            if(data is not null) _data = data;
            else throw new ArgumentNullException(nameof(data));

            FileName = fileName;
        }

        public string WriteToCSV()
        {
            string ret = $"{ Directory.GetCurrentDirectory() }\\{ _fileName}";
            File.WriteAllLines($"{Directory.GetCurrentDirectory()}\\{_fileName}", GetDataForCSV());
            return ret;
        }

        private string[] GetDataForCSV()
        {
            List<string> lines = new List<string>();
            string headLine = "";
            

            var header = _data.First()!.GetType().GetProperties().ToArray();


            foreach (var item in header)
            {
                headLine = headLine + $"{item.Name};";
            }

            lines.Add(headLine);

            foreach (var obj in _data)
            {
                string line = "";
                foreach (var item in header)
                {
                    line += $"{obj?.GetType().GetProperty(item.Name)?.GetValue(obj)};";
                }
                lines.Add(line);
            }
            return lines.ToArray();

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
