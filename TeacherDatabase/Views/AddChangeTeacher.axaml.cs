using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TeacherDatabase.ViewModels;

namespace TeacherDatabase;

public partial class AddChangeTeacher : UserControl
{
    public AddChangeTeacher()
    {
        InitializeComponent();
        DataContext = new AddChangeTeacherVM();
    }
    public AddChangeTeacher(int idTeacher)
    {
        InitializeComponent();
        DataContext = new AddChangeTeacherVM(idTeacher);
    }
}