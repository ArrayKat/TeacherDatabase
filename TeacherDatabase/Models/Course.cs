using System;
using System.Collections.Generic;

namespace TeacherDatabase.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public int Hours { get; set; }

    public virtual ICollection<TeachersCourse> TeachersCourses { get; set; } = new List<TeachersCourse>();
}
