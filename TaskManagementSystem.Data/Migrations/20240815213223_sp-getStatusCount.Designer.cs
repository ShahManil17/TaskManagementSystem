﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskManagementSystem.Data;

#nullable disable

namespace TaskManagementSystem.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240815213223_sp-getStatusCount")]
    partial class spgetStatusCount
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TaskManagementSystem.Data.DataModels.NotificationStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IsDelivered")
                        .HasColumnType("int");

                    b.Property<int>("IsRead")
                        .HasColumnType("int");

                    b.Property<int?>("NotificationId")
                        .HasColumnType("int");

                    b.Property<int?>("ReceiverId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NotificationId");

                    b.HasIndex("ReceiverId");

                    b.ToTable("NotificationStatus");
                });

            modelBuilder.Entity("TaskManagementSystem.Data.DataModels.Notifications", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SenderId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TaskId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SenderId");

                    b.HasIndex("TaskId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("TaskManagementSystem.Data.DataModels.Permissions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Permissions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "addUsers"
                        },
                        new
                        {
                            Id = 2,
                            Name = "assignRoles"
                        },
                        new
                        {
                            Id = 3,
                            Name = "assignPermissions"
                        },
                        new
                        {
                            Id = 4,
                            Name = "assignTasks"
                        },
                        new
                        {
                            Id = 5,
                            Name = "reqToChangeStatus"
                        },
                        new
                        {
                            Id = 6,
                            Name = "changeStatus"
                        },
                        new
                        {
                            Id = 7,
                            Name = "editTask"
                        },
                        new
                        {
                            Id = 8,
                            Name = "assignUsers"
                        });
                });

            modelBuilder.Entity("TaskManagementSystem.Data.DataModels.RefreshTokens", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<DateTime?>("ExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("TaskManagementSystem.Data.DataModels.Requests", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<int?>("AcceptedId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("IsOpen")
                        .HasColumnType("int");

                    b.Property<int?>("RequestedId")
                        .HasColumnType("int");

                    b.Property<string>("RequestedStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SubTaskId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AcceptedId");

                    b.HasIndex("RequestedId");

                    b.HasIndex("SubTaskId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("TaskManagementSystem.Data.DataModels.RoleHasPermissions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("PermissionId")
                        .HasColumnType("int");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleHasPermission");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PermissionId = 1,
                            RoleId = 1
                        },
                        new
                        {
                            Id = 2,
                            PermissionId = 2,
                            RoleId = 1
                        },
                        new
                        {
                            Id = 3,
                            PermissionId = 3,
                            RoleId = 1
                        },
                        new
                        {
                            Id = 4,
                            PermissionId = 4,
                            RoleId = 1
                        },
                        new
                        {
                            Id = 5,
                            PermissionId = 6,
                            RoleId = 1
                        },
                        new
                        {
                            Id = 6,
                            PermissionId = 4,
                            RoleId = 2
                        },
                        new
                        {
                            Id = 7,
                            PermissionId = 6,
                            RoleId = 2
                        },
                        new
                        {
                            Id = 8,
                            PermissionId = 6,
                            RoleId = 3
                        },
                        new
                        {
                            Id = 9,
                            PermissionId = 5,
                            RoleId = 4
                        },
                        new
                        {
                            Id = 10,
                            PermissionId = 7,
                            RoleId = 1
                        },
                        new
                        {
                            Id = 11,
                            PermissionId = 7,
                            RoleId = 2
                        },
                        new
                        {
                            Id = 12,
                            PermissionId = 8,
                            RoleId = 1
                        },
                        new
                        {
                            Id = 13,
                            PermissionId = 8,
                            RoleId = 2
                        });
                });

            modelBuilder.Entity("TaskManagementSystem.Data.DataModels.Roles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "CEO"
                        },
                        new
                        {
                            Id = 2,
                            Name = "CTO"
                        },
                        new
                        {
                            Id = 3,
                            Name = "QA"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Developer"
                        });
                });

            modelBuilder.Entity("TaskManagementSystem.Data.DataModels.SubTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TaskId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("SubTask");
                });

            modelBuilder.Entity("TaskManagementSystem.Data.DataModels.Tasks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedById")
                        .HasColumnType("int");

                    b.Property<int?>("HasSubTask")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaskName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("TaskManagementSystem.Data.DataModels.UserHasTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("TaskId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.HasIndex("UserId");

                    b.ToTable("UserHasTask");
                });

            modelBuilder.Entity("TaskManagementSystem.Data.DataModels.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Country = "India",
                            Email = "test.ceo@gmail.com",
                            IsDeleted = 0,
                            Password = "Dev@123",
                            PhoneNumber = "7894561032",
                            RoleId = 1,
                            UserName = "ceo"
                        },
                        new
                        {
                            Id = 2,
                            Country = "India",
                            Email = "test.cto@gmail.com",
                            IsDeleted = 0,
                            Password = "Dev@123",
                            PhoneNumber = "7894562532",
                            RoleId = 2,
                            UserName = "cto"
                        });
                });

            modelBuilder.Entity("TaskManagementSystem.Data.DataModels.NotificationStatus", b =>
                {
                    b.HasOne("TaskManagementSystem.Data.DataModels.Notifications", "Notification")
                        .WithMany()
                        .HasForeignKey("NotificationId");

                    b.HasOne("TaskManagementSystem.Data.DataModels.Users", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverId");

                    b.Navigation("Notification");

                    b.Navigation("Receiver");
                });

            modelBuilder.Entity("TaskManagementSystem.Data.DataModels.Notifications", b =>
                {
                    b.HasOne("TaskManagementSystem.Data.DataModels.Users", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId");

                    b.HasOne("TaskManagementSystem.Data.DataModels.SubTask", "Task")
                        .WithMany()
                        .HasForeignKey("TaskId");

                    b.Navigation("Sender");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("TaskManagementSystem.Data.DataModels.RefreshTokens", b =>
                {
                    b.HasOne("TaskManagementSystem.Data.DataModels.Users", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskManagementSystem.Data.DataModels.Requests", b =>
                {
                    b.HasOne("TaskManagementSystem.Data.DataModels.Users", "Accepted")
                        .WithMany()
                        .HasForeignKey("AcceptedId");

                    b.HasOne("TaskManagementSystem.Data.DataModels.Users", "Requested")
                        .WithMany()
                        .HasForeignKey("RequestedId");

                    b.HasOne("TaskManagementSystem.Data.DataModels.SubTask", "SubTask")
                        .WithMany()
                        .HasForeignKey("SubTaskId");

                    b.Navigation("Accepted");

                    b.Navigation("Requested");

                    b.Navigation("SubTask");
                });

            modelBuilder.Entity("TaskManagementSystem.Data.DataModels.RoleHasPermissions", b =>
                {
                    b.HasOne("TaskManagementSystem.Data.DataModels.Permissions", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId");

                    b.HasOne("TaskManagementSystem.Data.DataModels.Roles", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("TaskManagementSystem.Data.DataModels.SubTask", b =>
                {
                    b.HasOne("TaskManagementSystem.Data.DataModels.Tasks", "Task")
                        .WithMany()
                        .HasForeignKey("TaskId");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("TaskManagementSystem.Data.DataModels.Tasks", b =>
                {
                    b.HasOne("TaskManagementSystem.Data.DataModels.Users", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("TaskManagementSystem.Data.DataModels.UserHasTask", b =>
                {
                    b.HasOne("TaskManagementSystem.Data.DataModels.SubTask", "Task")
                        .WithMany()
                        .HasForeignKey("TaskId");

                    b.HasOne("TaskManagementSystem.Data.DataModels.Users", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Task");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskManagementSystem.Data.DataModels.Users", b =>
                {
                    b.HasOne("TaskManagementSystem.Data.DataModels.Roles", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}
