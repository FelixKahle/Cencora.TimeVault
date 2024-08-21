// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Cencora.TimeVault.WebApi.Extensions;

/// <summary>
/// Operation filter to snake case parameter names.
/// </summary>
public class SnakeCaseParamOperationFilter : IOperationFilter
{
    /// <summary>
    /// Applies the operation filter to the specified operation.
    /// </summary>
    /// <param name="operation">The operation to apply the filter to.</param>
    /// <param name="context">The context of the operation filter.</param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        ArgumentNullException.ThrowIfNull(operation, nameof(operation));
        ArgumentNullException.ThrowIfNull(context, nameof(context));

        if (operation.Parameters == null)
        {
            operation.Parameters = [];
        }
        else 
        { 
            foreach(var item in operation.Parameters)
            {             
                 item.Name = item.Name.ToSnakeCase();
            }              
        }
    }
}