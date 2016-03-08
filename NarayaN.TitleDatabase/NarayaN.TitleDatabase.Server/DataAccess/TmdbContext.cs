using MySql.Data.Entity;
using NarayaN.TitleDatabase.Types.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NarayaN.TitleDatabase.Server.DataAccess
{

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class TmdbContext : DbContext
    {
        public virtual DbSet<TMDB_Movie> TMDB_Movie { get; set; }

        private bool _result;

        public TmdbContext()
            : base("DataContext")
        {
            Database.SetInitializer<TmdbContext>(new TitleDatabaseInitializer());

            this.Database.Log = delegate (string message) { Console.Write(message); };
        }

        public TmdbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
            Database.SetInitializer<TmdbContext>(new TitleDatabaseInitializer());

            this.Database.Log = delegate (string message) { Console.Write(message); };
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Car>().MapToStoredProcedures();
        }
    }
}