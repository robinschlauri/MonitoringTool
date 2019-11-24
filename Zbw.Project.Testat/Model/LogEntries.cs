using System;

namespace Zbw.Project.Testat
{
    public class LogEntries
    {
        public int Id { get; set; }
        public string Pod { get; set; }
        public string Location { get; set; }
        public string Hostname { get; set; }
        public int Severity { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
        public LogEntries()
        {
        }
    }
}
