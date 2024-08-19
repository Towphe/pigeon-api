using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace repo;

public partial class PigeonContext : DbContext
{
    public PigeonContext()
    {
    }

    public PigeonContext(DbContextOptions<PigeonContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DirectMessage> DirectMessages { get; set; }

    public virtual DbSet<ImageMessage> ImageMessages { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<TextMessage> TextMessages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DirectMessage>(entity =>
        {
            entity.HasKey(e => e.ChatId).HasName("direct_messages_pkey");

            entity.ToTable("direct_messages");

            entity.Property(e => e.ChatId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("chat_id");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("date_created");
            entity.Property(e => e.UserAId).HasColumnName("user_a_id");
            entity.Property(e => e.UserBId).HasColumnName("user_b_id");

            entity.HasOne(d => d.UserA).WithMany(p => p.DirectMessageUserAs)
                .HasForeignKey(d => d.UserAId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_a");

            entity.HasOne(d => d.UserB).WithMany(p => p.DirectMessageUserBs)
                .HasForeignKey(d => d.UserBId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_b");
        });

        modelBuilder.Entity<ImageMessage>(entity =>
        {
            entity.HasKey(e => e.ImageMessageId).HasName("image_messages_pkey");

            entity.ToTable("image_messages");

            entity.Property(e => e.ImageMessageId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("image_message_id");
            entity.Property(e => e.FileName)
                .HasMaxLength(255)
                .HasColumnName("file_name");
            entity.Property(e => e.FileType)
                .HasMaxLength(30)
                .HasColumnName("file_type");
            entity.Property(e => e.MessageId).HasColumnName("message_id");

            entity.HasOne(d => d.Message).WithMany(p => p.ImageMessages)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_message");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("messages_pkey");

            entity.ToTable("messages");

            entity.Property(e => e.MessageId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("message_id");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("date_created");
            entity.Property(e => e.DirectMessageId).HasColumnName("direct_message_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.SenderId).HasColumnName("sender_id");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");

            entity.HasOne(d => d.DirectMessage).WithMany(p => p.Messages)
                .HasForeignKey(d => d.DirectMessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_dm");

            entity.HasOne(d => d.Sender).WithMany(p => p.Messages)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_sender");
        });

        modelBuilder.Entity<TextMessage>(entity =>
        {
            entity.HasKey(e => e.TextMessageId).HasName("text_messages_pkey");

            entity.ToTable("text_messages");

            entity.Property(e => e.TextMessageId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("text_message_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.MessageId).HasColumnName("message_id");

            entity.HasOne(d => d.Message).WithMany(p => p.TextMessages)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_message");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.UserId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("user_id");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("date_created");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
