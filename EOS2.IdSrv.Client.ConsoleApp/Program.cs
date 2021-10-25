namespace EOS2.IdSrv.Client.ConsoleApp 
{
    using System;
    using System.Net.Http;
    using System.Text;

    using Thinktecture.IdentityModel.Client;

    public class Program 
    {
        internal static void Main(string[] args)
        {
            //var response = GetToken();
            //Console.WriteLine("Client Credentials Access Token : " + response.AccessToken);

             var response = GetTokenWithLogon();
            Console.WriteLine("Resource Owner Access Token: " + response.AccessToken);

            Console.WriteLine();

            CallAPI(null);

            CallAPI(response);
            
            Console.WriteLine("Press key to end");
            Console.ReadKey();
        }
        
        internal static TokenResponse GetToken()
            {
            var client = new OAuth2Client(
                new Uri("https://localhost:44302/connect/token"),
                "EOSAPI",
                "secret");

            var result = client.RequestClientCredentialsAsync("EOSAPI");
            return result.Result;
        }

        internal static TokenResponse GetTokenWithLogon()
            {
            var client = new OAuth2Client(
                new Uri("https://localhost:44302/connect/token"),
                "EOSAPI",
                "secret");

            var result = client.RequestResourceOwnerPasswordAsync("steve.sprott", "!12345678A", "EOSAPI");

            return result.Result;
        }

        internal static void CallAPI(TokenResponse token)
        {
            var client = new HttpClient();

            if (token != null) client.SetBearerToken(token.AccessToken);

            try
            {
                Console.WriteLine("Result " + (token != null ? "(tokened) " : "(untokened)"));
                Console.WriteLine("======================");
                var result = client.GetStringAsync("https://localhost:44304/api/v1/values").Result;
                Console.WriteLine(result);
                Console.WriteLine(Environment.NewLine);
                var result2 = client.GetStringAsync("https://localhost:44304/api/v2/values").Result;
                Console.WriteLine(result2);
                Console.WriteLine(Environment.NewLine);
                var result3 = client.GetStringAsync("https://localhost:44304/api/v1/instrument/100").Result;
                Console.WriteLine(result3);
                Console.WriteLine(Environment.NewLine);
            }
            catch (AggregateException ex)
            {
                var message = new StringBuilder("Exception Raised : " + ex.Message + Environment.NewLine);
                if (ex.InnerException != null)
                {
                    message.Append("Inner Exception : " + ex.InnerException.Message + Environment.NewLine);
                }

                Console.WriteLine(message);
            }
            catch (HttpRequestException ex)
            {
                var message = new StringBuilder("Exception Raised : " + ex.Message + Environment.NewLine);
                if (ex.InnerException != null)
                {
                    message.Append("Inner Exception : " + ex.InnerException.Message + Environment.NewLine);
                }

                Console.WriteLine(message);
            }
        }
    }
}
