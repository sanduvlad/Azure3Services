namespace NumberGenerators
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataBase : DbContext
    {
        public DataBase()
            : base("name=DataBase1")
        {
        }

        public virtual DbSet<Generator> Generators { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Generator>()
                .Property(e => e.UserName)
                .IsFixedLength();

            modelBuilder.Entity<Generator>()
                .Property(e => e.Password)
                .IsFixedLength();
        }
    }
}
