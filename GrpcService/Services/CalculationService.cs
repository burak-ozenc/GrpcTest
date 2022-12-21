﻿using Grpc.Core;
using GrpcService1;
using Microsoft.AspNetCore.Authorization;

namespace GrpcService.Protos;


public class CalculationService : Calculation.CalculationBase
{
    [Authorize(Roles = "Administrator")] 
    public override Task<CalculationResult> Add(InputNumbers request, ServerCallContext context)
    {
        return Task.FromResult(new CalculationResult { Result = request.Number1 + request.Number2 });
    }
    [Authorize(Roles = "Administrator,User")]
    public override Task<CalculationResult> Subtract(InputNumbers request, ServerCallContext context)
    {
        return Task.FromResult(new CalculationResult { Result = request.Number1 + request.Number2 });
    }
    [AllowAnonymous]
    public override Task<CalculationResult> Multiply(InputNumbers request, ServerCallContext context)
    {
        return Task.FromResult(new CalculationResult { Result = request.Number1 + request.Number2 });
    }
}