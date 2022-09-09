﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CoreSampleWebjob.Models;

namespace CoreSampleWebjob.Data
{
    public partial class TestAPISampleContext : DbContext
    {
        public TestAPISampleContext()
        {
        }

        public TestAPISampleContext(DbContextOptions<TestAPISampleContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.author).HasMaxLength(1000);

                entity.Property(e => e.description).HasMaxLength(1000);

                entity.Property(e => e.genre).HasMaxLength(1000);

                entity.Property(e => e.image).HasMaxLength(1000);

                entity.Property(e => e.isbn).HasMaxLength(1000);

                entity.Property(e => e.published).HasMaxLength(1000);

                entity.Property(e => e.publisher).HasMaxLength(1000);

                entity.Property(e => e.title).HasMaxLength(1000);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}