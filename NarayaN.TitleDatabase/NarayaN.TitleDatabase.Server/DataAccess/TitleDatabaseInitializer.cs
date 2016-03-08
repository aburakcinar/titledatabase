using System.Data.Entity;

namespace NarayaN.TitleDatabase.Server.DataAccess
{
    public class TitleDatabaseInitializer : CreateDatabaseIfNotExists<TmdbContext>
    {
        public override void InitializeDatabase(TmdbContext context)
        {
            base.InitializeDatabase(context);
        }

        protected override void Seed(TmdbContext context)
        {
            base.Seed(context);
        }
    }
}