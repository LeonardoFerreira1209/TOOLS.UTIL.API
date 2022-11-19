﻿// <auto-generated />
using System;
using APPLICATION.INFRAESTRUTURE.CONTEXTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APPLICATION.Migrations
{
    [DbContext(typeof(Contexto))]
    partial class ContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("APPLICATION.DOMAIN.DTOS.ENTITIES.TEMPLATES.Template", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Templates", (string)null);
                });

            modelBuilder.Entity("APPLICATION.DOMAIN.DTOS.TWILLIO.MessageTwillio", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccountSid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApiVersion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateSent")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ErrorCode")
                        .HasColumnType("int");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("From")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MediaUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MessageId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MessageStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumMedia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumSegments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Price")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PriceUnit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SmsSid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SmsStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("To")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MessagesTwillio", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
