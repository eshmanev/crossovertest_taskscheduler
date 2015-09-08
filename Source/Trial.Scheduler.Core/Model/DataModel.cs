using System.Data.Entity;

namespace Trial.Scheduler.Core.Model
{
    public class DataModel : DbContext
    {
        public DataModel()
            : base("name=DataModel")
        {
        }

        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Command> Command { get; set; }
        public virtual DbSet<Log> Log { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasMany(e => e.Command)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Command>()
                .HasMany(e => e.Log)
                .WithRequired(e => e.Command)
                .WillCascadeOnDelete(false);
        }
    }
}
