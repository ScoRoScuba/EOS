namespace EOS2.Common.Extensions
{
    using System;
    using System.ComponentModel;

    public static class EnumExtensionMethods
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Extension Method 'this' means it will have value")]
        public static string ToDescription(this Enum theEnum)
        {
            var type = theEnum.GetType();
            var memberInformation = type.GetMember(theEnum.ToString());

            if (memberInformation != null && memberInformation.Length > 0)
            {
                var attributes = memberInformation[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                {
                    return ((DescriptionAttribute)attributes[0]).Description;
                }
            }

            return theEnum.ToString();
        }
    }
}
