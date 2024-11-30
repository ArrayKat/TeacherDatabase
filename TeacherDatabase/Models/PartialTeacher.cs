using Metsys.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeacherDatabase.Models
{
    public partial class Teacher
    {
        //свойство для получения итогового количества курсов для преподавателей:
        public int TotalHours => TeachersCourses?.Sum(x => x.Course.Hours) ?? 0;
        //свойство для удобного вывода ФИО
        public string FIO => Surname + " " + Name + " " + Patronymic;
        public string ExperienceTeacher
        {
            get {
                string exp = "";
                int exper;
                if (Experience != null)
                {
                    exper = Convert.ToInt32(Experience);
                    int year = exper / 12;
                    int month = exper % 12;
                    if (year == 1) exp += year + " год ";
                    else if (year == 0) exp += "";
                    else if (year >= 2 && year <= 4) exp += year + " года ";
                    else exp += year + " лет ";

                    if (month == 1) exp += month + " месяц";
                    else if (month == 0) exp += "";
                    else if (month >= 2 && month <= 4) exp += month + " месяца";
                    else exp += month + " месяцев";
                } 
                else exp = "нет стажа";
                return exp;
            }
        }


    }
}
