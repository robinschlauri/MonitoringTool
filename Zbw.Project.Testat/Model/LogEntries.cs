using System;
using DuplicateCheckerLib;

namespace Zbw.Project.Testat
{
    public class LogEntries : IEntity
    {
        public int Id { get; set; }
        public string Pod { get; set; }
        public string Location { get; set; }
        public string Hostname { get; set; }
        public int Severity { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                const int HashingBase = (int)2166136261;
                const int HashingMultiplier = 16777619;
                int hash = HashingBase;
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Severity) ? Severity.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, this.Message) ? Message.GetHashCode() : 0);
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            LogEntries log = obj as LogEntries;
            if (ReferenceEquals(null, log)) return false;
            return int.Equals(Severity, log.Severity) && String.Equals(Message, log.Message);
        }
    }
}
