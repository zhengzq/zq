using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Example.Core.Domain.Roles;

namespace Example.Core.Data.Mapping
{

    public class RoleTypeConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleTypeConfiguration()
        {
            HasKey(c => c.Id);
            Property(c => c.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            ToTable("Role");
        }
    }
}
