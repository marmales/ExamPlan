using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Domain.Entities;
using Domain.Abstract;
namespace Domain.Concrete
{
    public partial class EFDbContext : DbContext
    {
        public EFDbContext()
            : base("name=UniversityModel")
        {
        }

        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Day> Day { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Lecturel> Lecturels { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentsList> StudentsLists { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Day>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Day>()
                .HasMany(e => e.Classes)
                .WithRequired(e => e.DayOfWeek)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lecturel>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Lecturel>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Lecturel>()
                .HasMany(e => e.Classes)
                .WithRequired(e => e.Lecturel)
                .HasForeignKey(e => e.LecturelID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lecturel>()
                .HasMany(e => e.Subjects)
                .WithOptional(e => e.Lecturel)
                .HasForeignKey(e => e.LecturelID);

            modelBuilder.Entity<Student>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Subject>()
                .Property(e => e.SubjectName)
                .IsUnicode(false);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.Classes)
                .WithRequired(e => e.Subject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.Exams)
                .WithRequired(e => e.Subject)
                .WillCascadeOnDelete(false);
        }
    }
}
