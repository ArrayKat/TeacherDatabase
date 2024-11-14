using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TeacherDatabase.ViewModels;

namespace TeacherDatabase;

public partial class AddCourse : UserControl
{
    public AddCourse()
    {
        InitializeComponent();
        DataContext = new AddCourseVM();
    }
}