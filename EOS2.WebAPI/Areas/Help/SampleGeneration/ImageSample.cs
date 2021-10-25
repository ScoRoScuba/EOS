namespace EOS2.Web.Areas.Help
{
    using System;

    /// <summary>
    /// This represents an image sample on the help page. There's a display template named ImageSample associated with this class.
    /// </summary>
    public class ImageSample
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageSample"/> class.
        /// </summary>
        /// <param name="source">The URL of an image.</param>
        public ImageSample(string source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            Source = source;
        }

        public string Source { get; private set; }

        public override bool Equals(object obj)
        {
            ImageSample other = obj as ImageSample;
            return other != null && Source == other.Source;
        }

        public override int GetHashCode()
        {
            return Source.GetHashCode();
        }

        public override string ToString()
        {
            return Source;
        }
    }
}