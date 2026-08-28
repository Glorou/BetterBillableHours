using Microsoft.EntityFrameworkCore;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BetterBillableHours.Data
{
    public class HoursDatabase : DbContext
    {
        public DbSet<Client> Clients => Set<Client>();
        SQLiteAsyncConnection database;
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={Constants.DatabasePath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().ToTable("Clients");
        }

    }
    
    public class Client
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }

        public List<Time> Actions { get; set; } = new List<Time>();
        public string Name { get; set; }
        public DateTime LastAccessed { get; set; }
    }

    public class Time
    {
        [Key]
        public long Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public TimeSpan? Span => new TimeSpan(End.Ticks - Start.Ticks);

    }
}
