using System.Data.Entity.ModelConfiguration;

namespace KnockoutFirstKickOfTheCat.Models
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            Ignore(p => p.ActiveDateString);
            Ignore(p => p.IsDirty);
            Ignore(p => p.IsActive);
        }
    }
}
