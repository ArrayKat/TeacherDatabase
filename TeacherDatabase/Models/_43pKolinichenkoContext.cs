using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TeacherDatabase.Models;

public partial class _43pKolinichenkoContext : DbContext
{
    public _43pKolinichenkoContext()
    {
    }

    public _43pKolinichenkoContext(DbContextOptions<_43pKolinichenkoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<TeachersCourse> TeachersCourses { get; set; }

    public virtual DbSet<TeachersSubject> TeachersSubjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=edu.pg.ngknn.ru;Port=5442;Database=43P_Kolinichenko;Username=43P;Password=444444");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("courses_pkey");

            entity.ToTable("courses");

            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.CourseName).HasColumnName("course_name");
            entity.Property(e => e.Hours).HasColumnName("hours");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.GenderId).HasName("gender_pkey");

            entity.ToTable("gender");

            entity.Property(e => e.GenderId).HasColumnName("gender_id");
            entity.Property(e => e.Gender1)
                .HasMaxLength(50)
                .HasColumnName("gender");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("subjects_pkey");

            entity.ToTable("subjects");

            entity.Property(e => e.SubjectId).HasColumnName("subject_id");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(100)
                .HasColumnName("subject_name");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.TeacherId).HasName("teachers_pkey");

            entity.ToTable("teachers");

            entity.Property(e => e.TeacherId).HasColumnName("teacher_id");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Experience).HasColumnName("experience");
            entity.Property(e => e.GenderId).HasColumnName("gender_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(100)
                .HasColumnName("patronymic");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.Surname)
                .HasMaxLength(100)
                .HasColumnName("surname");

            entity.HasOne(d => d.Gender).WithMany(p => p.Teachers)
                .HasForeignKey(d => d.GenderId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("teachers_gender_id_fkey");
        });

        modelBuilder.Entity<TeachersCourse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("teachers_courses_pkey");

            entity.ToTable("teachers_courses");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.TeacherId).HasColumnName("teacher_id");

            entity.HasOne(d => d.Course).WithMany(p => p.TeachersCourses)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("teachers_courses_course_id_fkey");

            entity.HasOne(d => d.Teacher).WithMany(p => p.TeachersCourses)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("teachers_courses_teacher_id_fkey");
        });

        modelBuilder.Entity<TeachersSubject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("teachers_subjects_pkey");

            entity.ToTable("teachers_subjects");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SubjectId).HasColumnName("subject_id");
            entity.Property(e => e.TeacherId).HasColumnName("teacher_id");

            entity.HasOne(d => d.Subject).WithMany(p => p.TeachersSubjects)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("teachers_subjects_subject_id_fkey");

            entity.HasOne(d => d.Teacher).WithMany(p => p.TeachersSubjects)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("teachers_subjects_teacher_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
