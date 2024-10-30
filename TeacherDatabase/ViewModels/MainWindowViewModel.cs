using Avalonia.Controls;
using ReactiveUI;
using TeacherDatabase.Models;

namespace TeacherDatabase.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public static MainWindowViewModel Instance;
        public MainWindowViewModel() { Instance = this; }

        UserControl _pageContent = new ShowTeacher();

        public UserControl PageContent { get => _pageContent; set => this.RaiseAndSetIfChanged( ref _pageContent, value); }
        
        public static _43pKolinichenkoContext myConnection = new _43pKolinichenkoContext();
    
    }
}