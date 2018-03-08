using System;
using System.ComponentModel.DataAnnotations;

namespace LogWatcher.Models
{
    public class LogItem
    {
        public Guid Id { get; set; }

        [StringLength(255)]
        public string Header { get; set; }
        public string Description { get; set; }

    }
}