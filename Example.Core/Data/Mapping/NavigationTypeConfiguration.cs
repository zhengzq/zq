using System.Data.Entity.ModelConfiguration;
using Example.Core.Domain.Navigations;

namespace Example.Core.Data.Mapping
{

    public class NavigationTypeConfiguration : EntityTypeConfiguration<Navigation>
    {
        public NavigationTypeConfiguration()
        {
            HasKey(c => c.Id);
            Property(c => c.Id)
                .IsRequired();


            ToTable("Navigation");
        }
    }
}
