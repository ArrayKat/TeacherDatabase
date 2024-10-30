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
        

    }
}
