using FFXICustomDats.Data.XiDatEntities;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data;

public partial class XiDatContext : DbContext
{
    public XiDatContext()
    {
    }

    public XiDatContext(DbContextOptions<XiDatContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Item> Items { get; set; }
    public virtual DbSet<ItemEquipment> ItemEquipments { get; set; }
    public virtual DbSet<ItemFurnishing> ItemFurnishings{ get; set; }
    public virtual DbSet<ItemPuppet> ItemPuppets { get; set; }
    public virtual DbSet<ItemString> ItemStrings { get; set; }
    public virtual DbSet<ItemUsable> ItemUsables { get; set; }
    public virtual DbSet<ItemWeapon> ItemWeapons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PRIMARY");

            entity.ToTable("item");

            entity.Property(e => e.ItemId)
                .ValueGeneratedNever()
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("itemid");
            entity.Property(e => e.Flags)
                .HasColumnType("int(8) unsigned")
                .HasColumnName("flags");
            entity.Property(e => e.StackSize)
                .HasDefaultValueSql("'1'")
                .HasColumnType("tinyint(2) unsigned")
                .HasColumnName("stackSize");
            entity.Property(e => e.ItemType)
                .HasDefaultValueSql("@`GENERAL_TYPE`")
                .HasColumnType("tinyint(1) unsigned")
                .HasColumnName("type");
            entity.Property(e => e.ResourceId)
                .HasColumnType("int(11)")
                .HasColumnName("resource_id");
            entity.Property(e => e.ValidTargets)
                .HasColumnType("smallint(3) unsigned")
                .HasColumnName("validTargets");
            entity.Property(e => e.DatFile)
                .HasColumnType("tinytext")
                .HasColumnName("dat_file");
            entity.Property(e => e.IconBytes)
                .HasColumnType("blob")
                .HasColumnName("icon_bytes");
        });

        modelBuilder.Entity<ItemString>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PRIMARY");

            entity.ToTable("item_string");

            entity.Property(e => e.ItemId)
                .ValueGeneratedNever()
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("itemid");
            entity.Property(e => e.Name)
                .HasColumnType("tinytext")
                .HasColumnName("name");
            entity.Property(e => e.ArticleType)
                .HasColumnType("smallint(3) unsigned")
                .HasColumnName("articleType");
            entity.Property(e => e.SingularName)
                .HasColumnType("tinytext")
                .HasColumnName("singular_name");
            entity.Property(e => e.PluralName)
                .HasColumnType("tinytext")
                .HasColumnName("plural_name");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
        });

        modelBuilder.Entity<ItemEquipment>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PRIMARY");

            entity.ToTable("item_equipment");

            entity.Property(e => e.ItemId)
                .ValueGeneratedNever()
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("itemId");
            entity.Property(e => e.Level)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("level");
            entity.Property(e => e.Slot)
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("slot");
            entity.Property(e => e.Races)
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("races");
            entity.Property(e => e.Jobs)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("jobs");
            entity.Property(e => e.SuperiorLevel)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("superior_level");
            entity.Property(e => e.ShieldSize)
                .HasColumnType("tinyint(1) unsigned")
                .HasColumnName("shieldSize");
            entity.Property(e => e.MaxCharges)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("maxCharges");
            entity.Property(e => e.CastingTime)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("castingTime");
            entity.Property(e => e.UseDelay)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("useDelay");
            entity.Property(e => e.ReuseDelay)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("reuseDelay");
            entity.Property(e => e.ILevel)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("ilevel");
            entity.Property(e => e.Unknown1)
                .HasColumnType("int(11)")
                .HasColumnName("unknown1");
            entity.Property(e => e.Unknown2)
                .HasColumnType("int(11)")
                .HasColumnName("unknown2");
            entity.Property(e => e.Unknown3)
                .HasColumnType("int(11)")
                .HasColumnName("unknown3");
        });

        modelBuilder.Entity<ItemWeapon>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PRIMARY");

            entity.ToTable("item_weapon");

            entity.Property(e => e.ItemId)
                .ValueGeneratedNever()
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("itemId");
            entity.Property(e => e.Damage)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("dmg");
            entity.Property(e => e.Delay)
                .HasColumnType("int(10)")
                .HasColumnName("delay");
            entity.Property(e => e.Dps)
                .HasColumnType("int(10)")
                .HasColumnName("dps");
            entity.Property(e => e.SkillType)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("skillType");
            entity.Property(e => e.JugSize)
                .HasColumnType("int(10)")
                .HasColumnName("jugSize");
            entity.Property(e => e.Unknown1)
                .HasColumnType("int(11)")
                .HasColumnName("unknown1");
        });

        modelBuilder.Entity<ItemFurnishing>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PRIMARY");

            entity.ToTable("item_furnishing");

            entity.Property(e => e.ItemId)
                .ValueGeneratedNever()
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("itemid");
            entity.Property(e => e.StorageSlots)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("storageSlots");
            entity.Property(e => e.Element)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("element");
            entity.Property(e => e.Unknown3)
                .HasColumnType("int(11)")
                .HasColumnName("unknown3");
        });

        modelBuilder.Entity<ItemPuppet>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PRIMARY");

            entity.ToTable("item_puppet");

            entity.Property(e => e.ItemId)
                .ValueGeneratedNever()
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("itemid");
            entity.Property(e => e.Slot)
                .HasColumnType("tinyint(2) unsigned")
                .HasColumnName("slot");
            entity.Property(e => e.Fire)
                .HasColumnType("int(11)")
                .HasColumnName("fire");
            entity.Property(e => e.Ice)
                .HasColumnType("int(11)")
                .HasColumnName("ice");
            entity.Property(e => e.Wind)
                .HasColumnType("int(11)")
                .HasColumnName("wind");
            entity.Property(e => e.Earth)
                .HasColumnType("int(11)")
                .HasColumnName("earth");
            entity.Property(e => e.Lightning)
                .HasColumnType("int(11)")
                .HasColumnName("lightning");
            entity.Property(e => e.Water)
                .HasColumnType("int(11)")
                .HasColumnName("water");
            entity.Property(e => e.Light)
                .HasColumnType("int(11)")
                .HasColumnName("light");
            entity.Property(e => e.Dark)
                .HasColumnType("int(11)")
                .HasColumnName("dark");
            entity.Property(e => e.Unknown1)
                .HasColumnType("int(11)")
                .HasColumnName("unknown1");
        });

        modelBuilder.Entity<ItemUsable>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PRIMARY");

            entity.ToTable("item_usable");

            entity.Property(e => e.ItemId)
                .ValueGeneratedNever()
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("itemId");
            entity.Property(e => e.ActivationTime)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("activationTime");
            entity.Property(e => e.Unknown1)
                .HasColumnType("int(11)")
                .HasColumnName("unknown1");
            entity.Property(e => e.Unknown2)
                .HasColumnType("int(11)")
                .HasColumnName("unknown2");
            entity.Property(e => e.Unknown3)
                .HasColumnType("int(11)")
                .HasColumnName("unknown3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
