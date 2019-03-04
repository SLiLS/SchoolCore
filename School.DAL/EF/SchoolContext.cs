using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using School.DAL.Entities;

namespace School.DAL.EF
{
  public  class SchoolContext : DbContext
    {
        public DbSet<SchoolClass> SchoolClasses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public SchoolContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=SchoolDB;Trusted_Connection=True;");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<ClassTeacher>()
                .HasKey(t => new { t.SchoolClassId, t.TeacherId });

            modelBuilder.Entity<ClassTeacher>()
                .HasOne(sc => sc.Teacher)
                .WithMany(s => s.ClassTeachers)
                .HasForeignKey(sc => sc.TeacherId);

            modelBuilder.Entity<ClassTeacher>()
                .HasOne(sc => sc.SchoolClass)
                .WithMany(c => c.ClassTeachers)
                .HasForeignKey(sc => sc.SchoolClassId);

            modelBuilder.Entity<Student>().HasData(
                 new Student[]
                 {
                    new Student{Id=1,Name="Игорь",MiddleName="Николаевич",Sex="мужской",SurName="Николаев",SchoolClassId=1 },
                    new Student{Id=2,Name="Евгений",MiddleName="Владимирович",Sex="мужской",SurName="Рожков",SchoolClassId=2 },
                    new Student{Id=3,Name="Анатолий",MiddleName="Алексеевич",Sex="мужской",SurName="Иванов",SchoolClassId=3 },
                    new Student{Id=4,Name="Евгения",MiddleName="Николаевна",Sex="женский",SurName="Рожкова",SchoolClassId=4 },
                    new Student{Id=5,Name="Анастасия",MiddleName="Алексеевна",Sex="женский",SurName="Иванова",SchoolClassId=5 },
                    new Student{Id=6,Name="Анна",MiddleName="Олеговна",Sex="женский",SurName="Николаева",SchoolClassId=6 },
                    new Student{Id=7,Name="Игорь",MiddleName="Николаевич",Sex="мужской",SurName="Николаев",SchoolClassId=2 },
                    new Student{Id=8,Name="Евгений",MiddleName="Владимирович",Sex="мужской",SurName="Рожков",SchoolClassId=2 },
                    new Student{Id=9,Name="Анатолий",MiddleName="Алексеевич",Sex="мужской",SurName="Иванов",SchoolClassId=4 },
                    new Student{Id=10,Name="Евгения",MiddleName="Николаевна",Sex="женский",SurName="Рожкова",SchoolClassId=4 },
                    new Student{Id=11,Name="Анастасия",MiddleName="Алексеевна",Sex="женский",SurName="Иванова",SchoolClassId=5 },
                    new Student{Id=12,Name="Анна",MiddleName="Олеговна",Sex="женский",SurName="Николаева",SchoolClassId=4}
                 });
            modelBuilder.Entity<SchoolClass>().HasData(
                new SchoolClass[]
                {
                    new SchoolClass{Id=1, Name="1А"},
                    new SchoolClass{ Id=2,Name="2Б"},
                    new SchoolClass{ Id=3,Name="3Ф"},
                    new SchoolClass{ Id=4,Name="4А"},
                    new SchoolClass{Id=5, Name="5А"},
                    new SchoolClass{ Id=6,Name="6А"}
                   
                });
            modelBuilder.Entity<ClassTeacher>().HasData(
                new ClassTeacher[]
                {
                    new ClassTeacher{SchoolClassId=1,TeacherId=1 },
                    new ClassTeacher{SchoolClassId=2,TeacherId=2 },
                    new ClassTeacher{SchoolClassId=3,TeacherId=3 },
                    new ClassTeacher{SchoolClassId=4,TeacherId=4 },
                    new ClassTeacher{SchoolClassId=5,TeacherId=5 },
                    new ClassTeacher{SchoolClassId=6,TeacherId=6 }
                    
              

                });
            modelBuilder.Entity<Teacher>().HasData(
                new Teacher[]
                {
                    new Teacher{Id=1,Name="Владислав",MiddleName="Андреевич",SurName="Дубойский",Position="директор" },
                    new Teacher{Id=2,Name="Эдуард",MiddleName="Олегович",SurName="Физикович",Position="учитель" },
                    new Teacher{Id=3,Name="Василий",MiddleName="Петрович",SurName="Математиков",Position="учитель" },
                    new Teacher{Id=4,Name="Валентина",MiddleName="Михайловна",SurName="Погромова",Position="учитель" },
                    new Teacher{Id=5,Name="Анна",MiddleName="Сергеевна",SurName="Дубойская",Position="завуч" },
                    new Teacher{Id=6,Name="Анна",MiddleName="Сергеевна",SurName="Дубойская",Position="завуч" },
                    new Teacher{Id=7,Name="Василий",MiddleName="Петрович",SurName="Математиков",Position="учитель" },
                    new Teacher{Id=8,Name="Валентина",MiddleName="Михайловна",SurName="Погромова",Position="учитель" },
                    new Teacher{Id=9,Name="Анна",MiddleName="Сергеевна",SurName="Дубойская",Position="завуч" },
                    new Teacher{Id=10,Name="Анна",MiddleName="Сергеевна",SurName="Дубойская",Position="завуч" }

                });

            base.OnModelCreating(modelBuilder);
        }
      

    }
}
