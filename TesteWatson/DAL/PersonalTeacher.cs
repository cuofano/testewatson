using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TesteWatson.Models;

namespace TesteWatson.DAL
{
    public class PersonalTeacher : DbContext
    {

        public DbSet<AlunoModels> Aluno { get; set; }
        //public DbSet<Enrollment> Enrollments { get; set; }
        //public DbSet<Course> Courses { get; set; }
        public PersonalTeacher() : base("personalteacherEntities")
        {
            //If model change, It will re-create new database.
            Database.SetInitializer<PersonalTeacher>(new DropCreateDatabaseIfModelChanges<PersonalTeacher>());
        }
    }
   
}