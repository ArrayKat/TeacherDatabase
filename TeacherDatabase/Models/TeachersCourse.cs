using System;
using System.Collections.Generic;

namespace TeacherDatabase.Models;

public partial class TeachersCourse
{
    public int Id { get; set; }

    public int? TeacherId { get; set; }

    public int? CourseId { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Teacher? Teacher { get; set; }
}
