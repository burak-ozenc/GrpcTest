using Grpc.Core;
using GrpcService1;

namespace GrpcService.Protos;

public class AuthenticationService : Authentication.AuthenticationBase
{
    public override async Task<AuthenticationResponse> Authenticate(AuthenticationRequest request, ServerCallContext context)
    {
        var response = JwtAuthenticationManager.Authenticate(request);
        if (response == null)
            throw new RpcException(new Status(StatusCode.Unauthenticated, "Invalid credentails"));

        return response;
    }
}