namespace EOS2.Common.Validation
{
    public class ServiceState
    {
        private readonly ErrorStateCollection errors = new ErrorStateCollection();

        public string Value { get; set; }

        public ErrorStateCollection Errors
        {
            get
            {
                return errors;
            }
        }
    }
}
