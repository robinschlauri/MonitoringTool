using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using Zbw.Project.Testat.Service;
using Zbw.Project.Testat.View;

namespace Zbw.Project.Testat.ViewModel
{
    public class LogEntryViewModel : BindableBase
    {
        private DbConnection _dbConnection;
        private List<LogEntries> _logEntries;
        private string _connectionstring = "Server=localhost;Port=3307;Database=inventarisierung;Uid=robin;Pwd=zbw123;";
        private string _getLogDataCommand = "SELECT id, pod, location, hostname, severity, timestamp, message FROM v_logentries";

        public LogEntryViewModel(LogEntryView logEntryView)
        {
            CmdLoad = new DelegateCommand(OnCmdLoad);
        }

        public DelegateCommand CmdLoad { get; }

        private void OnCmdLoad()
        {
            _dbConnection = new DbConnection();
            GetLogEntries = _dbConnection.GetLogData(_connectionstring, _getLogDataCommand);

        }


        public string ConnectionString
        {
            get { return _connectionstring; }
            set { SetProperty(ref _connectionstring, value); }
        }

        public List<LogEntries> GetLogEntries
        {
            get { return _logEntries; }
            set { SetProperty(ref _logEntries, value); }
        }

        public LogEntries SelectedLogEntries { get; set; }

    }
}
