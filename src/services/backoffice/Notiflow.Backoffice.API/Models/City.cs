using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notiflow.Lib.Database.Abstract;
using Notiflow.Lib.Database.Configurations;

namespace Notiflow.Backoffice.API.Models
{
    public class City : BaseHistoricalEntity
    {
        public int Code { get; set; }
    }

    public class CityConfiguration : BaseHistoricalEntityConfiguration<City>
    {
        public override void Configure(EntityTypeBuilder<City> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Code).IsRequired();
        }
    }
}
