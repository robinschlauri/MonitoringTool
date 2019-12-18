using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DuplicateCheckerLib;
using Zbw.Project.Testat.Repository;
using Zbw.Project.Testat.Service;
using Zbw.Project.Testat.View;

namespace Zbw.Project.Testat.ViewModel
{
    public class LogEntryViewModel : BindableBase
    {
        static LogEntriesRepository logEntriesRepository = new LogEntriesRepository();

        // private DbConnection _dbConnection;
        private List<LogEntry> _logEntries;
        // private string _connectionstring = "Server=localhost;Port=3306;Database=inventarisierung;Uid=root;Pwd=....;";
        private string _getLogDataCommand = "SELECT id, pod, location, hostname, severity, timestamp, message FROM v_logentries";
        private string _confirmLogEntry = "CALL `inventarisierung`.`LogClear`({0})";
        //private String _addLogEntry = "CALL `inventarisierung`.`LogMessageAdd`('{0}', '{1}', {2}, '{3}')";

        public LogEntryViewModel(LogEntryView logEntryView)
        {
            CmdLoad = new DelegateCommand(OnCmdLoad);
            CmdConfirm = new DelegateCommand(OnCmdConfirmLogEntry);
            CmdAdd = new DelegateCommand(OnCmdAdd);
            CmdFindDuplicates = new DelegateCommand(OnCmdFindDuplicates);
            // _dbConnection = new DbConnection();
        }

        public DelegateCommand CmdLoad { get; }
        public DelegateCommand CmdConfirm { get; }
        public DelegateCommand CmdAdd { get; }
        public DelegateCommand CmdFindDuplicates { get; }


        private void OnCmdLoad()
        {
            GetLogEntries = logEntriesRepository.GetAll();
        }

        private void OnCmdConfirmLogEntry()
        {
            try
            {
                _dbConnection.ConfirmLogEntry(_connectionstring, string.Format(_confirmLogEntry, SelectedLogEntry.Id));
                OnCmdLoad();
            }
            catch (Exception)
            {
                MessageBox.Show("Wählen Sie einen Eintrag aus", "Kein Eintrag ausgewählt",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void OnCmdAdd()
        {
            try
            {
                logEntriesRepository.Add(new LogEntry(Pod, Hostname, Severity, Message));
//                _dbConnection.AddLogEntry(_connectionstring, string.Format(_addLogEntry, Pod, Hostname, Severity, Message));
                OnCmdLoad();
            }
            catch (Exception)
            {
                MessageBox.Show("Wählen Sie einen Eintrag aus", "Kein Eintrag ausgewählt",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnCmdFindDuplicates()
        {
            try
            {
                var dupliChecker = new DuplicateChecker();
                var dupliList = dupliChecker.FindDuplicates(GetLogEntries).ToList();
                MessageBox.Show("Gefundene Duplikate: " + dupliList.Count.ToString(), "Duplikate");
            }
            catch (Exception)
            {
                MessageBox.Show("Es sind keine Daten vorhanden", "Keine Daten vorhanden, zersch Date lade du tubel",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public LogEntry SelectedLogEntry { get; set; }
        public string Pod { get; set; }
        public string Hostname { get; set; }
        public string Severity { get; set; }
        public string Message { get; set; }

/*
        public string ConnectionString
        {
            get { return _connectionstring; }
            set { SetProperty(ref _connectionstring, value); }
        }
*/

        public List<LogEntry> GetLogEntries
        {
            get { return _logEntries; }
            set { SetProperty(ref _logEntries, value); }
        }
    }
}
