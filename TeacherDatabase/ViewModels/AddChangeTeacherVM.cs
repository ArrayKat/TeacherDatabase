using Microsoft.EntityFrameworkCore;
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
        

        List<TeachersCourse>? _teachersCourses;
        List<TeachersSubject>? _teachersSubjects;

        public List<TeachersCourse>? TeachersCourses { get => _teachersCourses; set => this.RaiseAndSetIfChanged(ref _teachersCourses, value); }
        public List<TeachersSubject>? TeachersSubjects { get => _teachersSubjects; set => this.RaiseAndSetIfChanged(ref _teachersSubjects, value); }

        public List<Course> Courses => MainWindowViewModel.myConnection.Courses.ToList();
        public List<Gender> Gender => MainWindowViewModel.myConnection.Genders.ToList();
        public List<Subject> Subjects => MainWindowViewModel.myConnection.Subjects.ToList();

        Course? selectedCourse;
        public Course? SelectedCourse { 
            get => null; 
            set 
            {
                if (value != null) { 
                
                }
            } 
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
            NameButton = "Сохранить";
        }

        public void AddTeacher() {
            if (NewTeacher.TeacherId == 0) {
                MainWindowViewModel.myConnection.Teachers.Add(NewTeacher);
            }
            MainWindowViewModel.myConnection.SaveChanges();
            MainWindowViewModel.Instance.PageContent = new ShowTeacher();
        }

    }
}
