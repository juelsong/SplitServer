﻿// <auto-generated />
using System;
using ESys.Db.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ESys.Db.SQLite.Master
{
    [DbContext(typeof(MasterDbContext))]
    [Migration("20230307094356_v0.0.1")]
    partial class v001
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.8");

            modelBuilder.Entity("ESys.Security.Entity.Tenant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .HasColumnType("TEXT");

                    b.Property<int>("DbType")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Limits")
                        .HasColumnType("TEXT");

                    b.Property<string>("MasterDbConnStr")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("SlaveDbConnStr")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tenant");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "Seagull",
                            DbType = 0,
                            Limits = new DateTime(2123, 3, 7, 9, 43, 56, 210, DateTimeKind.Utc).AddTicks(8072),
                            MasterDbConnStr = "Data Source=192.168.31.51;Database=TenantMasterSeagull;User ID=sr;Password=Seagull@2020;pooling=true;port=3306;sslmode=none;CharSet=utf8;",
                            Name = "施格机器人",
                            SlaveDbConnStr = "Data Source=192.168.31.51;Database=TenantSlaveSeagull;User ID=sr;Password=Seagull@2020;pooling=true;port=3306;sslmode=none;CharSet=utf8;"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}