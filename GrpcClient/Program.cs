// See https://aka.ms/new-console-template for more information

// Console.WriteLine("Hello, World!");


// using Google.Protobuf.WellKnownTypes;

using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcService1;

namespace GrpcClient1;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        try
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5052");

            var cli = new Authentication.AuthenticationClient(channel);
            var response = cli.Authenticate(new AuthenticationRequest()
            {
                UserName = "user",
                Password = "user"
            });
        
            Console.WriteLine($"{response.AccessToken}");
            Console.WriteLine($"{response.Expires}");

            var cli2 = new Calculation.CalculationClient(channel);
            var headers = new Metadata();
        
            headers.Add("Authorization",$"Bearer { response.AccessToken }");

            var subtract = await cli2.SubtractAsync(new InputNumbers {Number1 = 3, Number2 = 5},headers);

            Console.WriteLine($"subtract {subtract}");
            
        }
        catch (RpcException e)
        {
            Console.WriteLine(e);
            throw;
        }

        return 1;
    }
}