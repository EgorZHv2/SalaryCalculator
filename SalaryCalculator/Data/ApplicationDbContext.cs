﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SalaryCalculator.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AllowancesAndFine> AllowancesAndFines { get; set; } = null!;
        public virtual DbSet<LaborStandart> LaborStandarts { get; set; } = null!;
        public virtual DbSet<PaymentForm> PaymentForms { get; set; } = null!;
        public virtual DbSet<Position> Positions { get; set; } = null!;
        public virtual DbSet<WorkedUnitsOfLabor> WorkedUnitsOfLabors { get; set; } = null!;
        public virtual DbSet<Worker> Workers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=Computer\\Server; Database=SalaryCalc; Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AllowancesAndFine>(entity =>
            {
                entity.Property(e => e.Bonus).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Fine).HasColumnType("decimal(9, 2)");

                entity.HasOne(d => d.Worker)
                    .WithMany(p => p.AllowancesAndFines)
                    .HasForeignKey(d => d.WorkerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AllowancesAndFines_Workers");
            });

            modelBuilder.Entity<LaborStandart>(entity =>
            {
                entity.HasOne(d => d.Position)
                    .WithMany(p => p.LaborStandarts)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LaborStandarts_Positions");
            });

            modelBuilder.Entity<PaymentForm>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.Property(e => e.BasicSalarePerWorkUnit).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.SalarePerWorkUnitOverTheNorm).HasColumnType("decimal(9, 2)");

                entity.HasOne(d => d.PaymentForm)
                    .WithMany(p => p.Positions)
                    .HasForeignKey(d => d.PaymentFormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Positions_PaymentForms");
            });

            modelBuilder.Entity<WorkedUnitsOfLabor>(entity =>
            {
                entity.ToTable("WorkedUnitsOfLabor");

                entity.HasOne(d => d.Worker)
                    .WithMany(p => p.WorkedUnitsOfLabors)
                    .HasForeignKey(d => d.WorkerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkedUnitsOfLabor_Workers");
            });

            modelBuilder.Entity<Worker>(entity =>
            {
                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.HiringDate).HasColumnType("date");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Patronimyc).HasMaxLength(50);

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Workers)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Workers_Positions");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}