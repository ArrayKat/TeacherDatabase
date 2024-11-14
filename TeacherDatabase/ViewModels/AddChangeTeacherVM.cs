using Microsoft.EntityFrameworkCore;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherDatabase.Models;

namespace TeacherDatabase.ViewModels
{
    public class AddChangeTeacherVM : ViewModelBase
    {   // Поле и свойство учителя, которого мы добавляем/изменяем
        Teacher? _newTeacher;
        public Teacher? NewTeacher { get => _newTeacher; set => this.RaiseAndSetIfChanged(ref _newTeacher, value); }

        public List<Gender> Gender => MainWindowViewModel.myConnection.Genders.ToList();

        List<TeachersCourse>? _teachersCourses;
        List<TeachersSubject>? _teachersSubjects;

        public List<TeachersCourse>? TeachersCourses { get => _teachersCourses; set => this.RaiseAndSetIfChanged(ref _teachersCourses, value); }
        public List<TeachersSubject>? TeachersSubjects { get => _teachersSubjects; set => this.RaiseAndSetIfChanged(ref _teachersSubjects, value); }

        public List<Course> Courses => MainWindowViewModel.myConnection.Courses.ToList().Except(_newTeacher.TeachersCourses.Select(x => x.Course)).ToList();
        public List<Subject> Subjects => MainWindowViewModel.myConnection.Subjects.ToList().Except(_newTeacher.TeachersSubjects.Select(x => x.Subject)).ToList();

        Course? selectedCourse;
        public Course? SelectedCourse { 
            get => null; 
            set 
            {
                if (value != null) {
                    NewTeacher.TeachersCourses.Add(new TeachersCourse { Course = value, Teacher = NewTeacher });
                    TeachersCourses = NewTeacher.TeachersCourses.ToList();
                    this.RaisePropertyChanged(nameof(Courses));
                }
            } 
        }
        public async void DeleteCourse(TeachersCourse deleteCourse)
        {
            ButtonResult result = await MessageBoxManager.GetMessageBoxStandard("Сообщение","Хотите удалить курс у преподавателя?", ButtonEnum.YesNo).ShowAsync();
            if (result == ButtonResult.Yes) {
                NewTeacher.TeachersCourses.Remove(deleteCourse);
                TeachersCourses = NewTeacher.TeachersCourses.ToList();
                await MessageBoxManager.GetMessageBoxStandard("Сообщение", "Курс успешно удален", ButtonEnum.Ok).ShowAsync();
            }
            this.RaisePropertyChanged(nameof(Courses));
        }
        Subject? selectedSubject;
        public Subject? SelectedSubject
        {
            get => null;
            set
            {
                if (value != null)
                {
                    NewTeacher.TeachersSubjects.Add(new TeachersSubject { Subject = value, Teacher = NewTeacher });
                    TeachersSubjects = NewTeacher.TeachersSubjects.ToList();
                    this.RaisePropertyChanged(nameof(Subjects));
                }
            }
        }
        
        public async void DeleteSubject(TeachersSubject deleteSubject)
        {
            ButtonResult result = await MessageBoxManager.GetMessageBoxStandard("Сообщение", "Хотите удалить предмет у преподавателя?", ButtonEnum.YesNo).ShowAsync();
            if (result == ButtonResult.Yes)
            {
                NewTeacher.TeachersSubjects.Remove(deleteSubject);
                TeachersSubjects = NewTeacher.TeachersSubjects.ToList();
                await MessageBoxManager.GetMessageBoxStandard("Сообщение", "Предмет успешно удален").ShowAsync();
            }
            this.RaisePropertyChanged(nameof(Subjects));
        }

        string _nameButton;
        public string NameButton { get => _nameButton; set => this.RaiseAndSetIfChanged(ref _nameButton, value); }
        

        public AddChangeTeacherVM() 
        {
            _newTeacher = new Teacher() { 
                Gender = new Gender()
            };
            NameButton = "Добавить";

        }
        public AddChangeTeacherVM(int idTeacher)
        {
            _newTeacher = MainWindowViewModel.myConnection.Teachers
            .Include(x => x.Gender)
            .Include(x => x.TeachersCourses).ThenInclude(x => x.Course)
            .Include(x => x.TeachersSubjects).ThenInclude(x => x.Subject)
            .FirstOrDefault(x => x.TeacherId == idTeacher);
            TeachersCourses = NewTeacher.TeachersCourses.ToList();
            TeachersSubjects = NewTeacher.TeachersSubjects.ToList();
            NameButton = "Сохранить изменения";
        }

        public async void AddTeacher() {
            if (NewTeacher.TeacherId == 0)
            {
                ButtonResult result = await MessageBoxManager.GetMessageBoxStandard("Сообщение", "Хотите добавить пользователя?", ButtonEnum.YesNo).ShowAsync();
                if (result == ButtonResult.Yes)
                {
                    MainWindowViewModel.myConnection.Teachers.Add(NewTeacher);
                    MainWindowViewModel.myConnection.SaveChanges();
                    MainWindowViewModel.Instance.PageContent = new ShowTeacher();
                }
                else {
                    MainWindowViewModel.Instance.PageContent = new ShowTeacher();
                }
            }
            else
            {
                ButtonResult result = await MessageBoxManager.GetMessageBoxStandard("Сообщение", "Хотите сохранить изменения?", ButtonEnum.YesNo).ShowAsync();
                if (result == ButtonResult.Yes)
                {
                    MainWindowViewModel.myConnection.SaveChanges();
                    MainWindowViewModel.Instance.PageContent = new ShowTeacher();
                }
                else {
                    MainWindowViewModel.Instance.PageContent = new ShowTeacher();
                }
            }
            
        }
        public void GoToPageAddcourse() {
            MainWindowViewModel.Instance.PageContent =new AddCourse();
        }

    }
}
