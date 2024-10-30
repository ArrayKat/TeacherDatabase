using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TeacherDatabase.ViewModels;

namespace TeacherDatabase;

public partial class ShowTeacher : UserControl
{
    public ShowTeacher()
    {
        InitializeComponent();
        DataContext = new ShowTeacherVM();
    }
}