using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherDatabase.Models;

namespace TeacherDatabase.ViewModels
{
    internal class ShowTeacherVM
    {
        public List <Teacher> TeacherList => MainWindowViewModel.myConnection.Teachers
            .Include(x=>x.Gender)
            .Include(x=>x.TeachersCourses).ThenInclude(x=>x.Course)
            .Include(x=>x.TeachersSubjects).ThenInclude(x=>x.Subject)
            .ToList();

        public void ToPageAddTeacher() {
            MainWindowViewModel.Instance.PageContent = new AddChangeTeacher();
        }
        public void UpdateTeacher(int idTeacher) {
            MainWindowViewModel.Instance.PageContent = new AddChangeTeacher(idTeacher);
        }
        public void DeleteTeacher(int idTeacher)
        {
            Teacher deleteTeacher = MainWindowViewModel.myConnection.Teachers.FirstOrDefault(x => x.TeacherId == idTeacher);
            MainWindowViewModel.myConnection.Teachers.Remove(deleteTeacher);
            MainWindowViewModel.myConnection.SaveChanges();
            MainWindowViewModel.Instance.PageContent = new ShowTeacher();
        }
    }
}
