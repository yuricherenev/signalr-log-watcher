using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using LogWatcher.Models;

namespace LogWatcher.Core
{
    public static class LogReader
    {
        public static LogFile GetFileFromPath(string fileName, long position = 0)
        {
            var logFile = new LogFile(fileName);

            var list = Read(fileName, ref position);

            logFile.LastPosition = position;
            logFile.Logs = list;
            Console.WriteLine("File position: {0}", logFile.LastPosition);
            return logFile;
        }

        private static List<LogItem> Read(string fileName, ref long position)
        {
            var list = new List<LogItem>();
            FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            fileStream.Position = position;
            using(StreamReader sr = new StreamReader(fileStream))
            {
                string s = String.Empty;
                while ((s = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(s)) continue;

                    var splittedLine = Regex.Split(s, @"\DEBUG");
                    list.Add(new LogItem()
                    {
                        Header = splittedLine[0],
                            Description = splittedLine[1]
                    });
                }
                position = fileStream.Position;
            }

            return list;
        }
    }
}