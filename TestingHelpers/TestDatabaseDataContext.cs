namespace TestingHelpers
{
     using EOS2.Repository;

    public class TestDatabaseDataContext : EOS2DataContext
    {
        public TestDatabaseDataContext()
        {
        }

        public TestDatabaseDataContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {            
        }
    }
}
