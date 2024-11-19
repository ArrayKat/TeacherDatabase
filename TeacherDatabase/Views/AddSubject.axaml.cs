using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TeacherDatabase.ViewModels;

namespace TeacherDatabase;

public partial class AddSubject : UserControl
{
    public AddSubject()
    {
        InitializeComponent();
        DataContext = new AddSubjectVM();
    }
}