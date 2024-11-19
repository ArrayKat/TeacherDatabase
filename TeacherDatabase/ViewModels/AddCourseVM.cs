using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherDatabase.Models;

namespace TeacherDatabase.ViewModels
{
    public class AddCourseVM:ViewModelBase
    {
        int _hourCourse;
        string _nameCourse;
        Course? newCourse;
        public int HourCourse { get => _hourCourse; set => this.RaiseAndSetIfChanged(ref _hourCourse, value); }
        public string NameCourse { get => _nameCourse; set => this.RaiseAndSetIfChanged(ref _nameCourse, value); }
        public Course? NewCourse { get => newCourse; set => this.RaiseAndSetIfChanged(ref newCourse, value); }


        public async void AddCourse() {
            ButtonResult result = await MessageBoxManager.GetMessageBoxStandard("Сообщение", "Хотите добавить курс у преподавателя?", ButtonEnum.YesNo).ShowAsync();
            if (result == ButtonResult.Yes)
            {
                NewCourse = new Course
                {
                    CourseName = NameCourse,
                    Hours = HourCourse,
                };
                MainWindowViewModel.myConnection.Courses.Add(NewCourse);
                MainWindowViewModel.myConnection.SaveChanges();
                MainWindowViewModel.Instance.PageContent = new AddChangeTeacher();
                await MessageBoxManager.GetMessageBoxStandard("Сообщение", "Курс успешно добавлен", ButtonEnum.Ok).ShowAsync();
            }
            else MainWindowViewModel.Instance.PageContent = new AddChangeTeacher();

        }

        public void ToBack() { 
            MainWindowViewModel.Instance.PageContent = new AddChangeTeacher();
        }

    }
}
