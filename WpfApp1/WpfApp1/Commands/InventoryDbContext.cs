using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WpfApp1;

public partial class InventoryDbContext : DbContext
{
    public InventoryDbContext()
    {
    }

    public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrdersList> Orders1 { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Producttype> Producttypes { get; set; }

    public virtual DbSet<Soldproduct> Soldproducts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=InventoryDB;Username=postgres;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("clients_pkey");

            entity.ToTable("clients");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.Login).HasColumnName("login");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Ransom)
                .HasColumnType("numeric")
                .HasColumnName("ransom");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orders_pkey");

            entity.ToTable("order");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('orders_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Orderid)
                .HasDefaultValueSql("nextval('orders_orderid_seq'::regclass)")
                .HasColumnName("orderid");
            entity.Property(e => e.Productcount).HasColumnName("productcount");
            entity.Property(e => e.Productid)
                .HasDefaultValueSql("nextval('orders_productid_seq'::regclass)")
                .HasColumnName("productid");

            entity.HasOne(d => d.OrderNavigation).WithMany(p => p.InverseOrderNavigation)
                .HasForeignKey(d => d.Orderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Orders");

            entity.HasOne(d => d.Product).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Productid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders");
        });

        modelBuilder.Entity<OrdersList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orders_pkey1");

            entity.ToTable("orders");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('orders_id_seq1'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Clientid)
                .ValueGeneratedOnAdd()
                .HasColumnName("clientid");
            entity.Property(e => e.Dateofcreate).HasColumnName("dateofcreate");
            entity.Property(e => e.Sum)
                .HasColumnType("money")
                .HasColumnName("sum");

            entity.HasOne(d => d.Client).WithMany(p => p.Order1s)
                .HasForeignKey(d => d.Clientid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Orders");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("products_pkey");

            entity.ToTable("products");

            entity.HasIndex(e => e.Producttypeid, "product");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Countofprod).HasColumnName("countofprod");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Model).HasColumnName("model");
            entity.Property(e => e.Nameproduct).HasColumnName("nameproduct");
            entity.Property(e => e.Price)
                .HasColumnType("numeric")
                .HasColumnName("price");
            entity.Property(e => e.Producername).HasColumnName("producername");
            entity.Property(e => e.Producttypeid)
                .ValueGeneratedOnAdd()
                .HasColumnName("producttypeid");

            entity.HasOne(d => d.Producttype).WithOne(p => p.Product)
                .HasForeignKey<Product>(d => d.Producttypeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products");
        });

        modelBuilder.Entity<Producttype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("producttype_pkey");

            entity.ToTable("producttype");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Productname).HasColumnName("productname");
        });

        modelBuilder.Entity<Soldproduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("soldproducts_pkey");

            entity.ToTable("soldproducts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Clientid)
                .ValueGeneratedOnAdd()
                .HasColumnName("clientid");
            entity.Property(e => e.Dateofsold).HasColumnName("dateofsold");
            entity.Property(e => e.Orderid)
                .ValueGeneratedOnAdd()
                .HasColumnName("orderid");
            entity.Property(e => e.Productid)
                .ValueGeneratedOnAdd()
                .HasColumnName("productid");

            entity.HasOne(d => d.Client).WithMany(p => p.Soldproducts)
                .HasForeignKey(d => d.Clientid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SoldProducts");

            entity.HasOne(d => d.Product).WithMany(p => p.Soldproducts)
                .HasForeignKey(d => d.Productid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("soldproducts");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Users_pkey");

            entity.Property(e => e.Documents).HasColumnType("json");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
