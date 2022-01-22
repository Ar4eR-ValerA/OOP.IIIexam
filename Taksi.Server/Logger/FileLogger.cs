using System;
using System.IO;

namespace Taksi.Server.Logger
{
    public class FileLogger : ILogger
    {
        private readonly FileInfo _fileInfo;
        private readonly bool _timePrefix;

        public FileLogger(string path, bool timePrefix = false)
        {
            _fileInfo = new FileInfo(path);
            if (!_fileInfo.Exists)
                _fileInfo.Create();
            _timePrefix = timePrefix;
        }

        public void LogInfo(string info)
        {
            using var sw = new StreamWriter(_fileInfo.FullName, true);
            if (_timePrefix)
            {
                sw.Write(DateTime.Now + " ");
            }

            sw.WriteLine(info);
        }
    }
}