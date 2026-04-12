using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RestApi.Data;

public partial class CreditDbContext : DbContext
{
    public CreditDbContext()
    {
    }

    public CreditDbContext(DbContextOptions<CreditDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<basic_member> basic_members { get; set; }

    public virtual DbSet<category> categories { get; set; }

    public virtual DbSet<charge> charges { get; set; }

    public virtual DbSet<charge_wide> charge_wides { get; set; }

    public virtual DbSet<corp_member> corp_members { get; set; }

    public virtual DbSet<corporation> corporations { get; set; }

    public virtual DbSet<member> members { get; set; }

    public virtual DbSet<member2> member2s { get; set; }

    public virtual DbSet<overdue> overdues { get; set; }

    public virtual DbSet<payment> payments { get; set; }

    public virtual DbSet<payment_wide> payment_wides { get; set; }

    public virtual DbSet<provider> providers { get; set; }

    public virtual DbSet<region> regions { get; set; }

    public virtual DbSet<statement> statements { get; set; }

    public virtual DbSet<statement_wide> statement_wides { get; set; }

    public virtual DbSet<status> statuses { get; set; }

    public virtual DbSet<test> tests { get; set; }

    public virtual DbSet<test2> test2s { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=SRIBINKK26\\SQLEXPRESS;Initial Catalog=Credit;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<basic_member>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("basic_member");

            entity.Property(e => e.city)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.expr_dt).HasColumnType("datetime");
            entity.Property(e => e.firstname)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.lastname)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.mail_code)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.member_code)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.middleinitial)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.phone_no)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.state_prov)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.street)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<category>(entity =>
        {
            entity.HasKey(e => e.category_no).HasName("category_ident");

            entity.ToTable("category");

            entity.Property(e => e.category_code)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasDefaultValue("  ", "category_status_default");
            entity.Property(e => e.category_desc)
                .HasMaxLength(31)
                .IsUnicode(false);
        });

        modelBuilder.Entity<charge>(entity =>
        {
            entity.HasKey(e => e.charge_no).HasName("ChargePK");

            entity.ToTable("charge");

            entity.HasIndex(e => e.category_no, "charge_category_link");

            entity.HasIndex(e => e.provider_no, "charge_provider_link");

            entity.HasIndex(e => e.statement_no, "charge_statement_link");

            entity.Property(e => e.charge_amt).HasColumnType("money");
            entity.Property(e => e.charge_code)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasDefaultValue("  ", "charge_status_default");
            entity.Property(e => e.charge_dt).HasColumnType("datetime");

            entity.HasOne(d => d.category_noNavigation).WithMany(p => p.charges)
                .HasForeignKey(d => d.category_no)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("charge_category_link");

            entity.HasOne(d => d.member_noNavigation).WithMany(p => p.charges)
                .HasForeignKey(d => d.member_no)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("charge_member_link");

            entity.HasOne(d => d.provider_noNavigation).WithMany(p => p.charges)
                .HasForeignKey(d => d.provider_no)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("charge_provider_link");
        });

        modelBuilder.Entity<charge_wide>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("charge_wide");

            entity.Property(e => e.category_desc)
                .HasMaxLength(31)
                .IsUnicode(false);
            entity.Property(e => e.charge_amt).HasColumnType("money");
            entity.Property(e => e.charge_code)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.charge_dt).HasColumnType("datetime");
            entity.Property(e => e.firstname)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.lastname)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.provider_name)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.region_name)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<corp_member>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("corp_member");

            entity.Property(e => e.city)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.corp_code)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.corp_name)
                .HasMaxLength(31)
                .IsUnicode(false);
            entity.Property(e => e.expr_dt).HasColumnType("datetime");
            entity.Property(e => e.firstname)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.lastname)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.mail_code)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.middleinitial)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.phone_no)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.state_prov)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.street)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<corporation>(entity =>
        {
            entity.HasKey(e => e.corp_no).HasName("corporation_ident");

            entity.ToTable("corporation");

            entity.HasIndex(e => e.region_no, "corporation_region_link");

            entity.Property(e => e.city)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.corp_code)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasDefaultValue("  ", "corporation_status_default");
            entity.Property(e => e.corp_name)
                .HasMaxLength(31)
                .IsUnicode(false);
            entity.Property(e => e.country)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.expr_dt).HasColumnType("datetime");
            entity.Property(e => e.mail_code)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.phone_no)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.state_prov)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.street)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.region_noNavigation).WithMany(p => p.corporations)
                .HasForeignKey(d => d.region_no)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("corporation_region_link");
        });

        modelBuilder.Entity<member>(entity =>
        {
            entity.HasKey(e => e.member_no).HasName("member_ident");

            entity.ToTable("member");

            entity.HasIndex(e => e.corp_no, "member_corporation_link");

            entity.HasIndex(e => e.region_no, "member_region_link");

            entity.Property(e => e.city)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.country)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.curr_balance)
                .HasDefaultValue(0m, "member_curr_balance_default")
                .HasColumnType("money");
            entity.Property(e => e.expr_dt)
                .HasDefaultValueSql("(dateadd(year,1,getdate()))", "member_expr_dt_default")
                .HasColumnType("datetime");
            entity.Property(e => e.firstname)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.issue_dt)
                .HasDefaultValueSql("(getdate())", "member_issue_dt_default")
                .HasColumnType("datetime");
            entity.Property(e => e.lastname)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.mail_code)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.member_code)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasDefaultValue("  ", "member_status_default");
            entity.Property(e => e.middleinitial)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.phone_no)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.photograph).HasColumnType("image");
            entity.Property(e => e.prev_balance)
                .HasDefaultValue(0m, "member_prev_balance_default")
                .HasColumnType("money");
            entity.Property(e => e.state_prov)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.street)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.corp_noNavigation).WithMany(p => p.members)
                .HasForeignKey(d => d.corp_no)
                .HasConstraintName("member_corporation_link");

            entity.HasOne(d => d.region_noNavigation).WithMany(p => p.members)
                .HasForeignKey(d => d.region_no)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("member_region_link");
        });

        modelBuilder.Entity<member2>(entity =>
        {
            entity.HasKey(e => e.member_no)
                .HasName("member2PK")
                .IsClustered(false);

            entity.ToTable("member2");

            entity.HasIndex(e => new { e.lastname, e.firstname, e.middleinitial }, "member2Cl").IsClustered();

            entity.HasIndex(e => e.corp_no, "member2CorpFK");

            entity.HasIndex(e => e.region_no, "member2RegionFK");

            entity.Property(e => e.member_no).ValueGeneratedNever();
            entity.Property(e => e.city)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.country)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.curr_balance).HasColumnType("money");
            entity.Property(e => e.expr_dt).HasColumnType("datetime");
            entity.Property(e => e.firstname)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.issue_dt).HasColumnType("datetime");
            entity.Property(e => e.lastname)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.mail_code)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.member_code)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.middleinitial)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.phone_no)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.photograph).HasColumnType("image");
            entity.Property(e => e.prev_balance).HasColumnType("money");
            entity.Property(e => e.state_prov)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.street)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<overdue>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("overdue");

            entity.Property(e => e.city)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.due_dt).HasColumnType("datetime");
            entity.Property(e => e.expr_dt).HasColumnType("datetime");
            entity.Property(e => e.firstname)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.lastname)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.mail_code)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.member_code)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.middleinitial)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.phone_no)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.region_name)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.state_prov)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.statement_amt).HasColumnType("money");
            entity.Property(e => e.statement_code)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.statement_dt).HasColumnType("datetime");
            entity.Property(e => e.street)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<payment>(entity =>
        {
            entity.HasKey(e => e.payment_no)
                .HasName("payment_ident")
                .IsClustered(false);

            entity.ToTable("payment");

            entity.HasIndex(e => e.member_no, "payment_member_link");

            entity.Property(e => e.payment_amt).HasColumnType("money");
            entity.Property(e => e.payment_code)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasDefaultValue("  ", "payment_status_default");
            entity.Property(e => e.payment_dt).HasColumnType("datetime");
            entity.Property(e => e.statement_no).HasDefaultValue(0, "payment_statement_no_default");

            entity.HasOne(d => d.member_noNavigation).WithMany(p => p.payments)
                .HasForeignKey(d => d.member_no)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_member_link");
        });

        modelBuilder.Entity<payment_wide>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("payment_wide");

            entity.Property(e => e.city)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.expr_dt).HasColumnType("datetime");
            entity.Property(e => e.firstname)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.lastname)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.mail_code)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.member_code)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.middleinitial)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.payment_amt).HasColumnType("money");
            entity.Property(e => e.payment_code)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.payment_dt).HasColumnType("datetime");
            entity.Property(e => e.phone_no)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.region_name)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.state_prov)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.street)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<provider>(entity =>
        {
            entity.HasKey(e => e.provider_no).HasName("provider_ident");

            entity.ToTable("provider");

            entity.HasIndex(e => e.region_no, "provider_region_link");

            entity.Property(e => e.city)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.country)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.expr_dt)
                .HasDefaultValueSql("(dateadd(year,1,getdate()))", "provider_expr_dt_default")
                .HasColumnType("datetime");
            entity.Property(e => e.issue_dt)
                .HasDefaultValueSql("(getdate())", "provider_issue_dt_default")
                .HasColumnType("datetime");
            entity.Property(e => e.mail_code)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.phone_no)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.provider_code)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasDefaultValue("  ", "provider_status_default");
            entity.Property(e => e.provider_name)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.state_prov)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.street)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.region_noNavigation).WithMany(p => p.providers)
                .HasForeignKey(d => d.region_no)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("provider_region_link");
        });

        modelBuilder.Entity<region>(entity =>
        {
            entity.HasKey(e => e.region_no).HasName("region_ident");

            entity.ToTable("region");

            entity.Property(e => e.city)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.country)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.mail_code)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.phone_no)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.region_code)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasDefaultValue("  ", "region_status_default");
            entity.Property(e => e.region_name)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.state_prov)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.street)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<statement>(entity =>
        {
            entity.HasKey(e => e.statement_no).HasName("statement_ident");

            entity.ToTable("statement");

            entity.HasIndex(e => e.member_no, "statement_member_link");

            entity.Property(e => e.due_dt).HasColumnType("datetime");
            entity.Property(e => e.statement_amt).HasColumnType("money");
            entity.Property(e => e.statement_code)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasDefaultValue("  ", "statement_status_default");
            entity.Property(e => e.statement_dt).HasColumnType("datetime");

            entity.HasOne(d => d.member_noNavigation).WithMany(p => p.statements)
                .HasForeignKey(d => d.member_no)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("statement_member_link");
        });

        modelBuilder.Entity<statement_wide>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("statement_wide");

            entity.Property(e => e.city)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.due_dt).HasColumnType("datetime");
            entity.Property(e => e.expr_dt).HasColumnType("datetime");
            entity.Property(e => e.firstname)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.lastname)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.mail_code)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.member_code)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.middleinitial)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.phone_no)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.region_name)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.state_prov)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.statement_amt).HasColumnType("money");
            entity.Property(e => e.statement_code)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.statement_dt).HasColumnType("datetime");
            entity.Property(e => e.street)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<status>(entity =>
        {
            entity.HasKey(e => e.status_code).HasName("status_ident");

            entity.ToTable("status");

            entity.Property(e => e.status_code)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.status_desc)
                .HasMaxLength(31)
                .IsUnicode(false);
        });

        modelBuilder.Entity<test>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("test");

            entity.Property(e => e.firstname)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.lastname)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.member_no).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<test2>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("test2");

            entity.Property(e => e.lastname)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.member_no).ValueGeneratedOnAdd();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
