using System;
using System.Collections.Generic;

namespace TeacherDatabase.Models;

public partial class Teacher
{
    public int TeacherId { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Patronymic { get; set; }

    public int? GenderId { get; set; }

    public DateTime BirthDate { get; set; }

    public int? Experience { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public virtual Gender? Gender { get; set; }

    public virtual ICollection<TeachersCourse> TeachersCourses { get; set; } = new List<TeachersCourse>();

    public virtual ICollection<TeachersSubject> TeachersSubjects { get; set; } = new List<TeachersSubject>();
}
