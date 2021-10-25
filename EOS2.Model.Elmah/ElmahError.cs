namespace EOS2.Model.Elmah
{
    using System;

    public class ElmahError
    {
        public Guid ErrorId { get; set; }

        public string Application { get; set; }

        public string Host { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "Elmah Framework Naming, outside scope")]
        public string Type { get; set; }

        public string Source { get; set; }

        public string Message { get; set; }

        public string User { get; set; }

        public int StatusCode { get; set; }

        public DateTime TimeUtc { get; set; }

        public int Sequence { get; set; }

        public string AllXml { get; set; }
    }
}
