using System.Data.Entity.ModelConfiguration;
using Example.Core.Domain.Permissions;

namespace Example.Core.Data.Mapping
{

    public class PermissionTypeConfiguration : EntityTypeConfiguration<Permission>
    {
        public PermissionTypeConfiguration()
        {
            HasKey(c => c.Id);
            Property(c => c.Id)
                .IsRequired();


            ToTable("Permission");
        }
    }
}
