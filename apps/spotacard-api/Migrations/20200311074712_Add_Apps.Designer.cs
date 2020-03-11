﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Spotacard.Infrastructure;

namespace Spotacard.Migrations
{
    [DbContext(typeof(GraphContext))]
    [Migration("20200311074712_Add_Apps")]
    partial class Add_Apps
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Spotacard.Domain.App", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Namingspace")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Organization")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Prefix")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Apps");
                });

            modelBuilder.Entity("Spotacard.Domain.Card", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slug")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("Slug")
                        .IsUnique()
                        .HasFilter("[Slug] IS NOT NULL");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("Spotacard.Domain.CardAttribute", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.ToTable("Attributes");
                });

            modelBuilder.Entity("Spotacard.Domain.CardFavorite", b =>
                {
                    b.Property<Guid>("CardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CardId", "PersonId");

                    b.HasIndex("PersonId");

                    b.ToTable("CardFavorites");
                });

            modelBuilder.Entity("Spotacard.Domain.CardTag", b =>
                {
                    b.Property<Guid>("CardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TagId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CardId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("CardTags");
                });

            modelBuilder.Entity("Spotacard.Domain.Cell", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Area")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("FieldId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<Guid>("PageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("WidgetId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FieldId");

                    b.HasIndex("PageId");

                    b.HasIndex("WidgetId");

                    b.ToTable("Cells");
                });

            modelBuilder.Entity("Spotacard.Domain.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CardId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Spotacard.Domain.Edge", b =>
                {
                    b.Property<Guid>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChildId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<int>("Label")
                        .HasColumnType("int");

                    b.HasKey("ParentId", "ChildId");

                    b.HasIndex("ChildId");

                    b.ToTable("Edges");
                });

            modelBuilder.Entity("Spotacard.Domain.Field", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<Guid>("TableId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("WidgetId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TableId");

                    b.HasIndex("WidgetId");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("Spotacard.Domain.FollowedPeople", b =>
                {
                    b.Property<Guid>("ObserverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TargetId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ObserverId", "TargetId");

                    b.HasIndex("TargetId");

                    b.ToTable("FollowedPeople");
                });

            modelBuilder.Entity("Spotacard.Domain.Layout", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CodeBehind")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Items")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Markup")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Styling")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Layouts");
                });

            modelBuilder.Entity("Spotacard.Domain.Page", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AppId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CodeBehind")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("LayoutId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Markup")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Styling")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("TableId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AppId");

                    b.HasIndex("LayoutId");

                    b.HasIndex("TableId");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("Spotacard.Domain.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Hash")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Salt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Spotacard.Domain.Table", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AppId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("AppId");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("Spotacard.Domain.Tag", b =>
                {
                    b.Property<string>("TagId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("TagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Spotacard.Domain.Widget", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CodeBehind")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Markup")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Package")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Styling")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Wiring")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Widgets");
                });

            modelBuilder.Entity("Spotacard.Domain.App", b =>
                {
                    b.HasOne("Spotacard.Domain.Card", "Card")
                        .WithOne("App")
                        .HasForeignKey("Spotacard.Domain.App", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Spotacard.Domain.Card", b =>
                {
                    b.HasOne("Spotacard.Domain.Person", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");
                });

            modelBuilder.Entity("Spotacard.Domain.CardAttribute", b =>
                {
                    b.HasOne("Spotacard.Domain.Card", "Card")
                        .WithMany("CardAttributes")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Spotacard.Domain.CardFavorite", b =>
                {
                    b.HasOne("Spotacard.Domain.Card", "Card")
                        .WithMany("CardFavorites")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Spotacard.Domain.Person", "Person")
                        .WithMany("CardFavorites")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Spotacard.Domain.CardTag", b =>
                {
                    b.HasOne("Spotacard.Domain.Card", "Card")
                        .WithMany("CardTags")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Spotacard.Domain.Tag", "Tag")
                        .WithMany("CardTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Spotacard.Domain.Cell", b =>
                {
                    b.HasOne("Spotacard.Domain.Field", "Field")
                        .WithMany("Cells")
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Spotacard.Domain.Page", "Page")
                        .WithMany("Cells")
                        .HasForeignKey("PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Spotacard.Domain.Widget", "Widget")
                        .WithMany("Cells")
                        .HasForeignKey("WidgetId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("Spotacard.Domain.Comment", b =>
                {
                    b.HasOne("Spotacard.Domain.Person", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Spotacard.Domain.Card", "Card")
                        .WithMany("Comments")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Spotacard.Domain.Edge", b =>
                {
                    b.HasOne("Spotacard.Domain.Card", "Child")
                        .WithMany("Children")
                        .HasForeignKey("ChildId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Spotacard.Domain.Card", "Parent")
                        .WithMany("Parents")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Spotacard.Domain.Field", b =>
                {
                    b.HasOne("Spotacard.Domain.Table", "Table")
                        .WithMany("Fields")
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Spotacard.Domain.Widget", "Widget")
                        .WithMany("Fields")
                        .HasForeignKey("WidgetId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Spotacard.Domain.FollowedPeople", b =>
                {
                    b.HasOne("Spotacard.Domain.Person", "Observer")
                        .WithMany("Followers")
                        .HasForeignKey("ObserverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Spotacard.Domain.Person", "Target")
                        .WithMany("Following")
                        .HasForeignKey("TargetId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Spotacard.Domain.Page", b =>
                {
                    b.HasOne("Spotacard.Domain.App", "App")
                        .WithMany("Pages")
                        .HasForeignKey("AppId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Spotacard.Domain.Layout", "Layout")
                        .WithMany("Pages")
                        .HasForeignKey("LayoutId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Spotacard.Domain.Table", "Table")
                        .WithMany("Pages")
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("Spotacard.Domain.Table", b =>
                {
                    b.HasOne("Spotacard.Domain.App", "App")
                        .WithMany("Tables")
                        .HasForeignKey("AppId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
