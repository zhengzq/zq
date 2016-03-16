using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Example.Web.Core.Domain.Permissions;

namespace Example.Web.Core.Data.Mapping
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
