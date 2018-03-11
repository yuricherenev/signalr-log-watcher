using System;
using System.Collections.Generic;

namespace LogWatcher.Models
{
    public class LogFile
    {
        public Guid Id { get; set; }
        public List<LogItem> Logs { get; set; }
        public string FileName { get; set; }
        public long LastPosition { get; set; }

        public LogFile(string fileName)
        {
            this.FileName = fileName;
        }
    }
}