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
        protected LogEntries(
            int id,
            string pod,
            string location,
            string hostname,
            int severity,
            DateTime datetime,
            string message)
        {
            this.Id = id;
            this.Pod = pod;
            this.Location = location;
            this.Hostname = hostname;
            this.Severity = severity;
            this.DateTime = datetime;
            this.Message = message;
        }
    }
}
