namespace EOS2.Repository
{
    using System;
    using System.Data.Entity;

    public static class DatabaseInitializer
    {
        public static void Initialize(string connectionStringOrName)
        {
            if (string.IsNullOrWhiteSpace(connectionStringOrName)) throw new ArgumentNullException("connectionStringOrName");

            using (var dataContext = new DbContext(connectionStringOrName))
            {
                if (dataContext.Database.Exists())
                {
                    // set the database to SINGLE_USER so it can be dropped
                    dataContext.Database.ExecuteSqlCommand(
                        TransactionalBehavior.DoNotEnsureTransaction,
                        "ALTER DATABASE [" + dataContext.Database.Connection.Database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");

                    // drop the database
                    dataContext.Database.ExecuteSqlCommand(
                        TransactionalBehavior.DoNotEnsureTransaction,
                        "USE master DROP DATABASE [" + dataContext.Database.Connection.Database + "]");
                }

                dataContext.Database.Connection.Close();
                dataContext.Database.Connection.Dispose();
            }
        }
    }
}
