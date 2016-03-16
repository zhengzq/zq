using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Example.Web.Core.Domain.Managers;

namespace Example.Web.Core.Data.Mapping
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
