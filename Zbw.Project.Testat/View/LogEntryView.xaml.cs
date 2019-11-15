using System.Windows.Controls;
using Zbw.Project.Testat.ViewModel;

namespace Zbw.Project.Testat.View
{
    /// <summary>
    /// Interaktionslogik für UserControl1.xaml
    /// </summary>
    public partial class LogEntryView : UserControl
    {
        public LogEntryView()
        {
            InitializeComponent();
            DataContext = new LogEntryViewModel(this);
        }
    }
}
