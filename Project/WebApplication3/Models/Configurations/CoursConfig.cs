using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Models.Domain;

namespace CodeFirst
{
    class CoursConfig : EntityTypeConfiguration<Cours>
    {
        public CoursConfig()
        { 
            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);
            Property(x => x.DateCreateCourse)
                .IsRequired();
        }
    }
}
