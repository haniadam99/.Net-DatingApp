﻿// <auto-generated />
using System;
using DatingProjekt.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataLayer.Migrations
{
    [DbContext(typeof(DatingDBContext))]
    partial class DatingDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:Collation", "Finnish_Swedish_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("DatingProjekt.Models.Friend", b =>
                {
                    b.Property<string>("User1")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("user1");

                    b.Property<string>("User2")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("user2");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("User1", "User2")
                        .HasName("PK__friends__E934B3F5D121C264");

                    b.HasIndex("User2");

                    b.ToTable("friends");
                });

            modelBuilder.Entity("DatingProjekt.Models.FriendRequest", b =>
                {
                    b.Property<string>("UserSent")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("userSent");

                    b.Property<string>("UserPending")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("userPending");

                    b.HasKey("UserSent", "UserPending")
                        .HasName("PK__friendRe__CA4D816B5E0CA4A5");

                    b.HasIndex("UserPending");

                    b.ToTable("friendRequest");
                });

            modelBuilder.Entity("DatingProjekt.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("postID")
                        .UseIdentityColumn();

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("authos");

                    b.Property<string>("Profile")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("dat_pos_profile");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("date")
                        .HasColumnName("publishDate");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("text");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PostId");

                    b.HasIndex("Profile");

                    b.HasIndex("UserId");

                    b.ToTable("posts");
                });

            modelBuilder.Entity("DatingProjekt.Models.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("userID");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("date")
                        .HasColumnName("birthday");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("gender");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("imagePath");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.Property<string>("Orientation")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("orientation");

                    b.Property<string>("VisibleSearch")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)")
                        .HasColumnName("visibleSearch");

                    b.HasKey("UserId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("DatingProjekt.Models.Friend", b =>
                {
                    b.HasOne("DatingProjekt.Models.User", "User1Navigation")
                        .WithMany("FriendUser1Navigations")
                        .HasForeignKey("User1")
                        .HasConstraintName("FK_postWall_user1")
                        .IsRequired();

                    b.HasOne("DatingProjekt.Models.User", "User2Navigation")
                        .WithMany("FriendUser2Navigations")
                        .HasForeignKey("User2")
                        .HasConstraintName("FK_postWall_user2")
                        .IsRequired();

                    b.Navigation("User1Navigation");

                    b.Navigation("User2Navigation");
                });

            modelBuilder.Entity("DatingProjekt.Models.FriendRequest", b =>
                {
                    b.HasOne("DatingProjekt.Models.User", "UserPendingNavigation")
                        .WithMany("FriendRequestUserPendingNavigations")
                        .HasForeignKey("UserPending")
                        .HasConstraintName("FK_postWall_userPending")
                        .IsRequired();

                    b.HasOne("DatingProjekt.Models.User", "UserSentNavigation")
                        .WithMany("FriendRequestUserSentNavigations")
                        .HasForeignKey("UserSent")
                        .HasConstraintName("FK_postWall_userSent")
                        .IsRequired();

                    b.Navigation("UserPendingNavigation");

                    b.Navigation("UserSentNavigation");
                });

            modelBuilder.Entity("DatingProjekt.Models.Post", b =>
                {
                    b.HasOne("DatingProjekt.Models.User", "ProfileNavigation")
                        .WithMany("PostsProfile")
                        .HasForeignKey("Profile")
                        .HasConstraintName("FK_users_userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DatingProjekt.Models.User", null)
                        .WithMany("PostsAuthor")
                        .HasForeignKey("UserId");

                    b.Navigation("ProfileNavigation");
                });

            modelBuilder.Entity("DatingProjekt.Models.User", b =>
                {
                    b.Navigation("FriendRequestUserPendingNavigations");

                    b.Navigation("FriendRequestUserSentNavigations");

                    b.Navigation("FriendUser1Navigations");

                    b.Navigation("FriendUser2Navigations");

                    b.Navigation("PostsAuthor");

                    b.Navigation("PostsProfile");
                });
#pragma warning restore 612, 618
        }
    }
}