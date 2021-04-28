using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DatingProjekt.Models
{
    public partial class DatingDBContext : DbContext
    {
        public DatingDBContext()
        {
        }

        public DatingDBContext(DbContextOptions<DatingDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<FriendRequest> FriendRequests { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DatingProjectDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Finnish_Swedish_CI_AS");

            modelBuilder.Entity<Friend>(entity =>
            {
                entity.HasKey(e => new { e.User1, e.User2 })
                    .HasName("PK__friends__E934B3F5D121C264");

                entity.ToTable("friends");

                entity.Property(e => e.User1).HasColumnName("user1");

                entity.Property(e => e.User2).HasColumnName("user2");

                entity.HasOne(d => d.User1Navigation)
                    .WithMany(p => p.FriendUser1Navigations)
                    .HasForeignKey(d => d.User1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_postWall_user1");

                entity.HasOne(d => d.User2Navigation)
                    .WithMany(p => p.FriendUser2Navigations)
                    .HasForeignKey(d => d.User2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_postWall_user2");
            });

            modelBuilder.Entity<FriendRequest>(entity =>
            {
                entity.HasKey(e => new { e.UserSent, e.UserPending })
                    .HasName("PK__friendRe__CA4D816B5E0CA4A5");

                entity.ToTable("friendRequest");

                entity.Property(e => e.UserSent).HasColumnName("userSent");

                entity.Property(e => e.UserPending).HasColumnName("userPending");

                entity.HasOne(d => d.UserPendingNavigation)
                    .WithMany(p => p.FriendRequestUserPendingNavigations)
                    .HasForeignKey(d => d.UserPending)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_postWall_userPending");

                entity.HasOne(d => d.UserSentNavigation)
                    .WithMany(p => p.FriendRequestUserSentNavigations)
                    .HasForeignKey(d => d.UserSent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_postWall_userSent");
            });


            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("posts");

                entity.Property(e => e.PostId).HasColumnName("postID");

                entity.Property(e => e.Author).HasColumnName("author");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("text");

                entity.Property(e => e.PublishDate)
                    .HasColumnType("date")
                    .HasColumnName("publishDate");

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("author");

                entity.HasOne(d => d.ProfileNavigation)
                    .WithMany(p => p.PostsProfile)
                    .HasForeignKey(d => d.Profile)
                    .HasConstraintName("FK_users_userId");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.Birthday)
                    .HasColumnType("date")
                    .HasColumnName("birthday");


                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("gender");


                entity.Property(e => e.Orientation)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("orientation");

                entity.Property(e => e.VisibleSearch)
                  .IsRequired()
                  .HasMaxLength(3)
                  .HasColumnName("visibleSearch");

                entity.Property(e => e.ImagePath)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("imagePath");


                OnModelCreatingPartial(modelBuilder);
            });
            }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
