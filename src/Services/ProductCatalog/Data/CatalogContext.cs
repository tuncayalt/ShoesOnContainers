using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Domain.Models;

namespace ProductCatalog.Data
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CatalogType> CatalogTypes { get; set; }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogItem> CatalogItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CatalogType>(ConfigureCatalogTypeEntity);
            builder.Entity<CatalogBrand>(ConfigureCatalogBrandEntity);
            builder.Entity<CatalogItem>(ConfigureCatalogItemEntity);
        }

        private void ConfigureCatalogItemEntity(EntityTypeBuilder<CatalogItem> builder)
        {
            builder.ToTable("CatalogItem");

            builder.Property(i => i.Id)
                .ForSqlServerUseSequenceHiLo("catalog-item-hilo")
                .IsRequired();

            builder.Property(i => i.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(i => i.Description);

            builder.Property(i => i.PictureFileName);

            builder.Property(i => i.PictureUrl);

            builder.Property(i => i.Price)
                .IsRequired();

            builder.HasOne(i => i.CatalogType)
                .WithMany(i => i.CatalogItems)
                .HasForeignKey(i => i.CatalogTypeId);

            builder.HasOne(i => i.CatalogBrand)
                .WithMany(i => i.CatalogItems)
                .HasForeignKey(i => i.CatalogBrandId);
        }

        private void ConfigureCatalogBrandEntity(EntityTypeBuilder<CatalogBrand> builder)
        {
            builder.ToTable("CatalogBrand");

            builder.Property(t => t.Id)
                .ForSqlServerUseSequenceHiLo("catalog-brand-hilo")
                .IsRequired();

            builder.Property(t => t.Brand)
                .HasMaxLength(50)
                .IsRequired();
        }

        private void ConfigureCatalogTypeEntity(EntityTypeBuilder<CatalogType> builder)
        {
            builder.ToTable("CatalogType");

            builder.Property(t => t.Id)
                .ForSqlServerUseSequenceHiLo("catalog-type-hilo")
                .IsRequired();

            builder.Property(t => t.Type)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
