using Note.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Note.DataAccess
{

    public class NoteDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<AdminEntity> Admins { get; set; }
        public DbSet<DocumentEntity> Documents { get; set; }
        public DbSet<ListEditorEntity> ListEditors { get; set; }
        public DbSet<QuestionEntity> Questions { get; set; }
        public DbSet<RulesEntity> Rules { get; set; }
        public DbSet<SessionEntity> Sessions { get; set; }
        
        public NoteDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<UserEntity>().HasIndex(x => x.ExternalId).IsUnique();
            modelBuilder.Entity<UserEntity>().HasIndex(x => x.Login).IsUnique();

            modelBuilder.Entity<AdminEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<AdminEntity>().HasIndex(x => x.ExternalId).IsUnique();
            modelBuilder.Entity<AdminEntity>().HasIndex(x => x.Login).IsUnique();

            modelBuilder.Entity<DocumentEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<DocumentEntity>().HasIndex(x => x.ExternalId).IsUnique();
            modelBuilder.Entity<DocumentEntity>().HasOne(x => x.Autor)
                .WithMany(x => x.Documents)
                .HasForeignKey(x => x.AutorId);

            modelBuilder.Entity<ListEditorEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<ListEditorEntity>().HasIndex(x => x.ExternalId).IsUnique();
            modelBuilder.Entity<ListEditorEntity>().HasIndex(x => new { x.EditorId, x.DocumentId }).IsUnique();
            modelBuilder.Entity<ListEditorEntity>().HasOne(x => x.Document)
                .WithMany(x => x.ListEditors)
                .HasForeignKey(x => x.DocumentId);
            modelBuilder.Entity<ListEditorEntity>().HasOne(x => x.Editor)
                .WithMany(x => x.ListEditors)
                .HasForeignKey(x => x.EditorId);

            modelBuilder.Entity<QuestionEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<QuestionEntity>().HasIndex(x => x.ExternalId).IsUnique();
            modelBuilder.Entity<QuestionEntity>().HasOne(x => x.User)
                .WithMany(x => x.Questions)
                .HasForeignKey(x => x.UserId);
            modelBuilder.Entity<QuestionEntity>().HasOne(x => x.Admin)
                .WithMany(x => x.Questions)
                .HasForeignKey(x => x.AdminId);

            modelBuilder.Entity<RulesEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<RulesEntity>().HasIndex(x => x.ExternalId).IsUnique();

            modelBuilder.Entity<SessionEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<SessionEntity>().HasIndex(x => x.ExternalId).IsUnique();
            modelBuilder.Entity<SessionEntity>().HasOne(x => x.User)
                .WithMany(x => x.Sessions)
                .HasForeignKey(x => x.UserId);
        }
    }
}