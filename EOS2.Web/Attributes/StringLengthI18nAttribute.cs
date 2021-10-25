namespace EOS2.Web.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "n", Justification = "Have added I18n as a 'word' and 'acronymn' not sure why this isnt being picked up here")]
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class StringLengthI18nAttribute : StringLengthAttribute
    {
        private const string DefaultErrorMessage = "[[[The %0 must be between %2 and %1 characters long.|||{0}|||{2}|||{1}]]]";

        public StringLengthI18nAttribute(int maximumLength)
            : base(maximumLength) 
        {
        }

        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage == null) ErrorMessage = DefaultErrorMessage;

            var message = "[[[" + string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, this.MinimumLength, this.MaximumLength).Replace("[[[", string.Empty).Replace("]]]", string.Empty) + "]]]";
            return message; 
        }
    }
}