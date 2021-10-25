namespace EOS2.Model.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class CodeAttribute : Attribute
    {
        private readonly string code;

        public CodeAttribute(string code)
        {
           this.code = code;
        }

        public string Code
        {
            get { return code; }
        }
    }
}
