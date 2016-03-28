using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Example.Core.Domain.Managers;

namespace Example.Core.Data.Mapping
{

    public class ManagerTypeConfiguration : EntityTypeConfiguration<Manager>
    {
        public ManagerTypeConfiguration()
        {
            HasKey(c => c.Id);
            Property(c => c.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            ToTable("Manager");
        }
    }
}
