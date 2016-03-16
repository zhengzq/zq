using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Example.Web.Core.Domain.Navigations;

namespace Example.Web.Core.Data.Mapping
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
