﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TasksManagement.Infra.Database.Contexts;

#nullable disable

namespace TasksManagement.Infra.Database.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TasksManagement.Infra.Database.Entities.ProjectDbEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("OwnerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Projects", (string)null);
                });

            modelBuilder.Entity("TasksManagement.Infra.Database.Entities.TaskChangeDbEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("AuthorId")
                        .HasColumnType("bigint");

                    b.Property<string>("NewValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreviousValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Property")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("TaskId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("When")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("TaskId");

                    b.ToTable("TaskChanges", (string)null);
                });

            modelBuilder.Entity("TasksManagement.Infra.Database.Entities.TaskCommentDbEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("AuthorId")
                        .HasColumnType("bigint");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("TaskId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("TaskId");

                    b.ToTable("TaskComments", (string)null);
                });

            modelBuilder.Entity("TasksManagement.Infra.Database.Entities.TaskDbEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<long>("ProjectId")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Tasks", (string)null);
                });

            modelBuilder.Entity("TasksManagement.Infra.Database.Entities.UserDbEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Logan",
                            Role = 1
                        },
                        new
                        {
                            Id = 2L,
                            Name = "Scott",
                            Role = 0
                        },
                        new
                        {
                            Id = 3L,
                            Name = "Jean",
                            Role = 0
                        });
                });

            modelBuilder.Entity("TasksManagement.Infra.Database.Entities.ProjectDbEntity", b =>
                {
                    b.HasOne("TasksManagement.Infra.Database.Entities.UserDbEntity", "Owner")
                        .WithMany("ProjectsOwned")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("TasksManagement.Infra.Database.Entities.TaskChangeDbEntity", b =>
                {
                    b.HasOne("TasksManagement.Infra.Database.Entities.UserDbEntity", "Author")
                        .WithMany("ChangesMade")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TasksManagement.Infra.Database.Entities.TaskDbEntity", "Task")
                        .WithMany("ChangesHistory")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("TasksManagement.Infra.Database.Entities.TaskCommentDbEntity", b =>
                {
                    b.HasOne("TasksManagement.Infra.Database.Entities.UserDbEntity", "Author")
                        .WithMany("CommentsMade")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TasksManagement.Infra.Database.Entities.TaskDbEntity", "Task")
                        .WithMany("Comments")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("TasksManagement.Infra.Database.Entities.TaskDbEntity", b =>
                {
                    b.HasOne("TasksManagement.Infra.Database.Entities.ProjectDbEntity", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("TasksManagement.Infra.Database.Entities.ProjectDbEntity", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("TasksManagement.Infra.Database.Entities.TaskDbEntity", b =>
                {
                    b.Navigation("ChangesHistory");

                    b.Navigation("Comments");
                });

            modelBuilder.Entity("TasksManagement.Infra.Database.Entities.UserDbEntity", b =>
                {
                    b.Navigation("ChangesMade");

                    b.Navigation("CommentsMade");

                    b.Navigation("ProjectsOwned");
                });
#pragma warning restore 612, 618
        }
    }
}
