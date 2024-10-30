using System;
using System.Collections.Generic;

namespace TeacherDatabase.Models;

public partial class Gender
{
    public int GenderId { get; set; }

    public string Gender1 { get; set; } = null!;

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
