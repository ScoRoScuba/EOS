namespace EOS2.Infrastructure.Interfaces.Security
{
    using System.Net;

    public interface IAuthorizationToken
    {
        bool IsHttpError { get; set; }

        HttpStatusCode HttpErrorStatusCode { get; set; }
    
        string HttpErrorReason { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Error", Justification = "Name accuratley states purpose of type")]
        string Error { get; set; }

        bool IsError { get; set; }
    }
}
