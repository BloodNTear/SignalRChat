﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository;

#nullable disable

namespace Repository.Migrations
{
    [DbContext(typeof(SignalRDBContext))]
    [Migration("20230324115519_addUniqueConstraint")]
    partial class addUniqueConstraint
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AccountChatGroup", b =>
                {
                    b.Property<Guid>("AccountsUserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChatGroupsChatGroupID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AccountsUserID", "ChatGroupsChatGroupID");

                    b.HasIndex("ChatGroupsChatGroupID");

                    b.ToTable("AccountChatGroup");
                });

            modelBuilder.Entity("Repository.Entities.Account", b =>
                {
                    b.Property<Guid>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserID");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("accounts");
                });

            modelBuilder.Entity("Repository.Entities.ChatGroup", b =>
                {
                    b.Property<Guid>("ChatGroupID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ChatGroupID");

                    b.HasIndex("GroupName")
                        .IsUnique();

                    b.ToTable("chatGroups");
                });

            modelBuilder.Entity("Repository.Entities.Message", b =>
                {
                    b.Property<Guid>("MessageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FromAccountUserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MessageContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("MessageTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ToChatGroupChatGroupID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MessageID");

                    b.HasIndex("FromAccountUserID");

                    b.HasIndex("ToChatGroupChatGroupID");

                    b.ToTable("messages");
                });

            modelBuilder.Entity("AccountChatGroup", b =>
                {
                    b.HasOne("Repository.Entities.Account", null)
                        .WithMany()
                        .HasForeignKey("AccountsUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Repository.Entities.ChatGroup", null)
                        .WithMany()
                        .HasForeignKey("ChatGroupsChatGroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Repository.Entities.Message", b =>
                {
                    b.HasOne("Repository.Entities.Account", "FromAccount")
                        .WithMany("Messages")
                        .HasForeignKey("FromAccountUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Repository.Entities.ChatGroup", "ToChatGroup")
                        .WithMany("Messages")
                        .HasForeignKey("ToChatGroupChatGroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FromAccount");

                    b.Navigation("ToChatGroup");
                });

            modelBuilder.Entity("Repository.Entities.Account", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Repository.Entities.ChatGroup", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
