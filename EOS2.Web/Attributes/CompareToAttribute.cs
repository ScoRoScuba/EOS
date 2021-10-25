namespace EOS2.Web.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Web.Mvc;

    using EOS2.Web.Enums;

    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class CompareToAttribute : ValidationAttribute, IClientValidatable
    {
        private const string DefaultErrorMessage = "{0} must be {1} {2}";

        private CompareToOperator operatorName = CompareToOperator.GreaterThanOrEqual;

        public CompareToAttribute(string otherProperty) : base(DefaultErrorMessage)
        {
            if (string.IsNullOrEmpty(otherProperty))
            {
                throw new ArgumentNullException("otherProperty");
            }

            CompareToPropertyName = otherProperty;
        }

        public string OtherProperty
        {
            get { return CompareToPropertyName; }
        }

        public string CompareToPropertyName { get; private set; }

        public CompareToOperator OperatorName
        {
            get { return operatorName; }
            set { operatorName = value; }
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, OperatorName, CompareToPropertyName);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var errorMessage = this.FormatErrorMessage(metadata.DisplayName);
            var compareRule = new ModelClientValidationRule
            {
                ErrorMessage = errorMessage,
                ValidationType = "compareto"
            };
            compareRule.ValidationParameters.Add("comparetopropertyname", CompareToPropertyName);
            compareRule.ValidationParameters.Add("operatorname", OperatorName.ToString());
            yield return compareRule;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext != null && value != null)
            {
                var propertyValue = (IComparable)value;
                var comparableProperty = validationContext.ObjectType.GetProperty(CompareToPropertyName);
                var comparablePropertyValue = (IComparable)comparableProperty.GetValue(validationContext.ObjectInstance, null);

                if ((operatorName == CompareToOperator.GreaterThan && propertyValue.CompareTo(comparablePropertyValue) <= 0)
                    || (operatorName == CompareToOperator.GreaterThanOrEqual && propertyValue.CompareTo(comparablePropertyValue) < 0)
                    || (operatorName == CompareToOperator.LessThan && propertyValue.CompareTo(comparablePropertyValue) >= 0)
                    || (operatorName == CompareToOperator.LessThanOrEqual && propertyValue.CompareTo(comparablePropertyValue) > 0)
                    || (operatorName == CompareToOperator.NotEqualTo && propertyValue.CompareTo(comparablePropertyValue) == 0)) return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return null;
        }
    }
}