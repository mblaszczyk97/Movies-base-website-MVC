namespace ProjektMovie.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public DbSet<Movie> Movies { get; set; }


        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Director> Director { get; set; }
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<GameDeveloper> GameDeveloper { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Director>()
                .Property(e => e.imie)
                .IsUnicode(false);

            modelBuilder.Entity<Director>()
                .Property(e => e.nazwisko)
                .IsUnicode(false);

            modelBuilder.Entity<Director>()
                .Property(e => e.kraj)
                .IsUnicode(false);

            modelBuilder.Entity<Director>()
                .HasMany(e => e.Movie)
                .WithOptional(e => e.Director);
                //.HasForeignKey(e => e.director_id);

            modelBuilder.Entity<Movie>()
                .Property(e => e.nazwa)
                .IsUnicode(false);

            modelBuilder.Entity<Movie>()
                .Property(e => e.rodzaj)
                .IsUnicode(false);

            modelBuilder.Entity<Roles>()
                .Property(e => e.rola)
                .IsUnicode(false);

            modelBuilder.Entity<Roles>()
                .Property(e => e.opis)
                .IsUnicode(false);

            modelBuilder.Entity<Roles>()
                .HasMany(e => e.User)
                .WithRequired(e => e.Roles)
                .HasForeignKey(e => e.rola_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.nick)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.haslo)
                .IsUnicode(false);

            modelBuilder.Entity<Game>()
                .Property(e => e.nazwa)
                .IsUnicode(false);

            modelBuilder.Entity<GameDeveloper>()
                .Property(e => e.nazwa)
                .IsUnicode(false);
        }

        public System.Data.Entity.DbSet<ProjektMovie.Models.AspNetUserRoles> AspNetUserRoles { get; set; }
    }
}
