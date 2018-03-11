using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogWatcher.Models
{
    [Table("LogItems")]
    public class LogItem
    {
        public Guid Id { get; set; }

        [StringLength(255)]
        public string Header { get; set; }
        public string Description { get; set; }

    }
}