﻿namespace EOS2.Model.Elmah
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ELMAH_Error
    {
        public Guid ErrorId { get; set; }

        public string Application { get; set; }

        public string Host { get; set; }

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
